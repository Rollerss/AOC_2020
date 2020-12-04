using System;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day03
    {
        public static void AOCDay03()
        {
            var fileName = @".\InputData\AOCDay03.txt";
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);
            var data = streamReader.ReadToEnd().Split('\n');
            AOCDay03Part1(data);
            AOCDay03Part2(data);
        }

        public static void AOCDay03Part1(string[] data)
        {
            var treeFriends = CycleThoughData(data, 3, 1);
            Console.WriteLine($"Day 3 Part 1: Trees hit = {treeFriends}");
        }

        public static void AOCDay03Part2(string[] data)
        {
            long totalTrees = 1;
            totalTrees *= CycleThoughData(data, 1, 1);
            totalTrees *= CycleThoughData(data, 3, 1);
            totalTrees *= CycleThoughData(data, 5, 1);
            totalTrees *= CycleThoughData(data, 7, 1);
            totalTrees *= CycleThoughData(data, 1, 2);
            Console.WriteLine($"Day 3 Part 2: Trees ??? = {totalTrees}");
        }

        public static int CycleThoughData(string[] data, int slope, int inc)
        {
            var repeat = data[0].Length - 1;
            var treeCount = 0;
            for (int i = 0; i < data.Length; i += inc)
            {
                var x = data[i][(slope * i / inc) % repeat];
                //Console.WriteLine($"i {i} r {(slope * i / inc) % repeat} x {x}");
                if (x == '#')
                {
                    treeCount++;
                }
            }
            //Console.WriteLine($"Trees {treeCount}");
            return treeCount;
        }
    }
}
