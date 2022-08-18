using FakeCsvSerializer.Serializers;

namespace FakeCsvSerializer.Providers;

public sealed class ObjectGraphCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new ObjectGraphCsvSerializerProvider();

    ObjectGraphCsvSerializerProvider()
    {
    }

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        return Cache<T>.Serializer;
    }

    static ICsvSerializer? CreateSerializer(Type type)
    {
        try
        {
            return (ICsvSerializer?)Activator.CreateInstance(typeof(CompiledObjectGraphCsvSerializer<>).MakeGenericType(type));
        }
        catch (Exception ex)
        {
            return ErrorSerializer.Create(type, ex);
        }
    }

    static class Cache<T>
    {
        public static readonly ICsvSerializer<T>? Serializer = (ICsvSerializer<T>?)CreateSerializer(typeof(T));
    }
}