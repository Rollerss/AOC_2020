using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day21
    {
        public static void AOCDay21()
        {
            //string[] fileName = { "AOCDay21test1.txt", "AOCDay21test2.txt", "AOCDay21.txt" };
            //foreach (var file in fileName)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            var fileName = @".\InputData\AOCDay21test1.txt";
            //var fileName = @".\InputData\AOCDay21test2.txt";
            fileName = @".\InputData\AOCDay21.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }
        //Part 2 answer fqfm,kxjttzg,ldm,mnzbc,zjmdst,ndvrq,fkjmz,kjkrm

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine);
            List<(List<string>, List<string>)> inList = new();
            CreatedLists(sd, inList);
            AOCDay21Part1(inList);
            //AOCDay21Part2(ints);
        }

        public static void CreatedLists(string[] sd, List<(List<string>, List<string>)> inList)
        {
            foreach (var item in sd)
            {
                if (item.Contains(')'))
                {
                    var ssd = item.Split('(');
                    var l1 = ssd[0].Split(" ").Where(x => x != "").ToList();
                    var als = ssd[1];
                    string[] remove = { "contains", ",", ")" };
                    als = als.Replace(remove[0], "");
                    als = als.Replace(remove[1], "");
                    als = als.Replace(remove[2], "");
                    var l2 = als.Split(" ").Where(x => x != "").ToList();
                    inList.Add((l1, l2));
                }
                else
                {
                    var l1 = item.Split(" ").Where(x => x != "").ToList();
                    inList.Add((l1, new List<string>()));
                }
            }
        }

        public static void AOCDay21Part1(List<(List<string>, List<string>)> inList)
        {
            //var allAls = inList.Where(x => x.Item2.Count == 1).Select((Value, Index) => new { Index }).ToList();
            List<int> dex = inList.Select((b, i) => b.Item2.Count == 1 ? i : -1).Where(i => i != -1).ToList();
            Dictionary<string, string> alsDict = new();
            foreach (var (l1, l2) in inList)
            {
                l2.ForEach(x => alsDict.TryAdd(x, x));
            }
            int tooMany = 0;
            while (alsDict.Any(x => x.Key == x.Value) && tooMany < 1000)
            {
                if (tooMany == 800)
                {
                    Console.WriteLine("Break");
                }
                if (tooMany < 10)
                {
                    foreach (var item in dex)
                    {
                        Dictionary<string, int> tempAlgs = new();
                        string algy = inList[item].Item2[0];
                        var lists = inList.Where(x => x.Item2.Contains(algy));
                        int cnt = lists.Count();
                        MakeTempDict(tempAlgs, lists);

                        int matches = tempAlgs.Where(x => x.Value == cnt).Count();
                        if (matches == 1)
                        {
                            alsDict[algy] = tempAlgs.Where(x => x.Value == cnt).First().Key;
                        }
                        else
                        {
                            var dmatches = tempAlgs.Where(x => x.Value == cnt);
                            var countD = 0;
                            string key = "";
                            foreach (var ditem in dmatches)
                            {
                                if (alsDict.ContainsValue(ditem.Key))
                                {
                                    countD++;
                                }
                                else
                                {
                                    key = ditem.Key;
                                }
                            }
                            if (matches == countD + 1)
                            {
                                alsDict[algy] = key;
                            }
                            if (tooMany > 5)
                            {
                                foreach (var d1 in dmatches)
                                {
                                    if(!alsDict.ContainsValue(d1.Key))
                                        alsDict.TryAdd(algy + d1.Key, d1.Key);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Dictionary<string, int> tempAlgs = new();
                    string algy = "peanuts";
                    var lists = inList.Where(x => x.Item2.Contains(algy));
                    int cnt = lists.Count();
                    MakeTempDict(tempAlgs, lists);

                    int matches = tempAlgs.Where(x => x.Value == cnt).Count();
                    if (matches == 1)
                    {
                        alsDict[algy] = tempAlgs.Where(x => x.Value == cnt).First().Key;
                    }
                    else
                    {
                        var dmatches = tempAlgs.Where(x => x.Value == cnt);

                        foreach (var d1 in dmatches)
                        {
                            if (!alsDict.ContainsValue(d1.Key))
                                alsDict.TryAdd(algy + d1.Key, d1.Key);
                        }
                    }
                    break;
                }
                //foreach (var (k, v) in alsDict)
                //{
                //    Console.WriteLine(k + " " + v);
                //}
                tooMany++;
                Console.WriteLine(tooMany);
            }
            Console.WriteLine($"trys {tooMany} {alsDict.Count}");
            alsDict.ToList().ForEach(x => Console.WriteLine(x.Key + " " + x.Value));
            Dictionary<string, int> allIngs = new();
            foreach (var i1 in inList)
            {
                foreach (var ii1 in i1.Item1)
                {
                    if (alsDict.ContainsValue(ii1))
                    {
                        allIngs.TryAdd(ii1, 0);
                    }
                    else
                    {
                        allIngs.TryAdd(ii1, 1);
                    }
                         
                }
            }
            Console.WriteLine(allIngs.Where(x => x.Value == 1).Count() + " " + allIngs.Count());
            var ingCount = 0;
            foreach (var i1 in inList)
            {
                foreach (var ii1 in i1.Item1)
                {
                    ingCount += allIngs[ii1];

                }
            }
            Console.WriteLine(ingCount);
        }

        private static void MakeTempDict(Dictionary<string, int> tempAlgs, IEnumerable<(List<string>, List<string>)> lists)
        {
            foreach (var i1 in lists)
            {
                foreach (var ii1 in i1.Item1)
                {
                    if (!tempAlgs.TryAdd(ii1, 1))
                    {
                        tempAlgs[ii1] += 1;
                    }
                }
            }
        }

        public static void AOCDay21Part2(int[] ints)
        {


            //Console.WriteLine($"Day 21 Part 2: {ways}");
        }
    }
}
