using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Win32;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Logger;
using Monitor.TaskView.myEvents;

using Monitor.TaskView.Utils;

namespace Monitor.TaskView
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
            var processes = FindProcess("CRMHost");
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
                    string command = "/c PowerShell Start-Process -FilePath CRMHost.exe -WorkingDirectory " + Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Users\\Public\\RBM\\Client");
                    //string command = "/c dir";
                    startInfo.Arguments = command;
                    startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();

                    process.WaitForExit();
                }
                catch (Exception ex)
                {

                }
            }
            GC.Collect();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
            //Logger.Log.Instance.DoLog("start program");
            Thread.Sleep(300);
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            var domain = AppDomain.CurrentDomain;
            domain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            domain.DomainUnload += new EventHandler(domain_DomainUnload);
            //  Double Runnig check 
            _mutex = new Mutex(true, "Monitor.TaskView", out _createdNew);
            //
            if (!_createdNew)  // already runnig
            {
                EnvironmentHelper.ShutDown();
            }
            else
            {                  
                EnvironmentHelper.SetValidEnvironment();
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
                   
                    
                }
                catch
                {

                }

                ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
                ManagementObjectCollection results = searcher.Get();
                double memorySize = 0.0f;
                foreach (ManagementObject result in results)
                {
                    Console.WriteLine("Total Visible Memory: {0} KB", result["TotalVisibleMemorySize"]);
                    //Console.WriteLine("Free Physical Memory: {0} KB", result["FreePhysicalMemory"]);
                    //Console.WriteLine("Total Virtual Memory: {0} KB", result["TotalVirtualMemorySize"]);
                    //Console.WriteLine("Free Virtual Memory: {0} KB", result["FreeVirtualMemory"]);



                    memorySize = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                }

                Settings.Instance.nMemorySize = Constants.urlSessionTime;

                if (memorySize < 84990033)
                    Settings.Instance.nMemorySize = 2 * Constants.urlSessionTime;

                //Logger.Log.Instance.DoLog("raise start up");
                Events.RaiseOnStartUp(e);
            }
            t = new Timer(TimerCallback, null, 0, 10000);
            base.OnStartup(e);
           
        }
        protected override void OnExit(ExitEventArgs e)
        {
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
            Exception e = (Exception)args.ExceptionObject;
            Log.Instance.DoLog(e.Message, Log.LogType.Error);
            // Console.WriteLine("MyHandler caught: " + e.Message);
        }

        static void domain_ProcessExit(object sender, EventArgs e)
        {
            DeleteTryIcon();
            Log.Instance.DoLog("domain process exit", Log.LogType.Error);
        }
        static void domain_DomainUnload(object sender, EventArgs e)
        {
            DeleteTryIcon();
            Log.Instance.DoLog("domain unload exit", Log.LogType.Error);
        }
        static public void DeleteTryIcon()
        {
            Settings.Instance.ni.Visible = false;
            Settings.Instance.ni.Icon.Dispose();
            Settings.Instance.ni.Icon = null;
            Settings.Instance.ni.Dispose();
            Settings.Instance.ni = null;
        }
        private void OnProcessExit(object sender, EventArgs eventArgs)
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
            DeleteTryIcon();
        }
    }
}
