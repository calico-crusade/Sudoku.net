using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Board
    {
        private const string READONLY_CHAR = "!";
        private const string NULL_CHAR = "x";
        private const string NOTES_CHAR = ":";
        private const string NOTES_SPLIT_CHAR = "-";
        private const string COLUMN_CHAR = ",";
        private const string ROW_CHAR = "\r\n";

        #region Properties
        public int Size { get; set; } = 9;

        public int SquareSize { get; set; }

        public Square[,] Squares { get; set; }
        #endregion

        #region Indexors
        public Square this[int x, int y]
        {
            get
            {
                if (x < 1 || x > Size)
                    throw new ArgumentOutOfRangeException("X", "X is out of range of the size");

                if (y < 1 || y > Size)
                    throw new ArgumentOutOfRangeException("Y", "Y is  out of range of the size");

                return Squares[y - 1, x - 1];
            }
        }

        public IEnumerable<Square> this[int position, CollectionType type]
        {
            get
            {
                if (position < 1 || position > Size)
                    throw new ArgumentOutOfRangeException("position", $"Position can only be between 1 and {Size}");

                var p = position - 1;

                if (type == CollectionType.Square)
                {
                    var x = p / SquareSize * SquareSize;
                    var y = p % SquareSize * SquareSize;

                    for (var r = 0; r < SquareSize; r++)
                    {
                        for (var c = 0; c < SquareSize; c++)
                        {
                            yield return Squares[y + c, x + r];
                        }
                    }

                    yield break;
                }

                for(var i = 0; i < Size; i++)
                {
                    var col = type == CollectionType.Column ? p : i;
                    var row = type == CollectionType.Column ? i : p;

                    yield return Squares[col, row];
                }
            }
        }
        #endregion

        #region ctor
        public Board() : this(9) { }

        public Board(int size)
        {
            Size = size;
            SquareSize = (int)Math.Round(Math.Sqrt(Size));

            Validate();
        }

        public Board(string fen)
        {
            ParseFen(fen);
        }
        #endregion

        #region Public Methods
        public IEnumerable<int> Available(int position, CollectionType type)
        {
            var ss = this[position, type]
                .Where(t => t.Number.HasValue)
                .Select(t => t.Number)
                .ToArray();

            for (var i = 1; i <= Size; i++)
                if (!ss.Contains(i))
                    yield return i;
        }
        
        public IEnumerable<int> Available(int row, int col)
        {
            var square = SquareFromRowCol(row, col);

            var c = Available(col, CollectionType.Column).ToArray();
            var r = Available(row, CollectionType.Row).ToArray();
            var s = Available(square, CollectionType.Square).ToArray();

            return s.Intersect(c).Intersect(r);
        }

        public bool Solved(int position, CollectionType type)
        {
            return Available(position, type).Count() == 0;
        }
        
        public bool Solved()
        {
            for (var i = 1; i <= Size; i++)
            {
                if (!Solved(i, CollectionType.Column) ||
                    !Solved(i, CollectionType.Row) ||
                    !Solved(i, CollectionType.Square))
                    return false;
            }

            return true;
        }

        public int SquareFromRowCol(int row, int col)
        {
            var y = row - 1;
            var x = col - 1;

            var boxx = x / SquareSize;
            var innerx = x % SquareSize;

            var boxy = y / SquareSize;
            var innery = y % SquareSize;

            return (boxx + SquareSize * boxy) + 1;
        }

        public string CreateFen()
        {
            var bob = new StringBuilder();

            for(var r = 0; r < Size; r ++)
            {
                for(var c = 0; c < Size; c++)
                {
                    var s = this[r + 1, c + 1];

                    if (s.ReadOnly)
                        bob.Append(READONLY_CHAR);

                    bob.Append(s.Number?.ToString() ?? NULL_CHAR);

                    if (s.Notes != null && s.Notes.Count > 0)
                        bob.Append(NOTES_CHAR + string.Join(NOTES_SPLIT_CHAR, s.Notes));

                    if (c + 1 < Size)
                        bob.Append(COLUMN_CHAR);    
                }

                if (r + 1 < Size)
                    bob.Append(ROW_CHAR);
            }

            return bob.ToString();
        }
        #endregion

        #region Private Methods
        private void Validate()
        {
            var sqr = Math.Sqrt(Size);

            if (sqr % 1 != 0)
                throw new ArgumentOutOfRangeException("size", $"{Size} is not a square root, it can't be used as size.");
        }

        private void ParseFen(string fen)
        {
            var lines = fen.Split(new[] { ROW_CHAR }, StringSplitOptions.RemoveEmptyEntries);

            var size = lines.Length;

            Size = size;
            SquareSize = (int)Math.Round(Math.Sqrt(Size));
            Squares = new Square[size, size];

            Validate();

            for (var r = 0; r < size; r++)
            {
                var cols = lines[r].Split(new[] { COLUMN_CHAR }, StringSplitOptions.RemoveEmptyEntries);

                if (cols.Length != size)
                    throw new ArgumentException($"Invalid column line. Row #{r + 1} has invalid number of columns. It needs to be {size}, not {cols.Length}.");

                for (var c = 0; c < size; c++)
                {
                    var square = new Square { X = c + 1, Y = r + 1 };
                    var ch = cols[c];

                    if (ch.StartsWith(READONLY_CHAR))
                    {
                        square.ReadOnly = true;
                        ch = ch.Remove(0, READONLY_CHAR.Length);
                    }

                    var sp = ch.Split(new[] { NOTES_CHAR }, StringSplitOptions.RemoveEmptyEntries);

                    var ac = sp[0];

                    if (ac == NULL_CHAR)
                        square.Number = null;
                    else
                        square.Number = int.Parse(ac);

                    if (sp.Length > 1)
                        square.Notes = sp[1]
                            .Split(new[] { NOTES_SPLIT_CHAR }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => int.Parse(t))
                            .ToList();

                    Squares[c, r] = square;
                }
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            var board = "    ";

            for (var i = 0; i < Size; i++)
                board += $" {i + 1}  ";
            
            for(var r = 0; r < Size; r++)
            {
                board += "\r\n   +";

                for (var i = 0; i < Size; i++)
                    board += " - +";

                board += "\r\n " + (r + 1) + " |";

                for(var c = 0; c < Size; c++)
                {
                    var s = Squares[c, r];
                    board += $" {(s.Number?.ToString() ?? " ")} |";
                }
            }

            board += "\r\n   +";

            for (var i = 0; i < Size; i++)
                board += " - +";

            return board;
        }
        #endregion
    }
}
