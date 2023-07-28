using System.IO;
using System.Reflection;
using System;
using System.Linq; // Make 'Select' extension available
using PluralizeService;

using Day_1;
using System.Diagnostics.CodeAnalysis;
// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

Assembly asm = Assembly.GetExecutingAssembly();
var resourceName = "Runner.Day_1_input.txt";

using (Stream stream = asm.GetManifestResourceStream(resourceName))
using (StreamReader reader = new StreamReader(stream))
{
    string result = reader.ReadToEnd();
    Console.WriteLine("{0} {1}",
    Part_1.process(result),
    Part_2.process(result));
}


Customer _customer = new Customer(
    Name: "Zeeshan",
    Age: 37,
    WorkingDays: (ShiftDays)62
);

Customer _customer1 = _customer with { };
Console.WriteLine(_customer == _customer1);
Console.WriteLine(_customer);
ShiftDays AvailableDays = (ShiftDays)62;
Console.WriteLine(AvailableDays.HasFlag(ShiftDays.Friday));
Console.WriteLine(AvailableDays);

var input = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

var max = input
    .Split("\n\n")
    .Select(
        set => set.Split("\n").Select(set => Int32.Parse(set)).Sum()
    ).Max();
var max_3 = input
    .Split("\n\n")
    .Select(
        set => set.Split("\n").Select(set => Int32.Parse(set)).Sum()
    ).OrderByDescending(i => i).Take(3).Sum();

Console.WriteLine(max);
Console.WriteLine(max_3);

Console.WriteLine(PluralizeService.Core.PluralizationProvider.Pluralize("User"));

List<Customer> customers = new List<Customer>
{
new Customer("Zeeshan", 23, ShiftDays.Monday | ShiftDays.Tuesday | ShiftDays.Wednesday),
new Customer("Marwah", 1, ShiftDays.Tuesday),
};

var find = from c in customers
           where c.WorkingDays.HasFlag(ShiftDays.Tuesday)
           select c;

foreach (Customer customer in find)
{
    Console.WriteLine(customer);
}

Dictionary<string, Customer> customerDic = new Dictionary<string, Customer>();
customerDic.Add("L", new Customer("Z", 20, ShiftDays.Friday));
customerDic.Add("M", new Customer("M", 25, ShiftDays.Wednesday));
foreach (var item in customerDic.Keys)
{
    Console.WriteLine(item);
}

var Age = (Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) - Int32.Parse(new DateOnly(1986, 02, 07).ToString("yyyyMMdd"))) / 10_000;
Console.WriteLine($"{Age,10:C2}");
string? input_1 = null;

Console.WriteLine(PadAndTrim(input_1));

int value;

TrySomething(out value);
// System.Console.WriteLine(value);

bool TrySomething(out int value)
{
    value = 1;
    return false;
}

static string PadAndTrim([AllowNull] string input)
{
    if (input == null)
        return string.Empty.PadLeft(15, '0');
    return input.PadLeft(15, '0');
}

public record Customer(string Name, int Age, ShiftDays WorkingDays);

[Flags]
public enum ShiftDays : short
{
    Sunday = 1,
    Monday = 2,
    Tuesday = 4,
    Wednesday = 8,
    Thursday = 16,
    Friday = 32,
    Saturday = 64
}