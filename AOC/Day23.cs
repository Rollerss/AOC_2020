using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC
{
    public static class Day23
    {
        public static void AOCDay23()
        {
            var numberList1 = new List<int>() { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
            //var numberList1 = new List<int>() { 1, 3, 7, 8, 2, 6, 4, 9, 5 };

            int turns = 10000000;
            //AOCDay23Part1(numberList1, turns);

            var numberList = Enumerable.Range(1, 1000000).ToList();
            for (int i = 0; i < numberList1.Count; i++)
            {
                numberList[i] = numberList1[i];
            }
            turns = 10000000;
            //turns = 100002;
            AOCDay23Part1(numberList, turns);
        }

        public static string AOCDay23Part1(List<int> cups, int turns)
        {
            int cupCount = cups.Count();
            int cupCntLess4 = cupCount - 4;
            int cc = 0;
            int cCup;
            int dCup;
            int indexDCup = 0;
            DateTime start = DateTime.Now;

            for (int i = 0; i < turns; i++)
            {
                //List<int> move = new() { cups[(cc + 1) % cupCount], cups[(cc + 2) % cupCount], cups[(cc + 3) % cupCount] };
                var one = cups[(cc + 1) % cupCount];
                var two = cups[(cc + 2) % cupCount];
                var three = cups[(cc + 3) % cupCount];

                cups.Remove(one);
                cups.Remove(two);
                cups.Remove(three);

                //foreach (var item in move)
                //{
                //    cups.Remove(item);
                //}

                if (cc > cupCntLess4)
                    cc = cupCntLess4;
                cCup = cups[cc];
                dCup = cCup - 1;
                bool notFound = true;
                do
                {
                    if (one != dCup && two != dCup && three != dCup && dCup > 0 && cups.Contains(dCup))
                    {
                        indexDCup = cups.IndexOf(dCup) + 1;
                        notFound = false;
                    }
                    else
                        dCup--;
                    if (dCup <= 0)
                        dCup = cupCount;
                } while (notFound);

                cups.Insert(indexDCup, one);
                cups.Insert(indexDCup + 1, two);
                cups.Insert(indexDCup + 2, three);

                //for (int j = 0; j < 3; j++)
                //{
                //    cups.Insert(indexDCup + j, move[j]);
                //}

                cc = (cups.IndexOf(cCup) + 1) % cupCount;

                if (1 == i % 100000)
                    Console.WriteLine(DateTime.Now - start);

                // Console.WriteLine($"{i + 1}: {string.Join(",", cups)}");
            }
            if (cupCount < 10000)
            {
                var result = string.Join(",", cups);
                Console.WriteLine(result);
                return result;
            }
            else
            {
                var indx = cups.IndexOf(1);
                var two = cups[(indx + 1) % cupCount];
                var three = cups[(indx + 2) % cupCount];
                Console.WriteLine(two + " " + three);
                var answer = (two * three).ToString();
                Console.WriteLine("Answer: " + answer);
                return answer;
            }
        }

        public static string AOCDay23Part1A(List<int> cup, int turns)
        {
            LinkedList<int> cups = new(cup);
            LinkedListNode<int> node = cups.First;
            int cupCount = cups.Count;
            int nodeDesV = node.Value - 1;

            LinkedListNode<int> nodeOne = node.Next;
            LinkedListNode<int> nodeTwo = nodeOne.Next;
            LinkedListNode<int> nodeThree = nodeTwo.Next;
            cups.Remove(nodeOne);
            cups.Remove(nodeTwo);
            cups.Remove(nodeThree);
            int nodeOneV = nodeOne.Value;
            int nodeTwoV = nodeTwo.Value;
            int nodeThreeV = nodeThree.Value;

            DateTime start = DateTime.Now;
            for (int i = 0; i < turns; i++)
            {
                LinkedListNode<int> nodeDes = cups.First;
                bool notFound = true;
                do
                {
                    if (nodeDesV != nodeOneV &&
                       nodeDesV != nodeTwoV &&
                       nodeDesV != nodeThreeV &&
                       nodeDesV <= cupCount &&
                       nodeDesV > 0)
                    {
                        do
                        {
                            nodeDes = nodeDes.Next;
                        } while (nodeDes.Value != nodeDesV);
                        notFound = false;
                    }
                    else
                        nodeDesV--;
                    if (nodeDesV <= 0)
                        nodeDesV = cupCount;
                } while (notFound);

                cups.AddAfter(nodeDes, nodeThree);
                cups.AddAfter(nodeDes, nodeTwo);
                cups.AddAfter(nodeDes, nodeOne);
                LinkedListNode<int> nodeLast = cups.Last;

                if(node == nodeLast)
                {
                    node = cups.First;
                    nodeDesV = node.Value - 1;
                    nodeOne = node.Next;
                    nodeTwo = nodeOne.Next;
                    nodeThree = nodeTwo.Next;
                    cups.Remove(nodeOne);
                    cups.Remove(nodeTwo);
                    cups.Remove(nodeThree);
                    nodeOneV = nodeOne.Value;
                    nodeTwoV = nodeTwo.Value;
                    nodeThreeV = nodeThree.Value;
                }
                else
                {
                    node = node.Next;
                    if (node == nodeLast)
                    {

                    }

                }

                if (1 == i % 100000)
                    Console.WriteLine(DateTime.Now - start);

                // Console.WriteLine($"{i + 1}: {string.Join(",", cups)}");
            }

            if (cupCount < 10000)
            {
                var result = string.Join(",", cups);
                Console.WriteLine(result);
                return result;
            }
            else
            {

                var oneNode = cups.First;
                while (oneNode.Value != 1)
                {
                    oneNode = oneNode.Next;
                }
                var twoNode = oneNode.Next;
                var threeNode = twoNode.Next;
                string answer = (twoNode.Value * threeNode.Value).ToString();
                Console.WriteLine(answer);

                return answer;
            }
        }
    }
}
