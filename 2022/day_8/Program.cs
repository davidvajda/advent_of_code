using System.IO;

namespace day_8;
class Program
{
    static void Main(string[] args)
    {
        //Part1(File.ReadAllLines("./test-input.txt"));
        //Part1(File.ReadAllLines("./input.txt"));

        Part2(File.ReadAllLines("./test-input.txt"));
        Part2(File.ReadAllLines("./input.txt"));
    }


    static void Part2(string[] treeHeights)
    {
        int height = treeHeights.Length;
        int width = treeHeights[0].Length;

        int highextScenicScore = 0;
        for (int r = 0; r < height; r++)
            for (int c = 0; c < width; c++)
                highextScenicScore = Math.Max(highextScenicScore, GetScenicScore(treeHeights, r, c));

        Console.WriteLine("Highest scenic score is {0}", highextScenicScore);
    }

    static int GetScenicScore(string[] treeHeights, int r, int c)
    {
        int height = treeHeights.Length;
        int width = treeHeights[0].Length;
        int houseTreeHeight = Convert.ToInt32(treeHeights[r][c]);

        int rrCount = 0;
        for (int x = c + 1; x < width; x++)
        {
            int treeHeight = Convert.ToInt32(treeHeights[r][x]);
            rrCount++;
            if (treeHeight >= houseTreeHeight)
                break;

        }

        int rlCount = 0;
        for (int x = c - 1; x >= 0; x--)
        {
            int treeHeight = Convert.ToInt32(treeHeights[r][x]);
            rlCount++;
            if (treeHeight >= houseTreeHeight)
                break;

        }

        int cdCount = 0;
        for (int x = r + 1; x < width; x++)
        {
            int treeHeight = Convert.ToInt32(treeHeights[x][c]);
            cdCount++;
            if (treeHeight >= houseTreeHeight)
                break;

        }

        int cuCount = 0;
        for (int x = r - 1; x >= 0; x--)
        {
            int treeHeight = Convert.ToInt32(treeHeights[x][c]);
            cuCount++;
            if (treeHeight >= houseTreeHeight)
                break;

        }
        rrCount = Math.Max(1, rrCount);
        rlCount = Math.Max(1, rlCount);
        cuCount = Math.Max(1, cuCount);
        cdCount = Math.Max(1, cdCount);
        // Console.WriteLine($"Score {rrCount * rlCount * cuCount * cdCount} where rr {rrCount} rl {rlCount} cu {cuCount} cd {cdCount} at r{r} c{c}");
        return rrCount * rlCount * cuCount * cdCount;
    }

    static void Part1(string[] lines)
    {
        int height = lines.Length;
        int width = lines[0].Length;

        bool[,] forest = new bool[height, width];

        // traverse grid from left to right and from right to left
        for (int r = 0; r < height; r++)
        {
            int rR = height - r - 1;
            int tallestR = -1;

            int tallest = -1;
            for (int c = 0; c < width; c++)
            {
                int cR = width - c - 1;
                int treeHeight = Convert.ToInt32(lines[r][c]);
                int treeHeightR = Convert.ToInt32(lines[rR][cR]);
                if (treeHeight > tallest)
                {
                    tallest = treeHeight;
                    forest[r, c] = true;
                }
                if (treeHeightR > tallestR)
                {
                    tallestR = treeHeightR;
                    forest[rR, cR] = true;
                }
            }
        }

        // traverse grid from top to bottom and from bottom to top
        for (int r = 0; r < height; r++)
        {
            int rR = height - r - 1;
            int tallestR = -1;

            int tallest = -1;
            for (int c = 0; c < width; c++)
            {
                int cR = width - c - 1;
                int treeHeight = Convert.ToInt32(lines[c][r]);
                int treeHeightR = Convert.ToInt32(lines[cR][rR]);
                if (treeHeight > tallest)
                {
                    tallest = treeHeight;
                    forest[c, r] = true;
                }
                if (treeHeightR > tallestR)
                {
                    tallestR = treeHeightR;
                    forest[cR, rR] = true;
                }
            }
        }

        //PrintForest(forest, height, width);
        Console.WriteLine("There are {0} visible trees in this forest", CountVisibleTrees(forest));
    }

    static int CountVisibleTrees(bool[,] forest)
    {
        int count = 0;
        foreach (bool treeVisible in forest)
            if (treeVisible)
                count++;
        return count;
    }

    static void PrintForest(bool[,] forest, int h, int w)
    {
        for (int r = 0; r < h; r++)
        {
            Console.Write("|");
            for (int c = 0; c < w; c++)
                Console.Write("\t{0}", forest[r, c]);
            Console.WriteLine("\t|");
        }
    }
}