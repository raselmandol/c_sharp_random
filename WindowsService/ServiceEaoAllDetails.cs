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
            Console.WriteLine("Start Type: {0}", service.StartType);
            Console.WriteLine("Can Pause and Continue: {0}", service.CanPauseAndContinue);
            Console.WriteLine("Can Shutdown: {0}", service.CanShutdown);
            Console.WriteLine("Can Stop: {0}", service.CanStop);
            Console.WriteLine("Machine Name: {0}", service.MachineName);
            Console.WriteLine();
            try
            {
                service.Refresh();
                Console.WriteLine("Service Type: {0}", service.ServiceType);
                Console.WriteLine("Description: {0}", service.Description);
                Console.WriteLine("Start Name: {0}", service.StartName);
                Console.WriteLine("Dependent Services: {0}", string.Join(", ", service.DependentServices));
                Console.WriteLine("Services Depended On: {0}", string.Join(", ", service.ServicesDependedOn));
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving additional details: {0}", ex.Message);
                Console.WriteLine();
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
