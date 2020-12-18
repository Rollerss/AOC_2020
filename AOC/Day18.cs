using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day18
    {
        public static void AOCDay18()
        {
            //string[] fileName = { "AOCDay18test1.txt", "AOCDay18test2.txt", "AOCDay18.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            //var fileName = @".\InputData\AOCDay18test1.txt";
            //var fileName = @".\InputData\AOCDay18test2.txt";
            var fileName = @".\InputData\AOCDay18.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine).ToList();

            AOCDay18Part(sd, true);
            AOCDay18Part(sd, false);
        }

        public static void AOCDay18Part(List<string> data, bool part1)
        {
            decimal s = 0;
            foreach (var line in data)
            {
                var p = LineSolver(line.Replace(" ", ""), part1);
                s += p;
            }
            if (part1)
                Console.WriteLine($"Day 18 Part 1: {s}");
            else
                Console.WriteLine($"Day 18 Part 1: {s}");
        }

        public static decimal LineSolver(string line, bool part1)
        {
            List<string> c = Array.ConvertAll(line.ToCharArray(), s => s.ToString()).ToList();
            var x = -1;
            var open = 0;
            while (c.Any(x => x.Contains(")")))
            {
                x++;
                if (c[x] == "(") open = x;
                else if (c[x] == ")")
                {
                    c[open] = PartSolver(c.GetRange(open + 1, x - open - 1), part1).ToString();
                    c.RemoveRange(open + 1, x - open);
                    x = -1;
                }
            }
            return PartSolver(c, part1);
        }

        public static decimal PartSolver(List<string> l, bool part1)
        {
            if (!part1)
            {
                while (l.Any(x => x.Contains("+")))
                {
                    int pI = l.IndexOf("+");
                    decimal a = decimal.Parse(l[pI - 1]) + decimal.Parse(l[pI + 1]);
                    l[pI - 1] = a.ToString();
                    l.RemoveRange(pI, 2);
                }
            }

            decimal x = -1;
            bool plus = true;
            for (int i = 0; i < l.Count; i++)
            {
                var a = l[i];
                decimal v;
                if (decimal.TryParse(a, out v))
                {
                    if (x == -1) x = v;
                    else if (plus) x += v;
                    else x *= v;
                }
                else if (a == "+") plus = true;
                else if (a == "*") plus = false;
            }
            return x;
        }
    }
}
