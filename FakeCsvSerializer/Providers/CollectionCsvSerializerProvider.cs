using FakeCsvSerializer.Serializers;

namespace FakeCsvSerializer.Providers;

public sealed class CollectionCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new CollectionCsvSerializerProvider();

    CollectionCsvSerializerProvider()
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
            // Wellknown specialized types
            if (type == typeof(Dictionary<string, string>))
            {
                return new DictionaryCsvSerializer<Dictionary<string, string>, string, string>();
            }
            else if (type == typeof(Dictionary<string, object>))
            {
                return new DictionaryCsvSerializer<Dictionary<string, object>, string, object>();
            }
            else if (type == typeof(KeyValuePair<string, string>[]))
            {
                return new EnumerableKeyValuePairCsvSerializer<KeyValuePair<string, string>[], string, string>();
            }
            else if (type == typeof(KeyValuePair<string, object>[]))
            {
                return new EnumerableKeyValuePairCsvSerializer<KeyValuePair<string, object>[], string, object>();
            }

            if (type.IsGenericType || type.IsArray)
            {
                // Generic Dictionary
                var dictionaryDef = type.GetImplementedGenericType(typeof(IDictionary<,>));
                if (dictionaryDef != null)
                {
                    var keyType = dictionaryDef.GenericTypeArguments[0];
                    var valueType = dictionaryDef.GenericTypeArguments[1];
                    return CreateInstance(typeof(DictionaryCsvSerializer<,,>), new[] { type, keyType, valueType });
                }

                // Generic Collections
                var enumerableDef = type.GetImplementedGenericType(typeof(IEnumerable<>));
                if (enumerableDef != null)
                {
                    var elementType = enumerableDef.GenericTypeArguments[0];
                    if (elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                    {
                        var keyType = elementType.GenericTypeArguments[0];
                        var valueType = elementType.GenericTypeArguments[1];
                        return CreateInstance(typeof(EnumerableKeyValuePairCsvSerializer<,,>), new[] { type, keyType, valueType });
                    }
                    else
                    {
                        return CreateInstance(typeof(EnumerableCsvSerializer<,>), new[] { type, elementType });
                    }
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            return ErrorSerializer.Create(type, ex);
        }
    }

    static ICsvSerializer? CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
    {
        return (ICsvSerializer?)Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
    }

    static class Cache<T>
    {
        public static readonly ICsvSerializer<T>? Serializer = (ICsvSerializer<T>?)CreateSerializer(typeof(T));
    }
}
