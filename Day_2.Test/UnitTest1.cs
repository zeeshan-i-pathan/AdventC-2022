namespace Day_2.Test;

public class Tests
{
    string input = @"A Y
B X
C Z";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Process.Part1(input), Is.EqualTo(15));
    }

    [Test]
    public void Test2()
    {
        Assert.That(Process.Part2(input), Is.EqualTo(12));
    }
}
