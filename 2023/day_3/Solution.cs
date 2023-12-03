namespace _2023.day_3
{
    public class Part1
    {
        public static void Run()
        {
            var inputLines = ReadInputLines("D:\\projects\\aoc\\2023\\2023\\2023\\day_3\\input_1.txt");
            var sum = 0;
            var numIndexes = GetNumberIndexes(inputLines);
            foreach (var indexes in numIndexes)
            {
                if (IsAdjacentToCharacter(indexes, inputLines, GetSymbols(inputLines)))
                {
                    sum += GetNumber(indexes, inputLines);
                }

            }
            Console.WriteLine(sum);
        }

        public static bool IsAdjacentToCharacter(List<(int r, int c)> numIndexes, string[] lines, HashSet<char> symbols) 
        {
            var adjIndexes = GetAdjacentIndexes(numIndexes);
            foreach(var adjIndex in adjIndexes)
            {
                if (!IsValidIndex(adjIndex, lines))
                    continue;

                if (symbols.Contains(lines[adjIndex.r][adjIndex.c]))
                    return true;
            }
            return false;
        }

        public static bool IsValidIndex((int r, int c) rc, string[] lines)
        {
            if (rc.r < 0 || rc.c < 0) return false;
            if (rc.r >= lines.Length || rc.c >= lines[rc.r].Length) return false;
            return true;
        }

        public static List<(int r, int c)> GetAdjacentIndexes(List<(int r, int c)> indexes)
        {
            var idk = new List<int> { -1, 0, 1 }.SelectMany(x => new List<int> { -1, 0, 1 }.Select(y => (x, y))).SelectMany(adj => indexes.Select(i => (adj.x+i.r, adj.y+i.c))).ToList();
            return idk;
        }
        public static HashSet<char> GetSymbols(string[] inputLines)
        {
            var symbols = new HashSet<char>();
            foreach (var line in inputLines)
                foreach (var character in line)
                {
                    if (character != '.' && !Char.IsDigit(character))
                    {
                        symbols.Add(character);
                    }
                }
            return symbols;
        }
        public static int GetNumber(List<(int r, int c)> indexes, string[] lines) 
        {
            var x = lines[indexes.First().r];
            var y = x.Substring(indexes.First().c, indexes.Count);
            return Convert.ToInt32(y);
        }
        
        public static List<List<(int r, int c)>> GetNumberIndexes(string[] inputLines)
        {
            var numberLists = new List<List<(int r, int c)>>();
            for (var row = 0; row < inputLines.Length; row++)
            {
                for (var col = 0; col < inputLines[row].Length; col++)
                {
                    var cellCharacter = inputLines[row][col];
                    var symbols = GetSymbols(inputLines);
                    if (cellCharacter == '.' || symbols.Contains(cellCharacter))
                    {
                        continue;
                    }
                    if (col == 0 || inputLines[row][col-1] == '.' || symbols.Contains(inputLines[row][col - 1]))
                    {
                        numberLists.Add(new List<(int r, int c)>() { (row, col) });
                    }
                    else
                    {
                        numberLists.Last().Add((row, col));
                    }
                }
            }
            return numberLists;
        }
        public static string[] ReadInputLines(string filePath) => File.ReadAllLines(filePath);
    }

    public class Part2
    {
        public static void Run()
        {
            var inputLines = Part1.ReadInputLines("D:\\projects\\aoc\\2023\\2023\\2023\\day_3\\input_1.txt");
            var sum = 0;
            var starIndexes = GetStarIndexes(inputLines);
            var numbers = Part1.GetNumberIndexes(inputLines);
            foreach (var starIndex in starIndexes)
            {
                var numCount = 0;
                var numSum = 1;
                foreach(var number in numbers)
                {
                    if (IsAdjacentToCoordinates(number, inputLines, starIndex))
                    {
                        numCount++;
                        numSum *= Part1.GetNumber(number, inputLines);
                    }
                }
                sum += numCount == 2 ? numSum : 0;
            }
            Console.WriteLine(sum);
        }

        public static bool IsAdjacentToCoordinates(List<(int r, int c)> numIndexes, string[] lines, (int r, int c) symbolRC)
        {
            var adjIndexes = Part1.GetAdjacentIndexes(numIndexes);
            foreach (var adjIndex in adjIndexes)
            {
                if (!Part1.IsValidIndex(adjIndex, lines))
                    continue;

                if (adjIndex.r == symbolRC.r && adjIndex.c == symbolRC.c)
                    return true;
            }
            return false;
        }

        public static T[] Split<T>(T[] array, int idxFrom, int idxTo) => array.Take(idxTo).Skip(idxFrom).ToArray();

        public static List<(int r, int c)> GetStarIndexes(string[] inputLines)
        {
            var starIndexes = new List<(int r, int c)>();
            for (var r = 0; r < inputLines.Length; r++)
                for (var c = 0; c < inputLines[r].Length; c++)
                {
                    if (inputLines[r][c] == '*')
                    {
                        starIndexes.Add((r, c));
                    }
                }
            return starIndexes;
        }
    }
}
