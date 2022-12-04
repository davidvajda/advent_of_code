namespace day_4;
class Program
{
    static void Main(string[] args)
    {
        // Part1(System.IO.File.ReadAllLines("./test-input.txt"));
        // Part1(System.IO.File.ReadAllLines("./input.txt"));

        Part2(System.IO.File.ReadAllLines("./test-input.txt"));
        Part2(System.IO.File.ReadAllLines("./input.txt"));
    }

    static void Part1(string[] lines)
    {
        int count = 0;
        foreach (string line in lines)
            if (FullyContainsOneAnother(line))
                count++;
        Console.WriteLine(count);
    }

    static void Part2(string[] lines)
    {
        int count = 0;
        foreach (string line in lines)
            if (Overlaps(line))
                count++;
        Console.WriteLine(count);
    }

    static bool FullyContainsOneAnother(string sections)
    {
        int[] cs = ConvertSectinions(sections);
        int firstLow = cs[0];
        int firstHigh = cs[1];
        int secondLow = cs[2];
        int secondHigh = cs[3];

        if ((firstLow <= secondLow && firstHigh >= secondHigh) || (secondLow <= firstLow && secondHigh >= firstHigh))
            return true;
        return false;
    }

    static bool Overlaps(string sections)
    {

        int[] cs = ConvertSectinions(sections);
        int firstLow = cs[0];
        int firstHigh = cs[1];
        int secondLow = cs[2];
        int secondHigh = cs[3];

        if ((secondLow <= firstHigh && secondHigh >= firstHigh) || (firstLow <= secondHigh && firstHigh >= secondHigh))
        {
            return true;
        }
        return false;
    }

    static int[] ConvertSectinions(string sections)
    {
        var pair = sections.Split(",").ToArray();
        var firstRange = pair[0].Split("-").ToArray();
        var secondRange = pair[1].Split("-").ToArray();

        int firstLow = Convert.ToInt32(firstRange[0]);
        int firstHigh = Convert.ToInt32(firstRange[1]);
        int secondLow = Convert.ToInt32(secondRange[0]);
        int secondHigh = Convert.ToInt32(secondRange[1]);

        return new int[4] {firstLow,
firstHigh,
secondLow,
secondHigh,};
    }
}
