using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day20
    {
        public static void AOCDay20()
        {
            //string[] fileNames = { "AOCDay20test1.txt", "AOCDay20test2.txt", "AOCDay20.txt" };
            //foreach (var file in fileNames)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            var fileName = @".\InputData\AOCDay20test1.txt";
            //var fileName = @".\InputData\AOCDay20test2.txt";
           //fileName = @".\InputData\AOCDay20.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var ds = data.Split(Environment.NewLine + Environment.NewLine);
            AOCDay20Part1(ds);
        }

        public static void AOCDay20Part1(string[] data)
        {
            Dictionary<string, string> tileDict = new();
            Dictionary<string, string[]> tileInfoDict = new();
            foreach (var tile in data)
            {
                GetTileSides(tile, tileDict, tileInfoDict);
            }
            Dictionary<string, string> tileMatched = new();
            List<List<string>> matchList = new();
            foreach (var t in tileDict)
            {
                //    var tile = tileDict..Value;
                //    var tKey = tileDict.First().Key;
                string tVR = new string(t.Value.ToCharArray().Reverse().ToArray());
                //Console.WriteLine(k);
                var cnt = tileDict.Where(x => x.Value.Equals(t.Value) || x.Value.Equals(tVR)).ToList();
                if (cnt.Count == 2)
                {
                    List<string> matches = new();
                    matches.Add(tVR);
                    foreach (var (k1,v1) in cnt)
                    {
                        matches.Add(k1);
                        matches.Add(v1);
                        tileMatched.TryAdd(k1, v1);
                        //tileDict.Remove(k);
                    }
                    matchList.Add(matches);
                }

            }

            Console.WriteLine(tileMatched.Count);
            Dictionary<string, string> tileNotMatched = new();
            foreach (var (k,v) in tileDict)
            {
                if (!tileMatched.ContainsKey(k)) 
                {
                    tileNotMatched.TryAdd(k, v);
                    Console.WriteLine(k+ " " +v);
                }
            }

            var topLeftTile = "";
            foreach (var (k, v) in tileNotMatched)
            {
                if (k.Contains("TOP"))
                {
                    topLeftTile = k.Substring(0, 10);
                    if (tileNotMatched.ContainsKey(topLeftTile + "LFT"))
                    {
                        Console.WriteLine(k + $" {topLeftTile}LFT");
                    }
                }
            }

            AOCDay20Part2(tileInfoDict, matchList, topLeftTile);
        }

        public static void GetTileSides(string tile, Dictionary<string, string> tileDict, Dictionary<string, string[]> tileInfoDict)
        {
            //Dictionary<string, string> tileDict = new();

            var spiltTile = tile.Split(Environment.NewLine);
            tileInfoDict.TryAdd(spiltTile[0], spiltTile[1..]);
            var tileName = spiltTile[0];
            tileDict.TryAdd(tileName + "TOP", spiltTile[1]);
            tileDict.TryAdd(tileName + "BTM", spiltTile[spiltTile.Length - 1]);
            var leftSide = "";
            var rightSide = "";

            for (int i = 1; i < spiltTile.Length; i++)
            {
                var l = spiltTile[1].Length - 1;
                leftSide += spiltTile[i][0];
                rightSide += spiltTile[i][l];
            }
            tileDict.TryAdd(tileName + "LFT", leftSide);
            tileDict.TryAdd(tileName + "RHT", rightSide);
        }

        public static void AOCDay20Part2(Dictionary<string, string[]> tileInfoDict, List<List<string>> matchList, string topLeftTile)
        {
            int tileCount = tileInfoDict.Count();
            int sideCount = (int)Math.Sqrt(tileCount);
            int lineCount = tileInfoDict[topLeftTile][0].Length;
            int picLines = sideCount * lineCount - sideCount + 1;
            var wholePic = new List<List<char>>();


            //Console.WriteLine($"Day 20 Part 2: {ways}");
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
