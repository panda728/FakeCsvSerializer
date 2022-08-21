using FluentAssertions;

namespace FakeCsvSerializer.Tests
{
    public class BuiltinSerializersTest
    {
        void RunTest<T>(
            T value1, T value2,
            string columnXml,
            CsvSerializerOptions option)
        {
            var serializer = option.GetSerializer<T>();
            Assert.NotNull(serializer);
            if (serializer == null) return;
            var writer = new CsvSerializerWriter(option);
            try
            {
                writer.WriteDelimiter();
                serializer.Serialize(ref writer, value1, option);
                writer.WriteDelimiter();
                serializer.Serialize(ref writer, value2, option);
                writer.ToString().Should().Be(columnXml);
            }
            catch
            {
                throw;
            }
            finally
            {
                writer.Dispose();
            }
        }

        [Fact]
        public void Serializer_string()
        {
            RunTest(
                "column1", "column2",
                "\"column1\",\"column2\"",
                CsvSerializerOptions.Default);
        }

        [Fact]
        public void Serializer_Guid()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            RunTest(
                guid1, guid2,
                $"\"{guid1}\",\"{guid2}\"",
                CsvSerializerOptions.Default);
        }

        enum DayOfWeek
        {
            Mon, Tue, Wed, Thu, Fri, Sat, Sun
        }

        [Fact]
        public void Serializer_Enum()
        {
            RunTest(
                DayOfWeek.Mon, DayOfWeek.Tue,
                $"\"{DayOfWeek.Mon}\",\"{DayOfWeek.Tue}\"",
                CsvSerializerOptions.Default);
        }

        [Fact]
        public void Serializer_DateTime()
        {
            var option = CsvSerializerOptions.Default;
            var value1 = new DateTime(2000, 1, 1);
            var value2 = new DateTime(9999, 12, 31);
            RunTest(
                value1, value2,
                $"\"{value1.ToString(option.CultureInfo)}\",\"{value2.ToString(option.CultureInfo)}\"",
                CsvSerializerOptions.Default);
        }

        [Fact]
        public void Serializer_DateTimeOffset()
        {
            var value1 = DateTimeOffset.Now;
            var value2 = DateTimeOffset.UtcNow;
            var option = CsvSerializerOptions.Default;
            RunTest(
                value1, value2,
                $"\"{value1.ToString(option.CultureInfo)}\",\"{value2.ToString(option.CultureInfo)}\"",
                CsvSerializerOptions.Default);
        }

        [Fact]
        public void Serializer_TimeSpan()
        {
            var value1 = DateTime.Today.AddHours(10) - DateTime.Today;
            var value2 = DateTime.Today.AddHours(-10) - DateTime.Today;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Uri()
        {
            var value1 = new Uri("http://hoge.com/fuga");
            var value2 = new Uri("http://hoge.com/fugafuga");
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_DateOnly()
        {
            var value1 = DateOnly.FromDateTime(new DateTime(2000, 1, 1));
            var value2 = DateOnly.FromDateTime(new DateTime(9999, 12, 31));
            var option = CsvSerializerOptions.Default;
            RunTest(
                value1, value2,
                $"\"{value1.ToString(option.CultureInfo)}\",\"{value2.ToString(option.CultureInfo)}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_TimeOnly()
        {
            var value1 = TimeOnly.FromDateTime(new DateTime(2000, 1, 1, 0, 0, 0));
            var value2 = TimeOnly.FromDateTime(new DateTime(9999, 12, 31, 23, 59, 59));
            var option = CsvSerializerOptions.Default;
            RunTest(
                value1, value2,
                $"\"{value1.ToString(option.CultureInfo)}\",\"{value2.ToString(option.CultureInfo)}\"",
                CsvSerializerOptions.Default);
        }
    }
}
