using FluentAssertions;
using System.Collections.ObjectModel;

namespace FakeCsvSerializer.Tests
{
    public class SerializersTest
    {
        void RunTest<T>(
            T value1,
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
        public void Serializer_TCollection()
        {
            var dinosaurs = new Collection<string>
            {
                "Psitticosaurus",
                "Caudipteryx"
            };
            RunTest(dinosaurs, 
                "\"Psitticosaurus\",\"Caudipteryx\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_IDictionary()
        {
            var dic = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
            RunTest(dic,
                "\"key1\",\"1\",\"key2\",\"2\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_KeyValuePair()
        {
            var dic = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
            RunTest(dic.First(),
                "\"key1\",\"1\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_ObjectFallback()
        {
            var value = (object)"key1";
            RunTest(value,
                "\"key1\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_CompiledObject()
        {
            var potals1 = new Portal { Name = "Portal1", Owner = null, Level = 8 };
            RunTest(potals1,
                "\"Portal1\",\"\",\"8\"",
                CsvSerializerOptions.Default);
        }

        public class Portal
        {
            public string Name { get; set; } = "";
            public string? Owner { get; set; }
            public int Level { get; set; }
        }

    }
}
