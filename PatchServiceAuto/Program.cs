using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PatchServiceAuto
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            try
            {
                //File.AppendAllText(Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\Client\\csvautolog.txt", "---start---");
            }
            catch
            {
                // File.AppendAllText("D:\\log.txt", "---start---");
            }

            ShowWindow(handle, SW_HIDE);
            Timer t = new Timer(TimerCallback, null, 0, 1000);


            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            //var processes = FindProcess("PatchServiceHost");
            //if (processes.Length == 0)
            //{
            //    try
            //    {
            //        var proc = new Process();
            //        proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\PatchService\\PatchServiceHost.exe";
            //        proc.StartInfo.UseShellExecute = true;
            //        proc.Start();
            //    }
            //    catch
            //    {
            //        //File.WriteAllText("D:\\log.txt", ex.Message);
            //    }

            //}
            //else if (processes.Length > 1)
            //{
            //    try
            //    {
            //        processes[0].Kill();
            //    }
            //    catch
            //    {

            //    }

            //}

            var processes = FindProcess("PatchService");
            if (processes.Length == 0)
            {
                try
                {
                    var proc = new Process();
                    //proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\PatchService\\PatchService.exe";
                    //proc.StartInfo.FileName = "J:\\Ryonbong_Monitor\\PatchService.exe";
                    proc.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\PatchService.exe";
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
                catch
                {

                }

            }
            else if (processes.Length > 1)
            {
                try
                {
                    processes[0].Kill();
                }
                catch
                {

                }

            }

            GC.Collect();
        }

        public static Process[] FindProcess(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);

            return processList;
        }
    }
}
