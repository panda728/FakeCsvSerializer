using System.Buffers;

namespace FakeCsvSerializer;

public interface ICsvSerializer { }

public interface ICsvSerializer<T> : ICsvSerializer
{
    void WriteTitle(ref CsvSerializerWriter writer, T value, CsvSerializerOptions options, string name = "");
    void Serialize(ref CsvSerializerWriter writer, T value, CsvSerializerOptions options);
}
