using System;

namespace FakeCsvSerializer.Serializers;

internal class BuiltinSerializers
{
    public sealed class StringCsvSerializer : ICsvSerializer<string?>
    {
        public void Serialize(ref CsvSerializerWriter writer, string? value, CsvSerializerOptions options)
        {
            writer.Write(value);
        }
    }

    public sealed class GuidCsvSerializer : ICsvSerializer<Guid>
    {
        public void Serialize(ref CsvSerializerWriter writer, Guid value, CsvSerializerOptions options)
        {
            writer.Write($"{value}".AsSpan());
        }
    }

    public sealed class EnumCsvSerializer : ICsvSerializer<Enum>
    {
        public void Serialize(ref CsvSerializerWriter writer, Enum value, CsvSerializerOptions options)
        {
            writer.Write($"{value}".AsSpan());
        }
    }

    public sealed class DateTimeCsvSerializer : ICsvSerializer<DateTime>
    {
        public void Serialize(ref CsvSerializerWriter writer, DateTime value, CsvSerializerOptions options)
        {
            writer.Write(value.ToString(options.CultureInfo).AsSpan());
        }
    }

    public sealed class DateTimeOffsetCsvSerializer : ICsvSerializer<DateTimeOffset>
    {
        public void Serialize(ref CsvSerializerWriter writer, DateTimeOffset value, CsvSerializerOptions options)
        {
            writer.Write(value.ToString(options.CultureInfo).AsSpan());
        }
    }

    public sealed class TimeSpanCsvSerializer : ICsvSerializer<TimeSpan>
    {
        public void Serialize(ref CsvSerializerWriter writer, TimeSpan value, CsvSerializerOptions options)
        {
            writer.Write($"{value}".AsSpan());
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

            writer.Write($"{value}");
        }
    }

#if NET6_0_OR_GREATER
    public sealed class DateOnlyCsvSerializer : ICsvSerializer<DateOnly>
    {
        public void Serialize(ref CsvSerializerWriter writer, DateOnly value, CsvSerializerOptions options)
        {
            writer.Write(value);
        }
    }

    public sealed class TimeOnlyCsvSerializer : ICsvSerializer<TimeOnly>
    {
        public void Serialize(ref CsvSerializerWriter writer, TimeOnly value, CsvSerializerOptions options)
        {
            writer.Write(value);
        }
    }
#endif
}
