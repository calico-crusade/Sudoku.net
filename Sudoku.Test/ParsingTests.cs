using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace Sudoku.Test
{
    [TestClass]
    public class ParsingTests
    {
        private Board Board;
        private const string BoardString = "x,x,x,!3,!4,!5,x:1-2-3,!2,x\r\n!3,!4,!9,!7,x,!6,x,x,!1\r\n!2,x,!6,x,x,!1,!4,!7,x\r\n!1,x,!2,x,!7,!9,x,!8,!5\r\n!5,!9,x,x,x,x,x,x,x\r\nx,!6,x,x,x,x,!2,!1,!9\r\n!9,!1,x,x,x,!7,x,!4,!2\r\nx,!2,!7,!9,!1,!3,x,!6,x\r\nx,x,x,x,x,x,x,x,x";

        [TestInitialize]
        public void Init()
        {
            Board = new Board(BoardString);
        }

        [TestMethod]
        public void TestParse()
        {
            Assert.IsNotNull(Board, "Board null test");

            var lines = BoardString.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (var r = 0; r < lines.Length; r++)
            {
                var cols = lines[r].Split(',');
                Assert.AreEqual(lines.Length, cols.Length, "Testing column length.");

                for (var c = 0; c < cols.Length; c++)
                {
                    var ch = cols[c].Replace("!", "").Split(':')[0];

                    int x = r + 1,
                        y = c + 1;

                    var s = Board[x, y];

                    if (ch == "x")
                        Assert.IsNull(s.Number, $"{x},{y} null test");
                    else
                        Assert.AreEqual(int.Parse(ch), s.Number, $"{x},{y} number test @ {ch}");
                }
            }
        }

        [TestMethod]
        public void GetRow()
        {
            var p = 3;

            var expected = new int?[] { 2, null, 6, null, null, 1, 4, 7, null };
            var row = Board[p, CollectionType.Row].ToArray();

            Assert.AreEqual(9, row.Length, "Row length test");
            Assert.AreEqual(expected.Length, row.Length, "Collection lengths");


            for (var i = 0; i < 9; i++)
            {
                var e = expected[i];
                var a = row[i];

                if (!e.HasValue)
                {
                    Assert.IsNull(a.Number, "Null position test: " + i);
                    continue;
                }

                Assert.AreEqual(e.Value, a.Number, $"Position test: {i}");
            }
        }

        [TestMethod]
        public void GetColumn()
        {
            var p = 1;

            var expected = new int?[] { null, 3, 2, 1, 5, null, 9, null, null };
            var col = Board[p, CollectionType.Column].ToArray();

            Assert.AreEqual(9, col.Length, "Col length test");
            Assert.AreEqual(expected.Length, col.Length, "Collection lengths");


            for (var i = 0; i < 9; i++)
            {
                var e = expected[i];
                var a = col[i];

                if (!e.HasValue)
                {
                    Assert.IsNull(a.Number, "Null position test: " + i);
                    continue;
                }

                Assert.AreEqual(e.Value, a.Number, $"Position test: {i}");
            }
        }

        [TestMethod]
        public void GetSquare()
        {
            var p = 9;

            var expected = new int?[] { null, 4, 2, null, 6, null, null, null, null };
            var square = Board[p, CollectionType.Square].ToArray();

            Assert.AreEqual(9, square.Length, "Square length test");
            Assert.AreEqual(expected.Length, square.Length, "Collection lengths");


            for (var i = 0; i < 9; i++)
            {
                var e = expected[i];
                var a = square[i];

                if (!e.HasValue)
                {
                    Assert.IsNull(a.Number, "Null position test: " + i);
                    continue;
                }

                Assert.AreEqual(e.Value, a.Number, $"Position test: {i}");
            }
        }

        [TestMethod]
        public void TestFenCreation()
        {
            var fen = Board.CreateFen();
            Assert.IsNotNull(fen);
            Assert.AreEqual(BoardString, fen, "Fen string");
        }
    }
}
