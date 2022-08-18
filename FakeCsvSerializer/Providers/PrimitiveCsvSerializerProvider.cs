using FakeCsvSerializer.Serializers;

namespace FakeCsvSerializer.Providers;

public sealed partial class PrimitiveCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new PrimitiveCsvSerializerProvider();
    readonly Dictionary<Type, ICsvSerializer> serializers = new Dictionary<Type, ICsvSerializer>();

    internal partial void InitPrimitives(); // implement from PrimitiveSerializers.cs

    PrimitiveCsvSerializerProvider()
    {
        InitPrimitives();
    }

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        if (serializers.TryGetValue(typeof(T), out var value))
        {
            return (ICsvSerializer<T>)value;
        }
        return null;
    }
}