using AOC;
using System.Collections.Generic;
using Xunit;

namespace TestForAOC
{
    public class Day23Test
    {
        public List<int> TestNumberList = new List<int>() { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
        public List<int> ProdNumberList = new List<int>() { 1, 3, 7, 8, 2, 6, 4, 9, 5 };

        [Fact]
        public void TestResultAfter10Turns()
        {
            int turns = 10;
            string result = Day23.AOCDay23Part1(TestNumberList, turns);
            string answer = "5,8,3,7,4,1,9,2,6";
            Assert.Equal(answer, result);
        }


        [Fact]
        public void TestResultAfter100Turns()
        {
            int turns = 100;
            string result = Day23.AOCDay23Part1(TestNumberList, turns);
            string answer = "2,9,1,6,7,3,8,4,5";
            Assert.Equal(answer, result);
        }

        [Fact]
        public void TestResultAfter1000000Turns()
        {
            int turns = 1000000;
            string result = Day23.AOCDay23Part1(TestNumberList, turns);
            string answer = "9,7,2,4,3,6,8,1,5";
            Assert.Equal(answer, result);
        }

        [Fact]
        public void ProdResultAfter100Turns()
        {
            int turns = 100;
            string result = Day23.AOCDay23Part1(ProdNumberList, turns);
            string answer = "9,3,7,4,8,2,6,1,5";
            Assert.Equal(answer, result);
        }


        [Fact]
        public void ProdResultAfter1000000Turns()
        {
            int turns = 1000000;
            string result = Day23.AOCDay23Part1(ProdNumberList, turns);
            string answer = "5,7,2,8,1,3,9,4,6";
            Assert.Equal(answer, result);
        }

        [Fact]
        public void TestResultAfter10TurnsA()
        {
            int turns = 10;
            string result = Day23.AOCDay23Part1A(TestNumberList, turns);
            string answer = "5,8,3,7,4,1,9,2,6";
            Assert.Equal(answer, result);
        }


        [Fact]
        public void TestResultAfter100TurnsA()
        {
            int turns = 100;
            string result = Day23.AOCDay23Part1A(TestNumberList, turns);
            string answer = "2,9,1,6,7,3,8,4,5";
            Assert.Equal(answer, result);
        }

        [Fact]
        public void TestResultAfter1000000TurnsA()
        {
            int turns = 1000000;
            string result = Day23.AOCDay23Part1A(TestNumberList, turns);
            string answer = "9,7,2,4,3,6,8,1,5";
            Assert.Equal(answer, result);
        }

        [Fact]
        public void ProdResultAfter100TurnsA()
        {
            int turns = 100;
            string result = Day23.AOCDay23Part1A(ProdNumberList, turns);
            string answer = "9,3,7,4,8,2,6,1,5";
            Assert.Equal(answer, result);
        }


        [Fact]
        public void ProdResultAfter1000000TurnsA()
        {
            int turns = 1000000;
            string result = Day23.AOCDay23Part1A(ProdNumberList, turns);
            string answer = "5,7,2,8,1,3,9,4,6";
            Assert.Equal(answer, result);
        }
    }
}
