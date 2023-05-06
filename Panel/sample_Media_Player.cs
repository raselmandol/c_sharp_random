namespace Media_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }
        private void customizeDesign()
        {
            panelMedia.Visible = false;
            panelPlaylist.Visible = false;
            panelTools.Visible = false;
            panelHelp.Visible = false;
        }
        private void HideSubmenu()
        {
            if(panelMedia.Visible==true)
                panelMedia.Visible = false;
            if(panelPlaylist.Visible==true)
                panelPlaylist.Visible = false;
            if(panelTools.Visible==true)
                panelTools.Visible = false;
            if(panelHelp.Visible==true)
                panelHelp.Visible = false;

        }
        private void showSubmenu(Panel subMenu)
        {
            if(subMenu.Visible==false) {
                HideSubmenu();
                subMenu.Visible = true;
            }
            else
            {
              subMenu.Visible = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HideSubmenu();
        }

        private void buttonMedia_Click(object sender, EventArgs e)
        {
            showSubmenu(panelMedia);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubmenu(panelPlaylist);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            HideSubmenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HideSubmenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HideSubmenu();
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonTools_Click(object sender, EventArgs e)
        {
            showSubmenu(panelTools);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            showSubmenu(panelHelp);
        }
    }
}
