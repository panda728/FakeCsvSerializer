using FakeCsvSerializer.Providers;
using FakeCsvSerializer.Serializers;
using System.Collections.Concurrent;

namespace FakeCsvSerializer;

public interface ICsvSerializerProvider
{
    ICsvSerializer<T>? GetSerializer<T>();
}

public static class CsvSerializerProvider
{
    public static ICsvSerializerProvider Default { get; } = new DefaultCsvSerializerProvider();

    public static ICsvSerializerProvider Create(params ICsvSerializerProvider[] providers)
    {
        return new CompositeSerializerProvider(providers);
    }

    public static ICsvSerializerProvider Create(ICsvSerializer[] serializers, ICsvSerializerProvider[] providers)
    {
        var adhocProvider = new AdhocCsvSerializerProvider(serializers);
        return new CompositeSerializerProvider(providers.Prepend(adhocProvider).ToArray());
    }
}

public class DefaultCsvSerializerProvider : ICsvSerializerProvider
{
    static readonly ICsvSerializerProvider[] providers = new[]
    {
            PrimitiveCsvSerializerProvider.Instance,
            BuiltinCsvSerializerProvider.Instance,
            AttributeCsvSerializerProvider.Instance,
            GenericsCsvSerializerProvider.Instance,
            CollectionCsvSerializerProvider.Instance,
            ObjectFallbackCsvSerializerProvider.Instance,
            ObjectGraphCsvSerializerProvider.Instance
        };

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        return Cache<T>.Serializer;
    }

    static class Cache<T>
    {
        public static readonly ICsvSerializer<T>? Serializer;

        static Cache()
        {
            try
            {
                foreach (var provider in providers)
                {
                    var serializer = provider.GetSerializer<T>();
                    if (serializer != null)
                    {
                        Serializer = serializer;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Serializer = new ErrorSerializer<T>(ex);
            }
        }
    }
}

internal class CompositeSerializerProvider : ICsvSerializerProvider
{
    readonly ICsvSerializerProvider[] providers;
    readonly ConcurrentDictionary<Type, ICsvSerializer?> cache;

    public CompositeSerializerProvider(ICsvSerializerProvider[] providers)
    {
        this.providers = providers;
        this.cache = new ConcurrentDictionary<Type, ICsvSerializer?>();
    }

    public ICsvSerializer<T>? GetSerializer<T>()
    {
        if (!cache.TryGetValue(typeof(T), out var serializer))
        {
            serializer = CreateSerializer<T>();
            if (!cache.TryAdd(typeof(T), serializer))
            {
                serializer = cache[typeof(T)];
            }
        }

        return (ICsvSerializer<T>?)serializer;
    }

    ICsvSerializer? CreateSerializer<T>()
    {
        foreach (var provider in providers)
        {
            var serializer = provider.GetSerializer<T>();
            if (serializer != null)
            {
                return serializer;
            }
        }

        return null;
    }
}
