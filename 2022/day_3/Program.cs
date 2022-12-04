using System;
using System.Collections.Generic;

namespace day_3;
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
        int value = 0;

        foreach (var line in lines)
            value += ItemValue(CheckRucksack(line));

        Console.WriteLine(value);
    }

    static void Part2(string[] lines)
    {
        int len = lines.Length;
        if (len % 3 != 0)
            throw new Exception("unvalid length of input!");

        int value = 0;

        for (int i = 0; i < len - 2; i+=3) {
            
            var one = new HashSet<char>(lines[i]);
            var two = new HashSet<char>(lines[i + 1]);
            var three = new HashSet<char>(lines[i + 2]);

            one.IntersectWith(two);
            one.IntersectWith(three);
            value += ItemValue(PopOneFromHashSet(one));
        }

        Console.WriteLine(value);
    }

    static char CheckRucksack(string rucksack)
    {
        int len = rucksack.Length;

        if (len % 2 == 1)
            throw new Exception("Rucksack has odd length!!!");

        var left = new HashSet<char>();
        var right = new HashSet<char>();

        for (int i = 0; i < len / 2; i++)
        {
            left.Add(rucksack[i]);
            right.Add(rucksack[i + len / 2]);
        }

        left.IntersectWith(right);
        return PopOneFromHashSet(left);
    }

    static char PopOneFromHashSet(HashSet<char> set)
    {
        char item = '-';
        foreach (var a in set)
            item = a;

        if (item == '-')
            throw new Exception("set is empty!!");

        return item;
    }

    static int ItemValue(char item)
    {
        return Char.IsLower(item) ? (int)item - 96 : (int)item - 38;
    }
}
