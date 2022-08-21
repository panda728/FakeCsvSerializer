# FakeCsvSerializer
Convert the object to an csv-like file.

## Getting Started
Supporting platform is .NET 6.

~~~
PM> Install-Package FakeCsvSerializer
~~~

## Usage
You can use `CsvSerializer.ToFile`.

~~~
CsvSerializer.ToFile(Users, "test.csv", CsvSerializerOptions.Default);
~~~

## Examples

If you pass an object, it will be converted to an Excel file.
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


Options can be set to display a title line and automatically adjust column widths.
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

