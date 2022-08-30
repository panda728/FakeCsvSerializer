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
|            Method |   N |      Mean |     Error |    StdDev | Ratio | RatioSD |      Gen0 |    Gen1 |   Gen2 |   Allocated | Alloc Ratio |
|------------------ |---- |----------:|----------:|----------:|------:|--------:|----------:|--------:|-------:|------------:|------------:|
|         CsvHelper |   1 |  8.878 ms |  2.130 ms | 0.1167 ms |  1.00 |    0.00 |   93.7500 |       - |      - |    406.7 KB |        1.00 |
| FakeCsvSerializer |   1 |  1.560 ms | 11.263 ms | 0.6174 ms |  0.18 |    0.07 |   12.6953 |  5.8594 | 0.9766 |     53.9 KB |        0.13 |
|                   |     |           |           |           |       |         |           |         |        |             |             |
|         CsvHelper |  10 | 11.748 ms |  5.464 ms | 0.2995 ms |  1.00 |    0.00 |  515.6250 |       - |      - |  2148.45 KB |        1.00 |
| FakeCsvSerializer |  10 |  5.149 ms |  1.686 ms | 0.0924 ms |  0.44 |    0.01 |  117.1875 |  7.8125 |      - |    497.6 KB |        0.23 |
|                   |     |           |           |           |       |         |           |         |        |             |             |
|         CsvHelper | 100 | 37.832 ms |  8.318 ms | 0.4559 ms |  1.00 |    0.00 | 4785.7143 | 71.4286 |      - | 19558.86 KB |        1.00 |
| FakeCsvSerializer | 100 | 47.393 ms | 10.683 ms | 0.5856 ms |  1.25 |    0.03 | 1181.8182 | 90.9091 |      - |  4936.11 KB |        0.25 |

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

By setting attributes on the class, you can specify the name of the title or change the order of the columns.
~~~
public class PortalEx
{
    [DataMember(Name = "Name Ex", Order = 3)]
    public string Name { get; set; }
    [DataMember(Name = "Owner Ex", Order = 1)]
    public string Owner { get; set; }
    [DataMember(Name = "Level Ex", Order = 2)]
    public int Level { get; set; }
}

var potalsEx = new PortalEx[] {
    new PortalEx { Name = "Portal1", Owner = "panda728", Level = 8 },
    new PortalEx { Name = "Portal2", Owner = "panda728", Level = 1 },
    new PortalEx { Name = "Portal3", Owner = "panda728", Level = 2 },
};

CsvSerializer.ToFile(potalsEx, @"c:\test\potalsEx.csv", newConfigEx);
~~~

potalsEx.csv:
~~~
"Owner Ex","Level Ex","Name Ex"
"panda728","8","Portal1"
"panda728","1","Portal2"
"panda728","2","Portal3"
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

