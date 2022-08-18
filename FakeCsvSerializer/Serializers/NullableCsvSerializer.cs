using System.Buffers;

namespace FakeCsvSerializer.Serializers;

public sealed class NullableCsvSerializer<T> : ICsvSerializer<T?>
    where T : struct
{
    public void Serialize(ref CsvSerializerWriter writer, T? value, CsvSerializerOptions options)
    {
        if (value == null) return;
        options.GetRequiredSerializer<T>().Serialize(ref writer, value.Value, options);
    }
}