using System.Buffers;

namespace FakeCsvSerializer;

public interface ICsvSerializer { }

public interface ICsvSerializer<T> : ICsvSerializer
{
    void Serialize(ref CsvSerializerWriter writer, T value, CsvSerializerOptions options);
}
