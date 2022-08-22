namespace FakeCsvSerializer.Serializers;

internal class BuiltinSerializers
{
    public sealed class StringCsvSerializer : ICsvSerializer<string?>
    {
        public void Serialize(ref CsvSerializerWriter writer, string? value, CsvSerializerOptions options)
        {
            if (string.IsNullOrEmpty(value))
            {
                writer.WriteEmpty();
                return;
            }

            var containsDoubleQuote = value.Contains('"');
            if (containsDoubleQuote
                || value.Contains('\r')
                || value.Contains('\n')
                || value.Contains('\t')
                || value.Contains(',')
                || value.Contains(' '))
            {
                writer.Write("\"");
                writer.Write(containsDoubleQuote ? value.Replace("\"", "\"\"") : value);
                writer.Write("\"");
                return;
            }

            writer.WriteQuote();
            writer.Write(value);
            writer.WriteQuote();
        }
    }

    public sealed class GuidCsvSerializer : ICsvSerializer<Guid>
    {
        public void Serialize(ref CsvSerializerWriter writer, Guid value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }

    public sealed class EnumCsvSerializer : ICsvSerializer<Enum>
    {
        public void Serialize(ref CsvSerializerWriter writer, Enum value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }

    public sealed class DateTimeCsvSerializer : ICsvSerializer<DateTime>
    {
        public void Serialize(ref CsvSerializerWriter writer, DateTime value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write(value.ToString(options.CultureInfo));
            writer.WriteQuote();
        }
    }

    public sealed class DateTimeOffsetCsvSerializer : ICsvSerializer<DateTimeOffset>
    {
        public void Serialize(ref CsvSerializerWriter writer, DateTimeOffset value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write(value.ToString(options.CultureInfo));
            writer.WriteQuote();
        }
    }

    public sealed class TimeSpanCsvSerializer : ICsvSerializer<TimeSpan>
    {
        public void Serialize(ref CsvSerializerWriter writer, TimeSpan value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }

    public sealed class UriCsvSerializer : ICsvSerializer<Uri>
    {
        public void Serialize(ref CsvSerializerWriter writer, Uri value, CsvSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteEmpty();
                return;
            }

            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }

    public sealed class DateOnlyCsvSerializer : ICsvSerializer<DateOnly>
    {
        public void Serialize(ref CsvSerializerWriter writer, DateOnly value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write(value.ToString(options.CultureInfo));
            writer.WriteQuote();
        }
    }

    public sealed class TimeOnlyCsvSerializer : ICsvSerializer<TimeOnly>
    {
        public void Serialize(ref CsvSerializerWriter writer, TimeOnly value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write(value.ToString(options.CultureInfo));
            writer.WriteQuote();
        }
    }
}
