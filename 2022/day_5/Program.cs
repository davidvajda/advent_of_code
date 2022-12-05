namespace day_5;
class Program
{
    static void Main(string[] args)
    {
        string[] testInput = System.IO.File.ReadAllLines("./test-input.txt");
        string[] input = System.IO.File.ReadAllLines("./input.txt");

        // Part1(ParseStack(testInput), ParseInstructions(testInput));
        // Part1(ParseStack(input), ParseInstructions(input));

        Part2(ParseStack(testInput), ParseInstructions(testInput));
        Part2(ParseStack(input), ParseInstructions(input));
    }

    static void Part1(List<Stack<char>> stacks, List<(int Amount, int From, int To)> instructions)
    {
        foreach (var instr in instructions)
        {
            var fromStact = stacks[instr.From - 1];
            var toStack = stacks[instr.To - 1];
            for (int i = 0; i < instr.Amount; i++)
                toStack.Push(fromStact.Pop());
        }
        PrintTopOfStacks(stacks);
    }

    static void Part2(List<Stack<char>> stacks, List<(int Amount, int From, int To)> instructions)
    {
        foreach (var instr in instructions)
        {
            var fromStact = stacks[instr.From - 1];
            var toStack = stacks[instr.To - 1];

            var tmpStack = new Stack<char>();

            for (int i = 0; i < instr.Amount; i++)
                tmpStack.Push(fromStact.Pop());

            for (int i = 0; i < instr.Amount; i++)
                toStack.Push(tmpStack.Pop());
        }
        PrintTopOfStacks(stacks);
    }
    static void PrintTopOfStacks(List<Stack<char>> stacks)
    {
        foreach (var stack in stacks)
        {
            if (stack.Count > 0)
                Console.Write($"{stack.Peek()}");
        }
        Console.WriteLine();
    }

    static List<(int Amount, int From, int To)> ParseInstructions(string[] input)
    {
        var instructions = new List<(int Amount, int From, int To)>();

        for (int row = GetStackHeight(input) + 2; row < input.Length; row++)
        {
            string[] rowString = input[row].Split(' ');
            instructions.Add((Convert.ToInt32(rowString[1]), Convert.ToInt32(rowString[3]), Convert.ToInt32(rowString[5])));
        }

        return instructions;
    }

    static List<Stack<char>> ParseStack(string[] input)
    {
        var stacks = new List<Stack<char>>();

        int stackHeight = GetStackHeight(input);
        int stackWidth = input[0].Length;

        for (int col = 1; col < stackWidth; col += 4)
            stacks.Add(GetColumn(input, col, stackHeight));

        return stacks;
    }

    static Stack<char> GetColumn(string[] input, int col, int stackHeight)
    {
        var que = new Stack<char>();

        for (int i = stackHeight - 1; i >= 0; i--)
        {
            char c = input[i][col];
            if (c != ' ')
                que.Push(c);
        }
        return que;
    }

    static int GetStackHeight(string[] input)
    {
        int stackHeight = 0;
        foreach (string line in input)
        {
            if (line == "")
                break;
            stackHeight++;
        }
        return stackHeight - 1;
    }
}
