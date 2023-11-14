using System;
using System.Timers;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Net.Sockets;

namespace Monitor.TaskView.Globals
{    
    [Serializable]
    public class Settings
    {
        private static Timer SaveTimer { get; set; }
        public SettingsDirectories Directories { get; private set; }
        public RegisterValue RegValue { get; set; }
        public Socket SockCom { get; set; }
        public bool bSend { get; set; }
        public bool bCapture { get; set; }
        public bool bConnect { get; set; }
        public bool bStart { get; set; }
        public bool isProcess { get; set; }
        public bool bPhoto { get; set; }
        public bool bLock { get; set; }

        public static readonly object _locker = new object();
        public static string current_version = "1.0";

        public int nDownloadCount = 0;
        public int nForbiddenCount = 0;
        public int nUrlCount = 0;
        public int nMemorySize = 0;
        public bool bSocketFree { get; set; }
        public ListOfProcessByOrder LOPBO = new ListOfProcessByOrder();
        public List<ListOfProcessByOrder> ProcessList = new List<ListOfProcessByOrder>();
        public List<ListOfProcessByOrder> ProcessClientList = new List<ListOfProcessByOrder>();

        public ListOfProcessByOrder LOPBO_Day = new ListOfProcessByOrder();
        public List<ListOfProcessByOrder> ProcessList_Day = new List<ListOfProcessByOrder>();

        public ListOfUrl LOU = new ListOfUrl();
        public List<ListOfUrl> URLList = new List<ListOfUrl>();

        public ListOfUrl LOU_Day = new ListOfUrl();
        public List<ListOfUrl> URLList_Day = new List<ListOfUrl>();

        public ListOfAudio LOA = new ListOfAudio();
        public List<ListOfAudio> AudioList = new List<ListOfAudio>();

        public ListOfAudio LOA_Day = new ListOfAudio();
        public List<ListOfAudio> AudioList_Day = new List<ListOfAudio>();

        private static Settings _instance;

        public System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }

                return _instance;
            }
        }
        static Settings()
        {
            SaveTimer = new Timer(60000);
         //   SaveTimer.Elapsed += OnSaveTimerElapsed;
        //    SaveTimer.Start();
        }
        private static void OnSaveTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Save();
        }

        public static void Save()
        {

        }

        public static void Load()
        {

        }
        private Settings()
        {
            RegValue = new RegisterValue();
            Directories = new SettingsDirectories();
            bConnect = false;
            bSend = false;
            bStart = false;
            bSocketFree = false;
            bLock = false;
            bPhoto = false;
            Directories.Verify();
        }
    }
    public class RegisterValue
    {
        public string ServerIP { get; set; }
        public string BaseDirectory { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SessionTime { get; set; }
        public int ActiveDuration { get; set; }
        public int CaptureTime { get; set; }
        public string Version { get; set; }
        public string Company { get; set; }
        public int SlideWidth { get; set; }
        public int SlideHeight { get; set; }
        public int CaptureWidth { get; set; }
        public int CaptureHeight { get; set; }
        public bool isClientServer_Span { get; set; }
        public TimeSpan ClientServer_Span { get; set; }
        public RegisterValue()
        {
            ServerIP = "127.0.0.1";
            BaseDirectory = Constants.BaseDirectory;
            UserName = "";
            Password = "";//Constants.InitPassword;
            Version = Constants.Version;
            Company = "Single";
            SessionTime = Constants.SessionTime;
            ActiveDuration = Constants.ActiveDuration;
            CaptureTime = Constants.CaptureTime;
            SlideWidth = Constants.SlideWidth;
            SlideHeight = Constants.SlideHeight;
            CaptureWidth = Constants.CaptureWidth;
            CaptureHeight = Constants.CaptureHeight;
            isClientServer_Span = false;
        }
        public void WriteValue()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.CreateSubKey(Constants.RegPath);
            key.SetValue("ServerIP", ServerIP);
            key.SetValue("BaseDirectory", BaseDirectory);
            key.SetValue("UserName", UserName);
            key.SetValue("Password", Password);
            key.SetValue("SessionTime", SessionTime);
            key.SetValue("ActiveDuration", ActiveDuration);
            key.SetValue("CaptureTime", CaptureTime);
            key.SetValue("Version", Version);
            key.SetValue("Company", Company);
            key.SetValue("SlideWidth", SlideWidth);
            key.SetValue("SlideHeight", SlideHeight);
            key.SetValue("CaptureWidth", CaptureWidth);
            key.SetValue("CaptureHeight", CaptureHeight);
            key.Close();
        }
        public bool ExistsGetValue()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                var rk = key.OpenSubKey(Constants.RegPath);
                //Logger.Log.Instance.DoLog("check registry");
                if (rk == null)
                    return false;

                ServerIP = (string)rk.GetValue("ServerIP");
                if (ServerIP == null)
                    return false;

                BaseDirectory = (string)rk.GetValue("BaseDirectory");
                if (BaseDirectory == null)
                    return false;

                UserName = (string)rk.GetValue("UserName");
                if (UserName == null)
                    return false;

                Password = (string)rk.GetValue("Password");

                Version = (string)rk.GetValue("Version");
                if (Version == null)
                    return false;

                Company = (string)rk.GetValue("Company");
                if (Company == null)
                    return false;
                //Logger.Log.Instance.DoLog("registry exists" + rk.Name);
                SessionTime = (int)rk.GetValue("SessionTime");

                ActiveDuration = (int)rk.GetValue("ActiveDuration");

                CaptureTime = (int)rk.GetValue("CaptureTime");

                SlideWidth = (int)rk.GetValue("SlideWidth");

                SlideHeight = (int)rk.GetValue("SlideHeight");

                CaptureWidth = (int)rk.GetValue("CaptureWidth");

                CaptureHeight = (int)rk.GetValue("CaptureHeight");
            }
            catch
            {
                ServerIP = "127.0.0.1";
                BaseDirectory = Constants.BaseDirectory;
                UserName = "";
                Password = "";//Constants.InitPassword;
                Version = Constants.Version;
                Company = "Single";
                SessionTime = Constants.SessionTime;
                ActiveDuration = Constants.ActiveDuration;
                CaptureTime = Constants.CaptureTime;
                SlideWidth = Constants.SlideWidth;
                SlideHeight = Constants.SlideHeight;
                CaptureWidth = Constants.CaptureWidth;
                CaptureHeight = Constants.CaptureHeight;
            }
        
            return true;
        }

    }
    public class SettingsDirectories
    {
        public string TodayDirectory { get; set; } 
        public string WorkDirectory { get; set; } 
        public string SlideDirectory { get; set; }
        public string CaptureDirectory { get; set; }
        public string CurrentDirectory 
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
        public string SetupPath
        {
            get { return Constants.SetupPath; }
        }


        internal SettingsDirectories()
        {
        //    CreateDirectories();
        }


        internal void Verify()
        {      

         //   CreateDirectories();
        }
        public void CreateDirectories()
        {
            DateTime localDate = DateTime.Now;
            WorkDirectory = Settings.Instance.RegValue.BaseDirectory;
            if (!Directory.Exists(WorkDirectory))
                Directory.CreateDirectory(WorkDirectory);

            string today = localDate.Year.ToString() + "-" + localDate.Month.ToString() + "-" + localDate.Day.ToString();
            TodayDirectory = Path.Combine(WorkDirectory, today);
            if (!Directory.Exists(TodayDirectory))
                Directory.CreateDirectory(TodayDirectory);

            SlideDirectory = Path.Combine(TodayDirectory, "Slide");
            if (!Directory.Exists(SlideDirectory))
                Directory.CreateDirectory(SlideDirectory);

            CaptureDirectory = Path.Combine(TodayDirectory, "Capture");
            if (!Directory.Exists(CaptureDirectory))
                Directory.CreateDirectory(CaptureDirectory);
        }
    }

    public struct ListOfProcessByOrder
    {
        public string ProcessName;
        public string ProcessWindow;
        public string ProcessPath;
        public string ProcessColor;
        public DateTime ProcessStartTime;
        public DateTime ProcessEndTime;
    }

    public struct ListOfUrl
    {
        public byte BrowserType;
        public string strWindow;
        public string strURL;
        public DateTime URLStartTime;
        public DateTime URLEndTime;
    }

    public struct ListOfAudio
    {
        public string ProcessName;
        public string ProcessWindow;
        public string ProcessPath;
        public DateTime ProcessStartTime;
        public DateTime ProcessEndTime;
        public string FileName;
        public string FileSize;
    }
    
}
