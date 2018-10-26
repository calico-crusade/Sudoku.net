using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Solver
    {
        private Board board;

        private Solver(Board board)
        {
            this.board = board;
        }

        public void Solve()
        {
            SolveForSingles();
        }

        public void Hint()
        {
            for(var r = 1; r <= board.Size; r++)
            {
                for(var c = 1; c <= board.Size; c++)
                {
                    var s = board[r, c];

                    if (s.ReadOnly || s.Number != null)
                        continue;

                    s.Notes = board.Available(r, c).ToList();
                }
            }
        }

        private void SolveForSingles()
        {
            for(var r = 1; r <= board.Size; r++)
            {
                for(var c = 1; c <= board.Size; c++)
                {
                    var s = board[r, c];

                    //Already has solutiona
                    if (s.Number != null)
                        continue;

                    var avail = board.Available(r, c).ToArray();

                    if (avail.Length == 1)
                    {
                        board[r, c].Number = avail[0];
                        SolveForSingles();
                        return;
                    }
                }
            }
        }

        public static void Solve(Board board)
        {
            var solver = new Solver(board);

            solver.Solve();
        }

        public static void Hint(Board board)
        {
            var solver = new Solver(board);

            solver.Hint();
        }
    }
}
