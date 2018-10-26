using System.Drawing;

namespace Sudoku.Imaging
{
    public struct Textual
    {
        public Font Font { get; set; }
        public Color Color { get; set; }
        public Rectangle Placement { get; set; }
        public StringAlignment VerticalAlignment { get; set; }
        public StringAlignment HorizontalAlignment { get; set; }
        
        public void Draw(Graphics gfx, string text)
        {
            var f = StringFormat.GenericDefault;

            f.Alignment = HorizontalAlignment;
            f.LineAlignment = VerticalAlignment;

            gfx.DrawString(text, Font, new SolidBrush(Color), Placement, f);
        }

        public Textual MoveTo(int x, int y)
        {
            var t = Copy();
            t.Placement = new Rectangle(x, y, Placement.Width, Placement.Height);
            return t;
        }

        public Textual Copy()
        {
            return new Textual
            {
                Color = Color,
                Font = Font,
                HorizontalAlignment = HorizontalAlignment,
                Placement = Placement,
                VerticalAlignment = VerticalAlignment
            };
        }

        public static Textual Center(Font font, Color color, Rectangle rectangle)
        {
            return new Textual
            {
                Font = font,
                Color = color,
                Placement = rectangle,
                VerticalAlignment = StringAlignment.Center,
                HorizontalAlignment = StringAlignment.Center
            };
        }
    }
}
