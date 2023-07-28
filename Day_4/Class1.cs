namespace Day_4;

public class Process
{
    public static int Part_1(string input)
    {
        return input
            .Split("\n") // Processing each line as a case
            .Select(line =>
                {
                    // Processing each assignment into its own tuple of start and end
                    var assignments = line
                        .Split(",")
                        .Select(assignment => GetInterval(assignment)).ToList();
                    // If assignment between the start and end boundries
                    // of another assigment it is contained within it
                    if (
                        assignments[0].Contains(assignments[1]) ||
                        assignments[1].Contains(assignments[0])
                    ) return 1;
                    return 0;
                }
            ).Sum(i => i);
    }

    public static int Part_2(string input)
    {
        return input.Split("\n")
            .Select(line =>
            {
                var assignments = line
                    .Split(",")
                    .Select(assignment => GetInterval(assignment)).ToList();
                if (
                    assignments[0].Overlaps(assignments[1]) ||
                    assignments[1].Overlaps(assignments[0])
                 ) return 1;
                return 0;
            }).Sum();
    }

    static Assignment GetInterval(string assignment) => new Assignment(
        int.Parse(assignment.Split("-")[0]),
        int.Parse(assignment.Split("-")[1])
    );
}

public class Assignment
{
    public Assignment(int s, int e)
    {
        start = s;
        end = e;
    }
    private int start { get; set; }
    private int end { get; set; }

    public bool Contains(Assignment assignment)
    {
        if (start <= assignment.start && end >= assignment.end) return true;
        return false;
    }

    public bool Overlaps(Assignment assignment)
    {
        if (start <= assignment.start && end >= assignment.start) return true;
        return false;
    }
}

