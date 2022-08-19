# FakeCsvSerializer

IEnumerable\<T\>をCSVっぽいデータに変換します。

### Usage

~~~
CsvSerializer.ToFile(Users, "test.csv", CsvSerializerOptions.Default);
~~~

### シリアライズの方式について

IEnumerable<T>から値を取り出す方法については
Cysharp様のWebSerializerの方式を使用しています。

効率的でなおかつ拡張性もある非常に素晴らしい構成です。

https://github.com/Cysharp/WebSerializer
  
### .xlsxなデータを出力するバージョンはこちら
  
  https://github.com/panda728/FakeExcelSerializer


