using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku.Test
{
    [TestClass]
    public class SolutionTests
    {
        [TestMethod]
        public void Incomplete()
        {
            var b = "x,x,x,!3,!4,!5,x:1-2-3,!2,x\r\n!3,!4,!9,!7,x,!6,x,x,!1\r\n!2,x,!6,x,x,!1,!4,!7,x\r\n!1,x,!2,x,!7,!9,x,!8,!5\r\n!5,!9,x,x,x,x,x,x,x\r\nx,!6,x,x,x,x,!2,!1,!9\r\n!9,!1,x,x,x,!7,x,!4,!2\r\nx,!2,!7,!9,!1,!3,x,!6,x\r\nx,x,x,x,x,x,x,x,x";
            var board = new Board(b);

            Assert.IsFalse(board.Solved());
        }

        [TestMethod]
        public void Complete()
        {
            var b = "8,7,1,3,4,5,9,2,6\r\n3,4,9,7,2,6,8,5,1\r\n2,5,6,8,9,1,4,7,3\r\n1,3,2,4,7,9,6,8,5\r\n5,9,8,1,6,2,7,3,4\r\n7,6,4,5,3,8,2,1,9\r\n9,1,5,6,8,7,3,4,2\r\n4,2,7,9,1,3,5,6,8\r\n6,8,3,2,5,4,1,9,7";
            var board = new Board(b);
            Assert.IsTrue(board.Solved());
        }

        [TestMethod]
        public void Error()
        {
            var b = "8,7,1,3,4,5,9,2,6\r\n3,4,9,7,2,6,8,5,1\r\n2,5,6,8,9,1,4,7,3\r\n1,3,2,4,7,9,6,8,8\r\n5,9,8,1,6,2,7,3,4\r\n7,6,4,5,3,8,2,1,9\r\n9,1,5,6,8,7,3,4,2\r\n4,2,7,9,1,3,5,6,8\r\n6,8,3,2,5,4,1,9,7";
            var board = new Board(b);
            Assert.IsFalse(board.Solved());
        }
    }
}
