namespace day_12;
class Program
{
    static void Main(string[] args)
    {
        // Part1(ParseInput(System.IO.File.ReadAllLines("./test-input.txt")));
        // Part1(ParseInput(System.IO.File.ReadAllLines("./input.txt")));


        //Part2(ParseInput(System.IO.File.ReadAllLines("./test-input.txt")));
        Part2(ParseInput(System.IO.File.ReadAllLines("./input.txt")));
    }

    static void Part1(((int row, int col) startCoords, (int row, int col) endCoords, int[,] matrix, int matrixWidth) input)
    {
        var end = input.endCoords;
        var matrix = input.matrix;
        var matrixWidth = input.matrixWidth;
        var visited = new HashSet<(int, int)>();
        var que = new Queue<(int row, int col, int steps)>();

        que.Enqueue((input.startCoords.row, input.startCoords.col, 0));

        while (que.Count > 0)
        {
            var position = que.Dequeue();

            if (position.row == end.row && position.col == end.col)
            {
                Console.WriteLine(position.steps);
                return;
            }

            if (visited.Contains((position.row, position.col)))
                continue;

            visited.Add((position.row, position.col));

            foreach (var possibleCoord in PossibleCoords(matrix, matrixWidth, position))
                que.Enqueue(possibleCoord);
        }
    }

    static void Part2(((int row, int col) startCoords, (int row, int col) endCoords, int[,] matrix, int matrixWidth) input)
    {
        var matrix = input.matrix;
        var matrixWidth = input.matrixWidth;
        var visited = new HashSet<(int, int)>();
        var que = new Queue<(int row, int col, int steps)>();

        que.Enqueue((input.endCoords.row, input.endCoords.col, 0));

        while (que.Count > 0)
        {
            var position = que.Dequeue();

            if (matrix[position.row, position.col] == 1)
            {
                Console.WriteLine(position.steps);
                return;
            }

            if (visited.Contains((position.row, position.col)))
                continue;

            visited.Add((position.row, position.col));

            foreach (var possibleCoord in PossibleCoords2(matrix, matrixWidth, position))
                que.Enqueue(possibleCoord);
        }
    }

    static List<(int row, int col, int steps)> PossibleCoords2(int[,] matrix, int matrixWidth, (int row, int col, int steps) currentCoords)
    {
        var possibleCoords = new List<(int row, int col, int steps)>();
        var possibleMoves = new (int r, int c)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        int currentVal = matrix[currentCoords.row, currentCoords.col];

        foreach (var move in possibleMoves)
        {
            int r = currentCoords.row + move.r;
            int c = currentCoords.col + move.c;
            int s = currentCoords.steps + 1;

            if (r < 0 || r >= matrix.Length / matrixWidth || c < 0 || c >= matrixWidth)
                continue;

            int possibleValue = matrix[r, c];
            if (possibleValue >= currentVal || currentVal - possibleValue == 1)
                possibleCoords.Add((r, c, s));

        }
        return possibleCoords;
    }

    static List<(int row, int col, int steps)> PossibleCoords(int[,] matrix, int matrixWidth, (int row, int col, int steps) currentCoords)
    {
        var possibleCoords = new List<(int row, int col, int steps)>();
        var possibleMoves = new (int r, int c)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        int currentVal = matrix[currentCoords.row, currentCoords.col];

        foreach (var move in possibleMoves)
        {
            int r = currentCoords.row + move.r;
            int c = currentCoords.col + move.c;
            int s = currentCoords.steps + 1;

            if (r < 0 || r >= matrix.Length / matrixWidth || c < 0 || c >= matrixWidth)
                continue;

            int possibleValue = matrix[r, c];
            if (possibleValue <= currentVal || possibleValue - currentVal == 1)
                possibleCoords.Add((r, c, s));

        }
        return possibleCoords;
    }

    static ((int, int), (int, int), int[,], int) ParseInput(string[] lines)
    {
        var matrix = new int[lines.Length, lines[0].Length];
        var matrixWidth = lines[0].Length;
        var startCoords = (0, 0);
        var endCoords = (0, 0);

        for (int row = 0; row < lines.Length; row++)
            for (int col = 0; col < lines[row].Length; col++)
            {
                matrix[row, col] = ((int)Char.ToLower(lines[row][col])) - 96; // a == 1, b == 2 ...  
                if (lines[row][col] == 'S')
                {
                    startCoords = (row, col);
                    matrix[row, col] = 1;
                }
                if (lines[row][col] == 'E')
                {
                    endCoords = (row, col);
                    matrix[row, col] = 26;
                }
            }
        return (startCoords, endCoords, matrix, matrixWidth);
    }
}
