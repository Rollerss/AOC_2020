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
            //var fileName = @".\InputData\AOCDay11test1.txt";
            ////fileName = @".\InputData\AOCDay11test2.txt";
            ////fileName = @".\InputData\AOCDay11.txt";
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
            bool changed;
            do { (changed, c) = AddPeople(c, 3, true); } while (changed);

            var count = CountSeats(c);
            Console.WriteLine($"Day 11 Part 1: {count}");
        }

        public static void AOCDay11Part2(List<List<char>> c)
        {
            bool changed;
            do { (changed, c) = AddPeople(c, 4, false); } while (changed);

            var count = CountSeats(c);
            Console.WriteLine($"Day 11 Part 2: {count}");
        }

        public static (bool, List<List<char>>) AddPeople(List<List<char>> ch, int occupiedCount, bool part1)
        {
            var changed = false;
            var cDY = ch.Count;
            var cRX = ch[0].Count;
            var c2 = new List<List<char>>();
            char open = 'L';
            char taken = '#';
            for (int iY = 0; iY < cDY; iY++)
            {
                c2.Add(new(ch[iY]));
                for (int iX = 0; iX < cRX; iX++)
                {
                    var occupied = CheckSeats(iY, cDY, iX, cRX, part1, ch);
                    var seat = ch[iY][iX];
                    if (seat == open && occupied == 0)
                    {
                        c2[iY][iX] = taken;
                        changed = true;
                    }
                    if (seat == taken && occupied > occupiedCount)
                    {
                        c2[iY][iX] = open;
                        changed = true;
                    }
                }
            }
            return (changed, c2);
        }

        private static int CheckSeats(int iY, int cDY, int iX, int cRX, bool part1, List<List<char>> ch)
        {
            List<Tuple<int, int>> directions = new()
            {
                new(0, 1),
                new(0, -1),
                new(1, 0),
                new(-1, 0),
                new(1, 1),
                new(1, -1),
                new(-1, 1),
                new(-1, -1)
            };
            var outOfRange = -1;
            char floor = '.';
            char takenSeat = '#';
            var occupied = 0;

            foreach (var item in directions)
            {
                var y = iY + item.Item1;
                var x = iX + item.Item2;
                var i = 1;
                while (y > outOfRange && y < cDY && x > outOfRange && x < cRX)
                {
                    if (ch[y][x] != floor)
                    {
                        if (ch[y][x] == takenSeat) occupied++;
                        break;
                    }
                    else if (part1) break;
                    i++;
                    y = iY + item.Item1 * i;
                    x = iX + item.Item2 * i;
                }
            }
            return occupied;
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
    }
}