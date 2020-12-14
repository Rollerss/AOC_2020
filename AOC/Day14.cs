using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day14
    {
        public static void AOCDay14()
        {
            var fileName = @".\InputData\AOCDay14test1.txt";
            fileName = @".\InputData\AOCDay14test2.txt";
            fileName = @".\InputData\AOCDay14.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine);
            AOCDay14Part1(sd);
            AOCDay14Part2(sd);
        }

        public static void AOCDay14Part1(string[] data)
        {
            Dictionary<string, char[]> men = new();
            char[] mask = data[0].Split(" = ")[1].ToCharArray();
            var l = mask.Length;
            for (int i = 0; i < data.Length; i++)
            {
                var mem = data[i].Split(" = ");
                if (mem[0] == "mask")
                {
                    mask = mem[1].ToCharArray();                
                }
                else
                {
                    int ints = int.Parse(mem[1]);
                    var bit = Convert.ToString(ints, 2).PadLeft(l, '0').ToCharArray();
                    for (int k = 0; k < l; k++)
                    {
                        var m = mask[k];
                        if (m != 'X')
                            bit[k] = m;
                    }
                    var fail = men.TryAdd(mem[0], bit);
                    if (!fail)
                        men[mem[0]] = bit;
                }
            }
            long s = 0;
            men.ToList().ForEach(c => s += Convert.ToInt64(new string(c.Value), 2));

            Console.WriteLine($"Day 14 Part 1: {s}");
        }

        public static void AOCDay14Part2(string[] data)
        {

            Dictionary<string, int> men = new();
            char[] mask = data[0].Split(" = ")[1].ToCharArray();
            var l = mask.Length;
            for (int i = 0; i < data.Length; i++)
            {
                var mem = data[i].Split(" = ");
                if (mem[0] == "mask")
                {
                    mask = mem[1].ToCharArray();
                }
                else
                {
                    var ins = int.Parse(mem[0].Substring(4, mem[0].Length - 5));
                    var memNum = Convert.ToString(ins, 2).PadLeft(l, '0').ToCharArray();
                    Dictionary<string, int> mems = CreatAllMems(memNum, mask, int.Parse(mem[1]));
                    men.ToList().ForEach(c => mems.TryAdd(c.Key, c.Value));
                    men = mems;
                }
            }
            long s = 0;
            men.ToList().ForEach(c => s+=c.Value);

            Console.WriteLine($"Day 14 Part 2: {s} {3926790061594 == s}");
        }

        private static Dictionary<string, int> CreatAllMems(char[] memNum, char[] mask, int ins)
        {
            var l = mask.Length;
            char[] b = memNum;
            for (int k = 0; k < l; k++)
            {
                var m = mask[k];
                if (m == '1')
                {
                    b[k] = (m);
                }
                else if (m == 'X')
                {
                    b[k] = ('1');
                }
            }
            Dictionary<string, int> dict = new();
            dict.TryAdd(new string(b), ins);
            int[] dex = mask.Select((b, i) => b == 'X' ? i : -1).Where(i => i != -1).ToArray();
            int times = dex.Length;
            var cnt = 0;
            while (times > cnt)
            {
                var t = dex[cnt];
                Dictionary<string, int> dict2 = new();
                foreach (var item in dict)
                {
                    dict2.TryAdd(item.Key, ins);
                    var item2 = item.Key.ToCharArray();
                    _ = item2[t] == '0' ? item2[t] = '1' : item2[t] = '0';
                    dict2.TryAdd(new string(item2), ins);
                }

                dict2.ToList().ForEach(x => dict.TryAdd(x.Key, x.Value));

                cnt++;
            }
            return dict;
        }
    }
}
