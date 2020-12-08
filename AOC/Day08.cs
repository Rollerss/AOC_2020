using System;
using System.Collections.Generic;
using System.IO;

namespace AOC
{
    public static class Day08
    {
        public static void AOCDay08()
        {
            var fileName = @".\InputData\AOCDay08.txt";
            //var fileName = @".\InputData\AOCDay08test.txt";
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);

            var data = streamReader.ReadToEnd().Split('\n');
            ParseData(data);
        }

        public static void ParseData(string[] data)
        {
            List<Tuple<string, int>> dataList = new();

            foreach (var item in data)
            {
                var s = item.Split(" ");
                dataList.Add(new Tuple<string, int>(s[0], int.Parse(s[1])));
            }
            AOCDay08Part1(dataList);
            AOCDay08Part2(dataList);
        }

        public static void AOCDay08Part1(List<Tuple<string, int>> dataList)
        {
            Console.WriteLine($"Day 8 Part 1: {CycleThoughData(dataList).Item2}");
        }

        public static void AOCDay08Part2(List<Tuple<string, int>> dataList)
        {
            var len = dataList.Count;
            for (int i = 0; i < len; i++)
            {
                var inst = dataList[i].Item1;
                if (inst != "acc")
                {
                    List<Tuple<string, int>> list2 = new(dataList);
                    list2[i] = new Tuple<string, int>(inst == "jmp" ? "nop" : "jmp", list2[i].Item2);
                    var answer = CycleThoughData(list2);

                    if (answer.Item1 == len)
                    {
                        Console.WriteLine($"Day 8 Part 2: {answer.Item2}");
                        break;
                    }
                }
            }
        }

        public static Tuple<int,int> CycleThoughData(List<Tuple<string, int>> dataList)
        {
            var acc = 0;
            var pointer = 0;
            List<int> visted = new();
            while (!visted.Contains(pointer) && pointer < dataList.Count)
            {
                visted.Add(pointer);
                var inst = dataList[pointer].Item1;

                if (inst == "acc")
                {
                    acc += dataList[pointer].Item2;
                    pointer++;
                }
                else if (inst == "nop")
                {
                    pointer++;
                }
                else if (inst == "jmp")
                {
                    pointer += dataList[pointer].Item2;
                }
            }
            return new Tuple<int, int>(pointer, acc);
        }
    }
}
