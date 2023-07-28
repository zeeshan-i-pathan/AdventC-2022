namespace Day_3_Test;
using Day_3;
public class Tests
{
    string input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Process.Part_1(input), Is.EqualTo(157));
    }

    [Test]
    public void Test2()
    {
        Assert.That(Process.Part_2(input), Is.EqualTo(70));
    }
}
