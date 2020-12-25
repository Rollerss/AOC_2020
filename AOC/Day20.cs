using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
            fileName = @".\InputData\AOCDay20.txt";
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
                    foreach (var (k1, v1) in cnt)
                    {
                        matches.Add(k1);
                        matches.Add(v1);
                        tileMatched.TryAdd(k1, v1);
                        //tileDict.Remove(k);
                    }
                    matchList.Add(matches);
                }

            }

            //Console.WriteLine(tileMatched.Count);
            Dictionary<string, string> tileNotMatched = new();
            foreach (var (k, v) in tileDict)
            {
                if (!tileMatched.ContainsKey(k))
                {
                    tileNotMatched.TryAdd(k, v);
                    //Console.WriteLine(k + " " + v);
                }
            }

            var topLeftTile = "";
            foreach (var (k, v) in tileNotMatched)
            {
                if (k.Contains("TOP"))
                {
                    var top = k.Substring(0, 10);
                    if (tileNotMatched.ContainsKey(top + "LFT"))
                    {
                        topLeftTile = top;
                        //Console.WriteLine(k + $" {topLeftTile}");
                    }
                }
            }

            //foreach (var item in matchList)
            //{
            //    foreach (var t in item)
            //    {

            //        Console.Write(t + " ");

            //    }
            //    Console.WriteLine();
            //}

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
            int lineCount = tileInfoDict[topLeftTile][0].Length - 1;
            int picLines = sideCount * lineCount + 1;
            List<char[]> wholePic = new(picLines);
            for (int i = 0; i < picLines; i++)
            {
                wholePic.Add(new char[picLines]);
            }
            AddTileToPicture(wholePic, 0, 0, lineCount, tileInfoDict[topLeftTile]);
            (string tileID, string nextTop) tile = (topLeftTile, tileInfoDict[topLeftTile][lineCount]);
            tileInfoDict.Remove(topLeftTile);
            //PrintPic(wholePic);
            var topLine = topLeftTile;

            for (int j = 0; j < sideCount; j++)
            {
                if (j > 0)
                {
                    string Rline = GetRightLine(wholePic, lineCount, j * lineCount);
                    var cTile = FindBelow((topLine, Rline), matchList);
                    var tileLines = tileInfoDict[cTile];
                    var tries = 0;
                    while (vertalMatch(tileLines, Rline, lineCount))
                    {
                        if (tries % 2 == 0)
                        {
                            tileLines = Flip(tileLines, lineCount);
                        }
                        else
                        {
                            tileLines = Roate(tileLines, lineCount);
                        }
                        tries++;
                    }

                    AddTileToPicture(wholePic, lineCount * j, lineCount * 0, lineCount, tileLines);
                    tile = (cTile, tileLines[lineCount]);
                    topLine = cTile;
                }

                for (int i = 1; i < sideCount; i++)
                {
                    var cTile = FindBelow(tile, matchList);
                    var tileLines = tileInfoDict[cTile];
                    if (tileLines[0] != tile.nextTop)
                    {
                        tileLines = RoateTile(tile.nextTop, tileLines, lineCount);
                    }
                    AddTileToPicture(wholePic, lineCount * j, lineCount * i, lineCount, tileLines);
                    tile = (cTile, tileLines[lineCount]);
                    //PrintPic(wholePic);
                    tileInfoDict.Remove(cTile);
                }

            }
            var board = @".#.#..#.##...#.##..#####
###....#.#....#..#......
##.##.###.#.#..######...
###.#####...#.#####.#..#
##.#....#.##.####...#.##
...########.#....#####.#
....#..#...##..#.#.###..
.####...#..#.....#......
#..#.##..#..###.#.##....
#.####..#.####.#.#.###..
###.#.#...#.######.#..##
#.####....##..########.#
##..##.#...#...#.#.#.#..
...#..#..#.#.##..###.###
.#.#....#.##.#...###.##.
###.#...#..#.##.######..
.#.#.###.##.##.#..#.##..
.####.###.#...###.#..#.#
..#.#..#..#.#.#.####.###
#..####...#.#.#.###.###.
#####..#####...###....##
#.##..#..#...#..####...#
.#.###..##..##..####.##.
...###...##...#...#..###";

            var monster =
@"(?=                  # )
#    ##    ##    ###
 #  #  #  #  #  #   ".Replace(" ", ".");
            //    01234567890123456789
            // (?=..................#.)
            //    xxxxxxxxxxxxxxxxxx8x
            //    #....##....##....###
            //    0xxxx56xxxx12xxxx789
            //    .#..#..#..#..#..#...
            //    x1xx4xx7xx0xx3xx6xxx
            var m0 = "                  # ".Replace(" ", ".");
            var m1 = "#    ##    ##    ###".Replace(" ", ".");
            var m2 = " #  #  #  #  #  #   ".Replace(" ", ".");

            string input = "";
            List<string> playPic = new();
            foreach (var item in wholePic)
            {
                playPic.Add(new string(item));
            }
            //Console.WriteLine(monster.Where(x => x == '#').Count());
            //Console.WriteLine(m2.Length);
            //Console.WriteLine(m1.Length);
            //Console.WriteLine(m0.Length);
            MatchCollection matches = Regex.Matches(input, monster);
            var times = 0;
            var count = 0;
            //count == 0 && matches.Count == 0 &&
            var comp = board.Split(Environment.NewLine).ToList();
            //playPic = board.Split(Environment.NewLine).ToList();
            playPic = STrim(playPic);
            playPic.ForEach(x => input += x + Environment.NewLine);
            var cnt = input.Where(x => x == '#').Count();
            Console.WriteLine(cnt);
            var mcnt = monster.Where(x => x == '#').Count();
            Console.WriteLine(mcnt);

            while (times < 9)
            {
                //if (input.Equals(board))
                //    Console.WriteLine("There is a match");


                count = 0;
                //Console.WriteLine();
                //Console.WriteLine(input);
                //Console.WriteLine();
                //Console.WriteLine();

                for (int i = 0; i < playPic.Count - 2; i++)
                {
                    //if (playPic[i] == comp[i])
                    //    Console.WriteLine("There is a match");
                    for (int j = 0; j < playPic[0].Length - m0.Length; j++)
                    {
                        var x1 = playPic[i].Substring(j, m0.Length);
                        var x2 = playPic[i + 1].Substring(j, m0.Length);
                        var x3 = playPic[i + 2].Substring(j, m0.Length);
                        var x = '#';
                        if (x1[18] == x)
                            if (x2[0] == x && x2[5] == x && x2[6] == x && x2[11] == x && x2[12] == x && x2[17] == x && x2[18] == x && x2[19] == x)
                                if (x3[1] == x && x3[4] == x && x3[7] == x && x3[10] == x && x3[13] == x && x3[16] == x)
                                    count++;
                        //    0xxxx56xxxx12xxxx789
                        //    x1xx4xx7xx0xx3xx6xxx
                        //Console.WriteLine(playPic[i].Substring(j, m0.Length));
                        //if (Regex.IsMatch(x1, m0))
                        //if (Regex.IsMatch(x2, m1))
                        //    if (Regex.IsMatch(x3, m2))
                        //    {
                        //        count++;
                        //    }
                    }
                }
                if (count != 0)
                {
                    Console.WriteLine(count);

                    Console.WriteLine(cnt - (mcnt * count));
                }

                //for (int i = 2; i < playPic.Count; i++)
                //{
                //    //if (Regex.IsMatch(playPic[i - 1], m1))

                //    if (Regex.IsMatch(playPic[i], m2))
                //        if (Regex.IsMatch(playPic[i - 2], m0))
                //        {
                //            count++;
                //            //Console.WriteLine(playPic[i - 2]);
                //            //Console.WriteLine(playPic[i - 1]);
                //            //Console.WriteLine(playPic[i]);
                //Console.WriteLine();
                //            //Console.WriteLine(playPic[i + 1]);
                //            //Console.WriteLine(playPic[i + 2]);
                //        }

                //}

                //matches = Regex.Matches(input, "$" + monster, RegexOptions.Multiline);
                //foreach (Match match in matches)
                //{
                //    // Loop through captures.
                //    foreach (Capture capture in match.Captures)
                //    {
                //        // Display them.
                //        Console.WriteLine("--" + capture.Value);
                //    }
                //}


                if (times % 2 == 0)
                {
                    playPic = SFlip(playPic);
                }
                else
                {
                    playPic = SRoate(playPic);
                }
                input = "";
                playPic.ForEach(x => input += x + Environment.NewLine);

                //matches = Regex.Matches(input, monster);
                times++;
                //Console.WriteLine(matches.Count + " " + times);

            }



            //Console.WriteLine(monster);
            //Console.WriteLine($"Day 20 Part 2: {ways}");


        }
        private static List<string> STrim(List<string> wholePic)
        {
            List<string> rTile = new();

            for (int i = 1; i < wholePic.Count; i++)
            {
                if (i % 9 != 0)
                {

                    string newline = "";
                    for (int j = 1; j < wholePic[0].Length; j++)
                    {
                        if (j % 9 != 0)
                            newline += wholePic[j][i];
                    }
                    rTile.Add(newline);
                }
            }
            return rTile;
        }

        private static List<string> SFlip(List<string> wholePic)
        {
            List<string> rTile = new();

            for (int j = wholePic.Count - 1; j >= 0; j--)
            {
                rTile.Add(wholePic[j]);
            }
            return rTile;
        }

        private static List<string> SRoate(List<string> pic)
        {
            List<string> rTile = new();
            var count = pic.Count();
            for (int i = 0; i < count; i++)
            {
                string newline = "";
                for (int j = 0; j < count; j++)
                {
                    newline += pic[j][i];
                }
                rTile.Add(newline);
            }
            return rTile;
        }
        public static bool vertalMatch(string[] tileLines, string tile, int lineCount)
        {
            string rline = "";

            for (int i = 0; i <= lineCount; i++)
            {
                rline += tileLines[i][0];
            }

            //Console.WriteLine($"{rline} {rline.Length} {tile} {tile.Length}");
            return rline != tile;
        }

        private static string GetRightLine(List<char[]> wholePic, int lineCount, int j)
        {
            string rline = "";

            for (int i = 0; i <= lineCount; i++)
            {
                rline += wholePic[i][j];
            }

            return rline;
        }

        private static string[] RoateTile(string nextTop, string[] tileLines, int count)
        {
            //Console.WriteLine(nextTop);
            //Console.WriteLine();
            //PrintTile(tileLines);
            var tries = 0;
            while (tileLines[0] != nextTop)
            {
                if (tries % 2 == 0 || tileLines[count] == nextTop)
                {
                    tileLines = Flip(tileLines, count);
                }
                else
                {
                    tileLines = Roate(tileLines, count);
                }
                tries++;
                //PrintTile(tileLines);
            }
            return tileLines;
        }

        private static string[] Flip(string[] tileLines, int count)
        {
            List<string> rTile = new();
            for (int j = count; j >= 0; j--)
            {
                rTile.Add(tileLines[j]);
            }
            return rTile.ToArray();
        }

        private static string[] Roate(string[] tileLines, int count)
        {
            List<string> rTile = new();
            for (int i = 0; i <= count; i++)
            {
                string newline = "";
                for (int j = 0; j <= count; j++)
                {
                    newline += tileLines[j][i];
                }
                rTile.Add(newline);
            }
            return rTile.ToArray();
        }

        public static string FindBelow((string tileID, string nextTop) tile, List<List<string>> matchList)
        {
            foreach (var item in matchList)
            {
                if (item[0] == tile.nextTop || item[2] == tile.nextTop)
                {
                    if (item[1].Contains(tile.tileID))
                    {
                        return item[3].Substring(0, item[0].Length);
                    }
                    else
                    {
                        return item[1].Substring(0, item[0].Length);
                    }
                }
            }
            return "no match found";
        }

        public static void AddTileToPicture(List<char[]> pic, int x, int y, int count, string[] part)
        {
            //var lines = part[1..]; 

            for (int i = 0; i <= count; i++)
            {
                for (int j = 0; j <= count; j++)
                {
                    char c = part[i][j];
                    //Console.WriteLine($"{i + y} {j + x}");
                    pic[i + y][j + x] = c;
                }
            }
        }
        public static void PrintTile(string[] tile)
        {
            Console.WriteLine();
            foreach (var line in tile)
            {
                Console.WriteLine(line);
            }
        }
        public static void PrintPic(List<char[]> pic)
        {
            Console.WriteLine();
            foreach (var line in pic)
            {
                Console.WriteLine(string.Join("", line));
            }
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
