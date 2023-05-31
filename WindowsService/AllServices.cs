using System;
using System.ServiceProcess;

class Program
{
    static void Main()
    {
        ServiceController[] services = ServiceController.GetServices();
        foreach (ServiceController service in services)
        {
            Console.WriteLine("Service Name: {0}", service.ServiceName);
            Console.WriteLine("Display Name: {0}", service.DisplayName);
            Console.WriteLine("Status: {0}", service.Status);
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
