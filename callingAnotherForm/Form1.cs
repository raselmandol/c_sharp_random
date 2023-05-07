namespace FunPop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fixNow_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                FormPop f2 = new FormPop();
                f2.StartPosition = FormStartPosition.Manual;
                f2.Location = new Point(50 * i, 50 * i); //set the location of the form
                f2.Show();
                Visible = false;
            }
          //  FormPop f2 = new FormPop();
        //    f2.Show();
            //Visible = false;
        }
    }
}
