using System.IO;

namespace day_10;
class Program
{
    static void Main(string[] args)
    {
        Part1(ParseInput(File.ReadAllLines("./test-input-big.txt")));
        Part1(ParseInput(File.ReadAllLines("./input.txt")));

        //Part2(ParseInput(File.ReadAllLines("./test-input-big.txt")));
        //Part2(ParseInput(File.ReadAllLines("./input.txt")));
    }

    static void Part1((string instrunction, int value)[] instructions)
    {
        var cycleResults = GetCycleResults(instructions);
        int sumOfSignals = 0;

        for (int i = 19; i < 220; i += 40)
            sumOfSignals += cycleResults[i].x * (i + 1);


        Console.WriteLine("The sum of these signal strengths is {0}.", sumOfSignals);
    }

    static void Part2((string instrunction, int value)[] instructions)
    {
        var cycleResults = GetCycleResults(instructions);

        for (int i = 0; i < 6 * 40; i++)
        {
            int crt = i % 40;
            int cycle = cycleResults[i].cycle;
            int x = cycleResults[i].x;

            if (crt == x - 1 || crt == x || crt == x + 1)
                Console.Write("#");
            else
                Console.Write(".");
            if (crt == 39)
                Console.WriteLine();
        }



    }

    static List<(string instr, int val, int x, int cycle)> GetCycleResults((string instrunction, int value)[] instructions)
    {
        var results = new List<(string, int, int, int)>();

        int x = 1;
        int cycle = 1;

        foreach (var instr in instructions)
        {
            if (instr.instrunction == "noop")
            {
                // beginning of the noop execution
                results.Add((instr.instrunction, instr.value, x, cycle));
                // end of noop execution
                cycle++;
                continue;
            }
            // beginning of addx execution
            results.Add((instr.instrunction, instr.value, x, cycle)); // during the cycle
            // end of first cycle
            cycle++;
            results.Add((instr.instrunction, instr.value, x, cycle));
            // end of second cycle
            cycle++;
            x += instr.value;
        }
        results.Add(("", 0, x, cycle));
        return results;
    }

    static (string, int)[] ParseInput(string[] lines)
    {
        var instructions = new (string, int)[lines.Length];
        for (int i = 0; i < instructions.Length; i++)
        {
            string[] splitLine = lines[i].Split(" ");
            if (splitLine.Length == 1)
                instructions[i] = (splitLine[0], 0);
            else
                instructions[i] = (splitLine[0], Convert.ToInt32(splitLine[1]));
        }
        return instructions;
    }
}
