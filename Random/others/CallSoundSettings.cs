using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OpenSoundSettings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpenSoundSettings_Click(object sender, EventArgs e)
        {
            Process.Start("mmsys.cpl"); // Open sound settings
        }
    }
}
