using System.IO;
using System.Text;


namespace day_7;
class Program
{
    static void Main(string[] args)
    {
        // Part1(File.ReadAllLines("./test-input.txt"));
        // Part1(File.ReadAllLines("./input.txt"));        

        // Part2(File.ReadAllLines("./test-input.txt"));
        Part2(File.ReadAllLines("./input.txt"));
    }

    static void Part1(string[] lines)
    {
        var sizes = GetDirectorySizes(lines);

        PrintSizes(sizes);
        Console.WriteLine("Sum of all sizes that are under 100k is {0}", SumOfValuesUnder100k(sizes));
    }

    static void Part2(string[] lines)
    {
        var sizes = GetDirectorySizes(lines);

        long minDirSizeToDelete = 30000000 - (70000000 - sizes["//"]);
        long freesEnoughSpaceSize = sizes["//"];

        foreach (long dirSize in sizes.Values)
            if (dirSize >= minDirSizeToDelete)
                freesEnoughSpaceSize = Math.Min(freesEnoughSpaceSize, dirSize);


        Console.WriteLine($"Smallest directory that can be deleted and frees enough space has size {freesEnoughSpaceSize}");

    }

    static Dictionary<string, long> GetDirectorySizes(string[] lines)
    {
        var dirPoitner = new Stack<string>();
        var sizes = new Dictionary<string, long>();

        sizes.Add("//", 0);

        foreach (var line in lines)
        {
            var splitLine = line.Split(" ");

            string one = splitLine[0];
            string two = splitLine[1];
            string? three = splitLine.Length == 3 ? splitLine[2] : null;

            switch ((one, two, three))
            {
                case ("$", "cd", ".."): // how to know where to go back?
                    dirPoitner.Pop();
                    break;
                case ("$", "cd", _) when three != null:
                    dirPoitner.Push(three);
                    break;
                case ("$", "ls", null):
                    break;
                case ("dir", _, null):
                    dirPoitner.Push(two);
                    string path = GetPath(dirPoitner);
                    dirPoitner.Pop();

                    if (!sizes.ContainsKey(path))
                        sizes.Add(path, 0);
                    break;
                case (_, _, null):
                    var clonedStack = new Stack<string>(new Stack<string>(dirPoitner));
                    while (clonedStack.Count > 0)
                    {
                        sizes[GetPath(clonedStack)] += Convert.ToInt64(one);
                        clonedStack.Pop();
                    }
                    break;
                default:
                    throw new Exception("should not happen");
            }
        }
        return sizes;
    }

    static string GetPath(Stack<string> dirPointer)
    {
        var clonedStack = new Stack<string>(dirPointer);
        var sb = new StringBuilder();

        while (clonedStack.Count > 0)
            sb.Append($"{clonedStack.Pop()}/");
        return sb.ToString();
    }

    static void PrintSizes(Dictionary<string, long> sizes)
    {
        foreach (KeyValuePair<string, long> x in sizes)
            Console.WriteLine($"dir {x.Key} size({x.Value})");
    }

    static long SumOfValuesUnder100k(Dictionary<string, long> sizes)
    {
        long sum = 0;
        foreach (KeyValuePair<string, long> x in sizes)
            if (x.Value <= 100000)
                sum += x.Value;
        return sum;
    }
}
