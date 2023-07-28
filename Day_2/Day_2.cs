using System.Text;
using System.Linq;
namespace Day_2;

public class Process
{
    public static long Part1(string input)
    {
        long totalScore = 0;
        foreach (var line in input.Split("\n"))
        {
            Round round = new Round(line, 1);
            totalScore += round.GetRoundScore();
        }
        return totalScore;
        
    }

    public static long Part2(string input)
    {
        long totalScore = 0;
        foreach (var line in input.Split("\n"))
        {
            Round round = new Round(line, 2);
            totalScore += round.GetRoundScore();
        }
        return totalScore;
    }
}

public enum Move
{
    Rock,
    Paper,
    Scissors
}

public enum RoundOutcome
{
    Loss,
    Draw,
    Win
}

public class Round
{
    public Move Ours { get; set; }
    public Move Theirs { get; set; }
    public RoundOutcome Outcome { get; set; }

    public Round(string line, int flag)
    {
        var moves = line.Split(' ');
        Theirs = (Move)moves[0][0] - 'A';
        if (flag == 1)
        {
            Ours = (Move)moves[1][0] - 'X';
        } else
        {
            Outcome = (RoundOutcome)moves[1][0] - 'X';
            Ours = (Outcome == RoundOutcome.Win) ? (Move)(((int)Theirs + 1) % 3) : (Outcome == RoundOutcome.Draw) ? Theirs : (Move)(((int)Theirs + 2) % 3); 
        }
    }

    long GetMoveScore(Move move) => (int)move + 1;

    public long GetRoundScore()
    {
        long moveScore = GetMoveScore(Ours);
        long roundScore = ((int)Ours == ((int)Theirs+1) % 3) ? 6 : (int)Ours == (int)Theirs ? 3 : 0;
        return moveScore + roundScore;
    }
}