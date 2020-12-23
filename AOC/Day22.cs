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
            fileName = @".\InputData\AOCDay22.txt";
            using var streamReader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            ParseThatData(streamReader.ReadToEnd());
        }

        public static void ParseThatData(string data)
        {
            var sd = data.Split(Environment.NewLine + Environment.NewLine);
            var deck1 = Create(sd[0]);
            var deck2 = Create(sd[1]);
            AOCDay22Part1(new List<int>(deck1), new List<int>(deck2));
            AOCDay22Part2(deck1, deck2);
        }

        public static List<int> Create(string data)
        {
            var sd = data.Split(Environment.NewLine);
            return Array.ConvertAll(sd[1..], s => int.Parse(s)).ToList();
        }

        public static void AOCDay22Part1(List<int> p1deck, List<int> p2deck)
        {
            PlayGame(p1deck, p2deck, false);
            int score;
            if (p1deck.Count > 0)
                score = CalcScore(p1deck);
            else
                score = CalcScore(p2deck);

            Console.WriteLine($"Day 22 Part 1: {score}");
        }

        public static void AOCDay22Part2(List<int> p1deck, List<int> p2deck)
        {
            var winner = PlayGame(p1deck, p2deck, true);
            int score;
            if (winner == "p1")
                score = CalcScore(p1deck);
            else
                score = CalcScore(p2deck);

            Console.WriteLine($"Day 22 Part 2: {score}");
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

        public static string PlayGame(List<int> p1Deck, List<int> p2Deck, bool part2)
        {
            HashSet<(string, string)> seenGames = new();
            while (p1Deck.Count > 0 && p2Deck.Count > 0)
            {
                if (part2)
                {
                    (string, string) hands = (string.Join(",", p1Deck), string.Join(",", p2Deck));
                    if (seenGames.Contains(hands))
                    {
                        return "p1";
                    }
                    seenGames.Add(hands);
                }

                var p1Card = p1Deck[0];
                p1Deck.RemoveAt(0);
                var p2Card = p2Deck[0];
                p2Deck.RemoveAt(0);
                string winner;
                if (p1Card <= p1Deck.Count() && p2Card <= p2Deck.Count() && part2)
                {
                    List<int> p1 = new(p1Deck.GetRange(0, p1Card));
                    List<int> p2 = new(p2Deck.GetRange(0, p2Card));
                    winner = PlayGame(p1, p2, part2);
                }
                else if (p1Card > p2Card)
                    winner = "p1";
                else
                    winner = "p2";

                if (winner == "p1")
                {
                    p1Deck.Add(p1Card);
                    p1Deck.Add(p2Card);
                }
                else
                {
                    p2Deck.Add(p2Card);
                    p2Deck.Add(p1Card);
                }
            }
            if (p2Deck.Count == 0)
                return "p1";
            return "p2";
        }
    }
}
