namespace _2023.day_2
{
    public class Part1
    {
        public static void Run()
        {
            var inputLines = ReadInputLines("D:\\projects\\aoc\\2023\\2023\\2023\\day_2\\input_1.txt");
            var possibleGamesIdSum = 0;
            foreach (var line in inputLines)
            {
                var gameId = GetGameId(line);
                var colorSums = RGBCountByLine(line);

                if (colorSums.R <= 12 &&  colorSums.G <= 13 && colorSums.B <= 14)
                {
                    possibleGamesIdSum += gameId;
                }
            }
            Console.WriteLine(possibleGamesIdSum);
        }

        public static (int R, int G, int B) RGBCountByLine(string line) 
        {
            var colors = new Dictionary<string, int>() { { "red", 0 }, { "green", 0 }, { "blue", 0 } };

            foreach (var set in line.Split(": ")[1].Split("; "))
            {
                foreach (var s in set.Split(", "))
                {
                    var number = Convert.ToInt32(s.Split(" ")[0]);
                    var color = s.Split(" ")[1];
                    colors[color] =  Math.Max(colors[color], number);
                }
            }
            return (colors["red"], colors["green"], colors["blue"]);
        }
        public static int GetGameId(string line) => Convert.ToInt32(line.Split(": ")[0].Split(" ")[1]);
        public static string[] ReadInputLines(string filePath) => File.ReadAllLines(filePath);
    }

    public class Part2
    {
        public static void Run()
        {
            var inputLines = ReadInputLines("D:\\projects\\aoc\\2023\\2023\\2023\\day_2\\input_1.txt");
            var powerSum = 0;
            foreach (var line in inputLines)
            {
                var colors = RGBCountByLine(line);

                powerSum += colors.R * colors.G * colors.B;
                
            }
            Console.WriteLine(powerSum);
        }

        public static (int R, int G, int B) RGBCountByLine(string line)
        {
            var colors = new Dictionary<string, int>() { { "red", 0 }, { "green", 0 }, { "blue", 0 } };

            foreach (var set in line.Split(": ")[1].Split("; "))
            {
                foreach (var s in set.Split(", "))
                {
                    var number = Convert.ToInt32(s.Split(" ")[0]);
                    var color = s.Split(" ")[1];
                    colors[color] = Math.Max(colors[color], number);
                }
            }
            return (colors["red"], colors["green"], colors["blue"]);
        }
        public static string[] ReadInputLines(string filePath) => File.ReadAllLines(filePath);
    }
}
