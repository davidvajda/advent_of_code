namespace _2023.day_1
{
    public class Part1
    {
        public static void Run()
        {
            var inputLines = ReadInputLines("D:\\projects\\aoc\\2023\\2023\\2023\\day_1\\input_1.txt");
            var result = SumLineNumbers(inputLines);
            Console.WriteLine(result);
        }

        public static long SumLineNumbers(string[] lines)
        {
            var sum = 0L;
            foreach (var line in lines)
            {
                var chars = GetFirstAndLastNumberChar(line);
                var lineValue = GetNumberFromChars(new char[] { chars.first, chars.last });
                sum += lineValue;
            }
            return sum;
        }

        public static (char first, char last) GetFirstAndLastNumberChar(string line)
        {
            char? first = null;
            char? last = null;

            foreach (var c in line)
            {
                if (Char.IsNumber(c))
                {
                    if (!first.HasValue)
                    {
                        first = c;
                    }
                    last = c;
                }
            }

            if (!first.HasValue || !last.HasValue)
            {
                throw new Exception();
            }
            return (first.Value, last.Value);
        }

        public static long GetNumberFromChars(char[] chars) => Convert.ToInt64(String.Join("", chars));
        public static string[] ReadInputLines(string filePath) => File.ReadAllLines(filePath);
    }

    public class Part2
    {
        public static void Run()
        {
            var inputLines = ReadInputLines("D:\\projects\\aoc\\2023\\2023\\2023\\day_1\\input_1.txt");
            var result = SumLineNumbers(inputLines);
            Console.WriteLine(result);
        }

        public static long SumLineNumbers(string[] lines)
        {
            var sum = 0L;
            foreach (var line in lines)
            {
                var chars = GetFirstAndLastNumberChar(line);
                var lineValue = GetNumberFromChars(new char[] { chars.first, chars.last });
                sum += lineValue;
            }
            return sum;
        }

        public static (char first, char last) GetFirstAndLastNumberChar(string line)
        {
            var mapping = new Dictionary<string, char>()
            {
                { "one", '1' },
                { "two", '2' },
                { "three", '3' },
                { "four", '4' },
                { "five", '5' },
                { "six", '6' },
                { "seven", '7' },
                { "eight", '8' },
                { "nine", '9' }
            };

            char? first = null;
            char? last = null;

            int? idxFirst = null;
            int? idxLast = null;

            var i = 0;
            foreach (var c in line)
            {
                if (Char.IsNumber(c))
                {
                    if (!first.HasValue)
                    {
                        first = c;
                        idxFirst = i;
                    }
                    last = c;
                    idxLast = i;
                }
                i++;
            }

            foreach (var map in mapping)
            {

                if (line.Contains(map.Key))
                {
                    var idxFirst_ = line.IndexOf(map.Key);
                    var idxLast_ = line.LastIndexOf(map.Key);
                    if (!idxFirst.HasValue || idxFirst_ < idxFirst)
                    {
                        first = map.Value;
                        idxFirst = idxFirst_;

                    }
                    if (!idxLast.HasValue || idxLast_ > idxLast)
                    {
                        last = map.Value;
                        idxLast = idxLast_;
                    }
                }
            }

            return (first.Value, last.Value);
        }

        public static long GetNumberFromChars(char[] chars) => Convert.ToInt64(String.Join("", chars));
        public static string[] ReadInputLines(string filePath) => File.ReadAllLines(filePath);
    }
}
