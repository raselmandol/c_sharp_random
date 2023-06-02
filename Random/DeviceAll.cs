using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;

class Program
{
    static void Main()
    {
        GetInstalledRAM();
        GetRAMUsage();
        GetHardDiskStorages();
        GetProcessorInfo();

        Console.ReadLine();
    }

    static void GetInstalledRAM()
    {
        using (var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory"))
        {
            long totalMemory = 0;
            foreach (var managementObject in searcher.Get())
            {
                totalMemory += long.Parse(managementObject["Capacity"].ToString());
            }

            var totalMemoryGB = totalMemory / 1024 / 1024 / 1024.0;
            Console.WriteLine($"Installed RAM: {totalMemoryGB} GB");
        }
    }

    static void GetRAMUsage()
    {
        var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        var totalRam = new PerformanceCounter("Memory", "Total MBytes");
        var usedRam = totalRam.NextValue() - ramCounter.NextValue();
        var freeRam = ramCounter.NextValue();

        Console.WriteLine($"RAM Usage: {usedRam} MB");
        Console.WriteLine($"Free RAM: {freeRam} MB");

        ramCounter.Dispose();
        totalRam.Dispose();
    }

    static void GetHardDiskStorages()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        Console.WriteLine("Hard Disk Storages:");

        foreach (DriveInfo drive in drives)
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"Drive: {drive.Name}");
                Console.WriteLine($"Total Size: {FormatSize(drive.TotalSize)}");
                Console.WriteLine($"Used Space: {FormatSize(drive.TotalSize - drive.AvailableFreeSpace)}");
                Console.WriteLine($"Free Space: {FormatSize(drive.AvailableFreeSpace)}");
                Console.WriteLine();
            }
        }
    }

    static string FormatSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }

        return $"{len:0.##} {sizes[order]}";
    }

    static void GetProcessorInfo()
    {
        var processor = new ManagementObjectSearcher("SELECT Name, LoadPercentage FROM Win32_Processor")
                            .Get()
                            .Cast<ManagementObject>()
                            .FirstOrDefault();

        if (processor != null)
        {
            var processorName = processor["Name"].ToString();
            var loadPercentage = processor["LoadPercentage"].ToString();

            Console.WriteLine($"Processor Name: {processorName}");
            Console.WriteLine($"Processor Load: {loadPercentage}%");

            Console.WriteLine("\nRunning Processes:");
            foreach (var process in Process.GetProcesses())
            {
                Console.WriteLine($"{process.ProcessName} - CPU Usage: {process.TotalProcessorTime}");
            }
        }
    }
}
