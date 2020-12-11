using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day11
    {
        public static void AOCDay11()
        {
            string[] fileName = { "AOCDay11test1.txt", "AOCDay11.txt" };
            foreach (var file in fileName)
            {
                using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
                ParseThatData(streamReader.ReadToEnd());
            }
            //var fileName = "";
            //fileName = @".\InputData\AOCDay11test1.txt";
            //fileName = @".\InputData\AOCDay11test2.txt";
            //fileName = @".\InputData\AOCDay11.txt";
            //using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            //ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var i = data.Split(Environment.NewLine);
            List<List<char>> c = new();
            foreach (var item in i)
            {
                c.Add(item.ToCharArray().ToList());
            }
            AOCDay11Part1(c);
            AOCDay11Part2(c);
        }

        public static void AOCDay11Part1(List<List<char>> c)
        {
            var changed = false;
            do
            {
                (changed, c) = AddPeopel(c);
            } while (changed);

            var count = CountSeats(c);

            Console.WriteLine($"Day 11 Part 1: {count}");
        }

        public static int CountSeats(List<List<char>> c)
        {
            var count = 0;
            foreach (var l in c)
            {
                foreach (var s in l)
                {
                    //Console.Write(s);
                    if (s == '#') count++;
                }
                //Console.WriteLine();
            }
            //Console.WriteLine();
            return count;
        }

        public static (bool, List<List<char>>) AddPeopel(List<List<char>> ch)
        {
            var changed = false;
            var cnt = ch.Count;
            var len = ch[0].Count;
            var c2 = new List<List<char>>();
            for (int i = 0; i < cnt; i++)
            {
                c2.Add(new(ch[i]));
                for (int j = 0; j < len; j++)
                {
                    //      l     r 
                    // i-1 j-1 j j+1 t
                    // i   j-1 j j+1 
                    // i+1 j-1 j j+1 b

                    var s = ch[i][j];

                    if (s != '.')
                    {
                        var l = j - 1; //>= 0;
                        var r = j + 1; //< len;
                        var t = i - 1; //>= 0;
                        var b = i + 1; //< cnt;
                        var a = 0;

                        if (l >= 0)
                        {
                            if (t >= 0)
                                if (ch[t][l] == '#') a++;
                            if (b < cnt)
                                if (ch[b][l] == '#') a++;
                            if (ch[i][l] == '#') a++;
                        }
                        if (r < len)
                        {
                            if (t >= 0)
                                if (ch[t][r] == '#') a++;
                            if (b < cnt)
                                if (ch[b][r] == '#') a++;
                            if (ch[i][r] == '#') a++;
                        }
                        if (t >= 0 && ch[t][j] == '#') a++;
                        if (b < cnt && ch[b][j] == '#') a++;
                        if (s == 'L' && a == 0)
                        {
                            c2[i][j] = '#';
                            changed = true;
                        }
                        if (s == '#' && a > 3)
                        {
                            c2[i][j] = 'L';
                            changed = true;
                        }
                    }
                }
            }
            return (changed, c2);
        }

        public static void AOCDay11Part2(List<List<char>> c)
        {
            var changed = false;
            do
            {
                (changed, c) = CountPaths(c);
            } while (changed);

            var count = CountSeats(c);

            Console.WriteLine($"Day 11 Part 2: {count}");
        }

        public static (bool, List<List<char>>) CountPaths(List<List<char>> ch)
        {
            var changed = false;
            var cDY = ch.Count;
            var cRX = ch[0].Count;
            var c2 = new List<List<char>>();

            char floor = '.';
            char open = 'L';
            char taken = '#';
            var outOfRange = - 1;

            for (int i = 0; i < cDY; i++)
            {
                c2.Add(new(ch[i]));
                for (int j = 0; j < cRX; j++)
                {
                    //      l     r 
                    // i-1 j-1 j j+1 t
                    // i   j-1 j j+1 
                    // i+1 j-1 j j+1 b

                    var seat = ch[i][j];

                    if (seat != floor)
                    {
                        var left = j - 1; //>= 0;
                        var right = j + 1; //< cRX;
                        var up = i - 1; //>= 0;
                        var down = i + 1; //< cDY;
                        var occupied = 0;

                        if (left > outOfRange)
                        {
                            if (up > outOfRange)
                            {
                                if (ch[up][left] == taken) occupied++;
                                else if (ch[up][left] == floor)
                                {
                                    var y = up - 1;
                                    var x = left - 1;
                                    while (y > outOfRange && x > outOfRange)
                                    {
                                        if (ch[y][x] != floor)
                                        {
                                            if (ch[y][x] == taken) occupied++;
                                            break;
                                        }
                                        y--;
                                        x--;
                                    }
                                }
                            }
                            if (down < cDY)
                            {
                                if (ch[down][left] == taken) occupied++;
                                else if (ch[down][left] == floor)
                                {
                                    var y = down + 1;
                                    var x = left - 1;
                                    while (y < cDY && x > outOfRange)
                                    {
                                        if (ch[y][x] != floor)
                                        {
                                            if (ch[y][x] == taken) occupied++;
                                            break;
                                        }
                                        y++;
                                        x--;
                                    }
                                }
                            }
                            if (ch[i][left] == taken) occupied++;
                            else if (ch[i][left] == floor)
                            {
                                var x = left - 1;
                                while (x > outOfRange)
                                {
                                    if (ch[i][x] != floor)
                                    {
                                        if (ch[i][x] == taken) occupied++;
                                        break;
                                    }
                                    x--;
                                }
                            }
                        }
                        if (right < cRX)
                        {
                            if (up > outOfRange)
                            {
                                if (ch[up][right] == taken) occupied++;
                                else if (ch[up][right] == floor)
                                {
                                    var y = up - 1;
                                    var x = right + 1;
                                    while (y > outOfRange && x < cRX)
                                    {
                                        if (ch[y][x] != floor)
                                        {
                                            if (ch[y][x] == taken) occupied++;
                                            break;
                                        }
                                        y--;
                                        x++;
                                    }
                                }
                            }
                            if (down < cDY)
                            {
                                if (ch[down][right] == taken) occupied++;
                                else if (ch[down][right] == floor)
                                {
                                    var y = down + 1;
                                    var x = right + 1;
                                    while (y < cDY && x < cRX)
                                    {
                                        if (ch[y][x] != floor)
                                        {
                                            if (ch[y][x] == taken) occupied++;
                                            break;
                                        }
                                        y++;
                                        x++;
                                    }
                                }
                            }
                            if (ch[i][right] == taken) occupied++;
                            else if (ch[i][right] == floor)
                            {
                                var x = right + 1;
                                while (x < cRX)
                                {
                                    if (ch[i][x] != floor)
                                    {
                                        if (ch[i][x] == taken) occupied++;
                                        break;
                                    }
                                    x++;
                                }
                            }
                        }
                        if (up > outOfRange)
                        {
                            if (ch[up][j] == taken) occupied++;
                            else if (ch[up][j] == floor)
                            {
                                var y = up - 1;
                                while (y > outOfRange)
                                {
                                    if (ch[y][j] != floor)
                                    {
                                        if (ch[y][j] == taken) occupied++;
                                        break;
                                    }
                                    y--;
                                }
                            }
                        }
                        if (down < cDY)
                        {
                            if (ch[down][j] == taken) occupied++;
                            else if (ch[down][j] == floor)
                            {
                                var y = down + 1;
                                while (y < cDY)
                                {
                                    if (ch[y][j] != floor)
                                    {
                                        if (ch[y][j] == taken) occupied++;
                                        break;
                                    }
                                    y++;
                                }
                            }
                        }
                        //Console.WriteLine(a);
                        if (seat == 'L' && occupied == 0)
                        {
                            c2[i][j] = taken;
                            changed = true;
                        }
                        if (seat == taken && occupied > 4)
                        {
                            c2[i][j] = open;
                            changed = true;
                        }
                    }
                }
            }
            return (changed, c2);
        }
    }
}
