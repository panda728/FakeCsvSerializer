using FakeCsvSerializer.Serializers;

namespace FakeCsvSerializer.Providers;

public sealed class GenericsCsvSerializerProvider : ICsvSerializerProvider
{
    public static ICsvSerializerProvider Instance { get; } = new GenericsCsvSerializerProvider();

    GenericsCsvSerializerProvider()
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
            if (type.IsGenericType)
            {
                // Nullable<T>
                var nullableUnderlying = Nullable.GetUnderlyingType(type);
                if (nullableUnderlying != null)
                {
                    return CreateInstance(typeof(NullableCsvSerializer<>), new[] { nullableUnderlying });
                }

                // Tuple/ValueTuple
                var fullName = type.FullName;
                if (fullName != null && (fullName.StartsWith("System.Tuple") || fullName.StartsWith("System.ValueTuple")))
                {
                    var serializerType = (type.IsValueType)
                        ? TupleCsvSerializer.GetValueTupleCsvSerializerType(type.GenericTypeArguments.Length)
                        : TupleCsvSerializer.GetTupleCsvSerializerType(type.GenericTypeArguments.Length);

                    return CreateInstance(serializerType, type.GetGenericArguments());
                }
            }
            else if (type.IsEnum)
            {
                return CreateInstance(typeof(EnumStringCsvSerializer<>), new[] { type });
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