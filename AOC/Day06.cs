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

            var c = 0;
            foreach (var p in data)
            {
                var cs = p.Replace(Environment.NewLine, "").ToCharArray();
                c += cs.Distinct<char>().Count();
            }
            Console.WriteLine($"Day 6 Part 1: {c}");
        }

        public static void AOCDay06Part2(string[] data)
        {
            var c = 0;
            foreach (var item in data)
            {
                var s = item.Split(Environment.NewLine);
                var ct = s.Length;
                if (ct == 1)
                {
                    //Console.WriteLine(item.Count());
                    c += item.Count();
                }
                else
                {
                    var duplicates = item.GroupBy(p => p).Where(g => g.Count() >= ct).Select(g => g.Key);
                    //Console.WriteLine(duplicates.Count());
                    c += duplicates.Count();
                }

            }
            Console.WriteLine($"Day 6 Part 2: {c}");
        }

    }
}
