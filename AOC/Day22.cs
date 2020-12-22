using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public static class Day22
    {
        public static void AOCDay22()
        {
            //string[] fileNames = { "AOCDay22test1.txt", "AOCDay22test2.txt", "AOCDay22.txt" };
            //foreach (var file in fileNames)
            //{
            //    using var streamReader = new StreamReader(new FileStream(@".\InputData\" + file, FileMode.Open, FileAccess.Read));
            //    ParseThatData(streamReader.ReadToEnd());
            //}
            var fileName = @".\InputData\AOCDay22test1.txt";
            //var fileName = @".\InputData\AOCDay22test2.txt";
            //fileName = @".\InputData\AOCDay22.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine + Environment.NewLine);
            List<(string, List<int>)> decksList = new();
            Create(sd[0], decksList);
            Create(sd[1], decksList);
            //AOCDay22Part1(decksList);
            AOCDay22Part2(decksList);
        }

        public static void Create(string data, List<(string, List<int>)> decksList)
        {
            var sd = data.Split(Environment.NewLine);

            int[] ints = Array.ConvertAll(sd[1..], s => int.Parse(s));
            decksList.Add((sd[0], ints.ToList()));
        }

        public static void AOCDay22Part1(List<(string, List<int>)> decksList)
        {
            var p1deck = decksList[0];
            var p2deck = decksList[1];

            while(p1deck.Item2.Count > 0 && p2deck.Item2.Count > 0)
            {
                var p1Card = p1deck.Item2[0];
                var p2Card = p2deck.Item2[0];
                if (p1Card > p2Card)
                {
                    p1deck.Item2.Add(p1Card);
                    p1deck.Item2.Add(p2Card);
                }
                else
                {
                    p2deck.Item2.Add(p2Card);
                    p2deck.Item2.Add(p1Card);
                }
                    p1deck.Item2.RemoveAt(0);
                    p2deck.Item2.RemoveAt(0);
            }
            var score = 0;
            if (p1deck.Item2.Count > 0)
               score = CalcScore(p1deck.Item2);
            else
               score = CalcScore(p2deck.Item2);


            Console.WriteLine($"Day 22 Part 1: {score}");
        }

        public static int CalcScore(List<int> deck)
        {
            var score = 0;
            var len = deck.Count() - 1;
            for (int i = 0; i <= len; i++)
            {
                score += deck[len - i] * (i + 1);
            }
            return score;
        }

        public static void AOCDay22Part2(List<(string, List<int>)> decksList)
        {
            var p1deck = decksList[0];
            var p2deck = decksList[1];

            HashSet<(string, string)> hashRounds = new();

            var winner = CheckForMatch(hashRounds, p1deck, p2deck);
            
            var score = 0;
            score = CalcScore(winner.Item2);
            //if (winner.Item1 == "")
            //    score = CalcScore(p1deck.Item2);
            //else

            Console.WriteLine($"Day 22 Part 2: {score}");
        }

        public static (string, List<int>) CheckForMatch(HashSet<(string, string)> hashRounds, (string, List<int>) p1deck, (string, List<int>) p2deck)
        {
            while (p1deck.Item2.Count > 0 && p2deck.Item2.Count > 0)
            {
                string p1hand = string.Join(",", p1deck.Item2);
                string p2hand = string.Join(",", p2deck.Item2);
                bool matchingRounds = hashRounds.Contains((p1hand, p2hand));
                if (matchingRounds)
                {
                    Console.WriteLine($"Day 22 Part 2a: {CalcScore(p1deck.Item2)}");
                    break;
                    //return p1deck;
                }
                else
                    hashRounds.Add((p1hand,p2hand));
                var p1Card = p1deck.Item2[0];
                var p2Card = p2deck.Item2[0];
                var p1Cnt = p1deck.Item2.Count() - 1;
                var p2Cnt = p2deck.Item2.Count() - 1;
                if (p1Card <= p1Cnt && p2Card <= p2Cnt)
                {
                    (string, List<int>) p1 = new(p1deck.Item1, p1deck.Item2.GetRange(1, p1Card));
                    (string, List<int>) p2 = new(p2deck.Item1, p2deck.Item2.GetRange(1, p2Card));
                    var thisWinner = CheckForMatch(hashRounds, p1, p2);
                    if(thisWinner.Item1 == "")
                    {
                        return ("", new(p1deck.Item2));
                    }
                    else if(thisWinner.Item1 == p1deck.Item1)
                    {
                        p1deck.Item2.Add(p1Card);
                        p1deck.Item2.Add(p2Card);
                    }
                    else
                    {
                        p2deck.Item2.Add(p2Card);
                        p2deck.Item2.Add(p1Card);
                    }
                }
                else if (p1Card > p2Card)
                {
                    p1deck.Item2.Add(p1Card);
                    p1deck.Item2.Add(p2Card);
                }
                else
                {
                    p2deck.Item2.Add(p2Card);
                    p2deck.Item2.Add(p1Card);
                }
                p1deck.Item2.RemoveAt(0);
                p2deck.Item2.RemoveAt(0);
            }

            var p1count = p1deck.Item2.Count();
            var p2count = p2deck.Item2.Count();
            if (p1deck.Item2.Count > 0 && p2deck.Item2.Count > 0)
            {
                if(p1count + p2count == 50)
                    Console.WriteLine($"Day 22 Part 2b: {CalcScore(p1deck.Item2)}");
                return ("", new(p1deck.Item2));
            }
            else if (p1deck.Item2.Count > 0 && p2deck.Item2.Count == 0)
                return p1deck;
            else
                return p2deck;
        }
    }
}
