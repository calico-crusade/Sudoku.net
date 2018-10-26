using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.UI
{
    using Imaging;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var board = new Board(@"x,!9,x,x,x,!3,x,!5,x
!2,x,!5,!4,x,!8,x,!6,!7
x,!1,x,!6,!5,x,!3,x,x
x,x,!1,!9,!7,!6,x,x,x
x,x,x,x,x,x,x,x,x
x,x,x,!1,!8,!4,!5,x,x
x,x,!3,x,!4,!9,x,!7,x
!7,!4,x,!5,x,!1,!8,x,!2
x,!6,x,!8,x,x,x,!4,x");

            Solver.Solve(board);
            Solver.Hint(board);

            var renderer = new BoardRenderer();
            renderer.Board = board;
            pictureBox1.Image = renderer.Draw();
        }
    }
}
