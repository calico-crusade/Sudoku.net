using System;
using System.Linq;

namespace Sudoku.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(@"x,9,x,x,x,3,x,5,x
2,x,5,4,x,8,x,6,7
x,1,x,6,5,x,3,x,x
x,x,1,9,7,6,x,x,x
x,x,x,x,x,x,x,x,x
x,x,x,1,8,4,5,x,x
x,x,3,x,4,9,x,7,x
7,4,x,5,x,1,8,x,2
x,6,x,8,x,x,x,4,x");

            AttemptToSolve(board);

            //DisplayAllSquares(board);

            //Solver.Solve(board);

            //Console.WriteLine(board.ToString());
            //Console.Write("Point: ");
            //while (true)
            //{
                
            //    var points = Console.ReadLine();

            //    Console.Clear();

            //    if (string.IsNullOrEmpty(points) || !points.Contains(","))
            //        return;

            //    var x = int.Parse(points.Split(',')[0].Trim());
            //    var y = int.Parse(points.Split(',')[1].Trim());

            //    var available = board.Available(x, y);
            //    Console.WriteLine(string.Join(", ", available));
            //    Console.WriteLine(board.ToString());
            //    Console.Write("Point: ");
            //}
        }

        private static void DisplayAllSquares(Board board)
        {
            Console.WriteLine(board.ToString());

            for(var i = 1; i <= board.Size; i++)
            {
                Console.WriteLine($"{i}: {string.Join(", ", board[i, CollectionType.Column].Select(t => t.Number?.ToString() ?? "x"))}");
            }
        }

        private static void AttemptToSolve(Board board)
        {
            Solver.Solve(board);
            Console.WriteLine(board.ToString());
        }
    }
}
