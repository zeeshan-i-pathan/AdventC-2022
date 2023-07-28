namespace Day_5.Test;
using Day_5;
public class Tests
{
    string input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Process.Part_1(input), Is.EqualTo("CMZ"));
    }

    [Test]
    public void Test2()
    {
        Assert.That(Process.Part_2(input), Is.EqualTo("MCD"));
    }
}
