namespace Day_19;
using Xunit;
using FluentAssertions;
using System;

public class Tests
{
    string input = @"Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.";


    [Fact]
    public void Test1()
    {
        Process.Part_1(input).Should().Be(33);
    }

    [Fact]
    public void Test2()
    {
        //Process.Part_2(input);
    }
}

internal class Process
{
    public Process()
    {
    }

    internal static List<string> Parse(string input)
    {
        return input.Replace("Blueprint ", "")
            .Replace(": Each ore robot costs ", " ")
            .Replace(" ore. Each clay robot costs ", " ")
            .Replace(" ore. Each obsidian robot costs ", " ")
            .Replace(" ore and "," ")
            .Replace(" clay. Each geode robot costs "," ")
            .Replace(" ore and "," ")
            .Replace(" obsidian."," ").Split(" ").ToList<string>();
    }

    internal static int Part_1(string input)
    {
        List<string> bluePrints = Parse(input);
        Console.WriteLine(bluePrints);
        BluePrint bluePrint = new BluePrint();
        return 0;
    }

    internal static int Part_2(string input)
    {
        throw new NotImplementedException();
    }
}

internal class BluePrint
{
    public BluePrint()
    {
    }
}