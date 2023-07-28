namespace Day_5;

public class Process
{
    public static string Part_1(string input)
    {
        var lines = input.Split("\n");
        var divideIndex = Array.IndexOf(lines, string.Empty);
        var numberOfStacks = Int32.Parse(
            lines[divideIndex - 1]
            .Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Last()
         );
        var stackingService = new StackingService(lines.Take(divideIndex - 1), numberOfStacks);
        stackingService.StackingOperations(lines.TakeLast(lines.Length - 1 - divideIndex));
        return stackingService.StackTop();
    }

    public static string Part_2(string input)
    {
        var lines = input.Split("\n");
        var divideIndex = Array.IndexOf(lines, string.Empty);
        var numberOfStacks = Int32.Parse(
            lines[divideIndex - 1]
            .Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Last()
         );
        var stackingService = new StackingService(lines.Take(divideIndex - 1), numberOfStacks);
        stackingService.MultiMoveStackingOperations(lines.TakeLast(lines.Length - 1 - divideIndex));
        return stackingService.StackTop();
    }
}

public class StackingService
{
    SupplyStack[] supplyStacks;
    public StackingService(IEnumerable<string> input, int totalStacks)
    {
        supplyStacks = new SupplyStack[totalStacks];
        foreach (string stackItems in input.Reverse())
        {
            for(var stackIndex = 0; stackIndex<totalStacks; stackIndex++)
            {
                supplyStacks[stackIndex] ??= new SupplyStack();
                if (char.IsLetter(stackItems[stackIndex * 4 + 1]))
                {
                    supplyStacks[stackIndex].Items.Push(stackItems[stackIndex * 4 + 1]);
                }
            }
        }
    }

    List<(int crates_to_move, int source_crate_idx, int target_crate_idx)> getInstruction(IEnumerable<string> operations)
    {
        List<(int, int, int)> instructions = new List<(int, int, int)>();
        foreach (string operation in operations)
        {
            var instruction = operation.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var crates_to_move = Int32.Parse(instruction[1]);
            var source_crate_idx = Int32.Parse(instruction[3]) - 1;
            var tarket_crate_idx = Int32.Parse(instruction[5]) - 1;
            instructions.Add((crates_to_move, source_crate_idx, tarket_crate_idx));
        }
        return instructions;
    }

    public void StackingOperations(IEnumerable<string> operations)
    {
        var instructions = getInstruction(operations);
        foreach (var instruction in instructions)
        {
            var crates_to_move = instruction.crates_to_move;
            while (crates_to_move > 0)
            {
                crates_to_move--;
                var crate = supplyStacks[instruction.source_crate_idx].Items.Pop();
                supplyStacks[instruction.target_crate_idx].Items.Push(crate);
            }
        }
    }

    public void MultiMoveStackingOperations(IEnumerable<string> operations)
    {
        var instructions = getInstruction(operations);
        foreach (var instruction in instructions)
        {
            var crates_to_move = instruction.crates_to_move;
            Stack<char> temp_stack = new Stack<char>();
            while (crates_to_move > 0)
            {
                crates_to_move--;
                var crate = supplyStacks[instruction.source_crate_idx].Items.Pop();
                temp_stack.Push(crate);
            }
            while(temp_stack.TryPeek(out char result))
            {
                supplyStacks[instruction.target_crate_idx].Items.Push(temp_stack.Pop());
            }
        }
    }

    public string StackTop()
    {
        return string.Join("",supplyStacks.Select(crates => crates.Items.Peek()));
    }
}

public class SupplyStack
{
    public Stack<char> Items { get; set; } = new Stack<char>();
}