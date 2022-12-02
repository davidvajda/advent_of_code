namespace day_1;
class Program
{
    static int MaxCalories(string[] lines)
    {
        int maxCalories = 0;
        int currentCalories = 0;
        foreach (string line in lines)
        {
            if (line == "")
            {
                maxCalories = Math.Max(maxCalories, currentCalories);
                currentCalories = 0;
                continue;
            }

            currentCalories += Int32.Parse(line);
        }
        return Math.Max(maxCalories, currentCalories);
    }

    static int CountThreeLargest(string[] lines)
    {
        int caloriesSum = 0;
        var caloriesSumList = new List<int> { };

        foreach (string line in lines)
        {
            if (line == "")
            {
                caloriesSumList.Add(caloriesSum);
                caloriesSum = 0;
                continue;
            }

            caloriesSum += Int32.Parse(line);
        }
        caloriesSumList.Add(caloriesSum);

        caloriesSumList.Sort();
        caloriesSumList.Reverse();
        return caloriesSumList.Take(3).Sum();
    }


    static void Main(string[] args)
    {
        string[] testLines = System.IO.File.ReadAllLines("./test-input.txt");
        string[] lines = System.IO.File.ReadAllLines("./input.txt");

        // Part 1
        // Console.WriteLine($"Highest value in test -> {MaxCalories(testLines)}");
        // Console.WriteLine($"Highest value in input -> {MaxCalories(lines)}");

        // Part 2
        Console.WriteLine($"Sum of three highest in test -> {CountThreeLargest(testLines)}");
        Console.WriteLine($"Sum of three highest in input -> {CountThreeLargest(lines)}");
    }
}
