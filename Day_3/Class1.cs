namespace Day_3;

public class Process
{
    public static int Part_1(string input)
    {
        var total = 0;
        foreach (var line in input.Split("\n"))
        {
            total += line.Take(line.Length / 2)
                .Intersect(line.TakeLast(line.Length / 2))
                .Sum(c => c <= 'Z'? c + 27 -'A': c + 1 - 'a');
        }
        return total;
    }

    public static int Part_2(String input)
    {
        var total = 0;
        foreach (var chunk in input.Split("\n").Chunk(3))
        {
            total += chunk[0]
                .Intersect(chunk[1]).Intersect(chunk[2])
                .Sum(c => c <= 'Z' ? c + 27 - 'A' : c + 1 - 'a');
        }
        return total;
    }
}

