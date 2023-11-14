using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Threading;

using Monitor.TaskView.View;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Logger;
using Monitor.TaskView.Connect;
using Monitor.TaskView.Models;
using System.Diagnostics;
using Monitor.TaskView.Utils;

namespace Monitor.TaskView.myEvents
{
    class EventHandlers
    {
        static EventHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;

            Events.OnStartUp                += OnStartUp;
            Events.OnExit                   += OnExit;
            Events.OnConnectedFinished      += OnConnectedFinished;
            Events.OnRegister               += OnRegister;
            Events.OnMainProc               += OnMainProc;
            Events.OnPasswordCheck          += OnPasswordCheck;
            Events.OnReceiveData            += OnReceiveData;
            Events.OnChangeServer           += OnChangeServer;
        }

        public static void Initialize()
        {
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var exception = (Exception)args.ExceptionObject;

            if (args.IsTerminating)
            {
                //                Settings.Save();
            }
            
            
        }
        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            //foreach (var file in Directory.GetFiles(Settings.Instance.Directories.DependenciesDirectory))
            //{
            //    if (Path.GetFileNameWithoutExtension(file) == (new AssemblyName(args.Name).Name))
            //    {
            //        return Assembly.LoadFrom(file);
            //    }
            //}

            return null;
        }

        
        private static void OnStartUp(StartupEventArgs startupEventArgs)
        {
            
            if ( !Settings.Instance.RegValue.ExistsGetValue() )
            {
                
                Windows.LoginWindow = new SignInWindow();
                Windows.LoginWindow.Show();
                
            }
            else
            {
               
                Windows.LoginWindow = new SignInWindow();
                LoaderConnect.ConnectionSystem();
                
                Communications.StartUpSocket();
               
            }            
        }
        private static void OnMainProc()
        {
            MainProc.Instance.AllProcessStart();
        }
        private static void OnReceiveData(byte[] buff, int len)
        {
            CommProc.Instance.RecDataAnalysis( buff, len );
        }
        private static void OnPasswordCheck()
        {
            //if (Windows.PassWindow != null)
            //    Windows.PassWindow.Hide();
            // can not be connected.
            if (Settings.Instance.RegValue.Password == Constants.InitPassword)
            {
                Windows.LoginWindow = new SignInWindow();
                Windows.LoginWindow.Show();

                if (Windows.PassWindow != null)
                    Windows.PassWindow.Close();
            }
            else  // Connected
            {
                Settings.Instance.RegValue.WriteValue();
                Settings.Instance.RegValue.isClientServer_Span = false;
                Settings.Instance.bSend = true;
                Settings.Instance.bCapture = true;

                //MainProc.Instance.DoCaptureSend();

                LoaderConnect.ConnectionSystem();
            }


            if (Windows.PassWindow != null)
                Windows.PassWindow.Close();
        }
        private static void OnChangeServer()
        {
            //Communications.Disconnect();
            //Communications.StartUpSocket();            

            //if (Windows.MainWindow != null)
            //{
            //    Windows.MainWindow.Close();
            //}
            //Windows.PassWindow = new PasswordWindow();
            //Windows.PassWindow.Show();


            EnvironmentHelper.Restart(true);
        }

        private static void OnExit(ExitEventArgs exitEventArgs)
        {

             //Windows.MainWindow.DeleteTryIcon();
            //// Cleanup
            /// DeleteTryIcon();

            //           Settings.Save();

        }

        private static void OnRegister(EventArgs exitEventArgs)
        {
            Settings.Instance.RegValue.ServerIP = Windows.LoginWindow.ServerIP.Text;
            Settings.Instance.RegValue.UserName = Windows.LoginWindow.UserName.Text;
            Settings.Instance.RegValue.Company = Windows.LoginWindow.Company.Text;
            //    Settings.Instance.RegValue.Password = Constants.InitPassword;
            Settings.Instance.RegValue.BaseDirectory = Windows.LoginWindow.WorkDirectory.Text;
            Settings.Instance.RegValue.SessionTime = Constants.SessionTime;
            Settings.Instance.RegValue.CaptureTime = Constants.CaptureTime;
            Settings.Instance.RegValue.SlideWidth = Constants.SlideWidth;
            Settings.Instance.RegValue.SlideHeight = Constants.SlideHeight;
            Settings.Instance.RegValue.CaptureWidth = Constants.CaptureWidth;
            Settings.Instance.RegValue.CaptureHeight = Constants.CaptureHeight;
            Settings.Instance.RegValue.ActiveDuration = Constants.ActiveDuration;
            Settings.Instance.Directories.CreateDirectories();

            Communications.StartUpSocket();
            try
            {
                //if(Settings.Instance.bConnect == true)
                //{
                //    string strComName = Environment.MachineName;
                //    Process p = new Process();
                //    // Redirect the output stream of the child process.
                //    p.StartInfo.UseShellExecute = false;
                //    p.StartInfo.CreateNoWindow = true;
                //    p.StartInfo.RedirectStandardOutput = true;
                //    p.StartInfo.FileName = "cmd";
                //    p.StartInfo.Arguments = "/c systeminfo | find /i \"install date\"";
                //    p.Start();
                //    string output = p.StandardOutput.ReadToEnd();
                //    p.WaitForExit();
                //    string strOSDate = output.Replace("Original Install Date:", "").Trim(); //+ Directory.GetCreationTime(Path.GetPathRoot(Environment.SystemDirectory)+@"Windows").ToLongTimeString();
                //    string strTemp = Settings.Instance.RegValue.UserName + ":" + Settings.Instance.RegValue.Password + ":" + Settings.Instance.RegValue.Company + ":" + Settings.Instance.RegValue.SessionTime + ":" + Settings.Instance.RegValue.CaptureTime + ":" + Settings.Instance.RegValue.SlideWidth + ":" + Settings.Instance.RegValue.SlideHeight + ":" + Settings.Instance.RegValue.CaptureWidth + ":" + Settings.Instance.RegValue.CaptureHeight + ":" + strComName + ":" + strOSDate + ":" + Settings.Instance.RegValue.ActiveDuration;

                //    string strClientInfo = Constants.Re_ClientInfo + strTemp;
                //    byte[] buffer = Encoding.UTF8.GetBytes(strClientInfo);
                //    Settings.Instance.SockCom.Send(buffer);
                //}
                
            }
            catch(Exception ex)
            {
                CustomEx.DoExecption(Constants.exResume, ex);
                return;
            }
            

            if (Windows.LoginWindow != null)
            {
                Windows.LoginWindow.Hide();
            }

            Windows.PassWindow = new PasswordWindow();
            Windows.PassWindow.Show();
        //    LoaderConnect.ConnectionSystem();

            if (Windows.LoginWindow != null)
            {
                Windows.LoginWindow.Close();
            }
        }
        private static void OnConnectedFinished(EventArgs startupEventArgs)
        {
            if (Windows.LoginWindow != null)
            {
                Windows.LoginWindow.Hide();
            }     
            Settings.Instance.RegValue.ExistsGetValue();
            Settings.Instance.Directories.CreateDirectories();
            Windows.MainWindow = new MainWindow();
            Windows.MainWindow.Show();

            if (Windows.LoginWindow != null)
            {
                Windows.LoginWindow.Close();
            }

        }
    }
}
