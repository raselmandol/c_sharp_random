using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ChangeProgressBarColor
{
    public partial class Form1 : Form
    {
        private PerformanceCounter ramCounter;
        private const float MaxRamValue = 100f;

        public Form1()
        {
            InitializeComponent();
            InitializeRamCounter();
            StartRamUsageUpdate();
        }

        private void InitializeRamCounter()
        {
            ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }

        private void StartRamUsageUpdate()
        {
            timerUpdateRamUsage.Interval = 1000; // Update every second
            timerUpdateRamUsage.Tick += TimerUpdateRamUsage_Tick;
            timerUpdateRamUsage.Start();
        }

        private void TimerUpdateRamUsage_Tick(object sender, EventArgs e)
        {
            float ramUsage = ramCounter.NextValue();
            progressBarRamUsage.Value = (int)ramUsage;
            UpdateProgressBarColor(ramUsage);
        }

        private void UpdateProgressBarColor(float ramUsage)
        {
            Color color;
            if (ramUsage <= 50f)
                color = Color.Green;
            else if (ramUsage <= 80f)
                color = Color.Yellow;
            else
                color = Color.Red;

            progressBarRamUsage.ForeColor = color;
        }
    }
}
