using System.Buffers;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace FakeCsvSerializer.Serializers;

internal sealed class CompiledObjectGraphCsvSerializer<T> : ICsvSerializer<T>
{
    delegate void SerializeMethod(ref CsvSerializerWriter writer, ICsvSerializer?[]? alternateSerializers, T value, CsvSerializerOptions options);

    static readonly string[] names;
    static readonly ICsvSerializer?[]? alternateSerializers;
    static readonly SerializeMethod serialize;
    static readonly bool isReferenceType;

    static CompiledObjectGraphCsvSerializer()
    {
        isReferenceType = !typeof(T).IsValueType;

        var props = typeof(T).GetProperties();
        var fields = typeof(T).GetFields();
        var members = props.Cast<MemberInfo>().Concat(fields)
            .Where(x => x.GetCustomAttribute<IgnoreCsvSerializeAttribute>() == null)
            .Select((x, i) => new SerializableMemberInfo(x, i))
            .OrderBy(x => x.Order)
            .ThenBy(x => x.Name)
            .ToArray();

        names = members.Select(x => x.Name).ToArray();
        if (members.Any(x => x.CsvSerializer != null))
        {
            alternateSerializers = members.Select(x => x.CsvSerializer).ToArray();
        }
        serialize = CompileSerializer(typeof(T), members);
    }

    public static string[] Names => names;

    public void Serialize(ref CsvSerializerWriter writer, T value, CsvSerializerOptions options)
    {
        if (isReferenceType)
        {
            if (value == null)
            {
                writer.WriteDelimiter();
                writer.WriteEmpty();
                return;
            }
        }

        writer.EnterAndValidate();
        serialize(ref writer, alternateSerializers, value, options);
        writer.Exit();
    }

    static SerializeMethod CompileSerializer(Type valueType, SerializableMemberInfo[] memberInfos)
    {
        // SerializeMethod(ref CsvSerializerWriter writer, ICsvSerializer[]? alternateSerializers, T value, CsvSerializerOptions options)
        // foreach(members)
        //   if (value.Foo != null) // reference type || nullable type
        //     writer.WriteDelimiter();
        //     options.GetRequiredSerializer<T>() || ((ICsvSerialzier<T>)alternateSerializers[0] .Serialize(writer, value.Foo, options)

        var argWriterRef = Expression.Parameter(typeof(CsvSerializerWriter).MakeByRefType());
        var argAlternateSerializers = Expression.Parameter(typeof(ICsvSerializer[]));
        var argValue = Expression.Parameter(valueType);
        var argOptions = Expression.Parameter(typeof(CsvSerializerOptions));
        var foreachBodies = new List<Expression>();

        var i = 0;
        foreach (var memberInfo in memberInfos)
        {
            var body1 = Expression.Call(argWriterRef, ReflectionInfos.CsvWriter_Delimiter);

            Expression serializer = memberInfo.CsvSerializer == null
                ? Expression.Call(argOptions, ReflectionInfos.CsvSerializerOptions_GetRequiredSerializer(memberInfo.MemberType))
                : Expression.Convert(
                    Expression.ArrayIndex(argAlternateSerializers, Expression.Constant(i, typeof(int))),
                    typeof(ICsvSerializer<>).MakeGenericType(memberInfo.MemberType));

            var body2 = Expression.Call(serializer, ReflectionInfos.ICsvSerializer_Serialize(memberInfo.MemberType), argWriterRef, memberInfo.GetMemberExpression(argValue), argOptions);

            var bodyBlock = Expression.Block(body1, body2);
            if (!memberInfo.MemberType.IsValueType || memberInfo.MemberType.IsNullable())
            {
                var nullExpr = Expression.Constant(null, memberInfo.MemberType);
                var body3 = Expression.Call(argWriterRef, ReflectionInfos.CsvWriter_Empty);
                var ifBody = Expression.IfThenElse(
                    Expression.NotEqual(memberInfo.GetMemberExpression(argValue), nullExpr),
                    bodyBlock,
                    Expression.Block(body1, body3)
                );
                foreachBodies.Add(ifBody);
            }
            else
            {
                foreachBodies.Add(bodyBlock);
            }
            i++;
        }

        var body = Expression.Block(foreachBodies);
        var lambda = Expression.Lambda<SerializeMethod>(body, argWriterRef, argAlternateSerializers, argValue, argOptions);
        return lambda.Compile();
    }

    internal static class ReflectionInfos
    {
        internal static MethodInfo CsvWriter_Delimiter { get; } = typeof(CsvSerializerWriter).GetMethod("WriteDelimiter")!;
        internal static MethodInfo CsvWriter_Empty { get; } = typeof(CsvSerializerWriter).GetMethod("WriteEmpty")!;
        internal static MethodInfo CsvSerializerOptions_GetRequiredSerializer(Type type) => typeof(CsvSerializerOptions).GetMethod("GetRequiredSerializer", 1, Type.EmptyTypes)!.MakeGenericMethod(type);
        internal static MethodInfo ICsvSerializer_Serialize(Type type) => typeof(ICsvSerializer<>).MakeGenericType(type).GetMethod("Serialize")!;
    }
}

internal sealed class SerializableMemberInfo
{
    public string Name { get; }
    public int Order { get; }
    public ICsvSerializer? CsvSerializer { get; }
    public Type MemberType { get; }
    public MemberInfo MemberInfo { get; }

    public SerializableMemberInfo(MemberInfo member, int i)
    {
        var dataMember = member.GetCustomAttribute<DataMemberAttribute>();

        MemberInfo = member;
        Name = dataMember?.Name ?? member.Name;
        Order = dataMember?.Order ?? i;

        MemberType = member switch
        {
            PropertyInfo pi => pi.PropertyType,
            FieldInfo fi => fi.FieldType,
            _ => throw new InvalidOperationException()
        };

        var serializerAttr = member.GetCustomAttribute<CsvSerializerAttribute>();
        if (serializerAttr != null)
        {
            serializerAttr.Validate(MemberType);
            CsvSerializer = (ICsvSerializer?)Activator.CreateInstance(serializerAttr.Type);
        }
    }

    public MemberExpression GetMemberExpression(Expression expression)
    {
        if (MemberInfo is FieldInfo fi)
        {
            return Expression.Field(expression, fi);
        }
        else if (MemberInfo is PropertyInfo pi)
        {
            return Expression.Property(expression, pi);
        }
        throw new InvalidOperationException();
    }
}