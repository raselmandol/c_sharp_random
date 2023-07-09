using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string command = "Your command here";
        RunCmdCommandInBackground(command);
    }

    static void RunCmdCommandInBackground(string command)
    {
        try
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.Arguments = $"/C \"{command}\"";
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
