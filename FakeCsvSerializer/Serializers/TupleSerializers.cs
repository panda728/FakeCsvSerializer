﻿








// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY `Serializers/TupleSerializers.tt`. DO NOT CHANGE IT.
// </auto-generated>
#nullable enable
namespace FakeCsvSerializer.Serializers
{

    public sealed class TupleCsvSerializer<T1> : ICsvSerializer<Tuple<T1>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1> : ICsvSerializer<ValueTuple<T1>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2> : ICsvSerializer<Tuple<T1, T2>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2> : ICsvSerializer<ValueTuple<T1, T2>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2, T3> : ICsvSerializer<Tuple<T1, T2, T3>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2, T3> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2, T3> : ICsvSerializer<ValueTuple<T1, T2, T3>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2, T3> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2, T3, T4> : ICsvSerializer<Tuple<T1, T2, T3, T4>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2, T3, T4> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2, T3, T4> : ICsvSerializer<ValueTuple<T1, T2, T3, T4>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2, T3, T4> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2, T3, T4, T5> : ICsvSerializer<Tuple<T1, T2, T3, T4, T5>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2, T3, T4, T5> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2, T3, T4, T5> : ICsvSerializer<ValueTuple<T1, T2, T3, T4, T5>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2, T3, T4, T5, T6> : ICsvSerializer<Tuple<T1, T2, T3, T4, T5, T6>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2, T3, T4, T5, T6> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2, T3, T4, T5, T6> : ICsvSerializer<ValueTuple<T1, T2, T3, T4, T5, T6>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2, T3, T4, T5, T6, T7> : ICsvSerializer<Tuple<T1, T2, T3, T4, T5, T6, T7>>

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2, T3, T4, T5, T6, T7> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2, T3, T4, T5, T6, T7> : ICsvSerializer<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }


    public sealed class TupleCsvSerializer<T1, T2, T3, T4, T5, T6, T7, TRest> : ICsvSerializer<Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>>

        where TRest : notnull

    {
        public void Serialize(ref CsvSerializerWriter writer, Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, CsvSerializerOptions options)
        {
            if (value == null) {
                writer.WriteEmpty();
                return;
            }
            writer.EnterAndValidate();

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);

            writer.WriteDelimiter();
            options.GetRequiredSerializer<TRest>().Serialize(ref writer, value.Rest, options);

            writer.Exit();
        }
    }

    public sealed class ValueTupleCsvSerializer<T1, T2, T3, T4, T5, T6, T7, TRest> : ICsvSerializer<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>

        where TRest : struct

    {
        public void Serialize(ref CsvSerializerWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, CsvSerializerOptions options)
        {
            writer.EnterAndValidate();


            writer.WriteQuote();
            options.GetRequiredSerializer<T1>().Serialize(ref writer, value.Item1, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T2>().Serialize(ref writer, value.Item2, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T3>().Serialize(ref writer, value.Item3, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T4>().Serialize(ref writer, value.Item4, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T5>().Serialize(ref writer, value.Item5, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T6>().Serialize(ref writer, value.Item6, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<T7>().Serialize(ref writer, value.Item7, options);
            writer.WriteQuote();


            writer.WriteDelimiter();

            writer.WriteQuote();
            options.GetRequiredSerializer<TRest>().Serialize(ref writer, value.Rest, options);
            writer.WriteQuote();

            writer.Exit();
        }
    }



    internal static class TupleCsvSerializer
    {
        internal static Type GetTupleCsvSerializerType(int i)
        {
            switch (i)
            {

                case 1:
                    return typeof(TupleCsvSerializer<>);

                case 2:
                    return typeof(TupleCsvSerializer<,>);

                case 3:
                    return typeof(TupleCsvSerializer<,,>);

                case 4:
                    return typeof(TupleCsvSerializer<,,,>);

                case 5:
                    return typeof(TupleCsvSerializer<,,,,>);

                case 6:
                    return typeof(TupleCsvSerializer<,,,,,>);

                case 7:
                    return typeof(TupleCsvSerializer<,,,,,,>);

                case 8:
                    return typeof(TupleCsvSerializer<,,,,,,,>);

                default:
                    break;
            }

            throw new InvalidOperationException($"TupleCsvSerializer<T1...T{i}> is not found.");
        }

        internal static Type GetValueTupleCsvSerializerType(int i)
        {
            switch (i)
            {

                case 1:
                    return typeof(ValueTupleCsvSerializer<>);

                case 2:
                    return typeof(ValueTupleCsvSerializer<,>);

                case 3:
                    return typeof(ValueTupleCsvSerializer<,,>);

                case 4:
                    return typeof(ValueTupleCsvSerializer<,,,>);

                case 5:
                    return typeof(ValueTupleCsvSerializer<,,,,>);

                case 6:
                    return typeof(ValueTupleCsvSerializer<,,,,,>);

                case 7:
                    return typeof(ValueTupleCsvSerializer<,,,,,,>);

                case 8:
                    return typeof(ValueTupleCsvSerializer<,,,,,,,>);

                default:
                    break;
            }

            throw new InvalidOperationException($"ValueTupleCsvSerializer<T1...T{i}> is not found.");
        }
    }
}