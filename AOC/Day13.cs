using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AOC
{
    public static class Day13
    {
        public static void AOCDay13()
        {
            //string[] fileName = { "AOCDay13test1.txt", "AOCDay13.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}

            var generic = new List<string>();//to get the generic using

            var fileName = @".\InputData\AOCDay13test1.txt";
            fileName = @".\InputData\AOCDay13.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var i1 = data.Replace("x", "0");
            var items = i1.Split(Environment.NewLine);
            int[] ints = Array.ConvertAll(items[1].Split(','), s => int.Parse(s));
            //int[] ints = ins.Where(x => x != 0).ToArray();
            int it1 = int.Parse(items[0]);
            //AOCDay13Part1(it1, ints);
            AOCDay13Part2(ints);
        }

        public static void AOCDay13Part1(int d, int[] ints)
        {
            int bestBus = int.MaxValue;
            int busNum = 0;
            foreach (var item in ints)
            {
                if (item != 0)
                {
                    var depart = 0;
                    while (true)
                    {
                        depart += item;
                        if (d < depart)
                        {
                            break;
                        }
                    }
                    if (bestBus > depart)
                    {
                        bestBus = depart;
                        busNum = item;
                    }
                }
            }


            Console.WriteLine($"Day 13 Part 1: {(bestBus - d) * busNum}");
        }
        // Driver code 
        //public static void Main()
        //{
        //    int[] num = { 3, 4, 5 };
        //    int[] rem = { 2, 3, 1 };
        //    int k = num.Length;
        //    Console.WriteLine("x is " + findMinX(num,
        //                                    rem, k));
        //}
        public static void AOCDay13Part2(int[] ns)
        {
            int[] n = ns.Where(x => x != 0).ToArray();
            var l = n.Length;
            var r = new List<int>();
            for (int i = 0; i < ns.Length; i++)
            {
                if (i == 0)
                {
                    r.Add(i);
                }
                else if (ns[i] != 0)
                {
                    r.Add(i);
                }
            }

            Console.WriteLine($"Day 13 Part 1: {findMinX(n, r.ToArray(), l)}");
        }

        public static decimal findMinX(int[] num, int[] rem, int k)
        {

            // Initialize result 
            decimal y = 100000000000;
            int z = num.Max();
            // As per the Chinese remainder theorem, 
            // this loop will always break.
            decimal x = num[3];
            while (true)
            {
                // Check if remainder of x % num[j] is  
                // rem[j] or not (for all j from 0 to k-1) 
                x = num[2] * y + rem[2];
                int j;
                for (j = 0; j < k; j++)
                {
                    //Console.WriteLine($"{num[j] } {x % num[j] } {rem[j]} ");
                    //Console.WriteLine(x * num[0]);

                    if (x % num[j] != rem[j])
                    {
                        break;
                    }
                }

                // If all remainders matched, we found x 
                if (j == k)
                    return x;
                // Else try next number
                //x += num[0];
                y++;
            }

        }


    }
}
