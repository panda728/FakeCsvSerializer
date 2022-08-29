using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace FakeCsvSerializer;

public struct CsvSerializerWriter : IDisposable
{
    readonly ArrayPoolBufferWriter _writer = new();
    readonly CsvSerializerOptions _options;
    readonly byte[] _quote;
    readonly byte[] _quote2;
    readonly byte[] _delimiter;
    readonly byte[] _newLine;

    public void Dispose() => _writer.Dispose();

    int _currentDepth;
    bool _first;

    public CsvSerializerWriter(CsvSerializerOptions options)
    {
        _options = options;
        _currentDepth = 0;
        _first = true;
        _quote = options.Encoding.GetBytes($"{options.Quote}");
        _quote2 = options.Encoding.GetBytes($"{options.Quote}{options.Quote}");
        _delimiter = options.Encoding.GetBytes($"{options.Delimiter}");
        _newLine = options.Encoding.GetBytes(options.NewLine);
    }

    private void Clear()
    {
        _currentDepth = 0;
        _first = true;
        _writer.Clear();
    }

    public ReadOnlySpan<byte> AsSpan() => _writer.GetSpan();
    public ReadOnlyMemory<byte> AsMemory() => _writer.GetMemory();
    public long BytesCommitted() => _writer.BytesCommitted;
    public override string ToString() => Encoding.UTF8.GetString(
#if NETSTANDARD2_1_OR_GREATER
        _writer.OutputAsSpan);
#else
        _writer.OutputAsSpan.ToArray());
#endif

    public async Task CopyToAsync(Stream stream)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        await _writer.CopyToAsync(stream);
        Clear();
    }

    public void CopyTo(Stream stream)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        _writer.CopyTo(stream);
        Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnterAndValidate()
    {
        _first = true;
        _currentDepth++;
        if (_currentDepth >= _options.MaxDepth)
            ThrowReachedMaxDepth(_currentDepth);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Exit()
    {
        _currentDepth--;
    }

    /// <summary>Write raw bytes.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteRaw(ReadOnlySpan<byte> value) => _writer.Write(value);

    /// <summary>Write '"'.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteQuote()
    {
        if (_options.ShouldQuote)
            _writer.Write(_quote);
    }

    /// <summary>Write "\"\"".</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteEmpty()
    {
        if (_options.ShouldQuote)
            _writer.Write(_quote2);
    }

    /// <summary>Write ",".</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteDelimiter()
    {
        if (_first)
        {
            _first = false;
            return;
        }
        _writer.Write(_delimiter);
    }

    /// <summary>Write CRLF.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLine() => _writer.Write(_newLine);

    /// <summary>Write string.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteRaw(ReadOnlySpan<char> value)
    {
#if NET5_0_OR_GREATER
        _options.Encoding.GetBytes(value, _writer);
#else
        var bytes = _options.Encoding.GetBytes(value.ToArray(), 0, value.Length);
        _writer.Write(bytes);
#endif
    }

    private const string TargetChars = "\r\n\t, ";
    public void Write(in string? value)
    {
        if (value == null)
        {
            WriteEmpty();
            return;
        }

        var span = _options.Trim ? value.AsSpan().Trim() : value.AsSpan();
        var containsQuote = span.IndexOf(_options.Quote) > 0;
        if (containsQuote || _options.ShouldQuote || span.IndexOfAny(TargetChars.AsSpan()) > 0)
        {
            var quote = $"{_options.Quote}".AsSpan();
            WriteRaw(quote);
            WriteRaw(containsQuote ? Replace(span, quote, $"{_options.Quote}{_options.Quote}").AsSpan() : span);
            WriteRaw(quote);
            return;
        }
        Write(_options.Trim ? span.Trim() : span);
    }

    public static string? Replace(in ReadOnlySpan<char> value, ReadOnlySpan<char> oldValue, string newValue)
    {
        StringBuilder? sb = null;
        int pos;
        var span = value;
        while ((pos = span.IndexOfAny(oldValue)) >= 0)
        {
            sb ??= new StringBuilder();
#if NETSTANDARD2_1_OR_GREATER
            sb.Append(span.Slice(0, pos)).Append(newValue);
#else
            sb.Append(span.Slice(0, pos).ToString()).Append(newValue);
#endif
            span = span.Slice(pos + 1);
        }

#if NETSTANDARD2_1_OR_GREATER
        return sb == null ? span.ToString() : sb.Append(span).ToString();
#else
        return sb == null ? span.ToString() : sb.Append(span.ToString()).ToString();
#endif
    }

    /// <summary>Write string. with Quoate</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(ReadOnlySpan<char> value)
    {
        WriteQuote();
        WriteRaw(value);
        WriteQuote();
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(bool value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(byte value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(sbyte value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(char value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(decimal value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(double value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(float value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(int value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(uint value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(long value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(ulong value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(short value) => Write($"{value}".AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(ushort value) => Write($"{value}".AsSpan());

#if NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(DateOnly value) => Write(value.ToString(_options.CultureInfo).AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(TimeOnly value) => Write(value.ToString(_options.CultureInfo).AsSpan());
#endif

#if NETSTANDARD2_1_OR_GREATER
    [DoesNotReturn]
#endif
    static void ThrowReachedMaxDepth(int depth)
    {
        throw new InvalidOperationException($"Serializer detects reached max depth:{depth}. Please check the circular reference.");
    }
}