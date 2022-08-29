namespace FakeCsvSerializer;

public static class CsvSerializer
{
    public static void ToFile<T>(IEnumerable<T> rows, string fileName, CsvSerializerOptions options)
    {
        using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            ToStream(rows, stream, options);
    }

    public static void ToStream<T>(IEnumerable<T> rows, Stream stream, CsvSerializerOptions options)
    {
        using (var writer = new CsvSerializerWriter(options))
            WriteCsv(rows, stream, options, writer);
    }

    static void WriteCsv<T>(IEnumerable<T> rows, Stream stream, CsvSerializerOptions options, CsvSerializerWriter writer)
    {
        if (options.HasHeaderRecord && options.HeaderTitles != null)
        {
            foreach (var t in options.HeaderTitles)
            {
                writer.WriteDelimiter();
                writer.Write(t.AsSpan());
            }
            writer.WriteLine();
            writer.CopyTo(stream);
        }

        var serializer = options.GetSerializer<T>();
        foreach (var row in rows)
        {
            if (row == null) continue;
            serializer?.Serialize(ref writer, row, options);
            writer.WriteLine();
            writer.CopyTo(stream);
        }
    }
}
