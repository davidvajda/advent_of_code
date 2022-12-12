namespace day_11;

class Monkey
{
    Queue<int> items = new Queue<int>();
    char operationChar;
    string operationNumberString; // can be old
    int divisibleByTest;
    int ifTrueMonkeyIdx;
    int ifFalseMonkeyIdx;
    public int inspected = 0;
    long modulo;

    public Monkey(string startingItems, string operation, string test, string ifTrue, string ifFalse, List<string> strNums)
    {
        foreach (var strNum in startingItems.Split(": ")[1].Split(", "))
            items.Enqueue(Convert.ToInt32(strNum));

        modulo = 1;
        foreach (string strNum in strNums)
            modulo *= Convert.ToInt32(strNum.Split("by ")[1]);

        operationChar = operation.Split("old ")[1][0];
        operationNumberString = operation.Split("old ")[1].Split(" ")[1];
        divisibleByTest = Convert.ToInt32(test.Split("by ")[1]);
        ifTrueMonkeyIdx = Convert.ToInt32(ifTrue.Split("monkey ")[1]);
        ifFalseMonkeyIdx = Convert.ToInt32(ifFalse.Split("monkey ")[1]);
    }

    public bool HasItems() => items.Count > 0 ? true : false;

    public int InspectAndThrowItem(bool part2)
    {
        int ogItemValue = items.Dequeue();
        int operationNumber = operationNumberString == "old" ? ogItemValue : Convert.ToInt32(operationNumberString);
        long newItemValue = ogItemValue;
        if (operationChar == '*')
            newItemValue *= (long)operationNumber;
        else
            newItemValue += (long)operationNumber;
        inspected++;
        // Console.WriteLine();
        // Console.WriteLine($"Before modulo {newItemValue}");
        // Console.WriteLine($"After modulo {(int) newItemValue % modulo}");
        // Console.WriteLine($"To be tested by {divisibleByTest} {newItemValue % divisibleByTest == 0} and {(int)(newItemValue % modulo) % divisibleByTest == 0}");
        // Console.ReadKey();
        return (int)(newItemValue % modulo);
    }

    public int TestItem(int value) => value % divisibleByTest == 0 ? ifTrueMonkeyIdx : ifFalseMonkeyIdx;

    public void CatchItem(int value) => items.Enqueue(value);

    public void PrintMonkey()
    {
        Console.Write("Monkey with modulo {0} ", modulo);

        Console.Write("Had {0} inspections", inspected);
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Part2(ParseInput(System.IO.File.ReadAllLines("./test-input.txt")));
        Part2(ParseInput(System.IO.File.ReadAllLines("./input.txt")));
    }

    static void Part2(List<Monkey> monkeys)
    {
        for (int i = 0; i < 10000; i++)
        {
            for (int y = 0; y < monkeys.Count; y++)
            {
                while (monkeys[y].HasItems())
                {
                    int newItemValue = monkeys[y].InspectAndThrowItem(part2: true);
                    int newMonkeyIdx = monkeys[y].TestItem(newItemValue);
                    monkeys[newMonkeyIdx].CatchItem(newItemValue);
                }
            }
        }
        var interactions = new List<int>();
        foreach (var m in monkeys)
        {
            interactions.Add(m.inspected);
            m.PrintMonkey();
        }
        interactions.Sort();
        Console.WriteLine((long)interactions[interactions.Count - 1] * (long)interactions[interactions.Count - 2]);
    }

    static List<Monkey> ParseInput(string[] lines)
    {
        var monkeys = new List<Monkey>();
        var testNumbersStr = new List<string>();

        for (int i = 0; i < lines.Length; i += 7)
            testNumbersStr.Add(lines[i + 3]);

        for (int i = 0; i < lines.Length; i += 7)
            monkeys.Add(new Monkey(
                lines[i + 1], lines[i + 2], lines[i + 3], lines[i + 4], lines[i + 5], testNumbersStr
            ));
        return monkeys;
    }


}
