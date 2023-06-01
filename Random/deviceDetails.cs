using System;
using System.Management;

class DeviceDetailsPrinter
{
    static void Main()
    {
        string deviceName = Environment.MachineName;
        Console.WriteLine("Device Name: " + deviceName);

        // Print activation status
        string activationStatus = GetWindowsActivationStatus();
        Console.WriteLine("Activation Status: " + activationStatus);

        var driveInfo = new DriveInfo("C");
        long totalStorage = driveInfo.TotalSize;
        long freeStorage = driveInfo.TotalFreeSpace;
        Console.WriteLine("Total Storage: " + BytesToGB(totalStorage) + " GB");
        Console.WriteLine("Free Storage: " + BytesToGB(freeStorage) + " GB");

        long totalRAM = GetTotalRAM();
        Console.WriteLine("Total RAM: " + BytesToGB(totalRAM) + " GB");

        string processorInfo = GetProcessorInfo();
        Console.WriteLine("Processor: " + processorInfo);
    }

    static string GetWindowsActivationStatus()
    {
        using (var wmi = new ManagementObjectSearcher("SELECT * FROM SoftwareLicensingService"))
        {
            foreach (var obj in wmi.Get())
            {
                var objProperties = obj.Properties;
                foreach (var prop in objProperties)
                {
                    if (prop.Name == "OA3xOriginalProductKey")
                    {
                        return prop.Value.ToString();
                    }
                }
            }
        }
        return "Unknown";
    }

    static long GetTotalRAM()
    {
        using (var wmi = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
        {
            foreach (var obj in wmi.Get())
            {
                var objProperties = obj.Properties;
                foreach (var prop in objProperties)
                {
                    if (prop.Name == "TotalPhysicalMemory")
                    {
                        return Convert.ToInt64(prop.Value);
                    }
                }
            }
        }
        return 0;
    }

    static string GetProcessorInfo()
    {
        using (var wmi = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
        {
            foreach (var obj in wmi.Get())
            {
                var objProperties = obj.Properties;
                foreach (var prop in objProperties)
                {
                    if (prop.Name == "Name")
                    {
                        return prop.Value.ToString();
                    }
                }
            }
        }
        return "Unknown";
    }

    static double BytesToGB(long bytes)
    {
        return Math.Round(bytes / (1024.0 * 1024.0 * 1024.0), 2);
    }
}
