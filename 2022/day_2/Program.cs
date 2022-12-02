namespace day_2;
class Program
{
    static void Main(string[] args)
    {
        string[] testInput = System.IO.File.ReadAllLines("./test-input.txt");
        string[] input = System.IO.File.ReadAllLines("./input.txt");

        // Part 1
        Console.WriteLine("Part 1 - Test input score: " + getScore1(testInput));
        Console.WriteLine("Part 1 - Input score: " + getScore1(input));

        // Part 2
        Console.WriteLine("Part 2 - Test input score: " + getScore2(testInput));
        Console.WriteLine("Part 2 - Input score: " + getScore2(input));
    }

    static int getScore1(string[] moves)
    {
        var resultScore = new Dictionary<char, Dictionary<char, int>>(){
            {'X', new Dictionary<char, int>() {{'A', 3}, {'B', 0}, {'C', 6}}},
            {'Y', new Dictionary<char, int>() {{'A', 6}, {'B', 3}, {'C', 0}}},
            {'Z', new Dictionary<char, int>() {{'A', 0}, {'B', 6}, {'C', 3}}},
        };
        var moveScore = new Dictionary<char, int>() { { 'X', 1 }, { 'Y', 2 }, { 'Z', 3 } };

        int scoreCount = 0;
        for (int i = 0; i < moves.Length; i++) {
            char oponentMove = moves[i][0];
            char myMove = moves[i][2];
        scoreCount += resultScore[myMove][oponentMove] + moveScore[myMove];
        }

        return scoreCount;
    }

    static int getScore2(string[] moves)
    {
        var moveScore = new Dictionary<char, int>() { { 'X', 0 }, { 'Y', 3 }, { 'Z', 6 } };
        var resultScore = new Dictionary<char, Dictionary<char, int>>(){
            {'X', new Dictionary<char, int>() {{'A', 3}, {'B', 1}, {'C', 2}}},
            {'Y', new Dictionary<char, int>() {{'A', 1}, {'B', 2}, {'C', 3}}},
            {'Z', new Dictionary<char, int>() {{'A', 2}, {'B', 3}, {'C', 1}}},
        };

        int scoreCount = 0;
        for (int i = 0; i < moves.Length; i++) {
            char oponentMove = moves[i][0];
            char widhedResult = moves[i][2];
        scoreCount += resultScore[widhedResult][oponentMove] + moveScore[widhedResult];
        }
        return scoreCount;
    }
}
