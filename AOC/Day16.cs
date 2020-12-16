using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day16
    {
        public static void AOCDay16()
        {
            var fileName = @".\InputData\AOCDay16.txt";
            //fileName = @".\InputData\AOCDay16test2.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine + Environment.NewLine);
            AOCDay16Part1(sd);
        }

        public static Dictionary<string, List<(int, int)>> ParseRanges(string[] data1)
        {
            Dictionary<string, List<(int, int)>> dict = new();
            foreach (var data in data1)
            {
                var d1 = data.Split(':');
                for (int i = 0; i < d1.Length; i += 2)
                {
                    dict.TryAdd(d1[i], new List<(int, int)>());
                    var d2 = d1[i + 1].Split(" or ");
                    foreach (var item in d2)
                    {
                        var d3 = Array.ConvertAll(item.Split('-'), s => int.Parse(s));
                        dict[d1[i]].Add((d3[0], d3[1]));
                    }
                }
            }
            return dict;
        }

        public static List<int[]> ParseTickests(string data1)
        {
            List<int[]> list = new();
            var d1 = data1.Split(Environment.NewLine);
            for (int i = 1; i < d1.Length; i++)
            {
                var d3 = Array.ConvertAll(d1[i].Split(','), s => int.Parse(s));
                list.Add(d3);
            }
            return list;
        }

        public static void AOCDay16Part1(string[] data)
        {
            Dictionary<string, List<(int, int)>> keys = ParseRanges(data[0].Split(Environment.NewLine));
            List<int[]> ticket = ParseTickests(data[1]);
            List<int[]> tickets = ParseTickests(data[2]);
            List<int> notVal = new();
            List<int[]> goodTickest = new();
            var count = 0;

            for (int i = 0; i < tickets.Count; i++)
            {
                foreach (var p in tickets[i])
                {
                    var notInRange = true;
                    foreach (var it in keys)
                    {
                        var a = it.Value[0].Item1;
                        var b = it.Value[0].Item2;
                        var c = it.Value[1].Item1;
                        var d = it.Value[1].Item2;
                        if ((a <= p) && (b >= p) || (c <= p) && (d >= p))
                        {
                            notInRange = false;
                            break;
                        }
                    }
                    if (notInRange)
                    {
                        count += p;
                        if (!notVal.Contains(i))
                        {
                            notVal.Add(i);
                        }
                    }
                }
                if (!notVal.Contains(i))
                {
                    goodTickest.Add(tickets[i]);
                }
            }
            Console.WriteLine($"Day 16 Part 1: {count}");
            AOCDay16Part2(keys, ticket, goodTickest);
        }

        public static void AOCDay16Part2(Dictionary<string, List<(int, int)>> keys, List<int[]> ticket, List<int[]> goodTickest)
        {
            var gCount = goodTickest.Count;
            Dictionary<int, List<string>> valids = new();
            foreach (var it in keys)
            {
                for (int j = 0; j < goodTickest[0].Length; j++)
                {
                    var count = 0;
                    for (int i = 0; i < gCount; i++)
                    {
                        var p = goodTickest[i][j];

                        var a = it.Value[0].Item1;
                        var b = it.Value[0].Item2;
                        var c = it.Value[1].Item1;
                        var d = it.Value[1].Item2;
                        if ((a <= p) && (b >= p) || (c <= p) && (d >= p))
                        {
                            count++;
                        }
                    }
                    if (count == gCount)
                    {
                        valids.TryAdd(j, new List<string>());
                        valids[j].Add(it.Key);
                    }
                }
            }
            var maps = CrateMap(valids);

            long end = 1;
            var cnt = 0;
            foreach (var item in maps)
            {
                if (item.Key.Contains("departure"))
                {
                    var z = ticket[0][item.Value];
                    Console.WriteLine(z);
                    end *= z;
                    cnt++;
                }
            }
            Console.WriteLine($"Day 16 Part 2: {cnt} {end}");
        }

        public static Dictionary<string, int> CrateMap(Dictionary<int, List<string>> valids)
        {
            Dictionary<string, int> maps = new();
            var t = 1;
            while (maps.Count != valids.Count)
            {
                var x = valids.Where(x => x.Value.Count == t).First();

                for (int i = 0; i < x.Value.Count; i++)
                {
                    maps.TryAdd(x.Value[i], x.Key);
                }
                t++;
            }
            return maps;
        }
    }
}
