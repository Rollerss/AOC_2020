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
            Console.WriteLine($"Day 8 Part 1: {CycleThoughData(dataList, true)}");
        }

        public static void AOCDay08Part2(List<Tuple<string, int>> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                try
                {
                    var inst = dataList[i].Item1;
                    var answer = 0;
                    List<Tuple<string, int>> list2 = new(dataList);
                    if (inst != "acc")
                    {
                        list2[i] = new Tuple<string, int>(inst == "jmp" ? "nop" : "jmp", list2[i].Item2);
                        answer = CycleThoughData(list2, false);
                    }
                    
                    if (answer != 0)
                    {
                        Console.WriteLine($"Day 8 Part 2: {answer}");
                        break;
                    }
                }
                catch { }
            }
        }

        public static int CycleThoughData(List<Tuple<string, int>> dataList, bool part1)
        {
            var acc = 0;
            var pointer = 0;
            List<int> visted = new();
            var count = 0;
            while (pointer < dataList.Count)
            {
                count++;
                if (part1)
                {
                    if(visted.Contains(pointer))
                    {
                        return acc;
                    }
                    visted.Add(pointer);
                }
                else if (count == 400)
                {
                    return 0;
                }

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
            return acc;
        }
    }
}
