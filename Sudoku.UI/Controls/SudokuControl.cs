using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.UI.Controls
{
    public class SudokuControl : Control
    {
        private Board board;

        public int BoardSize => board?.Size ?? 9;
        public int BoldBorderBox => board?.SquareSize ?? 3;

        public int SquareSize => BoardSize / Height;

        public SudokuControl(Board board)
        {
            this.board = board;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
        }
    }
}
