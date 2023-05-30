using System;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface networkInterface in interfaces)
        {
            if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                Console.WriteLine("Interface Name: " + networkInterface.Name);
                Console.WriteLine("Interface Description: " + networkInterface.Description);
                Console.WriteLine("Interface Status: " + networkInterface.OperationalStatus);

                //getting SSID
                if (networkInterface is Wireless80211NetworkInterface wirelessInterface)
                {
                    Console.WriteLine("SSID: " + wirelessInterface.Ssid);
                }

                //current signal strength
                if (networkInterface.GetIPStatistics() is IPInterfaceStatistics statistics)
                {
                    Console.WriteLine("Signal Strength: " + statistics.BytesReceived);
                }

                Console.WriteLine();
            }
        }

        Console.ReadLine();
    }
}
