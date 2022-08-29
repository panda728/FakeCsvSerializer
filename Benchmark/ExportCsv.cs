using BenchmarkDotNet.Attributes;
using CsvHelper;
using CsvHelper.Configuration;
using FakeCsvSerializer;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace BenchmarkSample
{
    [MarkdownExporterAttribute.GitHub]
    [ShortRunJob]
    [MemoryDiagnoser]
    public class ExportCsv
    {
        readonly List<Row> rows = new();
        readonly byte[] _crlf = new[] { (byte)'\r', (byte)'\n' };
        static readonly string exePath = Assembly.GetEntryAssembly()?.Location ?? "";
        static readonly string workPath = Path.Combine(Path.GetDirectoryName(exePath) ?? "", "work");
        readonly string streamFileName = Path.Combine(workPath, $"stream-{Guid.NewGuid()}.csv");
        readonly string csvhelperFileName = Path.Combine(workPath, $"csvhelper-{Guid.NewGuid()}.csv");
        readonly string fakeCsvFileName = Path.Combine(workPath, $"FakeCsv-{Guid.NewGuid()}.csv");

        public ExportCsv()
        {
            if (!Directory.Exists(workPath))
                Directory.CreateDirectory(workPath);
        }

        [Params(1, 10, 100)]
        public int N;

        const int HEADER_LEN = 9 + 30;
        const int DETAIL_LEN = 10;
        const int FOOTER_LEN = 39;
        const int DETAIL_COUNT = 10;

        void CleanupFiles()
        {
            if (File.Exists(streamFileName))
                File.Delete(streamFileName);

            if (File.Exists(csvhelperFileName))
                File.Delete(csvhelperFileName);

            if (File.Exists(fakeCsvFileName))
                File.Delete(fakeCsvFileName);
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            CleanupFiles();

            var list = new List<Row>();
            var lineNum = 0;
            using var sr = new StreamReader("data01.dat");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (line == null)
                    break;

                if (!int.TryParse(line[..9], out var headerID))
                    throw new ApplicationException("Could not be converted to int.");
                for (int i = 0; i < DETAIL_COUNT; i++)
                {
                    list.Add(new Row
                    {
                        LineNum = lineNum++,
                        HeaderID = headerID,
                        DetailID = i + 1,
                        Data = line.Substring(HEADER_LEN + (DETAIL_LEN * i), DETAIL_LEN),
                        Header01 = line[9..13],
                        Header02 = line[13..20],
                        Header03 = line[20..25],
                        Header04 = line[25..27],
                        Header05 = line[27..30],
                        Header06 = line[30..33],
                        Header07 = line[33..39],
                        Footer01 = line[139..144],
                        Footer02 = line[144..153],
                        Footer03 = line[153..155],
                        Footer04 = line[155..158],
                        Footer05 = line[158..166],
                        Footer06 = line[166..171],
                        Footer07 = line[171..174],
                        Footer08 = line[174..178],
                    });
                }
            }

            rows.Clear();
            for (int i = 0; i < N; i++)
                rows.AddRange(list);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            CleanupFiles();
        }

        #region StreamWriter
        //[Benchmark]
        //public async void StreamWriter()
        //{
        //    using (var sw = new StreamWriter(streamFileName, false, Encoding.UTF8))
        //    {
        //        foreach (var r in rows)
        //        {
        //            await sw.WriteAsync($"\"{r.LineNum}\",");
        //            await sw.WriteAsync($"\"{r.HeaderID}\",");
        //            await sw.WriteAsync($"\"{r.DetailID}\",");
        //            await sw.WriteAsync($"\"{r.Data}\",");
        //            await sw.WriteAsync($"\"{r.Header01}\",");
        //            await sw.WriteAsync($"\"{r.Header02}\",");
        //            await sw.WriteAsync($"\"{r.Header03}\",");
        //            await sw.WriteAsync($"\"{r.Header04}\",");
        //            await sw.WriteAsync($"\"{r.Header05}\",");
        //            await sw.WriteAsync($"\"{r.Header06}\",");
        //            await sw.WriteAsync($"\"{r.Header07}\",");
        //            await sw.WriteAsync($"\"{r.Footer01}\",");
        //            await sw.WriteAsync($"\"{r.Footer02}\",");
        //            await sw.WriteAsync($"\"{r.Footer03}\",");
        //            await sw.WriteAsync($"\"{r.Footer04}\",");
        //            await sw.WriteAsync($"\"{r.Footer05}\",");
        //            await sw.WriteAsync($"\"{r.Footer06}\",");
        //            await sw.WriteAsync($"\"{r.Footer07}\",");
        //            await sw.WriteAsync($"\"{r.Footer08}\"");
        //            await sw.WriteLineAsync();
        //        }
        //    }
        //}
        #endregion

        #region CsvHelper
        [Benchmark(Baseline = true)]
        public void CsvHelper()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                ShouldQuote = (context) => true,
            };
            using (var writer = new StreamWriter(csvhelperFileName))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(rows);
            }
        }
        #endregion

        #region FakeCsvSerializer
        [Benchmark]
        public void FakeCsvSerializer()
        {
            var customOptions = CsvSerializerOptions.Default with
            {
                CultureInfo = CultureInfo.InvariantCulture,
                HasHeaderRecord = false,
            };
            CsvSerializer.ToFile(rows, fakeCsvFileName, customOptions);
        }
        #endregion
    }
}
