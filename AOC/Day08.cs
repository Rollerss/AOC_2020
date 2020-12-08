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
            //AOCDay08Part1(data);
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


        public static int AOCDay08Part1(List<Tuple<string, int>> dataList)
        {
            var acc = 0;
            var pointer = 0;
            List<int> visted = new();
            while (!visted.Contains(pointer))
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
                if (inst == "jmp")
                {
                    pointer = pointer + dataList[pointer].Item2;
                }
            }
            Console.WriteLine($"Day 8 Part 1: {acc}");
            return acc;
        }

        public static void AOCDay08Part2(List<Tuple<string, int>> dataList)
        {
            var answer = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                var inst = dataList[i].Item1;
                List<Tuple<string, int>> list2 = new(dataList);
                if (inst == "nop")
                {
                    list2[i] = new Tuple<string, int>("jmp", list2[i].Item2);
                    try
                    {
                        answer = CycleThoughData(list2);

                        //Console.WriteLine($"Day 8 Part 2: {answer}");
                    }
                    catch { }
                }
                else if (inst == "jmp")
                {
                    list2[i] = new Tuple<string, int>("nop", list2[i].Item2);
                    try
                    {
                        answer = CycleThoughData(list2);
                        //Console.WriteLine($"Day 8 Part 2: {answer}");
                    }
                    catch { }
                }
                if (answer != 0)
                {
                    Console.WriteLine($"Day 8 Part 2: {answer}");
                }
            }
            //Console.WriteLine($"Day 8 Part 2: {answer}");
        }

        public static int CycleThoughData(List<Tuple<string, int>> dataList)
        {
            var acc = 0;
            var pointer = 0;
            //List<int> visted = new();
            var count = 0;
            while (pointer < dataList.Count && count < 1000)
            {
                count++;
                //visted.Add(pointer);
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
                if (inst == "jmp")
                {
                    pointer = pointer + dataList[pointer].Item2;
                }
            }
            //Console.WriteLine($"Day 8 Part 1: {acc}");
            if (count == 1000)
            {
                return 0;
            }
            return acc;
        }
    }
}
