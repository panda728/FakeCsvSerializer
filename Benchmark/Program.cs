using BenchmarkDotNet.Running;
using BenchmarkSample;
using System.Diagnostics;

var workPath = "work";
if (Directory.Exists(workPath))
{
    var files = Directory.GetFiles(workPath, "*.csv");
    for (int i = 0; i < files.Length; i++)
        File.Delete(files[i]);
}
Directory.CreateDirectory(workPath);

#if DEBUG
var ex = new ExportCsv();

ex.N = 1;
ex.GlobalSetup();

var sw = Stopwatch.StartNew();
//ex.StreamWriter();
//sw.Stop();
//Console.WriteLine($"StreamWriter : {sw.ElapsedMilliseconds:#,##0}ms");

//sw.Restart();
ex.CsvHelper();
sw.Stop();
Console.WriteLine($"CsvHelper : {sw.ElapsedMilliseconds:#,##0}ms");

sw.Restart();
ex.FakeCsvSerializer();
Console.WriteLine($"FakeCsvSerializer : {sw.ElapsedMilliseconds:#,##0}ms");
sw.Stop();

#else
BenchmarkRunner.Run<ExportCsv>();
#endif