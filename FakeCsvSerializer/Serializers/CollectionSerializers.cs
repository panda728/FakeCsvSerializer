namespace FakeCsvSerializer.Serializers;

public sealed class EnumerableCsvSerializer<TCollection, TElement> : ICsvSerializer<TCollection>
    where TCollection : IEnumerable<TElement>
{
    public void WriteTitle(ref CsvSerializerWriter writer, TCollection value, CsvSerializerOptions options, string name = "value")
    {
        writer.EnterAndValidate();
        var serializer = options.GetRequiredSerializer<TElement>();
        foreach (var item in value)
        {
            writer.WriteDelimiter();
            serializer.WriteTitle(ref writer, item, options, name);
        }
        writer.Exit();
    }

    public void Serialize(ref CsvSerializerWriter writer, TCollection value, CsvSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteDelimiter();
            writer.WriteEmpty();
            return;
        }

        writer.EnterAndValidate();
        var serializer = options.GetRequiredSerializer<TElement>();
        foreach (var item in value)
        {
            writer.WriteDelimiter();
            serializer.Serialize(ref writer, item, options);
        }
        writer.Exit();
    }
}

public sealed class DictionaryCsvSerializer<TDictionary, TKey, TValue> : ICsvSerializer<TDictionary>
    where TDictionary : IDictionary<TKey, TValue>
{
    public void WriteTitle(ref CsvSerializerWriter writer, TDictionary value, CsvSerializerOptions options, string name = "value")
    {
        var keySerializer = options.GetRequiredSerializer<TKey>();
        var valueSerializer = options.GetRequiredSerializer<TValue>();
        writer.EnterAndValidate();
        foreach (var item in value)
        {
            writer.WriteDelimiter();
            keySerializer.WriteTitle(ref writer, item.Key, options, "key");
            writer.WriteDelimiter();
            valueSerializer.WriteTitle(ref writer, item.Value, options, name);
        }
        writer.Exit();
    }

    public void Serialize(ref CsvSerializerWriter writer, TDictionary value, CsvSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteDelimiter();
            writer.WriteEmpty();
            return;
        }

        writer.EnterAndValidate();
        var keySerializer = options.GetRequiredSerializer<TKey>();
        var valueSerializer = options.GetRequiredSerializer<TValue>();

        foreach (var item in value)
        {
            if (item.Value == null)
            {
                writer.WriteDelimiter();
                writer.WriteEmpty();
                continue;
            }

            writer.WriteDelimiter();
            keySerializer.Serialize(ref writer, item.Key, options);
            writer.WriteDelimiter();
            valueSerializer.Serialize(ref writer, item.Value, options);
        }
        writer.Exit();
    }
}

public sealed class EnumerableKeyValuePairCsvSerializer<TCollection, TKey, TValue> : ICsvSerializer<TCollection>
    where TCollection : IEnumerable<KeyValuePair<TKey, TValue>>
{
    public void WriteTitle(ref CsvSerializerWriter writer, TCollection value, CsvSerializerOptions options, string name = "value")
    {
        var keySerializer = options.GetRequiredSerializer<TKey>();
        var valueSerializer = options.GetRequiredSerializer<TValue>();
        writer.EnterAndValidate();
        foreach (var item in value)
        {
            writer.WriteDelimiter();
            keySerializer.WriteTitle(ref writer, item.Key, options, "key");
            writer.WriteDelimiter();
            valueSerializer.WriteTitle(ref writer, item.Value, options, name);
        }
        writer.Exit();
    }

    public void Serialize(ref CsvSerializerWriter writer, TCollection value, CsvSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteDelimiter();
            writer.WriteEmpty();
            return;
        }

        var keySerializer = options.GetRequiredSerializer<TKey>();
        var valueSerializer = options.GetRequiredSerializer<TValue>();
        writer.EnterAndValidate();

        foreach (var item in value)
        {
            if (item.Value == null)
            {
                writer.WriteDelimiter();
                writer.WriteEmpty();
                continue;
            }
            writer.WriteDelimiter();
            keySerializer.Serialize(ref writer, item.Key, options);
            writer.WriteDelimiter();
            valueSerializer.Serialize(ref writer, item.Value, options);
        }
        writer.Exit();
    }
}