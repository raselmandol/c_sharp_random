using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace FunPop
{
    public partial class FormPop : Form
    {
        System.Media.SoundPlayer player=new System.Media.SoundPlayer();
        public FormPop()
        {
            InitializeComponent();
            player.SoundLocation = "error.wav";
            player.Play();
        }

        private void FormPop_Load(object sender, EventArgs e)
        {

        }
    }
}
