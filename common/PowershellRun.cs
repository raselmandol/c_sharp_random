using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        //Prompt the user for the command to execute
        Console.WriteLine("Enter the command to execute:");
        string command = Console.ReadLine();

        //Prompt the user to choose the shell (cmd or powershell)
        Console.WriteLine("Select the shell (cmd or powershell):");
        string shell = Console.ReadLine();

        //Execute the command based on the chosen shell
        if (shell.Equals("cmd", StringComparison.OrdinalIgnoreCase))
        {
            ExecuteCommandWithCmd(command);
        }
        else if (shell.Equals("powershell", StringComparison.OrdinalIgnoreCase))
        {
            ExecuteCommandWithPowerShell(command);
        }
        else
        {
            Console.WriteLine("Invalid shell selection.");
        }

        Console.WriteLine("Command execution complete.");
    }

    static void ExecuteCommandWithCmd(string command)
    {
        ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe")
        {
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = Process.Start(processInfo);
        process.StandardInput.WriteLine(command);
        process.StandardInput.Flush();
        process.StandardInput.Close();
        process.WaitForExit();
    }

    static void ExecuteCommandWithPowerShell(string command)
    {
        ProcessStartInfo processInfo = new ProcessStartInfo("powershell.exe")
        {
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = Process.Start(processInfo);
        process.StandardInput.WriteLine(command);
        process.StandardInput.Flush();
        process.StandardInput.Close();
        process.WaitForExit();
    }
}
