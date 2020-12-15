using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day15
    {
        public static void AOCDay15()
        {
            //var fileName = @".\InputData\AOCDay15test.txt";
            ////fileName = @".\InputData\AOCDay15.txt";
            //using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            //ParseThatData(streamReader.ReadToEnd());
            List<int> data = new() { 999999999, 0, 8, 15, 2, 12, 1, 4 };
            var stop = 30000000;
            AOCDay15ParTuple(data, stop ,DateTime.Now);
            Console.WriteLine();
            AOCDay15Part2(data, stop, DateTime.Now);
            Console.WriteLine();
            stop = 2020;
            AOCDay15Part1(data, stop, DateTime.Now);
            Console.WriteLine();
            AOCDay15ParTuple(data, stop, DateTime.Now);
            Console.WriteLine();
            AOCDay15Part2(data, stop, DateTime.Now);
        }
        
        
        public static void AOCDay15Part1(List<int> dataA, int stop, DateTime start)
        {
            List<int> data = new();
            for (int i = 0; i <= stop; i++)
            {
                if (i < dataA.Count)
                {
                    data.Add(dataA[i]);
                }
                else
                {
                    var n = data[i - 1];
                    int[] dex = data.Select((b, i) => b == n ? i : -1).Where(i => i != -1).ToArray();
                    var y = dex.Length;
                    if (y == 1)
                    {
                        data.Add(0);
                    }
                    else
                    {
                        var x = dex[y - 1] - dex[y - 2];
                        data.Add(x);
                    }
                }
            }
            Console.WriteLine(DateTime.Now - start);
            Console.WriteLine($"Day 14 Part Tuple: {data[2020]}");
        }

       
        public static void AOCDay15Part2(List<int> data, int stop, DateTime start)
        {
            Dictionary<int, List<int>> dict = new();
            var cnt = data.Count;
            var x = -1;
            for (int i = 1; i <= stop; i++)
            {
                if (i < cnt)
                {
                    x = data[i];
                }
                else if (dict.ContainsKey(x))
                {
                    var c = dict[x].Count;
                    if (c == 1)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = dict[x][c - 1] - dict[x][c - 2];
                    }
                }
                dict.TryAdd(x, new List<int>());
                dict[x].Add(i);
                if (i == stop)
                {
                    Console.WriteLine(DateTime.Now - start);
                    Console.WriteLine($"Day 14 Part 2: {x}");
                }

            }
        }







        public static void AOCDay15ParTuple(List<int> data,int stop, DateTime start)
        {
            Dictionary<int, (int,int)> dict = new();
            var cnt = data.Count;
            var x = -1;
            for (int i = 1; i <= stop; i++)
            {
                var y = -1;
                if (i < cnt)
                {
                    x = data[i];
                }
                else if (dict.ContainsKey(x))
                {
                    var z = dict[x].Item1;
                    if (z == -1)
                    {
                        x = 0;
                    }
                    else
                    {
                        y = dict[x].Item2;
                        x = y - z;
                    }
                }
                dict.TryAdd(x, (-1, -1));
                dict[x] = (dict[x].Item2, i);
                if (i == stop)
                {
                    Console.WriteLine(DateTime.Now - start);
                    Console.WriteLine($"Day 14 Part Tuple: {x}");
                }
            }
        }
    }
}