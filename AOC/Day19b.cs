using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public static class Day19b
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
            //regDict.TryAdd(zero.Item1, RuleMaker(zero.Item2, zero.Item1, list, regDict));
            //RuleMaker(zero.Item2, zero.Item1, list, regDict);
            var zero = regDict[zeroVal];
            var zeroMatch = $"^{zero}$";
            var count = 0;
            foreach (var item in input)
            {
                if (Regex.IsMatch(item, zeroMatch))
                {
                    count++;
                    //Console.WriteLine(item);
                }
            }
            var count2 = 0;
            var count3 = 0;
            var count4 = 0;
            var count5 = 0;
            var count6 = 0;
            var reg42 = regDict["42"];
            //Console.WriteLine(reg42);
            var val31 = list.Where(x => x.Item1 == "31").FirstOrDefault().Item2;
            var reg31 = regDict[val31];
            //RuleMaker(list[i].Item2, list[i].Item1, list, regDict);
            foreach (var item in input)
            {
                if (Regex.IsMatch(item, $"^{reg42}" + "{2,}" + reg31 + "+$"))
                {
                    count2++;

                    var match42 = Regex.Match(item, $"^{reg42}" + "{2,6}");
                    var ix = item.Substring(match42.Length);
                    var match31 = Regex.Match(item, reg31 + "+?$");
                    //if (Regex.IsMatch(item, reg31 + "${1,}"))
                    if (ix.Length == match31.Length)
                        count6++;
                    ////Console.WriteLine(item);
                    //var match42 = Regex.Match(item, $"^{reg42}" + "{1,}");
                    ////Console.WriteLine(match42);
                    //var match31 = Regex.Match(item, reg31 + "${1,}");
                    ////Console.WriteLine(match31);
                    //if (item.Length == match42.Length + match31.Length)
                    //    count3++;
                }
                //List<string> subdata = new();
                //if (Regex.IsMatch(item, $"^{reg42}" + "{1,}") && Regex.IsMatch(item, reg31 + "${1,}"))
                //{
                //    count4++;
                //    var match42 = Regex.Match(item, $"^{reg42}" + "{2,}");
                //    //Console.WriteLine(match42);
                //    var match31 = Regex.Match(item, reg31 + "${1,}");
                //    //Console.WriteLine(match31);
                //    if (item.Length == match42.Length + match31.Length)
                //        count5++;
                //    //Console.WriteLine(item);
                //    var match42b = Regex.Match(item, $"^{reg42}" + "{3,}");
                //    //Console.WriteLine(match42);
                //    var match31b = Regex.Match(item, reg31 + "${2,}");
                //    //Console.WriteLine(match31);
                //    //if (item.Length == match42b.Length + match31b.Length)
                //}

                //if (Regex.IsMatch(item, $"^{reg42}" + "{2,}"))// + reg31 + "${1,}"))
                //{



                //count2++;
                //Console.WriteLine(item);
                //Console.WriteLine(match42);
                //Console.WriteLine(match31);
                //count3++;
                //}
            }

            //Console.WriteLine("count 1: " + count);
            Console.WriteLine("count 2: " + count2);
            Console.WriteLine("count 3: " + count3);
            Console.WriteLine("count 4: " + count4);
            Console.WriteLine("count 5: " + count5);
            Console.WriteLine("count 6: " + count6);
            //Console.WriteLine($"Day 19 Part 1: {cnt1} {cnt3} {cnt1 * cnt3}");
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
                    //regDict.TryAdd(key, newVal);
                    //return newVal;
                }
            }
            regDict.TryAdd(val, newVal);
            return newVal;
        }
    }
}
