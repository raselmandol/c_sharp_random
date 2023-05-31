using System;
using System.ServiceProcess;

class Program
{
    static void Main()
    {
        string serviceName = "YourServiceName";
        if (ServiceExists(serviceName))
        {
            ServiceController service = new ServiceController(serviceName);
            if (service.Status != ServiceControllerStatus.Running)
            {
                try
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running);
                    Console.WriteLine("Service started successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error starting service: {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Service is already running.");
            }
        }
        else
        {
            Console.WriteLine("Service does not exist.");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    static bool ServiceExists(string serviceName)
    {
        ServiceController[] services = ServiceController.GetServices();
        foreach (ServiceController service in services)
        {
            if (service.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
