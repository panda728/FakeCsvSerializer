using System.Buffers;

namespace FakeCsvSerializer.Serializers;

public sealed class NullableCsvSerializer<T> : ICsvSerializer<T?>
    where T : struct
{
    public void WriteTitle(ref CsvSerializerWriter writer, T? value, CsvSerializerOptions options, string name = "value")
    {
        if (value == null)
        {
            writer.WriteEmpty();
            return;
        }
        options.GetRequiredSerializer<T>().WriteTitle(ref writer, value.Value, options, name);
    }

    public void Serialize(ref CsvSerializerWriter writer, T? value, CsvSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteEmpty();
            return;
        }
        options.GetRequiredSerializer<T>().Serialize(ref writer, value.Value, options);
    }
}