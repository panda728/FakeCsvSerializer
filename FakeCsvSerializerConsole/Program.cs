﻿using FakeCsvSerializer;
using Bogus;
using System.Globalization;
using System.Text.Encodings.Web;
using static Bogus.DataSets.Name;
using System.Reflection;

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
    .RuleFor(u => u.SomeGuid, f => Guid.NewGuid())
    .RuleFor(u => u.SendFlag, f => userIds % 3 == 0)
    .RuleFor(u => u.CartId, f => Guid.NewGuid())
    .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
    .RuleFor(u => u.Orders, f => testOrders.Generate(3).ToList())
    .RuleFor(o => o.Value, f => f.Random.Double(0, 1000))
    .FinishWith((f, u) =>
    {
        //Console.WriteLine("User Created! Id={0}", u.Id);
    });

var Users = testUsers.Generate(100000);
var newConfig = CsvSerializerOptions.Default with
{
    CultureInfo = CultureInfo.InvariantCulture,
    MaxDepth = 32,
    Provider = CsvSerializerProvider.Create(
        new[] { new BoolZeroOneSerializer() },
        new[] { CsvSerializerProvider.Default }),
    HasHeaderRecord = true,
    HeaderTitles = new string[] { "Id", "名", "姓", "氏名", "ユーザー名", "Email", "ユニークキー", "Guid", "Flag", "プロフィール画像", "カートGuid", "TEL", "UnixTime", "作成日時", "性別", "オーダー番号1", "アイテム1", "数量1", "ロット1", "オーダー番号2", "アイテム2", "数量2", "ロット2", "オーダー番号3", "アイテム3", "数量3", "ロット3", "数値" },
};

CsvSerializer.ToFile(Users, "test.csv", newConfig);

public class BoolZeroOneSerializer : ICsvSerializer<bool>
{
    public void Serialize(ref CsvSerializerWriter writer, bool value, CsvSerializerOptions options)
    {
        // true => 0, false => 1
        writer.WritePrimitive(value ? 0 : 1);
    }
}

public class UnixSecondsSerializer : ICsvSerializer<DateTime>
{
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
    public Bogus.DataSets.Name.Gender Gender { get; set; }

    public List<Order> Orders { get; set; }
    public double Value { get; set; }
}
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
