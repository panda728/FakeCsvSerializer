namespace FakeCsvSerializer.Serializers;

public sealed class EnumerableCsvSerializer<TCollection, TElement> : ICsvSerializer<TCollection>
    where TCollection : IEnumerable<TElement>
{
    public void Serialize(ref CsvSerializerWriter writer, TCollection value, CsvSerializerOptions options)
    {
        if (value == null)
        {
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
    public void Serialize(ref CsvSerializerWriter writer, TDictionary value, CsvSerializerOptions options)
    {
        if (value == null)
        {
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
    public void Serialize(ref CsvSerializerWriter writer, TCollection value, CsvSerializerOptions options)
    {
        if (value == null)
        {
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