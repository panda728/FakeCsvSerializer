﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY `Serializers/PrimitiveSerializer.tt`. DO NOT CHANGE IT.
// </auto-generated>
#nullable enable
namespace FakeCsvSerializer.Tests
{
    public partial class PrimitiveSerializerTest 
    {
        [Fact]
        public void Serializer_Byte()
        {
            var value1 = Byte.MinValue;
            var value2 = Byte.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_SByte()
        {
            var value1 = SByte.MinValue;
            var value2 = SByte.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Char()
        {
            var value1 = Char.MinValue;
            var value2 = Char.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Decimal()
        {
            var value1 = Decimal.MinValue;
            var value2 = Decimal.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Double()
        {
            var value1 = Double.MinValue;
            var value2 = Double.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Single()
        {
            var value1 = Single.MinValue;
            var value2 = Single.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Int32()
        {
            var value1 = Int32.MinValue;
            var value2 = Int32.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_UInt32()
        {
            var value1 = UInt32.MinValue;
            var value2 = UInt32.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Int64()
        {
            var value1 = Int64.MinValue;
            var value2 = Int64.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_UInt64()
        {
            var value1 = UInt64.MinValue;
            var value2 = UInt64.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_Int16()
        {
            var value1 = Int16.MinValue;
            var value2 = Int16.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
        [Fact]
        public void Serializer_UInt16()
        {
            var value1 = UInt16.MinValue;
            var value2 = UInt16.MaxValue;
            RunTest(
                value1, value2,
                $"\"{value1}\",\"{value2}\"",
                CsvSerializerOptions.Default);
        }
    }
}