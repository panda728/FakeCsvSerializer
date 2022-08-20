# FakeExcelSerializer
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

## Note
For the method of retrieving values from IEnumerable\<T\>, Cysharp's WebSerializer method is used.

　https://github.com/Cysharp/WebSerializer
  
## License
This library is licensed under the MIT License.

