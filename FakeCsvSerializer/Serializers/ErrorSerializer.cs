using System.Buffers;
using System.Runtime.ExceptionServices;

namespace FakeCsvSerializer.Serializers;

public sealed class ErrorSerializer<T> : ICsvSerializer<T>
{
    readonly ExceptionDispatchInfo exception;

    public ErrorSerializer(Exception exception)
    {
        this.exception = ExceptionDispatchInfo.Capture(exception);
    }

    public void Serialize(ref CsvSerializerWriter writer, T value, CsvSerializerOptions options)
    {
        exception.Throw();
    }
}

public static class ErrorSerializer
{
    public static ICsvSerializer Create(Type type, Exception exception)
    {
        return (ICsvSerializer)Activator.CreateInstance(typeof(ErrorSerializer<>).MakeGenericType(type), exception)!;
    }
}
