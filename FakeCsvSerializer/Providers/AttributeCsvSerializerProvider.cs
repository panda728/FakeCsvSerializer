using FakeCsvSerializer.Serializers;
using System.Reflection;

namespace FakeCsvSerializer.Providers;

public sealed class AttributeCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new AttributeCsvSerializerProvider();

    AttributeCsvSerializerProvider()
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
            var attr = type.GetCustomAttribute<CsvSerializerAttribute>();
            if (attr != null)
            {
                attr.Validate(type);
                return (ICsvSerializer?)Activator.CreateInstance(attr.Type);
            }

            return null;
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