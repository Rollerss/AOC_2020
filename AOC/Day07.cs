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
            AOCDay07Part1(data);
            AOCDay07Part2(data);
        }

        public static void AOCDay07Part1(string[] data)
        {
            var dataDict = CreateDictPart1(data);
            
            var find = "shiny gold bag";

            var foundBagsDict = new Dictionary<string, string[]>();
            FindBags(dataDict, find, foundBagsDict);
            
            var count = -1;
            while (count != foundBagsDict.Keys.Count)
            {
                count = foundBagsDict.Keys.Count;
                var localDictCopy = foundBagsDict.ToDictionary(entry => entry.Key,entry => entry.Value);
                foreach (var item in localDictCopy)
                {
                    FindBags(dataDict, item.Key, foundBagsDict);
                }
            }

            Console.WriteLine($"Day 7 Part 1: {foundBagsDict.Keys.Count}");
        }

        public static void FindBags(Dictionary<string, string[]> dataDict, string find, 
                                           Dictionary<string, string[]> foundDict)
        {
            foreach (var item in dataDict)
            {
                foreach (var items in item.Value)
                {
                    if (items.Contains(find))
                    {
                        foundDict.TryAdd(item.Key, item.Value);
                    }
                }
            }
        }

        public static Dictionary<string, string[]> CreateDictPart1(string[] data)
        {
            var dataDict = new Dictionary<string, string[]>();
            foreach (var item in data)
            {
                var dataS = item.Split("s contain ");
                dataDict.Add(dataS[0], dataS[1].Split(", "));
            }
            return dataDict;
        }


        public static void AOCDay07Part2(string[] data)
        {
            var dataDict = CreateDataDict(data);
            var find = "shinygold";
            var bagsInBag = findBagCount(find, dataDict);

            Console.WriteLine($"Day 7 Part 2: {bagsInBag - 1}");
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

        public static Dictionary<string, List<Tuple<int, string>>> CreateDataDict(string[] data)
        {
            var dataDict = new Dictionary<string,List<Tuple<int,string>>>();
            foreach (var item in data)
            {
                var dataSplit = item.Split("s contain ");
                var dataSplit0 = dataSplit[0].Split(" ");
                var key = dataSplit0[0] + dataSplit0[1];
                var values = parseValues(dataSplit[1]);
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
    }
}
