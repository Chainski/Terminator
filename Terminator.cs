// made by https://github.com/chainski
// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /unsafe /platform:x64 /preferreduilang:en-US /out:Terminator.exe /target:exe Terminator.cs
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

public class Terminator
{
    public static void Main(string[] args)
    {   
		string url = "https://github.com/chainski/terminator";

        string filePath = "github project.url";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("[InternetShortcut]");
            writer.WriteLine("URL=" + url);
        }

        ShowBanner();
        Console.Write("[-] Enter the Process ID to terminate: ");
        string input = Console.ReadLine();
        int pid;
        if (int.TryParse(input, out pid))
        {
            UnProtectAndTerminate(pid);
        }
        else
        {
			Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[!] Invalid Process ID.");
			Thread.Sleep(4000);
        }
    }

    public static void ShowBanner()
    {
	    Console.ForegroundColor = ConsoleColor.Red;
        string bannerText =
            "████████╗███████╗██████╗ ███╗   ███╗██╗███╗   ██╗ █████╗ ████████╗ ██████╗ ██████╗ \n" +
            "╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗\n" +
            "   ██║   █████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║███████║   ██║   ██║   ██║██████╔╝\n" +
            "   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██╔══██║   ██║   ██║   ██║██╔══██╗\n" +
            "   ██║   ███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██║  ██║   ██║   ╚██████╔╝██║  ██║\n" +
            "   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝\n" +
            "                                                                                   \n" +
   		    "                          [RtlSetProcessIsCritical Killer]                         \n" +
            "                                 [made by chainski]                                \n" +
			"                                   [version 1.0]                                   \n" +
            "                          [github.com/chainski/terminator]                         \n";

        // Calculate padding for centering
        int padding = (Console.WindowWidth - bannerText.Split('\n')[0].Length) / 2;

        // Print each line of the banner with padding
        foreach (string line in bannerText.Split('\n'))
        {
            Console.WriteLine(line.PadLeft(padding + line.Length));
        }

        Console.ResetColor();
    }

    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

    public static void UnProtectAndTerminate(int pid)
    {
        try
        {
            Process.EnterDebugMode();
            int isCritical = 0;
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid); // PROCESS_ALL_ACCESS
            NtSetInformationProcess(hProcess, 0x1D, ref isCritical, sizeof(int)); // 0x1D = ProcessBreakOnTermination
            if (TerminateProcess(hProcess, 0)) // Terminate the process with exit code 0
            {
				Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] The process was terminated successfully !");
				Thread.Sleep(6000);
            }
            else
            {
				Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[-] Failed to terminate the process.");
				Thread.Sleep(6000);
            }
        }
        catch
        {
			Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[-] Failed to unprotect and terminate the process.");
			Thread.Sleep(6000);
        }
    }
}