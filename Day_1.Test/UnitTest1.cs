namespace Day_1.Test;
using Day_1;
public class Tests
{
    string input = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Part_1.process(input), Is.EqualTo(24000));
    }

    [Test]
    public void Test2()
    {
        Assert.That(Part_2.process(input), Is.EqualTo(45000));
    }
}
