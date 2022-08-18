﻿






// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY `Serializers/PrimitiveSerializer.tt`. DO NOT CHANGE IT.
// </auto-generated>
#nullable enable
namespace FakeCsvSerializer.Serializers
{

    public sealed class BooleanCsvSerializer : ICsvSerializer<Boolean>
    {
        public void Serialize(ref CsvSerializerWriter writer, Boolean value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class ByteCsvSerializer : ICsvSerializer<Byte>
    {
        public void Serialize(ref CsvSerializerWriter writer, Byte value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class SByteCsvSerializer : ICsvSerializer<SByte>
    {
        public void Serialize(ref CsvSerializerWriter writer, SByte value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class CharCsvSerializer : ICsvSerializer<Char>
    {
        public void Serialize(ref CsvSerializerWriter writer, Char value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class DecimalCsvSerializer : ICsvSerializer<Decimal>
    {
        public void Serialize(ref CsvSerializerWriter writer, Decimal value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class DoubleCsvSerializer : ICsvSerializer<Double>
    {
        public void Serialize(ref CsvSerializerWriter writer, Double value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class SingleCsvSerializer : ICsvSerializer<Single>
    {
        public void Serialize(ref CsvSerializerWriter writer, Single value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class Int32CsvSerializer : ICsvSerializer<Int32>
    {
        public void Serialize(ref CsvSerializerWriter writer, Int32 value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class UInt32CsvSerializer : ICsvSerializer<UInt32>
    {
        public void Serialize(ref CsvSerializerWriter writer, UInt32 value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class Int64CsvSerializer : ICsvSerializer<Int64>
    {
        public void Serialize(ref CsvSerializerWriter writer, Int64 value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class UInt64CsvSerializer : ICsvSerializer<UInt64>
    {
        public void Serialize(ref CsvSerializerWriter writer, UInt64 value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class Int16CsvSerializer : ICsvSerializer<Int16>
    {
        public void Serialize(ref CsvSerializerWriter writer, Int16 value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


    public sealed class UInt16CsvSerializer : ICsvSerializer<UInt16>
    {
        public void Serialize(ref CsvSerializerWriter writer, UInt16 value, CsvSerializerOptions options)
        {
            writer.WriteQuote();
            writer.Write($"{value}");
            writer.WriteQuote();
        }
    }


}

namespace FakeCsvSerializer.Providers
{
    public sealed partial class PrimitiveCsvSerializerProvider
    {
        internal partial void InitPrimitives()
        {

            serializers[typeof(System.Boolean)] = new FakeCsvSerializer.Serializers.BooleanCsvSerializer();

            serializers[typeof(System.Byte)] = new FakeCsvSerializer.Serializers.ByteCsvSerializer();

            serializers[typeof(System.SByte)] = new FakeCsvSerializer.Serializers.SByteCsvSerializer();

            serializers[typeof(System.Char)] = new FakeCsvSerializer.Serializers.CharCsvSerializer();

            serializers[typeof(System.Decimal)] = new FakeCsvSerializer.Serializers.DecimalCsvSerializer();

            serializers[typeof(System.Double)] = new FakeCsvSerializer.Serializers.DoubleCsvSerializer();

            serializers[typeof(System.Single)] = new FakeCsvSerializer.Serializers.SingleCsvSerializer();

            serializers[typeof(System.Int32)] = new FakeCsvSerializer.Serializers.Int32CsvSerializer();

            serializers[typeof(System.UInt32)] = new FakeCsvSerializer.Serializers.UInt32CsvSerializer();

            serializers[typeof(System.Int64)] = new FakeCsvSerializer.Serializers.Int64CsvSerializer();

            serializers[typeof(System.UInt64)] = new FakeCsvSerializer.Serializers.UInt64CsvSerializer();

            serializers[typeof(System.Int16)] = new FakeCsvSerializer.Serializers.Int16CsvSerializer();

            serializers[typeof(System.UInt16)] = new FakeCsvSerializer.Serializers.UInt16CsvSerializer();

        }
    }
}