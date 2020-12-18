using AOC;
using Xunit;

namespace TestForAOC
{
    public class Day18Test
    {
        public string testCase1 = "1 + (2 * 3) + (4 * (5 + 6))"; //51
        public string testCase2 = "2 * 3 + (4 * 5)"; //26
        public string testCase3 = "5 + (8 * 3 + 9 + 3 * 4 * 3)"; //437
        public string testCase4 = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4)) "; //12240
        public string testCase5 = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"; //13632

        [Fact]
        public void TestShouldEqual51()
        {
            string line = testCase1.Replace(" ", "");
            decimal answer = Day18.LineSolver(line, true);
            Assert.Equal(51M, answer);
        }

        [Fact]
        public void TestShouldEqual26()
        {
            string line = testCase2.Replace(" ", "");
            decimal answer = Day18.LineSolver(line, true);
            Assert.Equal(26M, answer);
        }

        [Fact]
        public void TestShouldEqual437()
        {
            string line = testCase3.Replace(" ", "");
            decimal answer = Day18.LineSolver(line, true);
            Assert.Equal(437M, answer);
        }

        [Fact]
        public void TestShouldEqual12240()
        {
            string line = testCase4.Replace(" ", "");
            decimal answer = Day18.LineSolver(line, true);
            Assert.Equal(12240M, answer);
        }

        [Fact]
        public void TestShouldEqual13632()
        {
            string line = testCase5.Replace(" ", "");
            decimal answer = Day18.LineSolver(line, true);
            Assert.Equal(13632M, answer);
        }
    }
}
