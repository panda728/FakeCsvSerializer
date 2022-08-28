using FluentAssertions;
using Xunit.Sdk;

namespace FakeCsvSerializer.Tests
{
    public partial class TupleSerializersTest
    {
        void RunTest<T>(
            T value1, string value1ShouldBe,
            CsvSerializerOptions option)
        {
            var serializer = option.GetSerializer<T>();
            Assert.NotNull(serializer);
            if (serializer == null) return;
            var writer = new CsvSerializerWriter(option);
            try
            {
                serializer.Serialize(ref writer, value1, option);
                writer.ToString().Should().Be(value1ShouldBe);
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
    }
}
