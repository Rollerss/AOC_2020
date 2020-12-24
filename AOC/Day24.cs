using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day24
    {
        public static void AOCDay24()
        {
            //string[] fileName = { "AOCDay24test1.txt", "AOCDay24test2.txt", "AOCDay24.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            var fileName = @".\InputData\AOCDay24test1.txt";
            //var fileName = @".\InputData\AOCDay24test2.txt";
            fileName = @".\InputData\AOCDay24.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine).ToList();
            AOCDay24Part(sd);
        }

        public static void AOCDay24Part(List<string> data)
        {
            Dictionary<(int, int), bool> tiles = new();
            foreach (var item in data)
            {
                (int, int) ti = new();
                var t = item.ToCharArray();
                var ns = 0;
                for (int i = 0; i < t.Length; i++)
                {
                    var t1 = t[i];
                    if (t1 == 'n')
                    {
                        ti.Item2 += 1;
                        ns = -1;
                    }
                    else if (t1 == 's')
                    {
                        ti.Item2 -= 1;
                        ns = -1;
                    }
                    else if (t1 == 'e')
                    {
                        ti.Item1 -= (2 + ns);
                        ns = 0;
                    }
                    else if (t1 == 'w')
                    {
                        ti.Item1 += (2 + ns);
                        ns = 0;
                    }
                }

                if (!tiles.TryAdd(ti, true))
                {
                    if (tiles[ti])
                        tiles[ti] = false;
                    else
                        tiles[ti] = true;
                }
            }
            var bc = 0;
            foreach (var item in tiles)
            {
                if (item.Value)
                    bc++;
            }

            Console.WriteLine($"Day 24 Part 1: {bc}");
            AOCDay24Part2(tiles);
        }

        public static void AOCDay24Part2(Dictionary<(int, int), bool> tiles)
        {
            List<(int, int)> dirs = new() { (2, 0), (1, 1), (-1, 1), (-2, 0), (-1, -1), (1, -1) };

            for (int i = 0; i < 100; i++)
            {
                Dictionary<(int, int), int> tile = new();
                foreach (var (k, v) in tiles)
                {
                    if (v)
                    {
                        foreach (var (x, y) in dirs)
                        {
                            (int, int) ti = (k.Item1 + x, k.Item2 + y);
                            if(!tile.TryAdd(ti, 1))
                            {
                                tile[ti] += 1;
                            }
                        }
                    }
                }
                Dictionary<(int, int), bool> tlx = new();
                foreach (var (k,v) in tile)
                {
                    bool tx = false;
                    tiles.TryGetValue(k,out tx);
                    if((tx && v > 0 && v < 3) || (!tx && v == 2))
                    {
                        tlx.Add(k, true);
                    }
                }
                tiles = tlx;
            }

            var bc = 0;
            foreach (var item in tiles)
            {
                if (item.Value)
                    bc++;
            }

            Console.WriteLine($"Day 24 Part 2: {bc}");
        }
    }
}
