using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Sudoku.Test
{
    [TestClass]
    public class AvailabilityTests
    {
        private Board Board;
        private const string BoardString = "x,x,x,3,4,5,x,2,x\r\n3,4,9,7,x,6,x,x,1\r\n2,x,6,x,x,1,4,7,x\r\n1,x,2,x,7,9,x,8,5\r\n5,9,x,x,x,x,x,x,x\r\nx,6,x,x,x,x,2,1,9\r\n9,1,x,x,x,7,x,4,2\r\nx,2,7,9,1,3,x,6,x\r\nx,x,x,x,x,x,x,x,x";

        [TestInitialize]
        public void Init()
        {
            Board = new Board(BoardString);
        }

        [TestMethod]
        public void AvailableRow()
        {
            var p = 5;

            var available = Board.Available(p, CollectionType.Row).ToArray();

            var expected = new[] { 1, 2, 3, 4, 6, 7, 8 };

            Assert.AreEqual(expected.Length, available.Length, "Length test");

            foreach(var e in expected)
            {
                Assert.IsTrue(available.Contains(e), "Available contains " + e);
            }
        }

        [TestMethod]
        public void AvailableColumn()
        {
            var p = 7;

            var available = Board.Available(p, CollectionType.Column).ToArray();

            var expected = new[] { 1, 3, 5, 6, 7, 8, 9};

            Assert.AreEqual(expected.Length, available.Length, "Length test");

            foreach (var e in expected)
            {
                Assert.IsTrue(available.Contains(e), "Available contains " + e);
            }
        }

        [TestMethod]
        public void AvailableSquare()
        {
            var p = 8;

            var available = Board.Available(p, CollectionType.Square).ToArray();

            var expected = new[] { 2, 4, 5, 6, 8 };

            Assert.AreEqual(expected.Length, available.Length, "Length test");

            foreach (var e in expected)
            {
                Assert.IsTrue(available.Contains(e), "Available contains " + e);
            }
        }

        [TestMethod]
        public void AvailableGrid()
        {
            int c = 5, r = 7;

            var expected = new[] { 5, 6, 8 };

            var available = Board.Available(r, c).ToArray();

            Assert.AreEqual(expected.Length, available.Length, "Length test");

            foreach (var e in expected)
            {
                Assert.IsTrue(available.Contains(e), "Available contains " + e);
            }
        }
    }
}
