namespace day_6;
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
        foreach (string line in lines)
            Console.WriteLine($"Marker after idx {GetMarkerIndice(line, 4)}");
    }

    static void Part2(string[] lines)
    {
        foreach (string line in lines)
            Console.WriteLine($"Message marker after idx {GetMarkerIndice(line, 14)}");
    }

    static int GetMarkerIndice(string line, int distinctChars)
    {
        int idx = 0;
        int size = 0;
        var que = new Queue<char>();

        foreach (char c in line)
        {
            if (size == distinctChars)
                que.Dequeue();
            else
                size++;

            que.Enqueue(c);
            idx++;
            if (QueueAllUnique(que, distinctChars))
                return idx;
        }
        return idx; // shouldn't happen
    }

    static bool QueueAllUnique(Queue<char> que, int distinctChars)
    {
        var set = new HashSet<char>(que);
        return set.Count() == distinctChars ? true : false;
    }
}
