using System.Globalization;
using System.Text;

namespace FakeCsvSerializer;

public record CsvSerializerOptions(ICsvSerializerProvider Provider)
{
    public static CsvSerializerOptions Default { get; } = new CsvSerializerOptions(CsvSerializerProvider.Default);

    public CultureInfo? CultureInfo { get; init; }
    public Encoding Encoding { get; init; } = Encoding.UTF8;
    public bool ShouldQuote { get; init; } = true;
    public char Quote { get; init; } = '\"';
    public char Delimiter { get; init; } = ',';
    public bool HasHeaderRecord { get; init; } = false;
    public string NewLine { get; init; } = Environment.NewLine;
    public int MaxDepth { get; init; } = 64;

    public string[]? HeaderTitles { get; init; }

    public ICsvSerializer<T>? GetSerializer<T>()
        => Provider.GetSerializer<T>();

    public ICsvSerializer<T> GetRequiredSerializer<T>()
    {
        var serializer = Provider.GetSerializer<T>();
        if (serializer == null) Throw(typeof(T));
        return serializer!;
    }

    void Throw(Type type)
    {
        throw new InvalidOperationException($"Type is not found in provider. Type:{type}");
    }
}
