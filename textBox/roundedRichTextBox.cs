using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedRichTextBox : RichTextBox
{
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        //Drawing rounded border
        using (GraphicsPath path = CreateRoundPath(ClientRectangle, 10))
        using (Pen borderPen = new Pen(Color.Black))
        {
            e.Graphics.DrawPath(borderPen, path);
        }
    }

    private GraphicsPath CreateRoundPath(Rectangle rectangle, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        //Top left arc
        path.AddArc(rectangle.Left, rectangle.Top, diameter, diameter, 180, 90);
        path.AddLine(rectangle.Left + radius, rectangle.Top, rectangle.Right - radius, rectangle.Top);

        //Top right arc
        path.AddArc(rectangle.Right - diameter, rectangle.Top, diameter, diameter, 270, 90);
        path.AddLine(rectangle.Right, rectangle.Top + radius, rectangle.Right, rectangle.Bottom - radius);

        //Bottom right arc
        path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddLine(rectangle.Right - radius, rectangle.Bottom, rectangle.Left + radius, rectangle.Bottom);

        //Bottom left arc
        path.AddArc(rectangle.Left, rectangle.Bottom - diameter, diameter, diameter, 90, 90);
        path.AddLine(rectangle.Left, rectangle.Bottom - radius, rectangle.Left, rectangle.Top + radius);

        path.CloseFigure();
        return path;
    }
}
