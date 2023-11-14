using Monitor.TaskControl.myEvents;
using Monitor.TaskControl.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Monitor.TaskControl.Logger;
using System.IO;
using Monitor.TaskControl.Globals;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Threading;

namespace Monitor.TaskControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _mutex;
        private bool _createdNew;
        private static Process[] FindProcess(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);

            return processList;
        }
        private static Timer t;
        private static void TimerCallback(Object o)
        {
            var processes = FindProcess("SRMHost");
            if (processes.Length == 0)
            {
                try
                {
                    var process = new System.Diagnostics.Process();
                    var startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32";
                    startInfo.UseShellExecute = true;
                    startInfo.CreateNoWindow = true;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    // startInfo.FileName = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Users\\Public\\RBM\\Client\\CRMHost.exe");
                    string command = "/c PowerShell Start-Process -FilePath SRMHost.exe -WorkingDirectory " + Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Users\\Public\\RBM\\Server");
                    //string command = "/c dir";
                    startInfo.Arguments = command;
                    startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();

                    process.WaitForExit();
                }
                catch
                {

                }
            }
            GC.Collect();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
            Thread.Sleep(300);
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Application.Current.Exit += OnExit;
            var domain = AppDomain.CurrentDomain;
            
            domain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            domain.DomainUnload += new EventHandler(domain_DomainUnload);
            //  Double Runnig check 
            _mutex = new Mutex(true, "Monitor.TaskControl", out _createdNew);
            //
            if (!_createdNew)  // already runnig
            {
                EnvironmentHelper.ShutDown();
            }
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                var rk = key.OpenSubKey(Constants.RegPath);
                string BaseDirectory = (string)rk.GetValue("BaseDirectory");
                if (BaseDirectory == "D:\\Work")
                {
                    string[] fileEntries = Directory.GetFiles("D:\\Work\\", "Contents.lib", SearchOption.AllDirectories);
                    if (fileEntries.Count() > 0)
                    {
                        Directory.Move("D:\\Work", Constants.BaseDirectory);

                        key = key.CreateSubKey(Constants.RegPath);
                        key.SetValue("BaseDirectory", Constants.BaseDirectory);
                        key.Close();
                    }
                }
                t = new Timer(TimerCallback, null, 0, 10000);
                
            }
            catch
            {

            }
            Events.RaiseOnStartUp(e); //
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Instance.DoLog("OnExit override", Log.LogType.Error);
            DeleteTryIcon();
            base.OnExit(e);
        }
        void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Log.Instance.DoLog(args.Exception.Message, Log.LogType.Error);
            DeleteTryIcon();
        }
        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Log.Instance.DoLog("unhandle exception", Log.LogType.Error);
            Exception e = (Exception)args.ExceptionObject;
            DeleteTryIcon();
            
        }

        static void domain_ProcessExit(object sender, EventArgs e)
        {
            Log.Instance.DoLog("domain_process", Log.LogType.Error);
            DeleteTryIcon();
            
        }
        static void domain_DomainUnload(object sender, EventArgs e)
        {
            Log.Instance.DoLog("domain_unload", Log.LogType.Error);
            DeleteTryIcon();
            
        }
        static public void DeleteTryIcon()
        {
            if (Settings.Instance.ni != null)
            {
                Settings.Instance.ni.Visible = false;
                if (Settings.Instance.ni.Icon != null)
                {
                    Settings.Instance.ni.Icon.Dispose();
                    Settings.Instance.ni.Icon = null;
                }
                Settings.Instance.ni.Dispose();
                Settings.Instance.ni = null;
            }
            
        }
        private void OnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Instance.DoLog("OnProcessExit", Log.LogType.Error);
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
            DeleteTryIcon();
           
        }
        private void OnExit(object sender, EventArgs eventArgs)
        {
            Log.Instance.DoLog("OnExit", Log.LogType.Error);
            DeleteTryIcon();
            
        }
    }
}
