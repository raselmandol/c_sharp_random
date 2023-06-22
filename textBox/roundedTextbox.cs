using System;
using System.Drawing;
using System.Windows.Forms;

public class RoundedTextBox : TextBox
{
    private const int WM_PAINT = 0x000F;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (Pen borderPen = new Pen(Color.Black))
        {
            e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
        }
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_PAINT)
        {
            using (Graphics graphics = CreateGraphics())
            using (SolidBrush backgroundBrush = new SolidBrush(BackColor))
            {
                graphics.FillRectangle(backgroundBrush, ClientRectangle);
            }
        }
    }
}

