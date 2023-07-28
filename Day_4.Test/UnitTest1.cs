namespace Day_4.Test;
using Day_4;
public class Tests
{
    string input = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Process.Part_1(input), Is.EqualTo(2));
    }

    [Test]
    public void Test2()
    {
        Assert.That(Process.Part_2(input), Is.EqualTo(4));
    }
}
