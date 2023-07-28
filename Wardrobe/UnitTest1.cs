namespace Wardrobe;

using System;
using FluentAssertions;

public class UnitTest1
{
    [Fact]
    public void FindPossibleCombinations()
    {
        int combinatinos = WardrobeSolver.Init();
        combinatinos.Should().Be(5);
    }
}

internal class WardrobeSolver
{
    static int branchesExplored = 0;
    internal static int Init()
    {
        BluePrint bluePrint = new BluePrint
        {
            itemA = (59, 50),
            itemB = (62, 75),
            itemC = (90, 100),
            itemD = (111, 120)
        };
        State state = new State
        {
            countA = 0,
            countB = 0,
            countC = 0,
            countD = 0,
            sizeOccupied =0
        };
        List<State> idealState = new List<State>();
        int wallSize = 250;
        // Some caching to make the code blazzingly fast
        Dictionary<string, State> keyValues = new Dictionary<string, State>();

        WardrobeSolver.Solve(bluePrint, state, wallSize, idealState, keyValues);
        Console.WriteLine("Branches Explored "+branchesExplored);
        return idealState.Count;
    }

    private static void Solve(BluePrint bluePrint, State state, int wallSize, List<State> idealState, Dictionary<string, State> keyValues)
    {
        State state1;
        if (keyValues.TryGetValue(state.getKey(), out state1))
        {
            return;
        }
        branchesExplored += 1;
        if (state.sizeOccupied == wallSize)
        {
            Console.WriteLine("Ideal state found "+state);
            keyValues[state.getKey()] = state;
            idealState.Add(state);
            return;
        }
        if (state.canAddA(bluePrint, wallSize))
        {
            Solve(bluePrint,state.addA(bluePrint), wallSize, idealState, keyValues);
        }
        if (state.canAddB(bluePrint, wallSize))
        {
            Solve(bluePrint, state.addB(bluePrint), wallSize, idealState, keyValues);
        }
        if (state.canAddC(bluePrint, wallSize))
        {
            Solve(bluePrint, state.addC(bluePrint), wallSize, idealState, keyValues);
        }
        if (state.canAddD(bluePrint, wallSize))
        {
            Solve(bluePrint, state.addD(bluePrint), wallSize, idealState, keyValues);
        }
        keyValues[state.getKey()] = state;

    }
}

internal record State
{
    public byte countA { get; set; }
    public byte countB { get; set; }
    public byte countC { get; set; }
    public byte countD { get; set; }
    public byte sizeOccupied { get; internal set; }

    public bool canAddA(BluePrint bluePrint, int wallSize)
    {
        if ((this.sizeOccupied + bluePrint.itemA.dimensions) <= wallSize) return true;
        return false;
    }
    public bool canAddB(BluePrint bluePrint, int wallSize)
    {
        if ((this.sizeOccupied + bluePrint.itemB.dimensions) <= wallSize) return true;
        return false;
    }
    public bool canAddC(BluePrint bluePrint, int wallSize)
    {
        if ((this.sizeOccupied + bluePrint.itemC.dimensions) <= wallSize) return true;
        return false;
    }
    public bool canAddD(BluePrint bluePrint, int wallSize)
    {
        if ((this.sizeOccupied + bluePrint.itemD.dimensions) <= wallSize) return true;
        return false;
    }

    internal State addA(BluePrint bluePrint)
    {
        State state = this with { };
        state.countA += 1;
        state.sizeOccupied += bluePrint.itemA.dimensions;
        return state;
    }

    internal State addB(BluePrint bluePrint)
    {
        State state = this with { };
        state.countB += 1;
        state.sizeOccupied += bluePrint.itemB.dimensions;
        return state;
    }

    internal State addC(BluePrint bluePrint)
    {
        State state = this with { };
        state.countC += 1;
        state.sizeOccupied += bluePrint.itemC.dimensions;
        return state;
    }

    internal State addD(BluePrint bluePrint)
    {
        State state = this with { };
        state.countD += 1;
        state.sizeOccupied += bluePrint.itemD.dimensions;
        return state;
    }

    internal string getKey()
    {
        return $"{countA}{countB}{countC}{countD}";
    }
}

internal record BluePrint
{
    public (byte price, byte dimensions) itemA { get; set; }
    public (byte price, byte dimensions) itemD { get; internal set; }
    public (byte price, byte dimensions) itemB { get; internal set; }
    public (byte price, byte dimensions) itemC { get; internal set; }
}