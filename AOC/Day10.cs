using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day10
    {
        public static void AOCDay10()
        {
            string[] fileName = { "AOCDay10test1.txt", "AOCDay10test2.txt", "AOCDay10.txt" };
            foreach (var file in fileName)
            {
                using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
                ParseThatData(streamReader.ReadToEnd());
            }
            //var fileName = @".\InputData\AOCDay10test1.txt";
            //var fileName = @".\InputData\AOCDay10test2.txt";
            //var fileName = @".\InputData\AOCDay10.txt";
            //using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            //ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            int[] ints = Array.ConvertAll(data.Split('\n'), s => int.Parse(s));
            Array.Sort(ints);
            AOCDay10Part1(ints);
            AOCDay10Part2(ints);
        }

        public static void AOCDay10Part1(int[] sortNum)
        {
            var len = sortNum.Length;

            var cnt1 = 1;
            var cnt3 = 1;

            for (int i = 1; i < len; i++)
            {
                if (sortNum[i] - sortNum[i - 1] == 3)
                {
                    cnt3++;
                }
                else
                {
                    cnt1++;
                }
            }

            Console.WriteLine($"Day 10 Part 1: {cnt1} {cnt3} {cnt1 * cnt3}");
        }

        public static void AOCDay10Part2(int[] ints)
        {
            var max = ints[ints.Length - 1];
            var start = 0;
            Dictionary<int, long> Dict = new Dictionary<int, long>();

            long ways = CountPaths(ints, start, max, Dict);

            Console.WriteLine($"Day 10 Part 2: {ways}");
        }

        public static long CountPaths(int[] ints, int start, int max, Dictionary<int, long> Dict)
        {
            long ways = 0;
            if (max == start) return 1;
            if (Dict.ContainsKey(start)) return Dict[start];

            var cnt = ints.Where(x => x >= start + 1 && x <= start + 3).ToArray();
            var l = cnt.Length;

            ways += CountPaths(ints, cnt[0], max, Dict);
            if (l > 1)
            {
                ways += CountPaths(ints, cnt[1], max, Dict);
                
                if (l == 3) ways += CountPaths(ints, cnt[2], max, Dict);
            }
            Dict.TryAdd(start, ways);

            return ways;
        }
    }
}
