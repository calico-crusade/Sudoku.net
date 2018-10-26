using System.Collections.Generic;

namespace Sudoku
{
    public class Square
    {
        public int? Number { get; set; }
        public bool ReadOnly { get; set; }
        public List<int> Notes { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
