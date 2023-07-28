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

    internal static List<List<string>> Parse(string input)
    {
        return input
            .Split("\n")
            .Select(x =>
                x.Replace("Blueprint ", "")
                .Replace(": Each ore robot costs ", " ")
                .Replace(" ore. Each clay robot costs ", " ")
                .Replace(" ore. Each obsidian robot costs ", " ")
                .Replace(" ore and ", " ")
                .Replace(" clay. Each geode robot costs ", " ")
                .Replace(" ore and ", " ")
                .Replace(" obsidian.", " ").Split(" ").ToList<string>()
            ).ToList<List<string>>();
    }

    internal static int Part_1(string input)
    {
        List<List<string>> bluePrints = Parse(input);
        List<BluePrint> blue_print = bluePrints.Select(bp => new BluePrint
        {
            Id = SByte.Parse(bp[0]),
            oreRobot = SByte.Parse(bp[1]),
            clayRobot = SByte.Parse(bp[2]),
            obsidianRobot = (ore: SByte.Parse(bp[3]), clay: SByte.Parse(bp[4])),
            geodeRobot = (ore: SByte.Parse(bp[5]), obsidian: SByte.Parse(bp[6]))
        }).ToList();
        int time_limit = 24;
        int result = 0;
        foreach (var bp in blue_print)
        {
            State state = new State
            {
                minutes = 0,
                oreBot = 1,
                clayBot = 0,
                obsidianBot = 0,
                geodeBot = 0,
                ore = 0,
                clay = 0,
                obsedian = 0,
                geode = 0
            };
            bp.maxOreCost = Math.Max(Math.Max(bp.oreRobot, bp.clayRobot), Math.Max(bp.obsidianRobot.ore, bp.geodeRobot.ore));
            bp.maxClayCost = bp.obsidianRobot.clay;
            bp.maxObsidianCost = bp.geodeRobot.obsidian;
            //// this is old code
            //Dictionary<State, int> memo = new Dictionary<State, int>();
            // Some to check time from state
            Dictionary<string, (int result, int time)> memo = new Dictionary<string, (int, int)>();
            int maxResult = 0;
            //var solution = Solve(bp, state, time_limit, memo);
            var solution = Solve(bp, state, time_limit, memo, maxResult, true, true, true);
            Console.WriteLine("Part "+bp.Id+" Solution "+solution);
            result += ( solution * bp.Id );
        }
        return result;
    }

    private static int Solve(BluePrint blue_print, State state, int time_limit,
        Dictionary<string, (int, int)> memo, int maxResult,
        bool canOre, bool canClay, bool canObsedian)
    {
        //int result;
        (int result,int time) mem_state;
        //if (memo.TryGetValue(state, out result))
        if (memo.TryGetValue(state.key(), out mem_state))
        {
            //return result;
            if (state.minutes >= mem_state.time)
            {
                maxResult = Math.Max(maxResult, mem_state.result);
                return mem_state.result;
            }
        }
        if (state.minutes == time_limit)
        {
            memo[state.key()] = (state.geode, state.minutes);
            return state.geode;
        }
        int result = 0;
        if (state.can_build_geode_robot(blue_print))
        {
            result = Math.Max(
                result,
                Solve(
                    blue_print,
                    state.step().build_geode_robot(blue_print),
                    time_limit,
                    memo,
                    maxResult,
                    true, true, true
                )
            );
        }
        bool newCanObsedian = true;
        if (state.can_build_obsidian_robot(blue_print))
        {
            newCanObsedian = false;
            if (state.obsidianBot<blue_print.maxObsidianCost && canObsedian)
            {
                result = Math.Max(
                    result,
                    Solve(
                        blue_print,
                        state.step().build_obsidian_robot(blue_print),
                        time_limit,
                        memo,
                        maxResult,
                        true,true, true
                    )
                );
            }
        }
        bool newCanClay = true;
        if (state.can_build_clay_robot(blue_print))
        {
            newCanClay = false;
            if (state.clayBot < blue_print.maxClayCost && canClay)
            {
                result = Math.Max(
                    result,
                    Solve(
                        blue_print,
                        state.step().build_clay_robot(blue_print),
                        time_limit,
                        memo,
                        maxResult,
                        true,true,true
                    )
                );
            }
        }
        bool newCanOre = true;
        if (state.can_build_ore_robot(blue_print))
        {
            newCanOre = false;
            if (state.oreBot < blue_print.maxOreCost && canOre)
            {
                result = Math.Max(
                    result,
                    Solve(
                        blue_print,
                        state.step().build_ore_robot(blue_print),
                        time_limit,
                        memo,
                        maxResult,
                        true,true,true
                    )
                );
            }
        }
        result = Math.Max(
                result, Solve(
                    blue_print,
                    state.step(),
                    time_limit,
                    memo,
                    maxResult,
                    canOre,
                    canClay,
                    canObsedian
                )
         );

        memo[state.key()] = (result, state.minutes);
        return result;
    }

    internal static int Part_2(string input)
    {
        throw new NotImplementedException();
    }
}

internal record State
{
    public sbyte oreBot { get; set; }
    public sbyte clayBot { get; set; }
    public sbyte obsidianBot { get; set; }
    public sbyte geodeBot { get; set; }
    public sbyte ore { get; set; }
    public sbyte clay { get; set; }
    public sbyte obsedian { get; set; }
    public sbyte geode { get; set; }
    public sbyte minutes { get; internal set; }
    public string key() {
        return $"{oreBot}{clayBot}{obsidianBot}{geodeBot}{ore}{clay}{obsedian}{geode}";
        // The below bit operations don't make the code run any faster
       // int key_1 = (oreBot << 24) |
       //     (clayBot << 16) |
       //     (obsidianBot << 8) |
       //     (geodeBot);
       //int key_2 = (ore << 24) |
       //     (clay << 16) |
       //     (obsedian << 8) |
       //     geode;
       // Int64 key = ((long)key_1 << 32) | key_2;
        //Console.WriteLine(
        //    "key : " + key +
        //    " oreBot: " + oreBot +
        //    " clayBot: " + clayBot +
        //    " obsidianBot: " + obsidianBot +
        //    " geodeBot: " + geodeBot +
        //    " ore: " + ore +
        //    " clay: " + clay +
        //    " obsidian: " + obsedian +
        //    " geode " + geode);
        //return key;
    }

    internal State build_clay_robot(BluePrint blue_print)
    {
        State state = this with { };
        state.ore -= blue_print.clayRobot;
        state.clayBot += 1;
        return state;
    }

    internal State build_geode_robot(BluePrint blue_print)
    {
        State state = this with { };
        state.obsedian -= blue_print.geodeRobot.obsidian;
        state.ore -= blue_print.geodeRobot.ore;
        state.geodeBot += 1;
        return state;
    }

    internal State build_obsidian_robot(BluePrint blue_print)
    {
        State state = this with { };
        state.obsidianBot += 1;
        state.ore -= blue_print.obsidianRobot.ore;
        state.clay -= blue_print.obsidianRobot.clay;
        return state;
    }

    internal State build_ore_robot(BluePrint blue_print)
    {
        State state = this with { };
        state.oreBot += 1;
        state.ore -= blue_print.oreRobot;
        return state;
    }

    internal bool can_build_clay_robot(BluePrint blue_print)
    {
        if (blue_print.clayRobot <= this.ore) return true;
        return false;
    }

    internal bool can_build_geode_robot(BluePrint blue_print)
    {
        if (
            blue_print.geodeRobot.ore <= this.ore &&
            blue_print.geodeRobot.obsidian <= this.obsedian
        )
        {
            return true;
        }
        return false;
    }

    internal bool can_build_obsidian_robot(BluePrint blue_print)
    {
        if (
            blue_print.obsidianRobot.ore <= this.ore &&
            blue_print.obsidianRobot.clay <= this.clay
        )
        {
            return true;
        }
        return false;
    }

    internal bool can_build_ore_robot(BluePrint blue_print)
    {
        if (blue_print.oreRobot <= this.ore) return true;
        return false;
    }

    internal State step()
    {
        State state = this with { };
        state.minutes += 1;
        state.ore += this.oreBot;
        state.clay += this.clayBot;
        state.obsedian += this.obsidianBot;
        state.geode += this.geodeBot;
        return state;
    }
}

internal class BluePrint
{
    public sbyte Id { get; set; }
    public sbyte oreRobot { get; set; }
    public sbyte clayRobot { get; set; }
    public (sbyte ore, sbyte clay) obsidianRobot { get; set; }
    public (sbyte ore, sbyte obsidian) geodeRobot { get; set; }
    public sbyte maxOreCost { get; internal set; }
    public sbyte maxObsidianCost { get; internal set; }
    public sbyte maxClayCost { get; internal set; }
}