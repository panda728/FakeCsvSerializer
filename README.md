# FakeCsvSerializer
Convert the object to an csv file.

## Getting Started
Supporting platform is .NET Standard 2.0, 2.1, .NET 5, .NET 6.

~~~
PM> Install-Package FakeCsvSerializer
~~~

## Usage
You can use `CsvSerializer.ToFile`.

~~~
CsvSerializer.ToFile(Users, "test.csv", CsvSerializerOptions.Default);
~~~

## Benchmark
|            Method |   N |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD |      Gen0 |     Gen1 |   Allocated | Alloc Ratio |
|------------------ |---- |------------:|----------:|----------:|------------:|------:|--------:|----------:|---------:|------------:|------------:|
|         CsvHelper |   1 |  8,015.1 us | 158.66 us | 305.69 us |  8,082.1 us |  1.00 |    0.00 |   93.7500 |  46.8750 |   406.25 KB |        1.00 |
| FakeCsvSerializer |   1 |    733.5 us |  30.33 us |  85.56 us |    702.3 us |  0.09 |    0.01 |    2.9297 |   0.9766 |    13.98 KB |        0.03 |
|                   |     |             |           |           |             |       |         |           |          |             |             |
|         CsvHelper |  10 | 10,787.5 us | 108.24 us |  90.39 us | 10,757.9 us |  1.00 |    0.00 |  515.6250 |        - |  2143.73 KB |        1.00 |
| FakeCsvSerializer |  10 |  3,770.4 us |  74.93 us | 170.65 us |  3,791.1 us |  0.33 |    0.01 |   23.4375 |   7.8125 |    98.36 KB |        0.05 |
|                   |     |             |           |           |             |       |         |           |          |             |             |
|         CsvHelper | 100 | 37,116.4 us | 456.98 us | 405.10 us | 36,987.8 us |  1.00 |    0.00 | 4700.0000 | 100.0000 | 19511.62 KB |        1.00 |
| FakeCsvSerializer | 100 | 33,396.7 us | 650.69 us | 543.36 us | 33,103.1 us |  0.90 |    0.02 |  187.5000 |        - |   942.15 KB |        0.05 |

## Examples

If you pass an object, it will be converted to an CSV file.
~~~
CsvSerializer.ToFile(new string[] { "test", "test2" }, @"c:\test\test.csv", CsvSerializerOptions.Default);
~~~

test.csv:
~~~
"test"
"test2"
~~~

Passing a class expands the property into a column.
~~~
public class Portal
{
    public string Name { get; set; }
    public string Owner { get; set; }
    public int Level { get; set; }
}

var potals = new Portal[] {
    new Portal { Name = "Portal1", Owner = "panda728", Level = 8 },
    new Portal { Name = "Portal2", Owner = "panda728", Level = 1 },
    new Portal { Name = "Portal3", Owner = "panda728", Level = 2 },
};

CsvSerializer.ToFile(potals, @"c:\test\potals.csv", CsvSerializerOptions.Default);
~~~

potals.csv:
~~~
"Portal1","panda728","8"
"Portal2","panda728","1"
"Portal3","panda728","2"
~~~


Options can be set to display a title line.
~~~
var newConfig = CsvSerializerOptions.Default with
{
    CultureInfo = CultureInfo.InvariantCulture,
    Encoding = Encoding.UTF8,
    HasHeaderRecord = true,
    HeaderTitles = new string[] { "Name", "Owner", "Level" },
    Quote='"',
    Delimiter=',',
    NewLine=Environment.NewLine,
};
CsvSerializer.ToFile(potals, @"c:\test\potalsOp.csv", newConfig);
~~~

potalsOp.csv:
~~~
"Name","Owner","Level"
"Portal1","panda728","8"
"Portal2","panda728","1"
"Portal3","panda728","2"
~~~


## Note
For the method of retrieving values from IEnumerable\<T\>, Cysharp's WebSerializer method is used.

　https://github.com/Cysharp/WebSerializer
  
## Link
Excel(.xlsx) output version
　https://github.com/panda728/FakeExcelSerializer
 
## License
This library is licensed under the MIT License.

