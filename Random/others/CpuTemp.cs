using System;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace CPUTemperatureApp
{
    public partial class Form1 : Form
    {
        private Computer computer;

        public Form1()
        {
            InitializeComponent();
            InitializeComputer();
        }

        private void InitializeComputer()
        {
            computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;

            timerTemperature.Start();
        }

        private void timerTemperature_Tick(object sender, EventArgs e)
        {
            float cpuTemperature = GetCPUTemperature();
            labelTemperature.Text = $"CPU Temperature: {cpuTemperature}°C";
        }

        private float GetCPUTemperature()
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            Console.WriteLine($"Sensor Name: {sensor.Name}");
                        }
                    }
                }
            }

            return 0;
        }
    }
}
