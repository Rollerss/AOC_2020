using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day06
    {
        public static void AOCDay06()
        {
            var fileName = @".\InputData\AOCDay06.txt";
            //var fileName = @".\InputData\AOCDay06test.txt";
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);

            var data = streamReader.ReadToEnd().Split(new string[] { Environment.NewLine + Environment.NewLine },
                                                                   StringSplitOptions.RemoveEmptyEntries);
            AOCDay06Part1(data);
            AOCDay06Part2(data);
        }

        public static void AOCDay06Part1(string[] data)
        {

            var count = 0;
            foreach (var p in data)
            {
                var cs = p.Replace(Environment.NewLine, "").ToCharArray();
                count += cs.Distinct<char>().Count();
            }
            Console.WriteLine($"Day 6 Part 1: {count}");
        }

        public static void AOCDay06Part2(string[] data)
        {
            var count = 0;
            foreach (var item in data)
            {
                var cnt = item.Split(Environment.NewLine).Length;
                var duplicates = item.GroupBy(p => p).Where(g => g.Count() >= cnt).Select(g => g.Key);
                count += duplicates.Count();
            }
            Console.WriteLine($"Day 6 Part 2: {count}");
        }
    }
}
