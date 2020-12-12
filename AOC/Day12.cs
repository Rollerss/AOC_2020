using System;
using System.IO;

namespace AOC
{
    public static class Day12
    {
        public static void AOCDay12()
        {
            //string[] fileName = { "AOCDay12test1.txt", "AOCDay12.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            var fileName = @".\InputData\AOCDay12test1.txt";
            //var fileName = @".\InputData\AOCDay12test2.txt";
            fileName = @".\InputData\AOCDay12.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var iData = data.Split(Environment.NewLine);
            AOCDay12Part1(iData);
            AOCDay12Part2(iData);
        }

        public static void AOCDay12Part1(string[] dirc)
        {
            var hor = 0;
            var ver = 0;
            var dir = 270;

            foreach (var item in dirc)
            {
                var d = item.Substring(0, 1);
                int n = int.Parse(item[1..]);
                var z = dir % 360;
                if (d == "F")
                {
                    if (z == 0) ver += n;
                    else if (z == 180 || z == -180) ver -= n;
                    else if (z == 90 || z == -270) hor += n;
                    else if (z == 270 || z == -90) hor -= n;
                }
                else if (d == "N") ver += n;
                else if (d == "S") ver -= n;
                else if (d == "W") hor += n;
                else if (d == "E") hor -= n;

                else if (d == "L") dir += n;
                else if (d == "R") dir -= n;
                //Console.WriteLine($"Day 12 Part 1: {item} {hor} {ver} {dir} {z} {Math.Abs(hor) + Math.Abs(ver)}");
            }
            //Console.WriteLine();
            Console.WriteLine($"Day 12 Part 1: {hor} {ver} {dir} {Math.Abs(hor) + Math.Abs(ver)}");
            //Console.WriteLine();
        }

        public static void AOCDay12Part2(string[] dirc)
        {
            var hor = 0;
            var ver = 0;
            var wHor = -10;
            var wVer = 1;

            foreach (var item in dirc)
            {
                var d = item.Substring(0, 1);
                int n = int.Parse(item[1..]);
                if (d == "F")
                {
                    hor += n * wHor;
                    ver += n * wVer;
                }
                else if (d == "N") wVer += n;
                else if (d == "S") wVer -= n;
                else if (d == "W") wHor += n;
                else if (d == "E") wHor -= n;
                else if (n == 180 && (d == "R" || d == "L"))
                {
                    wHor = -wHor;
                    wVer = -wVer;
                }
                else if ((d == "R" && n == 90) || (d == "L" && n == 270))
                {
                    var x = wHor;
                    wHor = -wVer;
                    wVer = x;
                }
                else if ((d == "L" && n == 90) || (d == "R" && n == 270))
                {
                    var x = wHor;
                    wHor = wVer;
                    wVer = -x;
                }

                //Console.WriteLine($"Day 12 Part 2: {item} h:{hor} v:{ver} wH:{wHor} wV:{wVer} {Math.Abs(hor) + Math.Abs(ver)}");
            }
            //Console.WriteLine();
            Console.WriteLine($"Day 12 Part 2: {hor} {ver} {Math.Abs(hor) + Math.Abs(ver)}");
            //Console.WriteLine();
        }
    }
}
