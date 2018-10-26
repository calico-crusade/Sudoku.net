using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Test
{
    [TestClass]
    public class PositionTests
    {
        private Board Board;
        private const string BoardString = "x,x,x,!3,!4,!5,x:1-2-3,!2,x\r\n!3,!4,!9,!7,x,!6,x,x,!1\r\n!2,x,!6,x,x,!1,!4,!7,x\r\n!1,x,!2,x,!7,!9,x,!8,!5\r\n!5,!9,x,x,x,x,x,x,x\r\nx,!6,x,x,x,x,!2,!1,!9\r\n!9,!1,x,x,x,!7,x,!4,!2\r\nx,!2,!7,!9,!1,!3,x,!6,x\r\nx,x,x,x,x,x,x,x,x";

        [TestInitialize]
        public void Init()
        {
            Board = new Board(BoardString);
        }

        [TestMethod]
        public void ZoneTest()
        {
            var points = new []
            {
                //Zone 1
                new [] { 1, 1, 1 },
                new [] { 1, 1, 2 },
                new [] { 1, 1, 3 },
                new [] { 1, 2, 1 },
                new [] { 1, 2, 2 },
                new [] { 1, 2, 3 },
                new [] { 1, 3, 1 }, 
                new [] { 1, 3, 2 },
                new [] { 1, 3, 3 },

                //Zone 2
                new [] { 2, 4, 1 },
                new [] { 2, 4, 2 },
                new [] { 2, 4, 3 },
                new [] { 2, 5, 1 },
                new [] { 2, 5, 2 },
                new [] { 2, 5, 3 },
                new [] { 2, 6, 1 },
                new [] { 2, 6, 2 },
                new [] { 2, 6, 3 },
                
                //Zone 3
                new [] { 3, 7, 1 },
                new [] { 3, 7, 2 },
                new [] { 3, 7, 3 },
                new [] { 3, 8, 1 },
                new [] { 3, 8, 2 },
                new [] { 3, 8, 3 },
                new [] { 3, 9, 1 },
                new [] { 3, 9, 2 },
                new [] { 3, 9, 3 },

                //Zone 4
                new [] { 4, 1, 4 },
                new [] { 4, 1, 5 },
                new [] { 4, 1, 6 },
                new [] { 4, 2, 4 },
                new [] { 4, 2, 5 },
                new [] { 4, 2, 6 },
                new [] { 4, 3, 4 },
                new [] { 4, 3, 5 },
                new [] { 4, 3, 6 },

                //Zone 5
                new [] { 5, 4, 4 },
                new [] { 5, 4, 5 },
                new [] { 5, 4, 6 },
                new [] { 5, 5, 4 },
                new [] { 5, 5, 5 },
                new [] { 5, 5, 6 },
                new [] { 5, 6, 4 },
                new [] { 5, 6, 5 },
                new [] { 5, 6, 6 },

                //Zone 6
                new [] { 6, 7, 4 },
                new [] { 6, 7, 5 },
                new [] { 6, 7, 6 },
                new [] { 6, 8, 4 },
                new [] { 6, 8, 5 },
                new [] { 6, 8, 6 },
                new [] { 6, 9, 4 },
                new [] { 6, 9, 5 },
                new [] { 6, 9, 6 },

                //Zone 7
                new [] { 7, 1, 7 },
                new [] { 7, 1, 8 },
                new [] { 7, 1, 9 },
                new [] { 7, 2, 7 },
                new [] { 7, 2, 8 },
                new [] { 7, 2, 9 },
                new [] { 7, 3, 7 },
                new [] { 7, 3, 8 },
                new [] { 7, 3, 9 },

                //Zone 8
                new [] { 8, 4, 7 },
                new [] { 8, 4, 8 },
                new [] { 8, 4, 9 },
                new [] { 8, 5, 7 },
                new [] { 8, 5, 8 },
                new [] { 8, 5, 9 },
                new [] { 8, 6, 7 },
                new [] { 8, 6, 8 },
                new [] { 8, 6, 9 },

                //Zone 9
                new [] { 9, 7, 7 },
                new [] { 9, 7, 8 },
                new [] { 9, 7, 9 },
                new [] { 9, 8, 7 },
                new [] { 9, 8, 8 },
                new [] { 9, 8, 9 },
                new [] { 9, 9, 7 },
                new [] { 9, 9, 8 },
                new [] { 9, 9, 9 }
            };

            for(var i = 0; i < points.Length; i++)
            {
                var expectedZone = points[i][0];
                var c = points[i][1];
                var r = points[i][2];

                var actualZone = Board.SquareFromRowCol(r, c);

                Assert.AreEqual(expectedZone, actualZone, $"Expected Zone: {expectedZone}");
            }
        }
    }
}
