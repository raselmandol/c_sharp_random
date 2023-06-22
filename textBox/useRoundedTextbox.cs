using System;
using System.Windows.Forms;

public class MainForm : Form
{
    private RoundedTextBox roundedTextBox;

    public MainForm()
    {
        //Creating the rounded textbox
        roundedTextBox = new RoundedTextBox();
        roundedTextBox.Location = new Point(50, 50);
        roundedTextBox.Size = new Size(200, 30);
        Controls.Add(roundedTextBox);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }
}
