using System.Buffers;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace FakeCsvSerializer.Serializers;

internal class ObjectFallbackCsvSerializer : ICsvSerializer<object>
{
    delegate void SerializeDelegate(ref CsvSerializerWriter writer, object value, CsvSerializerOptions options);
    static readonly ConcurrentDictionary<Type, SerializeDelegate> nongenericSerializers = new ConcurrentDictionary<Type, SerializeDelegate>();
    static readonly Func<Type, SerializeDelegate> factory = CompileSerializeDelegate;

    public void Serialize(ref CsvSerializerWriter writer, object value, CsvSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteDelimiter();
            writer.WriteEmpty();
            return;
        }

        var type = value.GetType();
        if (type == typeof(object))
        {
            writer.WriteDelimiter();
            writer.WriteEmpty();
            return;
        }

        var serializer = nongenericSerializers.GetOrAdd(type, factory);

        serializer.Invoke(ref writer, value, options);
    }

    static SerializeDelegate CompileSerializeDelegate(Type type)
    {
        // Serialize(ref CsvSerializerWriter writer, object value, CsvSerializerOptions options)
        //   options.GetRequiredSerializer<T>().Serialize(ref writer, (T)value, options)

        var writer = Expression.Parameter(typeof(CsvSerializerWriter).MakeByRefType());
        var value = Expression.Parameter(typeof(object));
        var options = Expression.Parameter(typeof(CsvSerializerOptions));

        var getRequiredSerializer = typeof(CsvSerializerOptions).GetMethod("GetRequiredSerializer", 1, Type.EmptyTypes)!.MakeGenericMethod(type);
        var serialize = typeof(ICsvSerializer<>).MakeGenericType(type).GetMethod("Serialize")!;

        var body = Expression.Call(
            Expression.Call(options, getRequiredSerializer),
            serialize,
            writer,
            Expression.Convert(value, type),
            options);

        var lambda = Expression.Lambda<SerializeDelegate>(body, writer, value, options);
        return lambda.Compile();
    }
}