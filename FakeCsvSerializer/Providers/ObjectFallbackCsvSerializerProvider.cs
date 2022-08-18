using FakeCsvSerializer.Serializers;

namespace FakeCsvSerializer.Providers;

public class ObjectFallbackCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new ObjectFallbackCsvSerializerProvider();

    ObjectFallbackCsvSerializerProvider()
    {

    }

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        if (typeof(T) == typeof(object))
        {
            return (ICsvSerializer<T>)new ObjectFallbackCsvSerializer();
        }

        return null;
    }
}
