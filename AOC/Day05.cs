using System;
using System.Collections.Generic;
using System.IO;

namespace AOC
{
    public static class Day05
    {
        public static void AOCDay05()
        {
            var fileName = @".\InputData\AOCDay05.txt";
            //var fileName = @".\InputData\AOCDay05test.txt";
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);

            var data = streamReader.ReadToEnd().Split('\n');
            AOCDay05Part1(data);
            
        }

        public static void AOCDay05Part1(string[] data)
        {
            var maxId = 0;
            List<int> passes = new();
            foreach (var pass in data)
            {
                var row = CycleThoughData(pass.Substring(0, 7), 127);
                var col = CycleThoughData(pass.Substring(7, 3), 7);
                var id = row * 8 + col;
                passes.Add(id);
                //Console.WriteLine($"Day 5 Part 1: {id}");
                if (maxId < id)
                {
                    maxId = id;
                }
            }
            Console.WriteLine($"Day 5 Part 1: {maxId}");
            AOCDay05Part2(passes);
        }

        public static void AOCDay05Part2(List<int> vs)
        {
            for (int i = 84; i < 867; i++)
            {
                if (!vs.Contains(i))
                    Console.WriteLine($"Day 5 Part 2: {i}");
            }
        }

        public static int CycleThoughData(string data, int max)
        {
            //Console.WriteLine(data);
            var rows = (x: 0, y: max);
            foreach (var item in data)
            {
                if (item == 'F' || item == 'L')
                {
                    rows.y = (rows.x + rows.y) / 2;
                }
                else
                {
                    rows.x = (rows.x + rows.y) / 2 + 1;
                }
                //Console.WriteLine($"{rows.x} {rows.y}");
            }
            return rows.x;
        }
    }
}
