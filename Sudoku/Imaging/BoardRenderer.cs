using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Sudoku.Imaging
{
    public class BoardRenderer
    {
        public Board Board { get; set; } = null;

        public Color BackColor { get; set; } = Color.White;
        public Color ForeColor { get; set; } = Color.Black;
        public Color SecondaryColor { get; set; } = Color.DarkBlue;
        public Font NoteFont { get; set; } = new Font("Tahoma", 10, FontStyle.Regular);
        public Font PrimaryFont { get; set; } = new Font("Tahoma", 15, FontStyle.Regular);
        public int ImageSize { get; set; } = 470;
        public bool IncludeAxisNumbers { get; set; } = true;
        public int BoldLineThickness { get; set; } = 3;
        public int LineThickness { get; set; } = 1;

        public Bitmap Draw()
        {
            #region Math and Styling decesions
            int board_totalSquares = Board?.Size ?? 9;
            int board_innerSquares = Board?.SquareSize ?? 3;

            int squareSize = ImageSize / (IncludeAxisNumbers ? board_totalSquares + 1 : board_totalSquares);
            int innerSquareSize = squareSize / board_innerSquares;

            Brush backBrush = new SolidBrush(BackColor),
                  foreBrush = new SolidBrush(ForeColor),
                  secondaryBrush = new SolidBrush(SecondaryColor);

            Pen borderPen = new Pen(ForeColor, BoldLineThickness),
                thinLinePen = new Pen(ForeColor, LineThickness);

            var format = StringFormat.GenericDefault;
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            #endregion

            var bmp = new Bitmap(ImageSize, ImageSize);

            using (var gfx = Graphics.FromImage(bmp))
            {
                #region Image readying
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.SmoothingMode = SmoothingMode.HighQuality;

                gfx.Clear(BackColor);
                #endregion

                int x = 1, 
                    y = 1, 
                    w = ImageSize - 1, 
                    h = ImageSize - 1;


                #region Draw Axis numbers, border and grid
                if (IncludeAxisNumbers)
                {
                    //Draw X and Y axis numbers
                    for (var i = 0; i < board_totalSquares; i++)
                    {
                        var xRect = new Rectangle(squareSize + (squareSize * i), 0, squareSize, squareSize);
                        gfx.DrawString((i + 1).ToString(), PrimaryFont, foreBrush, xRect, format);

                        var yRect = new Rectangle(0, squareSize + (squareSize * i), squareSize, squareSize);
                        gfx.DrawString((i + 1).ToString(), PrimaryFont, foreBrush, yRect, format);
                    }

                    //Modify X and Y positions for the board.
                    x = squareSize; y = squareSize;
                    w = ImageSize - squareSize; h = ImageSize - squareSize;
                }

                var borderRect = new Rectangle(x, y, w - BoldLineThickness, h - BoldLineThickness);
                gfx.DrawRectangle(borderPen, borderRect);

                //Draw thicker board lines
                for(var i = 1; i < board_innerSquares; i++)
                {
                    var bx = x + (i * (squareSize * board_innerSquares));
                    gfx.DrawLine(borderPen, bx, y, bx, ImageSize - BoldLineThickness);

                    var by = y + (i * (squareSize * board_innerSquares));
                    gfx.DrawLine(borderPen, x, by, ImageSize - BoldLineThickness, by);
                }

                //Draw thinner square lines
                for(var i = 1; i < board_totalSquares; i++)
                {
                    var bx = x + (i * squareSize);
                    gfx.DrawLine(thinLinePen, bx, y, bx, ImageSize - BoldLineThickness);

                    var by = y + (i * squareSize);
                    gfx.DrawLine(thinLinePen, x, by, ImageSize - BoldLineThickness, by);
                }
                #endregion

                #region Draw Numbers

                if (Board == null)
                    return bmp;

                for(var r = 0; r < board_totalSquares; r++)
                {
                    for(var c = 0; c < board_totalSquares; c++)
                    {
                        var s = Board[r + 1, c + 1];

                        if (s.Number == null && (s.Notes == null || s.Notes.Count == 0))
                            continue;

                        int square_x = x + (c * squareSize),
                            square_y = y + (r * squareSize);

                        var rect = new Rectangle(square_x, square_y, squareSize, squareSize);

                        if (s.Number != null)
                        {
                            var brush = s.ReadOnly ? foreBrush : secondaryBrush;
                            gfx.DrawString(s.Number.ToString(), PrimaryFont, brush, rect, format);
                            continue;
                        }
                        
                        for(int i = 0, ir = 0, ic = 0; i < board_totalSquares; i++, ic++)
                        {
                            if (ic >= board_innerSquares)
                            {
                                ir++;
                                ic = 0;
                            }

                            if (!s.Notes.Contains(i + 1))
                                continue;

                            int ix = square_x + (ic * innerSquareSize),
                                iy = square_y + (ir * innerSquareSize);

                            var irect = new Rectangle(ix, iy, innerSquareSize, innerSquareSize);
                            gfx.DrawString((i + 1).ToString(), NoteFont, secondaryBrush, irect, format);
                        }
                    }
                }

                #endregion
            }

            return bmp;
        }
    }
}
