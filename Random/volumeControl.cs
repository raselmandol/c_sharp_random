using System;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace VolumeControlApp
{
    public partial class Form1 : Form
    {
        private MMDeviceEnumerator deviceEnumerator;
        private MMDevice defaultDevice;

        public Form1()
        {
            InitializeComponent();
            InitializeVolumeControl();
        }

        private void InitializeVolumeControl()
        {
            deviceEnumerator = new MMDeviceEnumerator();
            defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        private void buttonIncreaseVolume_Click(object sender, EventArgs e)
        {
            IncreaseVolume();
        }

        private void buttonDecreaseVolume_Click(object sender, EventArgs e)
        {
            DecreaseVolume();
        }

        private void IncreaseVolume()
        {
            defaultDevice.AudioEndpointVolume.VolumeStepUp();
        }

        private void DecreaseVolume()
        {
            defaultDevice.AudioEndpointVolume.VolumeStepDown();
        }
    }
}
