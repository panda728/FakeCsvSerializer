using FluentAssertions;

namespace FakeCsvSerializer.Tests
{
    public partial class PrimitiveSerializerTest
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
        public void Serializer_Boolean()
        {
            var value1 = true;
            var value2 = false;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
    }
}
