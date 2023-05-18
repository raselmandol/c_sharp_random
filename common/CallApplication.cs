using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    static void Main()
    {
        string appPath = "C:\\path\\to\\yourApplication.exe";
        Process process = Process.Start(appPath);
        ShowWindow(process.MainWindowHandle, SW_HIDE);
        Console.WriteLine("press any key to exit...");
        Console.ReadKey();
        ShowWindow(process.MainWindowHandle, SW_SHOW);
        process.CloseMainWindow();
    }
}
