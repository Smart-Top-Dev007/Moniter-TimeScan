using Microsoft.Win32;
using SSVHost.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSVHost
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        public static int port = 9998;
        public static Timer t;
        public static int nTimeCount = 0;
        private static Mutex _mutex;
        private static bool _createdNew;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            _mutex = new Mutex(true, "SRMHost", out _createdNew);
            if (!_createdNew)  // already runnig
            {
                Environment.Exit(0);
            }

            var handle = GetConsoleWindow();
            try
            {
                //File.AppendAllText(Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\Server\\ssvhostlog.txt", "---start---");
            }
            catch
            {
                // File.AppendAllText("D:\\log.txt", "---start---");
            }

            ShowWindow(handle, SW_HIDE);
            try
            {
               
                
                
                t = new Timer(TimerCallback, null, 0, 10000);
                AutoPatch patch = new AutoPatch();

                ZeroPatch zeroPatch = new ZeroPatch();

                UsbDetect usbDetect = new UsbDetect();

                FileSend fileSend = new FileSend();

                SendArp arp = new SendArp();

            }
            catch
            {

            }
            

            Console.ReadLine();
        }

    

        


        private static void TimerCallback(Object o)
        {
            try
            {
                ServiceController sc = new ServiceController("SystemService");
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        break;
                    case ServiceControllerStatus.Stopped:
                        try
                        {
                            var process = new System.Diagnostics.Process();
                            var startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32";
                            startInfo.UseShellExecute = true;
                            startInfo.CreateNoWindow = true;
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.FileName = "cmd.exe";

                            string command = $"/c PowerShell Start-Service -Name \"SystemService\"";
                            startInfo.Arguments = command;
                            startInfo.Verb = "runas";
                            process.StartInfo = startInfo;
                            process.Start();

                            process.WaitForExit();
                        }
                        catch
                        {
                        }
                        break;
                    case ServiceControllerStatus.Paused:
                        break;
                    case ServiceControllerStatus.StopPending:
                        break;
                    case ServiceControllerStatus.StartPending:
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
            
            //nTimeCount++;
            //if (nTimeCount == 43140)
            //{
            //    nTimeCount = 0;
            //    Process.Start(FileName);
            //    Environment.Exit(0);
            //}
            //var processes = FindProcess("SRMAuto");
            //if (processes.Length == 0)
            //{
            //    try
            //    {
            //        var proc = new Process();
            //        proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Server\\SRMAuto.exe";
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
            //processes = FindProcess("Monitor.TaskControl");
            //if (processes.Length == 0)
            //{
            //    try
            //    {
            //        var proc = new Process();
            //        proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Server\\Monitor.TaskControl.exe";
            //        //proc.StartInfo.UseShellExecute = true;
            //        proc.Start();
            //    }
            //    catch
            //    {

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
