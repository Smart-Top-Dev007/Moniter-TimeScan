using Monitor.TaskView.Globals;
using Monitor.TaskView.Utils;
using Monitor.TaskView.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Monitor.TaskView.Models
{
    public class DownloadProc
    {
        public List<dlFileInfo> dlfList = new List<dlFileInfo>();
        private List<dlFileInfo> dlfNewList = new List<dlFileInfo>();
        public static string downLoadFolder;
        public DownloadProc()
        {
            FirstMakeList();
        }

        public void FindDownloadfile()
        {
            if (!Directory.Exists(downLoadFolder))
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(downLoadFolder);
            FileInfo[] downloadFiles = dir.GetFiles();
            dlFileInfo dlfInfo;
            dlfNewList.Clear();
            foreach (FileInfo file in downloadFiles.Where(file => file.CreationTime >= DateTime.Today))
            {
                dlfInfo.Filename = file.Name;
                dlfInfo.Extention = file.Extension;
                dlfInfo.Fullpath = file.FullName;
                dlfInfo.CreateTime = file.CreationTime;
                dlfInfo.Size = file.Length;
                dlfNewList.Add(dlfInfo);

                if (!dlfList.Contains(dlfInfo))
                {
                    try
                    {
                        string strMessage1;
                        byte[] DownloadBytes1;
                        if (file.Extension == ".crdownload" || file.Extension == ".fdmdownload")
                        {

                            string strDownloadingMessage = file.Name;
                            //App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                            //() =>
                            //{
                            //    var notify = new NotificationWindow();
                            //    notify.Show(strDownloadingMessage, Constants.Se_MsgDownLoading);

                            //}));

                            strMessage1 = file.Name;
                            DownloadBytes1 = Encoding.UTF8.GetBytes(strMessage1);
                            CommProc.Instance.SendDataAnalysis(Constants.Re_MsgDownLoading, DownloadBytes1, DownloadBytes1.Length);

                            continue;
                        }

                        Settings.Instance.nDownloadCount++;
                        App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                        () =>
                        {
                            Windows.MainWindow.lblDownloadCount.Content = Settings.Instance.nDownloadCount.ToString();

                        }));

                        string strPath = Settings.Instance.Directories.TodayDirectory;


                        string strDownloadedMessage = "You are downloaded  this: " + file.Name;
                        //App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                        //() =>
                        //{
                        //    var notify = new NotificationWindow();
                        //    notify.Show(strDownloadedMessage, Constants.Se_MsgDownload);

                        //}));
                        string strSize = "";
                        long nSize = long.Parse(file.Length.ToString());
                        if ((nSize / 1024) < 1000)
                        {
                            strSize = (nSize / 1024f).ToString("0.00") + "KB";
                        }
                        else
                        {
                            strSize = ((nSize / 1024f) / 1024f).ToString("0.00") + "MB";
                        }

                        string sendData = file.Name + Constants.filePattern + strSize + Constants.filePattern + file.CreationTime.ToString("HH-mm-ss") + Constants.filePattern + DateTime.Now.ToString("HH-mm-ss");

                        Md5Crypto.WriteCryptoFileAppend(strPath, Constants.DownloadFile, sendData);

                        if(Settings.Instance.bLock == true)
                        {
                            Thread.Sleep(3000);
                        }

                        byte[] bytes = Encoding.UTF8.GetBytes(sendData);
                        CommProc.Instance.SendDataAnalysis(Constants.Re_DataDownload, bytes, bytes.Length);
                        Thread.Sleep(100);

                        string strMessage = file.Name;
                        byte[] DownloadBytes = Encoding.UTF8.GetBytes(strMessage);
                        CommProc.Instance.SendDataAnalysis(Constants.Re_MsgDownload, DownloadBytes, DownloadBytes.Length);
                    }
                    catch (Exception ex)
                    {

                    }

                    //New download file process
                    //listBox1.Items.Add($"Name = {file.Name}      Size = {file.Length}    CreationTime = {file.CreationTime}");
                }
            }
            dlfList = dlfNewList.ToList();
        }

        private void FirstMakeList()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downLoadFolder = Path.Combine(userFolder + "\\Downloads");
            if (!Directory.Exists(downLoadFolder))
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(downLoadFolder);
            FileInfo[] downloadFiles = dir.GetFiles();
            dlFileInfo dlfInfo;
            foreach (FileInfo file in downloadFiles.Where(file => file.CreationTime >= DateTime.Today))
            {
                dlfInfo.Filename = file.Name;
                dlfInfo.Extention = file.Extension;
                dlfInfo.Fullpath = file.FullName;
                dlfInfo.CreateTime = file.CreationTime;
                dlfInfo.Size = file.Length;
                dlfList.Add(dlfInfo);
            }
        }

        public struct dlFileInfo
        {
            public string Filename;
            public string Extention;
            public long Size;
            public DateTime CreateTime;
            public string Fullpath;
            //    public DateTime downloadTime;
        }
    }
}
