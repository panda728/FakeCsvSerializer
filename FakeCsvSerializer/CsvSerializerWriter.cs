using System.Buffers;
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
    public override string ToString() => _options.Encoding.GetString(_writer.OutputAsSpan);

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
        {
            ThrowReachedMaxDepth(_currentDepth);
        }
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
    public void Write(ReadOnlySpan<char> value) => _options.Encoding.GetBytes(value, _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(bool value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(byte value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(sbyte value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(char value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(decimal value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(double value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(float value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(int value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(uint value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(long value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(ulong value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(short value) => _options.Encoding.GetBytes($"{value}", _writer);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WritePrimitive(ushort value) => _options.Encoding.GetBytes($"{value}", _writer);

    static void ThrowReachedMaxDepth(int depth)
    {
        throw new InvalidOperationException($"Serializer detects reached max depth:{depth}. Please check the circular reference.");
    }
}