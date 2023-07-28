namespace Day_1;

public class Part_1
{
    public static int process(string input)
    {
        return input
            .Split("\n\n")
            .Select(
                set => set.Split("\n").Select(set => Int32.Parse(set)).Sum()
            ).Max();
    }
}

public class Part_2
{
    public static int process(string input)
    {
        return input
            .Split("\n\n")
            .Select(
                set => set.Split("\n").Select(set => Int32.Parse(set)).Sum()
            ).OrderByDescending(i => i).Take(3).Sum();
    }
}