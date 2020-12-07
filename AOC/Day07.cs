using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC
{
    public static class Day07
    {
        public static void AOCDay07()
        {
            var fileName = @".\InputData\AOCDay07.txt";
            //var fileName = @".\InputData\AOCDay07test.txt";
            //var fileName = @".\InputData\AOCDay07test2.txt";
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);

            var data = streamReader.ReadToEnd().Split('\n');
            //var data = streamReader.ReadToEnd().Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            AOCDay07Part1(data);
            AOCDay07Part2(data);
        }

        public static Dictionary<string, string[]> founds = new Dictionary<string, string[]>();

        public static void AOCDay07Part1(string[] data)
        {
            var find = "shiny gold bag";
            var dataL = new Dictionary<string, string[]>();
            foreach (var item in data)
            {
                var dataS = item.Split("s contain ");
                dataL.Add(dataS[0], dataS[1].Split(", "));
            }
            //Console.WriteLine($"Day 7 Part 1a: {dataL.Keys.Count}");
            CycleThoughData(dataL, find);
            var count = -1;
            while (count != founds.Keys.Count)
            {
                count = founds.Keys.Count;
                //Console.WriteLine(count);
                var local = founds.ToDictionary(entry => entry.Key,entry => entry.Value);
                foreach (var item in local)
                {
                    //Console.WriteLine(item.Key);
                    CycleThoughData(dataL, item.Key);
                }
            }

            Console.WriteLine($"Day 7 Part 1: {founds.Keys.Count}");
            //AOCDay07Part2(dataL);
        }

        public static void CycleThoughData(Dictionary<string, string[]> data, string find)
        {
            var data2 = new List<string[]>();
            foreach (var item in data)
            {
                foreach (var items in item.Value)
                {
                    if (items.Contains(find))
                    {
                        founds.TryAdd(item.Key, item.Value);
                    }
                }
            }
        }

        public static void AOCDay07Part2(string[] data)
        {
            var dataDict = CreateDataDict(data);
            var find = "shinygold";
            var x = findBagCount(find, dataDict);

            Console.WriteLine($"Day 5 Part 2: {x - 1}");
        }

        public static Dictionary<string, List<Tuple<int, string>>> CreateDataDict(string[] data)
        {
            var dataDict = new Dictionary<string,List<Tuple<int,string>>>();
            foreach (var item in data)
            {
                var dataSplit1 = item.Split("s contain ");
                var dataSplit2 = dataSplit1[0].Split(" ");
                var key = dataSplit2[0] + dataSplit2[1];
                var values = parseValues(dataSplit1[1]);
                dataDict.Add(key, values);
            }
            return dataDict;
        }

        public static List<Tuple<int, string>> parseValues(string values)
        {
            List<Tuple<int, string>> items = new List<Tuple<int, string>>();
            var noBag = "no other bag";
            var valueSplit = values.Split(", ");
            foreach (var item in valueSplit)
            {
                if (item.Contains(noBag))
                {
                    items.Add(new Tuple<int, string>(1, "noBag"));
                }
                else
                {
                    var valueSplit2 = item.Split(" ");
                    var bagCount = int.Parse(valueSplit2[0]);
                    string bagName = valueSplit2[1] + valueSplit2[2];
                    items.Add(new Tuple<int, string>(bagCount, bagName));
                }
            }
            return items;
        }

        public static int findBagCount(string find, Dictionary<string, List<Tuple<int, string>>> dataDict)
        {
            var values = dataDict[find];
            var totalBags = 1;
            foreach (var item in values)
            {
                if(item.Item2 != "noBag")
                {
                    totalBags += item.Item1 * findBagCount(item.Item2, dataDict);
                }
            }
            return totalBags;
        }
    }
}
