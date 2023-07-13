using System;
using System.Windows.Forms;

namespace RandomFormPlacements
{
    public partial class MainForm : Form
    {
        private const int NumberOfForms = 10; //Number of forms to open

        private Random random;

        public MainForm()
        {
            InitializeComponent();
            random = new Random();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < NumberOfForms; i++)
            {
                OpenRandomForm();
            }
        }

        private void OpenRandomForm()
        {
            Form randomForm = new Form();
            randomForm.StartPosition = FormStartPosition.Manual;

            //Set the form's position randomly within the screen bounds
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int formWidth = randomForm.Width;
            int formHeight = randomForm.Height;
            int randomX = random.Next(0, screenWidth - formWidth);
            int randomY = random.Next(0, screenHeight - formHeight);

            randomForm.Location = new System.Drawing.Point(randomX, randomY);
            randomForm.Text = "Random Form";

            randomForm.ShowDialog();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
