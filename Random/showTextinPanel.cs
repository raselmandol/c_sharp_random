using System;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        Application.Run(new MainForm());
    }
}

class MainForm : Form
{
    private Panel panel1;
    private Panel panel2;

    public MainForm()
    {
        InitializeComponents();
        DisplayTextInPanel(panel1, "Text in Panel 1");
        DisplayTextInPanel(panel2, "Text in Panel 2");
    }

    private void InitializeComponents()
    {
        //creating panel 1
        panel1 = new Panel();
        panel1.Dock = DockStyle.Top;
        panel1.Height = 200;

        //creating panel 2
        panel2 = new Panel();
        panel2.Dock = DockStyle.Fill;

        //main form
        Text = "Text in Panels";
        Controls.Add(panel1);
        Controls.Add(panel2);
    }

    private void DisplayTextInPanel(Panel panel, string text)
    {
        //label for text
        Label textLabel = new Label();
        textLabel.Text = text;
        textLabel.AutoSize = true;
        textLabel.Location = new System.Drawing.Point(10, 10);

        //adding label to panel
        panel.Controls.Add(textLabel);
    }
}
