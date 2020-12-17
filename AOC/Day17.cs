using System;
using System.Collections.Generic;
using System.IO;

namespace AOC
{
    public static class Day17
    {
        public static void AOCDay17()
        {
            //string[] fileName = { "AOCDay17test1.txt", "AOCDay17test2.txt", "AOCDay17.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            //var fileName = @".\InputData\AOCDay17test1.txt";
            //var fileName = @".\InputData\AOCDay17test2.txt";
            var fileName = @".\InputData\AOCDay17.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            var data = streamReader.ReadToEnd();
            ParseThatData(data);
            ParseThatData2(data);
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine);
            Dictionary<(int, int, int), bool> list = new();
            int i = 0;
            for (int x = 0; x < sd.Length; x++)
            {
                for (int y = 0; y < sd[0].Length; y++)
                {
                    var c = sd[x][y];
                    if (c == '#')
                    {
                        list.Add((x, y, i), true);
                    }
                }
            }
            AOCDay17Part1B(list);
        }

        public static void ParseThatData2(string data)
        {
            var sd = data.Split(Environment.NewLine);
            Dictionary<(int, int, int,int), bool> list = new();
            int i = 0;
            for (int x = 0; x < sd.Length; x++)
            {
                for (int y = 0; y < sd[0].Length; y++)
                {
                    var c = sd[x][y];
                    if (c == '#')
                    {
                        list.Add((x, y, i,i), true);
                    }
                }
            }
            AOCDay17Part2(list);
        }

        public static void AOCDay17Part1B(Dictionary<(int, int, int), bool> l)
        {
            Console.WriteLine(l.Count);
            for (int c = 0; c < 6; c++)
            {

                Dictionary<(int, int, int), int> l2 = new();
                foreach (var item in l)
                {
                    var x = item.Key.Item1;
                    var y = item.Key.Item2;
                    var z = item.Key.Item3;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 2; k++)
                            {
                                if (i == 0 && j == 0 && k == 0)
                                { }
                                else
                                {
                                    var ky = (i + x, j + y, k + z);
                                    if (l2.ContainsKey(ky))
                                    {
                                        l2[ky] += 1;
                                    }
                                    else
                                    {
                                        l2.Add(ky, 1);
                                    }
                                }
                            }
                        }
                    }
                }
                Dictionary<(int, int, int), bool> l3 = new();
                foreach (var (key, v2) in l2)
                {
                    if (l.ContainsKey(key) && (v2 == 2 || v2 == 3))
                    {
                        l3.TryAdd(key, true);
                    }
                    else if (v2 == 3)
                    {
                        l3.TryAdd(key, true);
                    }
                }
                Console.WriteLine(l3.Count);
                l = l3;
            }
            Console.WriteLine(l.Count);
        }



        public static void AOCDay17Part2(Dictionary<(int, int, int, int), bool> l)
        {
            Console.WriteLine(l.Count);
            for (int c = 0; c < 6; c++)
            {

                Dictionary<(int, int, int, int), int> l2 = new();
                foreach (var item in l)
                {
                    var x = item.Key.Item1;
                    var y = item.Key.Item2;
                    var z = item.Key.Item3;
                    var w = item.Key.Item4;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 2; k++)
                            {
                                for (int h = -1; h < 2; h++)
                                {
                                    if (i == 0 && j == 0 && k == 0 && h == 0)
                                    { }
                                    else
                                    {
                                        var ky = (i + x, j + y, k + z, h + w);
                                        if (l2.ContainsKey(ky))
                                        {
                                            l2[ky] += 1;
                                        }
                                        else
                                        {
                                            l2.Add(ky, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Dictionary<(int, int, int, int), bool> l3 = new();
                foreach (var (key, v2) in l2)
                {
                    if (l.ContainsKey(key) && (v2 == 2 || v2 == 3))
                    {
                        l3.TryAdd(key, true);
                    }
                    else if (v2 == 3)
                    {
                        l3.TryAdd(key, true);
                    }
                }
                Console.WriteLine(l3.Count);
                l = l3;
            }
            Console.WriteLine(l.Count);
        }
    }
}
