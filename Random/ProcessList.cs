using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        DisplayTopProcessesByRAM(5);
    }

    static void DisplayTopProcessesByRAM(int count)
    {
        Process[] processes = Process.GetProcesses();
        Array.Sort(processes, (x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));
        Console.WriteLine("Top Processes by RAM Usage:");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Process Name\tCPU Usage\tRAM Usage\tPID");
        Console.WriteLine("-------------------------------------------");

        for (int i = 0; i < Math.Min(processes.Length, count); i++)
        {
            Process process = processes[i];

            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
            float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
            float ramUsage = ramCounter.NextValue() / (1024 * 1024);

            Console.WriteLine($"{process.ProcessName}\t{cpuUsage.ToString("0.00")}%\t\t{ramUsage.ToString("0.00")} MB\t\t{process.Id}");
        }

        Console.WriteLine("-------------------------------------------");
    }
}
