using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public static class Day19
    {
        public static void AOCDay19()
        {
            string[] fileNames = { "AOCDay19test2.txt", "AOCDay19.txt" };
            foreach (var file in fileNames)
            {
                using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
                ParseThatData(streamReader.ReadToEnd());
            }
            //var fileName = @".\InputData\AOCDay19test1.txt";
            ////var fileName = @".\InputData\AOCDay19test2.txt";
            //fileName = @".\InputData\AOCDay19.txt";
            //using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            //ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var nl = Environment.NewLine;
            var d = data.Split(nl + nl);
            var sd = d[0].Split(nl);
            var input = d[1].Split(nl);
            AOCDay19Part1(sd, input);
        }

        public static void AOCDay19Part1(string[] rules, string[] input)
        {
            List<(string, string)> list = new();
            foreach (var rule in rules)
            {
                var ruleSplit = rule.Split(": ");
                list.Add((ruleSplit[0], ruleSplit[1]));
            }

            Dictionary<string, string> regDict = new();
            for (int i = 0; i < list.Count; i++)
            {
                RuleMaker(list[i].Item2, list[i].Item1, list, regDict);
            }

            var zeroVal = list.Where(x => x.Item1 == "0").FirstOrDefault().Item2;
            var zero = regDict[zeroVal];
            var zeroMatch = $"^{zero}$";
            var count = 0;

            foreach (var item in input)
            {
                if (Regex.IsMatch(item, zeroMatch))
                {
                    count++;
                }
            }

            var reg42 = regDict["42"];
            var val31 = list.Where(x => x.Item1 == "31").FirstOrDefault().Item2;
            var reg31 = regDict[val31];
            var count2 = 0;

            foreach (var item in input)
            {
                var match42 = Regex.Match(item, $"^{reg42}" + "{2,}");
                if (match42.Length > 0)
                {
                    var ix = item.Substring(match42.Length);
                    var match31 = Regex.Match(item, reg31 + "+$");
                    if (ix.Length == match31.Length && match42.Length > match31.Length && match31.Length > 0)
                    {
                        count2++;
                    }
                }
            }

            Console.WriteLine("Day 19 Part 1: " + count);
            //Console.WriteLine("The correct answer for part 2 is 332 that is 332 and only 3 3 2");
            Console.WriteLine("Day 19 Part 2: " + count2);
            Console.WriteLine();
        }
        public static string RuleMaker(string val, string key, List<(string, string)> list, Dictionary<string, string> regDict)
        {
            if (regDict.ContainsKey(val))
                return regDict[val];

            string newVal = "";
            if (val.Contains('|'))
            {
                var vals = val.Split(" | ");
                newVal = $"({RuleMaker(vals[0], key, list, regDict)}|{RuleMaker(vals[1], key, list, regDict)})";
            }
            else
            {
                if (val.Contains('"') && val.Contains("a"))
                {
                    var a = "a";
                    list.Add((a, a));
                    regDict.TryAdd(key, a);
                    regDict.TryAdd(a, a);
                    return a;
                }
                else if (val.Contains('"') && val.Contains("b"))
                {
                    var b = "b";
                    list.Add((b, b));
                    regDict.TryAdd(key, b);
                    regDict.TryAdd(b, b);
                    return b;
                }
                else
                {
                    var items = val.Split(" ").Where(x => x != "").ToList();
                    foreach (var item in items)
                    {
                        var itm = list.Where(x => x.Item1 == item).FirstOrDefault();
                        newVal += RuleMaker(itm.Item2, itm.Item1, list, regDict);
                    }
                }
            }
            regDict.TryAdd(val, newVal);
            return newVal;
        }
    }
}
