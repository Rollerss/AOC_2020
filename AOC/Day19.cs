using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day19
    {
        public static void AOCDay19()
        {
            string[] fileNames = { "AOCDay19test1.txt", "AOCDay19.txt" };
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
            var sd = data.Split(Environment.NewLine);
            AOCDay19Part1(sd);
            //AOCDay19Part2(ints);
        }

        public static void AOCDay19Part1(string[] data)
        {
            var rules = data.Where(x => x != "" && char.IsDigit(x[0]));
            //var splitRules = data.Where(x => x.Contains('|'));
            var endRules = data.Where(x => x.Contains('"'));
            //var onePartRules = data.Where(x => !x.Contains('|') && !x.Contains('"') && x.Contains(':'));

            var baseRules = EndRules(endRules);
            //var onePart = OnePartRules(onePartRules);
            var twoPart = TwoPartRules(rules);
            //foreach (var item in onePart)
            //{
            //    if (item.Key != "0")
            //        twoPart.TryAdd(item.Key, item.Value);
            //    else
            //        zero = item.Value;
            //}

            foreach (var (k, v) in twoPart)
            {
                for (int i = 0; i < v.Count; i++)
                {
                    for (int j = 0; j < v[i].Count; j++)
                    {
                        foreach (var (k1, v1) in baseRules)
                        {
                            if (v[i][j] == k1)
                            {
                                v[i][j] = v1;
                            }
                        }
                    }
                }
            }
            //for (int i = 0; i < zero.Count; i++)
            //{
            //    for (int j = 0; j < zero[i].Count; j++)
            //    {
            //        var y = zero[i][j];
            //        if (baseRules.ContainsKey(y))
            //        {
            //            zero[i][j] = baseRules[y];
            //        }
            //    }
            //}


            List<List<string>> zero = new(twoPart["0"]);
            zero = UpdateZero(zero, twoPart);
            Dictionary<string, string> keys = new();
            foreach (var item in zero)
            {
                var s = "";
                item.ForEach(x => s += x);
                //foreach (var x in item)
                //{
                //    s += x;
                //}
                keys.TryAdd(s, s);
                //Console.WriteLine(s.Count() + " " + s);
                //Console.WriteLine(s);
            }
            //keys.TryAdd("aabbbbbbbababaaaabaaaabbbbbaabba", "aabbbbbbbababaaaabaaaabbbbbaabba");
            //Console.WriteLine($"len of 24: {zero.Where(x => x.Count != 24).Count()}");
            Console.WriteLine(zero.Count);
            Console.WriteLine(keys.Count);
            var count = 0;
            var count2 = 0;
            foreach (var item in data)
            {
                //Console.WriteLine(item.Count() + " " + item);
                //Console.WriteLine(item);
                if (keys.ContainsKey(item))
                {
                    //Console.WriteLine(item);
                    count++;
                }

                count2 += keys.Keys.ToList().Where(x => x == item).Count();
            }
            Console.WriteLine("count 1: " + count);
            Console.WriteLine("count 2: " + count2);
            //Console.WriteLine($"Day 19 Part 1: {cnt1} {cnt3} {cnt1 * cnt3}");
        }

        public static List<List<string>> UpdateZero(List<List<string>> zero, Dictionary<string, List<List<string>>> rules)
        {
            while (zero.Any(x => x.Any(y => y.Any(char.IsDigit))))
            {
                for (int i = 0; i < zero.Count; i++)
                {
                    if (zero[i].Any(x => x.Any(char.IsDigit)))
                    {

                        for (int j = 0; j < zero[i].Count; j++)
                        {
                            var str = zero[i][j];
                            if (str == "a" && str == "b")
                            { }
                            else if (rules.ContainsKey(str))
                            {
                                //Console.Write("rules vals: ");
                                //rules[str].ForEach(x => x.ForEach(y => Console.Write(y + " ")));
                                //Console.WriteLine();

                                //Console.Write("z of i: ");
                                //zero[i].ForEach(x => Console.Write(x + " "));
                                //Console.WriteLine();

                                var x = rules[str];
                                List<string> workingList = new(zero[i]);
                                bool firstRun = true;
                                foreach (var item in rules[str])
                                {
                                    List<string> newList = new();
                                    foreach (var strList in workingList)
                                    {
                                        if (strList == str)
                                        {
                                            foreach (var z in item)
                                            {
                                                newList.Add(z);
                                            }
                                        }
                                        else
                                        {
                                            newList.Add(strList);
                                        }
                                    }
                                    zero.Add(newList);
                                    //if (firstRun)
                                    //{
                                    //    zero[i] = newList;
                                    //    firstRun = false;

                                    //    //Console.Write("new z i: ");
                                    //    //zero[i].ForEach(x => Console.Write(x + " "));
                                    //    //Console.WriteLine();
                                    //}
                                    //else
                                    //{
                                    //    zero.Add(newList);

                                    //    //Console.Write("new list: ");
                                    //    //newList.ForEach(x => Console.Write(x + " "));
                                    //    //Console.WriteLine();
                                    //}
                                }
                                //Console.WriteLine();
                                //if (x.Count > 1)
                                //{ 
                                //    zero.Add(CreateCompositList(x[1], zero[i], str));
                                //}
                                //else if (x.Count > 2)
                                //{
                                //    Console.WriteLine("There is a bug in the code");
                                //}
                                //zero[i] = CreateCompositList(x[0], zero[i], str);

                                zero.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
            //for (int i = 0; i < zero.Count; i++)
            //{
            //    Console.Write($"{i}: ");
            //    zero[i].ForEach(x => Console.Write(x));
            //    Console.WriteLine();
            //}


            return zero;
        }
        public static List<string> CreateCompositList(List<string> keyValues, List<string> workingList, string key)
        {
            List<string> newList = new();
            foreach (var x in workingList)
            {
                if (key == x)
                {
                    foreach (var v in keyValues)
                    {
                        newList.Add(v);
                    }
                }
                else
                {
                    newList.Add(x);
                }
            }

            return newList;
        }

        public static Dictionary<string, List<List<string>>> TwoPartRules(IEnumerable<string> data)
        {
            Dictionary<string, List<List<string>>> dict = new();
            foreach (var item in data)
            {
                var r = item.Split(':');
                if (item.Contains('|'))
                {
                    dict.TryAdd(r[0], new List<List<string>>());
                    List<string> list = new();

                    foreach (var p in r[1].Split('|'))
                    {
                        dict[r[0]].Add(p.Split(" ").Where(x => x != "").ToList());
                    }
                }
                else
                {
                    dict.TryAdd(r[0], new List<List<string>>());
                    if (r[1].Contains("a"))
                    {
                        dict[r[0]].Add(new List<string>() { "a" });
                    }
                    else if (r[1].Contains("b"))
                    {
                        dict[r[0]].Add(new List<string>() { "b" });
                    }
                    else
                    {
                        List<string> list = r[1].Split(" ").Where(x => x != "").ToList();
                        dict[r[0]].Add(list);
                    }
                }
            }
            return dict;
        }
        public static Dictionary<string, List<List<string>>> OnePartRules(IEnumerable<string> data)
        {
            Dictionary<string, List<List<string>>> dict = new();
            foreach (var item in data)
            {
                var r = item.Split(':');
                List<string> list = r[1].Split(" ").Where(x => x != "").ToList();
                dict.TryAdd(r[0], new List<List<string>>());
                dict[r[0]].Add(list);
            }
            return dict;
        }

        public static Dictionary<string, string> EndRules(IEnumerable<string> data)
        {
            Dictionary<string, string> dict = new();
            foreach (var item in data)
            {
                var r = item.Split(':');
                if (r[1].Contains("a"))
                {
                    dict.TryAdd(r[0], "a");
                }
                else if (r[1].Contains("b"))
                {
                    dict.TryAdd(r[0], "b");
                }
                else
                {
                    Console.WriteLine($"end no match {item}");
                }
            }
            return dict;
        }

        public static void AOCDay19Part2(int[] ints)
        {
            var max = ints[ints.Length - 1];
            var start = 0;
            Dictionary<int, long> Dict = new Dictionary<int, long>();

            long ways = CountPaths(ints, start, max, Dict);

            Console.WriteLine($"Day 19 Part 2: {ways}");
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
