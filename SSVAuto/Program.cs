using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSVAuto
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private static Mutex _mutex;
        private static bool _createdNew;
        public static int nTimeCount = 0;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            _mutex = new Mutex(true, "SRMAuto", out _createdNew);
            if (!_createdNew)  // already runnig
            {
                Environment.Exit(0);
            }

            var handle = GetConsoleWindow();
            try
            {
                //File.AppendAllText(Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\Server\\ssvautolog.txt", "---start---");
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
            nTimeCount++;
            if (nTimeCount == 43020)
            {
                nTimeCount = 0;
                Process.Start(FileName);
                Environment.Exit(0);
            }
            var processes = FindProcess("SRMHost");
            if (processes.Length == 0)
            {
                try
                {
                    var proc = new Process();
                    proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Server\\SRMHost.exe";
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
                catch
                {
                    //File.WriteAllText("D:\\log.txt", ex.Message);
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
            processes = FindProcess("Monitor.TaskControl");
            if (processes.Length == 0)
            {
                try
                {
                    var proc = new Process();
                    proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Server\\Monitor.TaskControl.exe";
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

        private static void OnProcessExit(object sender, EventArgs eventArgs)
        {
            if (_mutex != null && _createdNew)
            {
                try
                {
                    _mutex.ReleaseMutex();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        public static string FileName
        {
            get { return Process.GetCurrentProcess().MainModule.FileName; }
        }
    }
}
