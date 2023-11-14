using murrayju.ProcessExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemService
{
    public partial class Service1 : ServiceBase
    {
        private static Timer t;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            t = new Timer(TimerCallback, null, 0, 10000);
        }
        private static void TimerCallback(Object o)
        {
            //var processes = FindProcess("SRMHost");
            //if (processes.Length == 0)
            //{
            //    try
            //    {
            //        ProcessExtensions.StartProcessAsCurrentUser(Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Users\\Public\\RBM\\Server\\SRMHost.exe"));

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
            var processes = FindProcess("Monitor.TaskControl");
            if (processes.Length == 0)
            {
                try
                {
                    ProcessExtensions.StartProcessAsCurrentUser(Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Users\\Public\\RBM\\Server\\Monitor.TaskControl.exe"));
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

        private static Process[] FindProcess(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);

            return processList;
        }
        protected override void OnStop()
        {
        }
    }
}
