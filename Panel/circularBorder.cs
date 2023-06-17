public partial class MyForm : Form
{
    public MyForm()
    {
        InitializeComponent();

        button1.FlatStyle = FlatStyle.Flat;
        button1.FlatAppearance.BorderSize = 0;
        button1.BackColor = Color.Transparent;
        button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
        button1.FlatAppearance.MouseOverBackColor = Color.Transparent;

        button1.Size = new Size(50, 50);
        button1.Region = new Region(new GraphicsPath(new Ellipse(new Rectangle(0, 0, button1.Width, button1.Height)), new Rectangle(0, 0, button1.Width, button1.Height)));
        button1.Text = "Button";
        button1.TextAlign = ContentAlignment.MiddleCenter;
    }
}
