using System;

namespace KernelInformationExample
{
    class Program
    {
        static void Main()
        {
            string osVersion = Environment.OSVersion.VersionString;
            string osPlatform = Environment.OSVersion.Platform.ToString();
            string osArchitecture = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";

            Console.WriteLine($"OS Version: {osVersion}");
            Console.WriteLine($"Platform: {osPlatform}");
            Console.WriteLine($"Architecture: {osArchitecture}");
        }
    }
}
