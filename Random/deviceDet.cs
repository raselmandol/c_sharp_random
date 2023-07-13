//infos
using System;
using System.Management;

namespace DeviceDetailsExample
{
    class Program
    {
        static void Main()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                string manufacturer = obj["Manufacturer"].ToString();
                string model = obj["Model"].ToString();
                string name = obj["Name"].ToString();
                string totalPhysicalMemory = obj["TotalPhysicalMemory"].ToString();
                string systemType = obj["SystemType"].ToString();

                Console.WriteLine($"Manufacturer: {manufacturer}");
                Console.WriteLine($"Model: {model}");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Total Physical Memory: {totalPhysicalMemory} bytes");
                Console.WriteLine($"System Type: {systemType}");
            }
        }
    }
}
