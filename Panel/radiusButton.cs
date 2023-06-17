using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedButton : Button
{
    private int borderRadius = 10;

    protected override void OnPaint(PaintEventArgs e)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(new Rectangle(0, 0, Width, Height));
        Region = new Region(path);

        base.OnPaint(e);
    }
}
