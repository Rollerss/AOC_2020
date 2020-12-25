using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day25
    {
        public static void AOCDay25()
        {
            //string[] fileName = { "AOCDay25test1.txt", "AOCDay25test2.txt", "AOCDay25.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            //var fileName = @".\InputData\AOCDay25test1.txt";
            ////var fileName = @".\InputData\AOCDay25test2.txt";
            ////fileName = @".\InputData\AOCDay25.txt";
            //using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            //ParseThatData(streamReader.ReadToEnd());
            AOCDay25Part();
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine).ToList();
            AOCDay25Part();
        }

        public static void AOCDay25Part()
        {
            //decimal cardKey = 5764801;
            //decimal doorKey = 17807724;
            decimal cardKey = 12232269;
            decimal doorKey = 19452773;
            int div = 20201227;
            int n = 7;
            decimal dkey = 1;
            decimal cKey = 1;
            int j = 0;
            while(cKey != cardKey)
            {
                cKey *= n;
                cKey %= div;
                j++;
            }

            for (int i = 0; i < j; i++)
            {
                dkey *= doorKey;
               dkey %= div;
            }
            //    for (int j = 1; j < 10; j++)
            //    {
            //        doorKey = doorKey * j * n % div;

            //        if (cardKey == doorKey)
            //            break;
            //    }


            Console.WriteLine($"Day 25 Part 1: {dkey} {j}");
            //AOCDay25Part2(tiles);
        }

        public static void AOCDay25Part2(Dictionary<(int, int), bool> tiles)
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

            Console.WriteLine($"Day 25 Part 2: {bc}");
        }
    }
}
