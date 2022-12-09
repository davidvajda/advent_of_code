using System.IO;

namespace day_9;
class Program
{
    static void Main(string[] args)
    {
        Part1(ParseInput(File.ReadAllLines("./test-input.txt")));
        Part1(ParseInput(File.ReadAllLines("./input.txt")));

    }

    static void Part1((char direction, int length)[] steps)
    {
        (int x, int y) head = (0, 0);
        (int x, int y) tail = (0, 0);

        var tailVisitedCoords = new Dictionary<int, HashSet<int>>();
        tailVisitedCoords.Add(0, new HashSet<int>());
        tailVisitedCoords[0].Add(0);        
        int countInst = 0;
        int countMoves = 0;
        foreach (var step in steps)
        {
            countInst++;
            for (int i = 0; i < step.length; i++)
            {
                countMoves++;
                var nextHead = GetNextPosition(head, step.direction);
                var nextTail = GetNextTailPosition(tail, head, nextHead, step.direction);

                if (tailVisitedCoords.ContainsKey(nextTail.x))
                    tailVisitedCoords[nextTail.x].Add(nextTail.y);
                else
                {
                    tailVisitedCoords.Add(nextTail.x, new HashSet<int>());
                    tailVisitedCoords[nextTail.x].Add(nextTail.y);
                }

                tail = nextTail;
                head = nextHead;
            }
        }

        Console.WriteLine($"instr {countInst} moves {countMoves}");

        int count = 0;
        foreach (var kv in tailVisitedCoords)
            count += kv.Value.Count();

        Console.WriteLine($"Tail visited {count} unique coords");
    }

    static (int x, int y) GetNextTailPosition((int x, int y) tail, (int x, int y) head, (int x, int y) nextHead, char direction)
    {
        if (TailHasToMove(tail, nextHead))
            return head;
        return tail;
    }

    static bool TailHasToMove((int x, int y) tail, (int x, int y) nextHead)
    {
        if (isTailDiagonally(nextHead, tail) || isHorizontallyOrVertically(nextHead, tail) || tail == nextHead)
            return false;
        return true;
    }

    static (int x, int y) GetNextPosition((int x, int y) coords, char direction)
    {
        int x = coords.x;
        int y = coords.y;

        switch (direction)
        {
            case ('U'):
                y++;
                break;
            case ('D'):
                y--;
                break;
            case ('R'):
                x++;
                break;
            case ('L'):
                x--;
                break;
            default:
                throw new Exception("Shouldn't happen");
        }
        return (x, y);
    }

    static bool isTailDiagonally((int x, int y) head, (int x, int y) tail)
    {
        // up-left, up-right, down-left, down-right
        var diagonally = new (int x, int y)[] { (-1, -1), (1, -1), (-1, 1), (1, 1) };

        foreach (var addToPos in diagonally)
        {
            var newTail = (tail.x + addToPos.x, tail.y + addToPos.y);
            if (newTail == head)
                return true;
        }

        return false;
    }

    static bool isHorizontallyOrVertically((int x, int y) head, (int x, int y) tail)
    {
        // up, down, left, right
        var horizontallyAndVertically = new (int x, int y)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

        foreach (var addToPos in horizontallyAndVertically)
        {
            var newTail = (tail.x + addToPos.x, tail.y + addToPos.y);
            if (newTail == head)
                return true;
        }
        return false;
    }

    static (char, int)[] ParseInput(string[] lines)
    {
        var steps = new (char, int)[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string [] splitLine = lines[i].Split(" ");
            steps[i] = (splitLine[0][0], Convert.ToInt32(splitLine[1]));
        }
        return steps;
    }
}
