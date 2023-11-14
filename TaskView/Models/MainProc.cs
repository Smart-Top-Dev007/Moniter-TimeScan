using System;
using System.IO;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Models;
using Monitor.TaskView.Utils;
using System.Diagnostics;
using System.Text;
using Microsoft.Win32;
using System.Management;
using Monitor.TaskView.Connect;
using System.Linq;
using Monitor.TaskView.Logger;

namespace Monitor.TaskView.Models
{
    public class MainProc
    {
        private static System.Timers.Timer CheckTimer { get; set; }
        public ScreenCaptures ScrCapture { get; set; }
        public ProcessInfos PrcInfo { get; set; }
        public URLProc UrlInfo { get; set; }
        public DownloadProc DownProc { get; set; }
        public AudioProc AudioInfo { get; set; }
        public ServerDataProc DisconnectedData { get; set; }

        public static Thread MainWorkThread;
        public static Thread VidCaptureThread;
        public static Thread UrlThread;
        public static Thread AudioThread;
        public static Thread CaptureThread;
        public static Thread SlideThread;
        public static Thread SendDataThread;

        public bool bCapture = false;
        public bool bSlider = false;
        public bool bFlag = false;
        public bool isSendData = false;

        public byte[] yesterdayData, todayData, tempTodayData;
        public byte[] sendData;

        private static MainProc _instance;
        public static MainProc Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainProc();
                }

                return _instance;
            }
        }
        private MainProc()
        {
            //   AllProcessStart();
        }
        private static void OnSaveTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            //    string today = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            //    string todayDir = Path.Combine(Settings.Instance.Directories.WorkDirectory, today);
            /////    DateTime.Now.
            //    try
            //    {
            //        if (!Directory.Exists(todayDir))
            //        {
            //            List<string> dirs = new List<string>(Directory.EnumerateDirectories( Settings.Instance.Directories.WorkDirectory ));
            //            foreach (var dir in dirs)
            //            {
            //                string[] TempDate = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1).Split('-');
            //                DateTime lastDay = new DateTime(int.Parse(TempDate[0]), int.Parse(TempDate[1]), int.Parse(TempDate[2]));
            //                double daysDiff = DateTime.Today.Subtract(lastDay).TotalDays;
            //                if (daysDiff > Constants.DelDataDay)
            //                {                            
            //                   Directory.Delete(dir, true);
            //                   Thread.Sleep(150);
            //                }
            //            }

            //            MainWorkThread.Suspend();
            //            // Process list clear
            //            Settings.Instance.ProcessList.Clear();
            //            if (Windows.MainWindow != null)
            //            {
            //                Windows.MainWindow.NewDayClearList();
            //            }
            //            MainWorkThread.Resume();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        CustomEx.DoExecption(Constants.exResume, ex);
            //    }      

        }

        public void CaptureStart()
        {
            VidCaptureThread = new Thread(new ThreadStart(DoVidCapture));
            VidCaptureThread.Start();
        }

        public void DoVidCapture()
        {
            while (true)
            {
                //byte[] dataSlide = ScrCapture.GetVideoStream();

                ScrCapture.GetStream("video");
                Thread.Sleep(1000);
            }

        }

        public void CaptureStop()
        {
            VidCaptureThread.Abort();
        }

        public void DoUrl()
        {
            int nCount = 0;
            Thread.Sleep(300);
            while (true)
            {
                try
                {
                    nCount += Settings.Instance.nMemorySize;

                    if (nCount % Settings.Instance.nMemorySize == 0)  // 3 or 6 seconds
                    {
                        UrlInfo.URLProcFunc();
                    }

                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
                if (nCount == 10000) nCount = 0;
                Thread.Sleep(Settings.Instance.nMemorySize * 1000);
            }
        }

        public void DoAudio()
        {

            while (true)
            {
                AudioInfo.CheckAudioLevels();
                Thread.Sleep(1000);
            }
        }

        public void AllProcessStart()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            var rk = key.OpenSubKey("RMClient");
            if (rk == null)
            {
                key = key.CreateSubKey("RMClient");
                key.SetValue("Version", "1.0");
            }
            else
            {
                Settings.current_version = rk.GetValue("Version").ToString();
            }
            ScrCapture = new ScreenCaptures();
            PrcInfo = new ProcessInfos();
            UrlInfo = new URLProc();
            DownProc = new DownloadProc();
            AudioInfo = new AudioProc();
            DisconnectedData = new ServerDataProc();

            PrcInfo.GetProcessInfo();

            MainWorkThread = new Thread(new ThreadStart(DoWork));
            MainWorkThread.Start();

            //UrlThread = new Thread(new ThreadStart(DoUrl));
            //UrlThread.Start();

            //AudioThread = new Thread(new ThreadStart(DoAudio));
            //AudioThread.Start();

            SendDataThread = new Thread(new ThreadStart(DoSend));
            SendDataThread.Start();

            //CaptureThread = new Thread(new ThreadStart(DoCapture));
            //CaptureThread.Start();

            //SlideThread = new Thread(new ThreadStart(DoSlide));
            //SlideThread.Start();
        }

        private void DoSend()
        {
            while (true)
            {
                if (Settings.Instance.bSend)
                {
                    //MainWorkThread.Suspend();
                    isSendData = true;
                    Thread.Sleep(500);

                    // Send ProcessInfo
                    //if (!File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName)))
                    //    File.Create(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName));
                    //try
                    //{
                    //    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName)) && Settings.Instance.SockCom.Connected == true)
                    //        DisconnectedData.SendData(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName), "", "Process");

                    //}
                    //catch { }

                    // Send SlideImage at Slide\Server
                    string strSlidePath = Path.Combine(Settings.Instance.Directories.SlideDirectory, "Server");

                    if (!Directory.Exists(strSlidePath))
                    {
                        Directory.CreateDirectory(strSlidePath);
                    }
                    string[] strSlideFiles = Directory.GetFiles(strSlidePath);

                    foreach (string strSlideFile in Directory.GetFiles(strSlidePath))
                    {
                        if (Settings.Instance.SockCom.Connected == false)
                        {
                            //MainWorkThread.Resume();
                            isSendData = false;
                            break;
                        }

                        DisconnectedData.SendData(Path.Combine(strSlidePath, strSlideFile), strSlideFile.Remove(0, strSlidePath.Length + 1), "Slide");
                    }



                    // Send CaptureImage
                    if (!Directory.Exists(Settings.Instance.Directories.CaptureDirectory))
                    {
                        Directory.CreateDirectory(Settings.Instance.Directories.CaptureDirectory);
                    }
                    string[] strCaptureFiles = Directory.GetFiles(Settings.Instance.Directories.CaptureDirectory);

                    foreach (string strCaptureFile in Directory.GetFiles(Settings.Instance.Directories.CaptureDirectory))
                    {
                        if (Settings.Instance.SockCom.Connected == false)
                        {
                            //MainWorkThread.Resume();
                            isSendData = false;
                            break;
                        }
                        DisconnectedData.SendData(Path.Combine(Settings.Instance.Directories.CaptureDirectory, strCaptureFile), strCaptureFile.Remove(0, Settings.Instance.Directories.CaptureDirectory.Length + 1), "Capture");
                    }


                    if (strSlideFiles.Length == 0 && strCaptureFiles.Length == 0 && !File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName)))
                    {
                        isSendData = false;
                        //MainWorkThread.Resume();

                    }


                }

                Thread.Sleep(150);
            }
        }

        public void DoWork()
        {
            DateTime virtualDate;
            int nCount = 0;
            string yesterday, yesterdayDirectory;

            Thread.Sleep(300);

            while (true)
            {
                nCount += Settings.Instance.RegValue.SessionTime;

                ChangeNewDay();

                // Capture Image 
                if (nCount % Settings.Instance.RegValue.CaptureTime == 0)   // 1 min
                {
                    
                        if (!Settings.Instance.bSend && !isSendData)
                        {
                            if (Settings.Instance.bLock == true)
                            {
                                Thread.Sleep(100);
                            }
                            ScrCapture.SaveServerImage("capture");
                        }
                        ScrCapture.GetStream("capture");
                    
                    

                }

                // Slide Image 
                if (nCount % Constants.slideTime == 0)  // 50 seconds
                {
                    ScrCapture.SaveImage("slide");

                    if (!Settings.Instance.bSend && !isSendData)
                    {
                        if (Settings.Instance.bLock == true)
                        {
                            Thread.Sleep(100);
                        }
                        ScrCapture.SaveServerImage("slide");
                    }

                    ScrCapture.GetStream("slide");
                }

                /////  process processing              
                if (nCount % Settings.Instance.RegValue.SessionTime == 0)  // 10 seconds
                {
                    try
                    {
                        int Forbidden_Count = 0;
                        Forbidden_Count = Md5Crypto.ReadCryptoFile(Settings.Instance.Directories.TodayDirectory + "\\" + Constants.ForbiddenFile).Count();


                        byte[] ForbiddenDatabytes = Encoding.ASCII.GetBytes(Constants.filePattern + Forbidden_Count.ToString());

                        byte[] dataProcess = PrcInfo.GetProcessInfo();

                        virtualDate = DateTime.Now.Add(Settings.Instance.RegValue.ClientServer_Span);

                        string strServerTime = virtualDate.ToString("HH:mm");

                        if (Settings.Instance.RegValue.ClientServer_Span.TotalMinutes >= 30)
                        {

                            if (virtualDate.Day == DateTime.Now.Day)
                            {

                                DateTime yesterdayDate = DateTime.Now.AddDays(-1);

                                yesterday = yesterdayDate.Year.ToString() + "-" + yesterdayDate.Month.ToString() + "-" + yesterdayDate.Day.ToString();
                                yesterdayDirectory = Path.Combine(Settings.Instance.RegValue.BaseDirectory, yesterday);


                                PrcInfo.GetProcessInfoServer(Constants.DbServerFileName + "_1.lib");

                                if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_1.lib")))
                                {
                                    todayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_1.lib"));
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                        todayData = Combine(todayData, tempTodayData);
                                    }
                                }
                                else
                                {
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        todayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                    }
                                }


                                if (File.Exists(Path.Combine(yesterdayDirectory, Constants.DbServerFileName + "_2.lib")))
                                {
                                    yesterdayData = File.ReadAllBytes(Path.Combine(yesterdayDirectory, Constants.DbServerFileName + "_2.lib"));
                                    sendData = Combine(yesterdayData, todayData);
                                }
                                else
                                    sendData = todayData;

                            }
                            else
                            {

                                PrcInfo.GetProcessInfoServer(Constants.DbServerFileName + "_2.lib");

                                if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_2.lib")))
                                {
                                    sendData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_2.lib"));
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                        sendData = Combine(sendData, tempTodayData);
                                    }
                                }

                                //sendData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_2.lib"));

                            }

                            //tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));
                            //sendData = Combine(sendData, tempTodayData);

                            sendData = Combine(sendData, ForbiddenDatabytes);
                            //CommProc.Instance.SendDataAnalysis(Constants.Re_ServerProcessData, sendData, sendData.Length);
                            
                        }
                        else if (Settings.Instance.RegValue.ClientServer_Span.TotalMinutes < -30)
                        {

                            if (virtualDate.Day == DateTime.Now.Day)
                            {

                                PrcInfo.GetProcessInfoServer(Constants.DbServerFileName + "_1.lib");

                                if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_1.lib")))
                                {
                                    todayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_1.lib"));
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                        todayData = Combine(todayData, tempTodayData);
                                    }
                                }
                                else
                                {
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        todayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                    }
                                }

                                sendData = todayData; //  Combine(todayData, ForbiddenDatabytes);


                            }
                            else
                            {
                                DateTime yesterdayDate = DateTime.Now.AddDays(-1);

                                yesterday = yesterdayDate.Year.ToString() + "-" + yesterdayDate.Month.ToString() + "-" + yesterdayDate.Day.ToString();
                                yesterdayDirectory = Path.Combine(Settings.Instance.RegValue.BaseDirectory, yesterday);

                                PrcInfo.GetProcessInfoServer(Constants.DbServerFileName + "_2.lib");

                                if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_2.lib")))
                                {
                                    todayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileName + "_2.lib"));
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                        todayData = Combine(todayData, tempTodayData);
                                    }
                                }
                                else
                                {
                                    if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp)))
                                    {
                                        todayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));

                                    }
                                }


                                if (File.Exists(Path.Combine(yesterdayDirectory, Constants.DbServerFileName + "_1.lib")))
                                {
                                    yesterdayData = File.ReadAllBytes(Path.Combine(yesterdayDirectory, Constants.DbServerFileName + "_1.lib"));
                                    sendData = Combine(yesterdayData, todayData);
                                }
                                else
                                    sendData = todayData;



                            }

                            //tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbServerFileNameTemp));
                            //sendData = Combine(sendData, tempTodayData);
                            sendData = Combine(sendData, ForbiddenDatabytes);
                            //CommProc.Instance.SendDataAnalysis(Constants.Re_ServerProcessData, sendData, sendData.Length);
                        }
                        else
                        {
                            if (!Settings.Instance.RegValue.isClientServer_Span && virtualDate.Day == DateTime.Now.Day)
                            {
                                if (File.Exists(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbFileName)))
                                {
                                    sendData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbFileName));
                                    tempTodayData = File.ReadAllBytes(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DbFileNameTemp));
                                    sendData = Combine(sendData, tempTodayData);
                                    sendData = Combine(sendData, ForbiddenDatabytes);

                                    //CommProc.Instance.SendDataAnalysis(Constants.Re_ServerProcessData, sendData, sendData.Length);
                                }
                            }


                        }

                        if (strServerTime != "23:59" && strServerTime != "00:00" && strServerTime != "00:01")
                        {
                            CommProc.Instance.SendDataAnalysis(Constants.Re_ServerProcessData, sendData, sendData.Length);
                        }
                        
                    }
                    catch { }


                }

                ///// Download File
                if (nCount % Constants.SessionTime == 0)   // 10 seconds
                {

                    DownProc.FindDownloadfile();

                }

                ///////////////////////  time repair and count 
                if (nCount == 10000) nCount = 0;
                Thread.Sleep(Settings.Instance.RegValue.SessionTime * 1000);
            }
        }

        public void ChangeNewDay()
        {
            string today = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            string todayDir = Path.Combine(Settings.Instance.Directories.WorkDirectory, today);
            string strSlidePath = Path.Combine(todayDir, "Slide");
            string strCapturePath = Path.Combine(todayDir, "Capture");

            if (!Directory.Exists(strSlidePath) || !Directory.Exists(strCapturePath))
            {

                try
                {
                    List<string> dirs = new List<string>(Directory.EnumerateDirectories(Settings.Instance.Directories.WorkDirectory));
                    foreach (var dir in dirs)
                    {
                        string[] TempDate = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1).Split('-');
                        DateTime lastDay = new DateTime(int.Parse(TempDate[0]), int.Parse(TempDate[1]), int.Parse(TempDate[2]));
                        double daysDiff = DateTime.Today.Subtract(lastDay).TotalDays;
                        if (daysDiff > Constants.DelDataDay)
                        {
                            List<string> _dirList = new List<string>(Directory.EnumerateDirectories(dir));

                            foreach (var _dir in _dirList)
                            {
                                if (_dir.Contains("."))
                                {
                                    Directory.Delete(dir + @"\Audio", true);
                                    Directory.Delete(dir + @"\Capture", true);
                                    Directory.Delete(dir + @"\Slide", true);
                                    File.Delete(dir + @"\Auo.dll");
                                    File.Delete(dir + @"\Contents.lib");
                                    File.Delete(dir + @"\Browser.dat");
                                    File.Delete(dir + @"\dwControl.dll");

                                    break;
                                }
                            }

                            if (File.Exists(dir + @"\Contents.lib"))
                            {
                                Directory.Delete(dir, true);
                            }

                            Thread.Sleep(150);
                        }
                    }
                    byte[] endData = Encoding.UTF8.GetBytes(Constants.Re_End);
                    Settings.Instance.SockCom.Send(endData);
                    //Windows.MainWindow.DeleteTryIcon();
                    EnvironmentHelper.Restart();
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                    //Windows.MainWindow.DeleteTryIcon();
                    EnvironmentHelper.Restart();
                }

            }

        }
        public void AllProcessStop()
        {
            MainWorkThread.Abort();
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            return first.Concat(second).ToArray();
        }
    }
}
