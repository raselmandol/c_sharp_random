using System.Drawing;
using System.Windows.Forms;

public class GradientBackground
{
    private Color _startColor;
    private Color _endColor;

    public GradientBackground(Color startColor, Color endColor)
    {
        _startColor = startColor;
        _endColor = endColor;
    }

    public void ApplyTo(Form form)
    {
        form.Paint += new PaintEventHandler((sender, e) => {
            Rectangle rect = new Rectangle(0, 0, form.Width, form.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, _startColor, _endColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        });
    }
}
