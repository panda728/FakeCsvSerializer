namespace FakeCsvSerializer;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false)]
public sealed class CsvSerializerAttribute : Attribute
{
    public Type Type { get; }

    public CsvSerializerAttribute(Type type)
    {
        Type = type;
    }

    internal void Validate(Type targetType)
    {
        var serializerType = Type.GetImplementedGenericType(typeof(ICsvSerializer<>));
        if (serializerType == null)
        {
            throw new InvalidOperationException($"Type is not implemented ICsvSerializer<T>, Type:{Type.FullName}");
        }

        var attrType = serializerType.GenericTypeArguments[0];
        if (attrType != targetType)
        {
            throw new InvalidOperationException($"Attribute CsvSerializer type is not same as target type. AttrType:{attrType.FullName} TargetType:{targetType.FullName}");
        }
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class IgnoreCsvSerializeAttribute : Attribute
{
}