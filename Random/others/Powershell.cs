using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string command = "Your PowerShell command here";
        RunPowerShellCommandInBackground(command);
    }

    static void RunPowerShellCommandInBackground(string command)
    {
        try
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "powershell.exe";
            processInfo.Arguments = $"-Command \"{command}\"";
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();

            //Optionally, you can read the output and error streams
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
