using FakeCsvSerializer.Serializers;

namespace FakeCsvSerializer.Providers;

public sealed class BuiltinCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new BuiltinCsvSerializerProvider();
    readonly Dictionary<Type, ICsvSerializer> serializers = new Dictionary<Type, ICsvSerializer>()
        {
            { typeof(string), new BuiltinSerializers.StringCsvSerializer() },
            { typeof(Guid), new  BuiltinSerializers.GuidCsvSerializer() },
            { typeof(Enum), new  BuiltinSerializers.EnumCsvSerializer() },
            { typeof(DateTime), new  BuiltinSerializers.DateTimeCsvSerializer() },
            { typeof(DateTimeOffset), new  BuiltinSerializers.DateTimeOffsetCsvSerializer() },
            { typeof(TimeSpan), new  BuiltinSerializers.TimeSpanCsvSerializer() },
            { typeof(Uri), new  BuiltinSerializers.UriCsvSerializer() },
#if NET6_0_OR_GREATER
            { typeof(DateOnly), new  BuiltinSerializers.DateOnlyCsvSerializer() },
            { typeof(TimeOnly), new  BuiltinSerializers.TimeOnlyCsvSerializer() },
#endif
        };

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        if (serializers.TryGetValue(typeof(T), out var value))
        {
            return (ICsvSerializer<T>)value;
        }
        return null;
    }
}