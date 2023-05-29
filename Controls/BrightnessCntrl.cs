using System;
using System.Windows.Forms;

namespace BrightnessControlApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void brightnessSetupButton_Click(object sender, EventArgs e)
        {
            BrightnessSetupForm brightnessForm = new BrightnessSetupForm();
            brightnessForm.Show();
        }
    }

    public partial class BrightnessSetupForm : Form
    {
        private TrackBar brightnessTrackBar;

        public BrightnessSetupForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            this.SuspendLayout();
            this.brightnessTrackBar.Location = new System.Drawing.Point(12, 12);
            this.brightnessTrackBar.Maximum = 100;
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.Size = new System.Drawing.Size(200, 45);
            this.brightnessTrackBar.TabIndex = 0;
            this.brightnessTrackBar.Scroll += new System.EventHandler(this.brightnessTrackBar_Scroll);
            this.ClientSize = new System.Drawing.Size(224, 73);
            this.Controls.Add(this.brightnessTrackBar);
            this.Name = "BrightnessSetupForm";
            this.Text = "Brightness Setup";
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void brightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            int brightness = brightnessTrackBar.Value;
        }
    }
}
