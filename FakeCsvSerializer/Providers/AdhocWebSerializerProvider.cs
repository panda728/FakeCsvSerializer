using System.Collections.Concurrent;

namespace FakeCsvSerializer.Providers;

public sealed class AdhocCsvSerializerProvider : ICsvSerializerProvider
{
    readonly ICsvSerializer[] serializers;
    readonly ConcurrentDictionary<Type, ICsvSerializer?> cache;
    readonly Func<Type, ICsvSerializer?> factory;

    public AdhocCsvSerializerProvider(ICsvSerializer[] serializers)
    {
        this.serializers = serializers;
        this.cache = new ConcurrentDictionary<Type, ICsvSerializer?>();
        this.factory = CreateSerializer;
    }

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        return (ICsvSerializer<T>?)cache.GetOrAdd(typeof(T), factory);
    }

    ICsvSerializer? CreateSerializer(Type type)
    {
        foreach (var serializer in serializers)
        {
            var webSerializerType = serializer.GetType().GetImplementedGenericType(typeof(ICsvSerializer<>));
            if (webSerializerType != null)
            {
                if (webSerializerType.GenericTypeArguments[0] == type)
                {
                    return serializer;
                }
            }
        }

        return null;
    }
}

