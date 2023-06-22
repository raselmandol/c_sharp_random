using System;
using System.Windows.Forms;

public class MainForm : Form
{
    private RoundedRichTextBox roundedRichTextBox;

    public MainForm()
    {
        //Creating the rounded rich text box
        roundedRichTextBox = new RoundedRichTextBox();
        roundedRichTextBox.Location = new Point(50, 50);
        roundedRichTextBox.Size = new Size(200, 150);
        Controls.Add(roundedRichTextBox);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }
}
