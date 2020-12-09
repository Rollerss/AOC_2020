using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day09
    {
        public static void AOCDay09()
        {
            var fileName = @".\InputData\AOCDay09.txt";
            //var fileName = @".\InputData\AOCDay09test.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            long[] longs = Array.ConvertAll(data.Split('\n'), s => long.Parse(s));
            AOCDay09Part1(longs);
        }

        public static void AOCDay09Part1(long[] longs)
        {
            for (int i = 25; i < longs.Length; i++)
            {

                var lngs = longs.Skip(i - 25).Take(25);
                var bo = false;
                foreach (var item in lngs)
                {
                    var x = Math.Abs(longs[i] - item);
                    if (lngs.Contains(x)) bo = true;
                }
                if (!bo)
                {
                    Console.WriteLine($"Day 9 Part 1: {longs[i]}");
                    AOCDay09Part2(longs, longs[i]);
                    break;
                }
            }
        }

        public static void AOCDay09Part2(long[] iD, long x)
        {
            for (int i = 0; i < iD.Length; i++)
            {
                long sum = 0, max = 0, min = long.MaxValue;
                var j = i;
                while(sum < x)
                {
                    long b = iD[j];
                    if (min > b) min = b;
                    if (max < b) max = b;
                    sum += b;
                    j++;
                }

                if (sum == x)
                {
                    Console.WriteLine($"Day 9 Part 2: {min + max}");
                    break;
                }
            }
        }
    }
}
