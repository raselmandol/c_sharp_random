//also as form1,starting_form
namespace Device_Informations
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        //condition for that Keyboard SHK(KeyDown)
        //start
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DetailsForm detailsForm = new DetailsForm();
                detailsForm.Show();
            }
        }
        //end
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void AboutMe_Click(object sender, EventArgs e)
        {
            DetailsForm f2= new DetailsForm();
            f2.Show();
            Visible= true;
        }

        private void MainForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.A)
            {
                AboutMe.PerformClick();
            }
        }

        //data control view start









 

        //data control view end

        private void MainForm_DockChanged(object sender, EventArgs e)
        {

        }

        private void mainheader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AboutMe_Click_1(object sender, EventArgs e)
        {
            DetailsForm view= new DetailsForm();
            view.Show();
            Visible= true;
        }
    }
}
