using Bogus;
using FakeCsvSerializer;
using System.Diagnostics;
using System.Globalization;
using static Bogus.DataSets.Name;

var sw = Stopwatch.StartNew();

Randomizer.Seed = new Random(8675309);

var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };

var orderIds = 0;
var testOrders = new Faker<Order>()
    .StrictMode(true)
    .RuleFor(o => o.OrderId, f => orderIds++)
    .RuleFor(o => o.Item, f => f.PickRandom(fruit))
    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
    .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f));

var userIds = 0;
var testUsers = new Faker<User>()
    .CustomInstantiator(f => new User(userIds++, f.Random.Replace("###-##-####")))
    .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
    .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
    .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
    .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
    .RuleFor(u => u.TimeStamp, f => DateTime.Now)
    .RuleFor(u => u.CreateTime, f => DateTime.Now)
    .RuleFor(u => u.DateOnlyValue, f => DateOnly.FromDateTime(DateTime.Today))
    .RuleFor(u => u.TimeOnlyValue, f => TimeOnly.FromDateTime(DateTime.Now))
    .RuleFor(u => u.TimeSpanValue, f => DateTime.Now - DateTime.Today)
    .RuleFor(u => u.DateTimeOffsetValue, f => DateTimeOffset.UtcNow)
    .RuleFor(u => u.Fallback, (f, u) => (object)userIds)
    .RuleFor(u => u.Uri, f => new Uri(f.Internet.Url()))
    .RuleFor(u => u.SomeGuid, f => Guid.NewGuid())
    .RuleFor(u => u.SendFlag, f => userIds % 3 == 0)
    .RuleFor(u => u.CartId, f => Guid.NewGuid())
    .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
    .RuleFor(u => u.Orders, f => testOrders.Generate(3).ToList())
    .RuleFor(o => o.Value, f => f.Random.Double(0, 1000));

var users = testUsers.Generate(100000);

sw.Stop();
Console.WriteLine($"testUsers.Generate count:{users.Count:#,##0} duration:{sw.ElapsedMilliseconds:#,##0}ms");
sw.Restart();

var newConfig = CsvSerializerOptions.Default with
{
    CultureInfo = CultureInfo.InvariantCulture,
    MaxDepth = 32,
    Provider = CsvSerializerProvider.Create(
        new[] { new BoolZeroOneSerializer() },
        new[] { CsvSerializerProvider.Default }),
    Trim = true,
    HasHeaderRecord = true,
    //HeaderTitles = new string[] { "Id", "FName", "LName", "Name", "UserID", "Email", "Key", "Guid", "Flag", "Profile", "CartGuid", "TEL", "UnixTime", "Create Time", "Date", "Time", "TimeSpan", "DateTimeOffset", "Fallback", "Uri", "Gender", "OrderNumber1", "Item1", "Qty1", "Lot1", "OrderNumber2", "Item2", "Qty", "Lot2", "OrderNumber3", "Item3", "Qty3", "Lot3", "Value" },
};

var fileName = Path.Combine(Environment.CurrentDirectory, "test.csv");
CsvSerializer.ToFile(users, fileName, newConfig);

sw.Stop();

Console.WriteLine($"CsvSerializer.ToFile duration:{sw.ElapsedMilliseconds:#,##0}ms");

Console.WriteLine($"Excel file created. Please check the file. {fileName}");

Console.WriteLine();
Console.WriteLine("press any key...");

Console.ReadLine();

public class BoolZeroOneSerializer : ICsvSerializer<bool>
{
    public void WriteTitle(ref CsvSerializerWriter writer, bool value, CsvSerializerOptions options, string name = "")
        => writer.Write(name);

    public void Serialize(ref CsvSerializerWriter writer, bool value, CsvSerializerOptions options)
    {
        // true => 0, false => 1
        writer.WritePrimitive(value ? 0 : 1);
    }
}

public class UnixSecondsSerializer : ICsvSerializer<DateTime>
{
    public void WriteTitle(ref CsvSerializerWriter writer, DateTime value, CsvSerializerOptions options, string name = "")
        => writer.Write(name);

    public void Serialize(ref CsvSerializerWriter writer, DateTime value, CsvSerializerOptions options)
    {
        writer.WritePrimitive(((DateTimeOffset)(value)).ToUnixTimeSeconds());
    }
}

#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
public class Order
{
    public int OrderId { get; set; }
    public string Item { get; set; }
    public int Quantity { get; set; }
    public int? LotNumber { get; set; }
}

public class User
{
    public User(int userId, string ssn)
    {
        Id = userId;
        SSN = ssn;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string SomethingUnique { get; set; }
    public Guid SomeGuid { get; set; }
    public bool SendFlag { get; set; }

    public string Avatar { get; set; }
    public Guid CartId { get; set; }
    public string SSN { get; set; }
    [CsvSerializer(typeof(UnixSecondsSerializer))]
    public DateTime TimeStamp { get; set; }
    public DateTime CreateTime { get; set; }
    public DateOnly DateOnlyValue { get; set; }
    public TimeOnly TimeOnlyValue { get; set; }
    public TimeSpan TimeSpanValue { get; set; }
    public DateTimeOffset DateTimeOffsetValue { get; set; }
    public object Fallback { get; set; }
    public Uri Uri { get; set; }
    public Bogus.DataSets.Name.Gender Gender { get; set; }

    public List<Order> Orders { get; set; }
    public double Value { get; set; }
}
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
