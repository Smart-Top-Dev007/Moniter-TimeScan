using Microsoft.Win32;
using PatchService.Globals;
using PatchService.Model;
using PatchService.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;
using System.Xml.Linq;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Timers;
using Path = System.IO.Path;
using PatchService.Logger;
using MySql.Data.MySqlClient;
using System.Management;
using System.Text.RegularExpressions;

namespace PatchService.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog m_clientFileDialog;
        private OpenFileDialog m_serverFileDialog;
        private OpenFileDialog m_openFileDialog;
        private string g_clientFileName;
        private string g_serverFileName;
        private string g_FileName;
        private string safe_clientFileName;
        private string safe_serverFileName;
        private string safe_FIleName;
        private string g_clientVersion;
        private string g_serverVersion;
        private byte[] g_clientBuffer;
        private byte[] g_serverBuffer;
        private Socket g_serverSocket;
        private Socket g_clientSocket;
        private Socket g_arpSocket;
        private Socket InspectSocket;
        private Thread InspectThread;


        public List<string> arptable = new List<string>();
        public List<string> arpInfo = new List<string>();
        public DateTime fileTime = DateTime.Now;
        public string opState = "";
        public string opFloor = "";
        public bool arpFlag = false;
        public string dbError = "";

        public int ServerPatchCount = 0;
        public int ClientPatchCount = 0;
        public bool bServer = false;
        public bool bClient = false;

        public string nFileIndex = "";

        public int nFilePort = Constants.ServerFilePort;
        public string g_startIP;
        public string g_endIP;
        private int nPatchPort;
        private string g_Version;
        private IPAddress IP;
        public List<IPItem> myList = new List<IPItem>();
        Dictionary<string, Version> serverDictionary = new Dictionary<string, Version>();
        Dictionary<string, Version> clientDictionary = new Dictionary<string, Version>();

        //public Socket clientSock;
        public bool flag = false;
        private string nSeleted = "";
        public List<StateData> Statelist = new List<StateData>();
        public List<USBData> USBList = new List<USBData>();
        public List<FileData> FileList = new List<FileData>();
        IPAddressRange IPRange;
        public string g_FileDate = "";
        public string g_FileSpeed = "";
        public string g_FilePath = "";

        public List<InspectInfo> inspectData = new List<InspectInfo>();

        public List<string> IPList = new List<string>();

        System.Timers.Timer checkingDayTimer;
        string prevDate = "";

        private bool isCheckedConnection;

        private static Mutex _mutex;
        private static bool _createdNew;
        private RegistryKey clientKey = Registry.CurrentUser.OpenSubKey("Software", true);
        private RegistryKey serverKey = Registry.CurrentUser.OpenSubKey("Software", true);
        private string ClientPatchList = "";
        private string ServerPatchList = "";
        public MainWindow()
        {
            InitializeComponent();
            ClietnCancel.IsEnabled = false;
            ServerCancel.IsEnabled = false;
            g_clientFileName = "";
            g_serverFileName = "";
            g_clientVersion = "";
            g_serverVersion = "";
            safe_clientFileName = "";
            safe_serverFileName = "";
            g_Version = "";
            prevDate = "";
            IP = GetIPAddress();
            CreateDirectory("Patch");
            IPRange = new IPAddressRange(IPAddress.Parse("192.168.103.1"), IPAddress.Parse("192.168.120.255"));
            //IPRange = new IPAddressRange(IPAddress.Parse("192.168.103.51"), IPAddress.Parse("192.168.103.69"));

            //Set Register_Key.......
            //Set Register Current Version...
            //Set File Version.........
            //Set Patch Status...........

            // Client Setting
            var rck = clientKey.OpenSubKey(Constants.ClientPath);
            if (rck != null)
            {

                string ClientStatus = rck.GetValue("ClientPatchStatus").ToString();
                if (ClientStatus != "")
                {
                    g_clientVersion = rck.GetValue("ClientCurrentVersion").ToString();
                    g_clientFileName = rck.GetValue("ClientFilePath").ToString();
                    ClientVersion.Text = g_clientVersion;
                    ClientPath.Text = g_clientFileName;
                    ShowClientList();
                    if (ClientStatus == "set")
                    {
                        SetClient();
                    }

                }
            }
            else
            {
                var key = clientKey.CreateSubKey(Constants.ClientPath);
                key.SetValue("ClientCurrentVersion", "");
                key.SetValue("ClientFilePath", "");
                key.SetValue("ClientPatchStatus", "");
                key.Close();
            }
            // Server Setting
            var rsk = serverKey.OpenSubKey(Constants.ServerPath);
            if (rsk != null)
            {

                string ServerStatus = rsk.GetValue("ServerPatchStatus").ToString();
                if (ServerStatus != "")
                {
                    g_serverVersion = rsk.GetValue("ServerCurrentVersion").ToString();
                    g_serverFileName = rsk.GetValue("ServerFilePath").ToString();
                    ServerVersion.Text = g_serverVersion;
                    ServerPath.Text = g_serverFileName;
                    ShowServerList();
                    if (ServerStatus == "set")
                    {
                        SetServer();
                    }

                }
            }
            else
            {
                var key = serverKey.CreateSubKey(Constants.ServerPath);
                key.SetValue("ServerCurrentVersion", "");
                key.SetValue("ServerFilePath", "");
                key.SetValue("ServerPatchStatus", "");
                key.Close();
            }




            m_clientFileDialog = new OpenFileDialog()
            {
                Filter = "Executable files (*.exe)|*.exe",
                Title = "Select executable  file"
            };
            m_serverFileDialog = new OpenFileDialog()
            {
                Filter = "Executable files (*.exe)|*.exe",
                Title = "Select executable  file"
            };

            m_openFileDialog = new OpenFileDialog()
            {
                Filter = "Executable files (*.exe)|*.exe",
                Title = "Select executable  file"
            };

            var processes = FindProcess("PatchServiceAuto");
            if (processes.Length == 0)
            {
                try
                {
                    var proc = new Process();
                    //proc.StartInfo.FileName = Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public\\RM\\PatchService\\PatchService.exe";
                    //proc.StartInfo.FileName = "D:\\Ryongbong\\Ryonbong_Monitor\\PatchServiceAuto.exe";
                    proc.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\PatchServiceAuto.exe";
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
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


            title.Content = Constants.TitleVersion;

            LoadIPList();

            ThreadPool.SetMaxThreads(15, 20);
            ThreadPool.SetMinThreads(5, 5);


            InspectThread = new Thread(new ThreadStart(Inspect));
            InspectThread.Start();



            checkingDayTimer = new System.Timers.Timer(1000);
            checkingDayTimer.Elapsed += CheckingDayTimerCallback;
            checkingDayTimer.Start();






            //InitArpTable();
            //UpdateArpTable();
            //CreateArpSocket();





            //System.Timers.Timer timer = new System.Timers.Timer(999 * 60 * 5);///*****
            //timer.Elapsed += AutoUpdateArpTable;
            //timer.AutoReset = true;
            //timer.Enabled = true;


            //FilePort.SelectedIndex = 1;
            if (!File.Exists(Constants.USBFileName))
            {
                File.Create(Constants.USBFileName);
            }
            Thread.Sleep(1000);
            try
            {
                USBList.Clear();

                string[] datas = File.ReadAllLines(Path.Combine(Constants.USBFileName));
                foreach (var dataline in datas)
                {
                    string[] data = dataline.Split('*');
                    if (!data[0].Contains("."))
                    {
                        USBData item = new USBData
                        {
                            UsbType = data[0],
                            Status = data[1],
                            UserIP = data[2],
                            Device = data[3],
                            Guid = data[4],
                            CreatedAt = data[5]

                        };

                        if (item.CreatedAt.Contains(DateTime.Now.ToString("MM/dd/yyyy")))
                            USBList.Add(item);
                    }

                }

                InsertUSBHistory(USBList);

                //USBDataGrid.Items.Clear();
                //int nNo = 1;
                //foreach (var usb in USBList.OrderByDescending(x => x.CreatedAt))
                //{
                //    if ( usb.UsbType != "KeyBoard")
                //        USBDataGrid.Items.Add(new USBData { No = nNo++, ImagePath = "pack://application:,,,/Resource/" + usb.UsbType + ".png", UsbType = usb.UsbType, Status = usb.Status, UserIP = usb.UserIP, Device = usb.Device, Guid = usb.Guid, CreatedAt = usb.CreatedAt });
                //}
            }
            catch (Exception ex)
            {

            }

            if (!File.Exists(Constants.FileCaptureName))
            {
                File.Create(Constants.FileCaptureName);
            }
            Thread.Sleep(1000);

            try
            {

                using (StreamReader reader = new StreamReader(Constants.FileCaptureName))
                {
                    if (reader.ReadLine() == null || reader.ReadLine() == "")
                    {
                        return;
                    }
                    string[] data = reader.ReadLine().Split('*');
                    FileData item = new FileData
                    {
                        Guid = data[0],
                        UserIP = data[1],
                        Speed = data[2],
                        FilePath = data[3],
                        CreatedAt = data[4]
                    };
                    FileList.Add(item);
                    //FileDownLoad.Items.Add(item);
                }

            }
            catch
            {

            }

            
            
           

            

        }

        private void ShowClientList()
        {
            string path = Constants.PatchPath + "client_" + g_clientVersion + ".txt";
            CreateFile(path);
            string line = "";
            using(var reader = new System.IO.StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    try
                    {
                        string UserIP = line.Split('|')[0];
                        string createAt = line.Split('|')[1];
                        Version version = new Version()
                        {
                            Status = g_clientVersion,
                            CreatedAt = createAt,
                            UserIP = UserIP
                        };

                        ShowClientDic(version);
                        
                        //version.Cr
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void ShowServerList()
        {
            string path = Constants.PatchPath + "server_" + g_serverVersion + ".txt";
            string line = "";
            CreateFile(path);
            using (var reader = new System.IO.StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    try
                    {
                        string UserIP = line.Split('|')[0];
                        string createAt = line.Split('|')[1];
                        Version version = new Version()
                        {
                            Status = g_serverVersion,
                            CreatedAt = createAt,
                            UserIP = UserIP
                        };

                        ShowServerDic(version);

                        //version.Cr
                    }
                    catch
                    {

                    }
                }
            }
        }

        public static Process[] FindProcess(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);

            return processList;
        }
        private void CheckingDayTimerCallback(object sender, ElapsedEventArgs e)
        {
            DateTime curTime = DateTime.Now;

            string curDate = string.Format("{0}-{1}-{2}", curTime.Year, curTime.Month, curTime.Day);

            if (prevDate != "" && prevDate != curDate)
            {
                DateTime yesterday = curTime.AddDays(-1);
                string strDate = string.Format("{0}-{1}-{2}", yesterday.Year, yesterday.Month, yesterday.Day);

                LoadInspect(strDate, true);
                SaveIPList();

                InitializeInspectFile(curDate);

                DisplayTodayData();
            }

            if (USBDataGrid.Items.Count > 500)
            {
                USBDataGrid.Items.Clear();
                USBList.Clear();
                //InsertUSBHistory(USBList);
            }
        }

        private void LoadIPList()
        {
            string strFilePath = Constants.InspectDirectory + "\\" + Constants.IPListFile;
            if (!File.Exists(strFilePath))
            {
                return;
            }
            while (true)
            {
                try
                {
                    IPList = File.ReadAllLines(strFilePath).ToList();
                    break;
                }
                catch
                {

                }
            }

        }

        private void SaveIPList()
        {
            string strPath = Constants.InspectDirectory + "\\" + Constants.IPListFile;
            while (true)
            {
                try
                {
                    File.WriteAllLines(strPath, IPList.ToArray());
                    break;
                }
                catch
                {

                }
            }
        }

        private List<InspectInfo> LoadInspect(string strDate, bool checkingIP = false)
        {
            if (strDate == "")
            {
                DateTime curTime = DateTime.Now;

                strDate = string.Format("{0}-{1}-{2}", curTime.Year, curTime.Month, curTime.Day);
            }
            List<InspectInfo> res = new List<InspectInfo>();

            List<string> checkedIPList = new List<string>();

            string strFilePath = Constants.InspectDirectory + "\\" + strDate + ".lib";

            if (!File.Exists(strFilePath))
            {
                if (checkingIP)
                {
                    IPList = new List<string>(checkedIPList);
                }
                return res;
            }

            while (true)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(strFilePath))
                    {


                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line == "")
                            {
                                continue;
                            }
                            try
                            {

                                string[] data = line.Split('*');
                                int imgId = Int32.Parse(data[1].Substring(0, data[1].Length - 1)) / 10 * 10;

                                //if (!IPList.Contains(data[0]))
                                //{
                                //    IPList.Add(data[0]);
                                //}

                                string[] rateData = data[2].Split('/');
                                int rateId = 0;

                                int readCnt = 0, totCnt = 0;

                                //rateData[0] = rateData[0].Trim();
                                //rateData[1] = rateData[1].Trim();

                                if (rateData[1] != "-" && rateData[1] != "0")
                                {

                                    if (Int32.TryParse(rateData[0], out readCnt) && Int32.TryParse(rateData[1], out totCnt))
                                    {
                                        if (totCnt > 0)
                                        {
                                            rateId = (readCnt * 100) / totCnt / 10 * 10;
                                        }

                                        if (rateId > 100)
                                        {
                                            rateId = 100;
                                        }

                                    }
                                }

                                if (imgId > 100)
                                {
                                    imgId = 100;
                                }

                                if (checkingIP && totCnt != 0)
                                {
                                    checkedIPList.Add(data[0]);
                                }

                                InspectInfo element = new InspectInfo
                                {
                                    IP = data[0],
                                    ImgPath = "pack://application:,,,/Resource/" + imgId + ".png",
                                    Percentage = data[1],
                                    NumberImgPath = "pack://application:,,,/Resource/" + rateId + ".png",
                                    Rate = readCnt.ToString() + "  /  " + totCnt.ToString()
                                };
                                res.Add(element);
                            }
                            catch
                            {

                            }
                        }



                    }

                    break;
                }
                catch
                {
                    res.Clear();
                }


            }

            if (checkingIP)
            {
                IPList = new List<string>(checkedIPList);
            }

            return res;
        }

        private void InitializeInspectFile(string strDate)
        {
            string strFilePath = Constants.InspectDirectory + "\\" + strDate + ".lib";


            if (!File.Exists(strFilePath))
            {
                while (true)
                {
                    try
                    {
                        using (TextWriter tw = new StreamWriter(strFilePath))
                        {
                            foreach (string IP in IPList)
                            {
                                tw.WriteLine(IP + "*0%*0/-");
                            }
                            tw.Close();
                        }
                        break;
                    }
                    catch
                    {

                    }
                }


            }
        }

        private void UpdateInspect(string data, string updateIP)
        {
            if (!Directory.Exists(Constants.InspectDirectory))
            {
                Directory.CreateDirectory(Constants.InspectDirectory);
            }

            if (!IPList.Contains(updateIP))
            {
                IPList.Add(updateIP);

                SaveIPList();


            }

            string[] dataTemp = data.Split('*');


            InitializeInspectFile(dataTemp[0]);

            List<InspectInfo> oldInspect = LoadInspect(dataTemp[0]);
            bool found = false;

            string strFilePath = Constants.InspectDirectory + "\\" + dataTemp[0] + ".lib";

            while (true)
            {
                try
                {
                    using (TextWriter tw = new StreamWriter(strFilePath))
                    {
                        foreach (InspectInfo element in oldInspect)
                        {
                            if (element.IP == updateIP)
                            {
                                found = true;
                                tw.WriteLine(updateIP + "*" + dataTemp[1] + "*" + dataTemp[2]);



                                string[] rateData = dataTemp[2].Split('/');
                                int rateId = 0;

                                int readCnt = 0, totCnt = 0;

                                if (rateData[1] != "-" && rateData[1] != "0")
                                {

                                    if (Int32.TryParse(rateData[0], out readCnt) && Int32.TryParse(rateData[1], out totCnt))
                                    {
                                        if (totCnt > 0)
                                        {
                                            rateId = (readCnt * 100) / totCnt / 10 * 10;
                                        }

                                    }
                                }


                                int imgId = Int32.Parse(dataTemp[1].Substring(0, dataTemp[1].Length - 1)) / 10 * 10;

                                if (rateId > 100)
                                {
                                    rateId = 100;
                                }

                                element.NumberImgPath = "pack://application:,,,/Resource/" + rateId + ".png";
                                element.Percentage = dataTemp[1];
                                element.Rate = dataTemp[2];
                            }
                            else
                            {
                                tw.WriteLine(element.IP + "*" + element.Percentage + "*" + element.Rate);
                            }
                        }

                        if (!found)
                        {
                            tw.WriteLine(updateIP + "*" + dataTemp[1] + "*" + dataTemp[2]);

                            string[] rateData = dataTemp[2].Split('/');
                            int rateId = 0;

                            int readCnt = 0, totCnt = 0;

                            if (rateData[1] != "-" && rateData[1] != "0")
                            {

                                if (Int32.TryParse(rateData[0], out readCnt) && Int32.TryParse(rateData[1], out totCnt))
                                {
                                    if (totCnt > 0)
                                    {
                                        rateId = (readCnt * 100) / totCnt / 10 * 10;
                                    }

                                }
                            }


                            int imgId = Int32.Parse(dataTemp[1].Substring(0, dataTemp[1].Length - 1)) / 10 * 10;
                            InspectInfo element = new InspectInfo
                            {
                                IP = dataTemp[0],
                                ImgPath = "pack://application:,,,/Resource/" + imgId + ".png",
                                Percentage = dataTemp[1],
                                NumberImgPath = "pack://application:,,,/Resource/" + rateId + ".png",
                                Rate = readCnt.ToString() + "  /  " + totCnt.ToString()
                            };

                            oldInspect.Add(element);
                        }
                        tw.Close();
                    }
                    break;
                }
                catch
                {

                }
            }

            Dispatcher.Invoke(() =>
            {
                DateTime curDate = (DateTime)(date_Picker.SelectedDate);
                string strCurDate = curDate.Year.ToString() + "-" + curDate.Month.ToString() + "-" + curDate.Day.ToString();

                if (strCurDate == dataTemp[0])
                {
                    inspectData = oldInspect;
                    DisplayInspect(strCurDate);
                }
            }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        private void Inspect()
        {
            IPAddress OwnerIP = GetIPAddress();
            IPEndPoint endPoint = new IPEndPoint(OwnerIP, Constants.InspectPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(endPoint);
                socket.Listen(10);
                while (true)
                {
                    Socket clientsocket = socket.Accept();
                    Thread thread = new Thread(new ParameterizedThreadStart(InspectRecv));
                    thread.Start(clientsocket);
                }


            }
            catch (Exception ex)
            {
                socket.Dispose();

            }
        }

        private void InspectRecv(object obj)
        {
            byte[] recvData = new byte[512];
            Socket clientsocket = (Socket)obj;

            while (true)
            {
                try
                {
                    int byteData = clientsocket.Receive(recvData);
                    string strData = Encoding.UTF8.GetString(recvData, 0, byteData);

                    UpdateInspect(strData, GetIPAddressOfClient(clientsocket));

                    break;
                }
                catch
                {

                }
                break;

            }
        }



        #region ArpTable
        private bool GetArpInfoFromMySql()
        {
            MySqlConnection conn = new MySqlConnection(Constants.MysqlConString);
            try
            {
                dbError = "";
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT ip_address FROM real_time WHERE capture_time = (SELECT MAX(capture_time) FROM real_time) ORDER BY ip_address ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string ipaddress = ((string)rdr[0]);
                    arpInfo.Add(ipaddress);
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                dbError = ex.ToString();
                //MessageBox.Show(dbError);
                //using (StreamWriter sw = File.AppendText("log.txt"))            /// last will change this part ***
                //{
                //    sw.WriteLine(dbError);
                //}
                return false;
            }


            File.WriteAllLines("database.txt", arpInfo, Encoding.UTF8);

            return true;
        }

        private void InitArpTable()
        {

            if (!File.Exists(Constants.ArpTablePath))
            {
                bool database = GetArpInfoFromMySql();
                if (database) arpInfo.ForEach(x => arptable.Add($"{x}#....#....#....#....#....#....#{DateTime.Now}#{new DateTime(2021, 1, 1)}"));
            }
            else
            {
                arptable = File.ReadAllLines(Constants.ArpTablePath, Encoding.UTF8).ToList();
            }

        }
        private void AutoUpdateArpTable(object sender, ElapsedEventArgs e)
        {
            UpdateArpTable();
        }

        private void UpdateArpTable()
        {
            bool database = GetArpInfoFromMySql();
            if (database == false) return;


            if (arpFlag) return;

            int i = 0;
            arpFlag = true;
            while (true)
            {
                if (i >= arptable.Count) break;

                string[] strTemp = arptable[i].Split('#');
                if (DateTime.Now.Subtract(DateTime.Parse(strTemp[7])).TotalSeconds > Constants.ArpDifferMin * 60)
                {
                    arptable.RemoveAt(i);
                    continue;
                }

                int pos = arptable.FindIndex(x => x.IndexOf($"{strTemp[0]}#{strTemp[1]}") > -1);
                if (pos != i)
                {
                    arptable.RemoveAt(i);
                    continue;

                }
                int index = arpInfo.FindIndex(x => x.IndexOf(arptable[i].Split('#')[0]) > -1);

                if (index > -1) arpInfo.RemoveAt(index);
                i++;
            }
            foreach (string arpClient in arpInfo)
            {
                if (arptable.Any(x => x.IndexOf(arpClient) > -1)) continue;

                string arpTemp = $"{arpClient}#....#....#....#....#....#....#{DateTime.Now}#{new DateTime(2021, 1, 2)}";
                arptable.Add(arpTemp);
            }

            File.WriteAllLines(Constants.ArpTablePath, arptable, Encoding.UTF8);
            if (flagFloor)
            {
            h3: Thread.Sleep(500);
                if (flagFloor) goto h3;
            }
            flagFloor = true;
            ShowTableToUI();
            flagFloor = false;

            arpFlag = false;
        }


        private void CreateArpSocket()
        {
            arpFlag = false;
            flagFloor = false;

            IPEndPoint endPoint = new IPEndPoint(IP, Constants.ArpInfoPort);
            g_arpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                g_arpSocket.Bind(endPoint);
                g_arpSocket.Listen(1000);
                arpFlag = false;

                Thread arpThread = new Thread(new ThreadStart(ArpThreadFunc));
                arpThread.Start();
            }
            catch (Exception ex)
            {
                g_arpSocket.Close();
                g_arpSocket.Dispose();
                Thread temp = new Thread(new ThreadStart(RecreateGThread));
                temp.Start();
            }
        }

        private void RecreateGThread()
        {
            CreateArpSocket();
        }

        private void ArpThreadFunc()
        {
            while (true)
            {
                Socket socket = g_arpSocket.Accept();
                byte[] dataReceive = new byte[1024 * 1024];
                int size = socket.Receive(dataReceive);
                if (size > 0)
                {
                    string data = Encoding.UTF8.GetString(dataReceive, 0, size);
                    //string data1 = "";
                    //if (data.IndexOf("&") > -1)
                    //{
                    //    data1 = data.Split('&')[1];
                    //    data = data.Split('&')[0];

                    //}
                    if (data.IndexOf("192.168.") == -1) continue;

                    using (StreamWriter sw = File.AppendText("Log_Service.txt"))
                    {
                        sw.WriteLine(data + "---" + DateTime.Now.ToString());
                    }

                    Thread temp = new Thread(new ParameterizedThreadStart(arpUpdateThreadFunc));
                    temp.Start(data);

                }
                socket.Dispose();
            }
        }
        List<string> frequencyOfArp = new List<string>();
        private void arpUpdateThreadFunc(object obj)
        {
            string data = (string)obj;
            string data1 = "";
            if (data.IndexOf("&") > -1)
            {
                data1 = data.Split('&')[1];
                data = data.Split('&')[0];
            }
            if (data.IndexOf("*") > -1)
            {
                string[] strTemp = data.Split('*');

                if (arpFlag) return;
                arpFlag = true;

                int index = arptable.FindIndex(x => x.IndexOf($"{strTemp[0]}#....") > -1);
                if (index > -1)
                {
                    arptable.RemoveAt(index);
                }
                index = arptable.FindIndex(x => x.IndexOf($"{strTemp[0]}#{strTemp[1]}") > -1);
                string arpClient = string.Join("#", strTemp) + $"#{DateTime.Now}#{DateTime.Now}";

                if (index == -1) arptable.Add(arpClient);
                else arptable[index] = arpClient;

                //if (strTemp[1] == "server" && data1.Length>0 )
                //{
                //    string[] strArpTemp = data1.Split(';');

                //    string prefixOfIP = strTemp[0].Substring(0, strTemp[0].LastIndexOf("."));
                //    int freqIndex = frequencyOfArp.FindIndex(x => x.IndexOf(prefixOfIP) > -1);
                //    if (freqIndex > -1)
                //    {
                //        DateTime oldDate = Convert.ToDateTime(frequencyOfArp[freqIndex].Split('#')[1]);
                //        if (DateTime.Compare(oldDate.AddMinutes(1), DateTime.Now) < 0)
                //        {
                //            frequencyOfArp[freqIndex] = prefixOfIP + "#" + DateTime.Now.ToString();
                //            arpInfo.RemoveAll(x => x.IndexOf(prefixOfIP) > -1);
                //            for (int i = 0; i < strArpTemp.Length; i++)
                //            {
                //                arpInfo.Add(strArpTemp[i].Split('#')[0]);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        for (int i = 0; i < strArpTemp.Length; i++)
                //        {
                //            arpInfo.Add(strArpTemp[i].Split('#')[0]);
                //        }
                //        frequencyOfArp.Add(prefixOfIP + "#" + DateTime.Now.ToString());
                //    }
                //}
                arpFlag = false;
            }
        }

        private void ShowTableToUI()
        {

            if (arptable == null || arptable.Count == 0) return;

            List<string> listInstalled = arptable.Where(x => DateTime.Now.Subtract(DateTime.Parse(x.Split('#')[8])).TotalMinutes < Constants.ArpRealMin).ToList();
            List<string> listUninstalled = arptable.Where(
                    x => DateTime.Now.Subtract(DateTime.Parse(x.Split('#')[8])).TotalMinutes >= Constants.ArpRealMin
                    && x.IndexOf("server") == -1
                ).ToList();

            List<string> servers = arptable.Where(x => x.IndexOf("server") > -1).ToList();
            listUninstalled.AddRange(
                servers.Where(
                    x => DateTime.Now.Subtract(DateTime.Parse(x.Split('#')[8])).TotalMinutes < Constants.ArpRealMin &&
                    !arptable.Any(xx => xx.IndexOf(x.Split('#')[0] + "#client") > -1)
                ).ToList());

            this.Dispatcher.Invoke(() =>
            {
                nTotal.Content = $"All Users: {listInstalled.Count}";
                nServer.Content = $"Servers: {listInstalled.Count(x => x.Split('#')[1] == "server")}";
                nClients.Content = $"Clients: {listInstalled.Count(x => x.Split('#')[1] == "client")}";
            });

            listInstalled = listInstalled.Where(x => x.Split('#')[0].Split('.')[2].IndexOf(opFloor) > -1).ToList();
            listUninstalled = listUninstalled.Where(x => x.Split('#')[0].Split('.')[2].IndexOf(opFloor) > -1).ToList();
            listInstalled = listInstalled.Where(x => x.Split('#')[1].IndexOf(opState) > -1).ToList();

            this.Dispatcher.Invoke(() =>
            {
                int i = 0;
                while (true)
                {
                    if (i >= StateDataGrid.Items.Count) break;
                    ArpClientInfo arp = (ArpClientInfo)StateDataGrid.Items[i];
                    if (listInstalled.Where(x => x.IndexOf(arp.Ip + "#" + arp.Server) > -1).Any())
                    {
                        int index = listInstalled.FindIndex(x => x.IndexOf(arp.Ip + "#" + arp.Server) > -1);
                        string[] strtemp = listInstalled[index].Split('#');
                        string[] ips = strtemp[0].Split('.');
                        string realIP = strtemp[0];
                        if (ips[3].Length == 1) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.00{ips[3]}";
                        if (ips[3].Length == 2) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.0{ips[3]}";

                        ((ArpClientInfo)StateDataGrid.Items[i]).Ip = realIP;
                        ((ArpClientInfo)StateDataGrid.Items[i]).Server = strtemp[1];
                        ((ArpClientInfo)StateDataGrid.Items[i]).SysCreated = strtemp[2];
                        ((ArpClientInfo)StateDataGrid.Items[i]).BootOs = strtemp[3];
                        ((ArpClientInfo)StateDataGrid.Items[i]).Partition = strtemp[4];
                        ((ArpClientInfo)StateDataGrid.Items[i]).Hidden = strtemp[5];
                        ((ArpClientInfo)StateDataGrid.Items[i]).ComputerName = strtemp[6];
                        listInstalled.RemoveAt(index);
                        i++; continue;
                    }
                    StateDataGrid.Items.RemoveAt(i);
                }
                foreach (string strClient in listInstalled)
                {
                    string[] strtemp = strClient.Split('#');
                    string[] ips = strtemp[0].Split('.');
                    string realIP = strtemp[0];
                    if (ips[3].Length == 1) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.00{ips[3]}";
                    if (ips[3].Length == 2) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.0{ips[3]}";

                    StateDataGrid.Items.Add(new ArpClientInfo()
                    {
                        State = "Installed",
                        Ip = realIP,
                        Server = strtemp[1],
                        SysCreated = strtemp[2],
                        BootOs = strtemp[3],
                        Partition = strtemp[4],
                        Hidden = strtemp[5],
                        ComputerName = strtemp[6],
                        Color = strtemp[1] == "server" ? "#2DAAE1" : "#FEA500",
                    });
                }

                i = 0;
                while (true)
                {
                    if (i >= UnClients.Items.Count) break;
                    ArpClientInfo arp = (ArpClientInfo)UnClients.Items[i];
                    if (listUninstalled.Where(x => x.IndexOf(arp.Ip) > -1).Any())
                    {

                        int index = listUninstalled.FindIndex(x => x.IndexOf(arp.Ip) > -1);
                        string[] strtemp = listUninstalled[index].Split('#');
                        string[] ips = strtemp[0].Split('.');
                        string realIP = strtemp[0];
                        if (ips[3].Length == 1) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.00{ips[3]}";
                        if (ips[3].Length == 2) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.0{ips[3]}";

                        ((ArpClientInfo)UnClients.Items[i]).Ip = realIP;
                        ((ArpClientInfo)UnClients.Items[i]).State = "Uninstalled";
                        listUninstalled.RemoveAt(index);
                        i++; continue;

                    }
                    UnClients.Items.RemoveAt(i);
                }

                foreach (string strClient in listUninstalled)
                {
                    string[] strtemp = strClient.Split('#');
                    string[] ips = strtemp[0].Split('.');
                    string realIP = strtemp[0];
                    if (ips[3].Length == 1) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.00{ips[3]}";
                    if (ips[3].Length == 2) realIP = $"{ips[0]}.{ips[1]}.{ips[2]}.0{ips[3]}";

                    UnClients.Items.Add(new ArpClientInfo()
                    {
                        State = "Uninstalled",
                        Ip = realIP,
                    });
                }

                nUninstall.Content = $"Uninstalled Clients: {UnClients.Items.Count}";
                StateDataGrid.Items.Refresh();
            });

            //if (DateTime.Now.Subtract(fileTime).TotalMinutes > 10)
            //{
            //    fileTime = DateTime.Now;
            //    using (TextWriter tw = new StreamWriter(Constants.ArpTablePath))
            //    {
            //        foreach (string str in arptable)
            //            tw.WriteLine(str);
            //    }
            //}
        }


        private void Export_Arp(object sender, RoutedEventArgs e)
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < UnClients.Items.Count; i++)
            {
                ArpClientInfo arp = (ArpClientInfo)UnClients.Items[i];
                temp.Add(arp.Ip);
            }
            temp.Sort();
            File.WriteAllLines("Report.txt", temp);
            Process.Start("Report.txt");
        }

        #endregion




        public static IPAddress GetIPAddress()
        {
            IPAddress m_IPAddress = null;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if (ni.Name.StartsWith("Ethernet"))
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                m_IPAddress = ip.Address;
                                break;
                            }
                        }
                    }

                }
            }
            return m_IPAddress;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Instance.DoLog(string.Format("Loaded Start"), Log.LogType.Error);
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0, UseAeroCaptionButtons = false });
            Thread thread = new Thread(new ThreadStart(UsbDetectThread));
            thread.Start();
            Log.Instance.DoLog(string.Format("Loaded End"), Log.LogType.Error);
        }

        private void UsbDetectThread()
        {
            Log.Instance.DoLog(string.Format("Usb Detect Thread"), Log.LogType.Error);
            IPEndPoint endPoint = new IPEndPoint(IP, Constants.UsbPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(endPoint);
                socket.Listen(10);
                while (true)
                {
                    Log.Instance.DoLog(string.Format("Start Thread"), Log.LogType.Error);
                    Socket clientsocket = socket.Accept();
                    Thread thread = new Thread(new ParameterizedThreadStart(UsbDetect));
                    thread.Start(clientsocket);
                    Log.Instance.DoLog(string.Format("Start Thread"), Log.LogType.Error);
                }
            }
            catch (Exception ex)
            {
                socket.Dispose();
                Log.Instance.DoLog(string.Format("Socket .\r\nException: {0}\r\n", ex), Log.LogType.Error);
            }
        }

        private void UsbDetect(object state)
        {
            Log.Instance.DoLog(string.Format("USB Detect Start"), Log.LogType.Error);
            Socket socket = (Socket)state;
            try
            {
                byte[] receiveData = new byte[1024];
                int length = socket.Receive(receiveData);
                string prefix = Encoding.UTF8.GetString(receiveData, 0, 4);
                byte[] temp = new byte[length - 4];
                Array.Copy(receiveData, 4, temp, 0, length - 4);
                if (prefix == Constants.Re_UsbDetect)
                {
                    string UsbData = Encoding.UTF8.GetString(temp);
                    string[] UsbDetail = UsbData.Split('*');
                    if (!UsbDetail[0].Trim().Contains("."))
                    {
                        USBData usbInfo = new USBData
                        {
                            UsbType = UsbDetail[0].Trim(),
                            Status = UsbDetail[1].Trim(),
                            UserIP = UsbDetail[2].Trim(),
                            Device = UsbDetail[3].Trim(),
                            Guid = Guid.NewGuid().ToString(),
                            CreatedAt = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")
                        };
                        USBList.Add(usbInfo);
                        isCheckedConnection = true;

                        this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                        {
                            message_body.Background = new SolidColorBrush(Color.FromRgb(0, 200, 0));
                            //USBDataGrid.Items.Clear();

                            //int nNo = 1;
                            //foreach (var usb in USBList.OrderByDescending(x => x.CreatedAt))
                            //{
                            //    if ( usb.UsbType.Trim() != "KeyBoard")
                            //    {
                            //        USBDataGrid.Items.Add(new USBData { No = nNo++, ImagePath = "pack://application:,,,/Resource/" + usb.UsbType.Trim() + ".png", UsbType = usb.UsbType, Status = usb.Status, UserIP = usb.UserIP, Device = usb.Device, Guid = usb.Guid, CreatedAt = usb.CreatedAt });
                            //    }

                            //}

                            InsertUSBHistory(USBList);

                            using (StreamWriter sw = File.AppendText(Constants.USBFileName))
                            {
                                if (usbInfo.UsbType.Trim() != "KeyBoard")
                                {
                                    var strTemp = usbInfo.UsbType + "*" + usbInfo.Status + "*" + usbInfo.UserIP + "*" + usbInfo.Device + "*" + usbInfo.Guid + "*" + usbInfo.CreatedAt;
                                    sw.WriteLine(strTemp);

                                }

                            }
                        });
                    }


                }

            }
            catch (Exception ex)
            {
                Log.Instance.DoLog(string.Format("Socket Close : {0}", ex.Message), Log.LogType.Error);
                socket.Close();
            }

        }

        private void USBClearButton_Click(object sender, RoutedEventArgs e)
        {

            if ((USBDataGrid.SelectedItems.Count < 1))
            {
                CustomMsg msg = new CustomMsg("Please choose the row to delete.");
                return;
            }
            USBData data = USBDataGrid.SelectedItem as USBData;
            for (int i = 0; i < USBList.Count; i++)
            {
                if (USBList[i].Guid == data.Guid)
                {
                    USBList.RemoveAt(i);
                    break;
                }
            }

            USBDataGrid.Items.Clear();
            if (File.Exists(Constants.USBFileName))
            {
                File.WriteAllText(Constants.USBFileName, "");
                //File.Delete(Constants.USBFileName);
                //File.Create(Constants.USBFileName);
            }
            Thread.Sleep(1000);

            //int nNo = 1;
            //List<string> list = new List<string>();

            //foreach (var usb in USBList.OrderByDescending(x => x.CreatedAt))
            //{
            //    if (usb.UsbType.Trim() != "KeyBoard")
            //    {
            //        USBDataGrid.Items.Add(new USBData { No = nNo++, ImagePath = "pack://application:,,,/Resource/" + usb.UsbType.Trim() + ".png", UsbType = usb.UsbType, Status = usb.Status, UserIP = usb.UserIP, Device = usb.Device, Guid = usb.Guid, CreatedAt = usb.CreatedAt });

            //        var strTemp = usb.UsbType.Trim() + "*" + usb.Status.Trim() + "*" + usb.UserIP.Trim() + "*" + usb.Device.Trim() + "*" + usb.Guid.Trim() + "*" + usb.CreatedAt.Trim();
            //        list.Add(strTemp);
            //    }


            //}

            string[] strDatas = InsertUSBHistory(USBList).ToArray();

            try
            {
                File.WriteAllLines(Constants.USBFileName, strDatas);
            }
            catch
            {

            }


            //using (StreamWriter sw = File.AppendText(Constants.USBFileName))
            //{
            //    int nNo = 1;
            //    foreach (var usb in USBList.OrderByDescending(x => x.CreatedAt))
            //    {
            //        USBDataGrid.Items.Add(new USBData { No = nNo++, ImagePath = "pack://application:,,,/Resource/" + usb.UsbType.Trim() + ".png", UsbType = usb.UsbType, Status = usb.Status, UserIP = usb.UserIP, Device = usb.Device, Guid = usb.Guid, CreatedAt = usb.CreatedAt });

            //        var strTemp = usb.UsbType.Trim() + "*" + usb.Status.Trim() + "*" + usb.UserIP.Trim() + "*" + usb.Device.Trim() + "*" + usb.Guid.Trim() + "*" + usb.CreatedAt.Trim();
            //        sw.WriteLine(strTemp);

            //    }

            //}
        }

        private void USBAllClearButton_Click(object sender, RoutedEventArgs e)
        {
            USBList.Clear();
            USBDataGrid.Items.Clear();
            File.Delete(Constants.USBFileName);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaxmizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;


            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            EnvironmentHelper.ShutDown(true);
        }

        private void ServerFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_serverFileDialog.ShowDialog() == true)
            {
                g_serverFileName = m_serverFileDialog.FileName;
                safe_serverFileName = m_clientFileDialog.SafeFileName;
                ServerPath.Text = g_serverFileName;
            }
        }

        private void SetServer()
        {
            g_serverVersion = ServerVersion.Text;
            if (g_serverVersion == "" || g_serverFileName == "")
            {
                CustomMsg msg = new CustomMsg("Please Fill the Server Fields.");
                return;
            }

            if (System.IO.Path.GetFileName(g_serverFileName) == "ClientSetup.exe")
            {
                CustomMsg msg = new CustomMsg("Please Select the Correct File.");
                return;
            }
            IPEndPoint endPoint = new IPEndPoint(IP, Constants.ServerAutoPort);

            g_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ServerSet.IsEnabled = false;

            ServerCancel.IsEnabled = true;
            ServerPatchStatus.Content = "Started: " + DateTime.Now.ToString();
            ServerBorder.Background = new SolidColorBrush(Color.FromRgb(0, 139, 139));
            ServerVersion.IsEnabled = false;
            ServerFileButton.IsEnabled = false;
            ServerPath.IsEnabled = false;

            try
            {
                var key = serverKey.CreateSubKey(Constants.ServerPath);
                key.SetValue("ServerCurrentVersion", g_serverVersion);
                key.SetValue("ServerFilePath", g_serverFileName);
                key.SetValue("ServerPatchStatus", "set");
                key.Close();

                ServerPatchList = Constants.PatchPath + "server_" + g_serverVersion + ".txt";
                g_serverBuffer = File.ReadAllBytes(g_serverFileName);
                g_serverSocket.Bind(endPoint);
                g_serverSocket.Listen(1000);
                Thread server = new Thread(new ThreadStart(ServerThread));
                server.Start();
            }
            catch (Exception ex)
            {
                g_serverSocket.Dispose();
            }
        }

        private void ServerSet_Click(object sender, RoutedEventArgs e)
        {
            ServerPatchGrid.Items.Clear();
            SetServer();
        }

        private void ServerThread()
        {
            while (true)
            {
                try
                {
                    Socket socket = g_serverSocket.Accept();
                    if (bServer == true)
                    {
                        socket.Dispose();
                        continue;
                    }
                    if (IPRange.Contains(IPAddress.Parse(GetIPAddressOfClient(socket))))
                    {
                        bServer = true;
                        ServerFile(socket);
                    }
                    else
                    {
                        socket.Dispose();
                    }

                }
                catch
                {
                    g_serverSocket.Dispose();
                    break;
                }
            }
        }

        private void ServerFile(Socket socket)
        {
            byte[] dataReceive = new byte[100];
            try
            {
                int size = socket.Receive(dataReceive);
                string data = Encoding.UTF8.GetString(dataReceive, 0, size);

                string userIP = data.Split('*')[0];
                string version = data.Split('*')[1].TrimEnd();

                if (version == g_serverVersion)
                {
                    socket.Send(Encoding.UTF8.GetBytes(Constants.Se_Version));
                }
                else
                {
                    socket.Send(Encoding.UTF8.GetBytes(Constants.Se_AutoVersion + g_serverVersion));
                    Thread.Sleep(500);
                    socket.Send(g_serverBuffer);
                    Thread.Sleep(1000);
                    Version client = new Version
                    {
                        UserIP = GetIPAddressOfClient(socket),
                        Status = g_serverVersion,
                        CreatedAt = DateTime.Now.ToString()
                    };
                    SaveServerList(client);
                    ShowServerDic(client);
                    Thread.Sleep(500);
                }
                socket.Dispose();
            }
            catch
            {
                socket.Dispose();
            }
            bServer = false;

        }
        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public void CreateFile(string Path)
        {
            if (!File.Exists(Path))
            {
                File.Create(Path);
            }
        }
        private void SaveClientList(Version version)
        {
            CreateFile(ClientPatchList);
            File.AppendAllText(ClientPatchList, version.UserIP + "|" + version.CreatedAt + "\n");
            
          
        }

        public void SaveServerList(Version version)
        {
            CreateFile(ClientPatchList);
            File.AppendAllText(ServerPatchList, version.UserIP + "|" + version.CreatedAt + "\n");
        }
        private void ServerCancel_Click(object sender, RoutedEventArgs e)
        {
            //ClietnCancel.IsEnabled = false;
            ServerCancel.IsEnabled = false;
            bServer = false;
            // ClietnCancel.IsEnabled = true;
            ServerSet.IsEnabled = true;
            ServerPatchStatus.Content = "Started: ";
            ServerBorder.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ServerVersion.IsEnabled = true;
            ServerFileButton.IsEnabled = true;
            ServerPath.IsEnabled = true;
            try
            {
                var key = serverKey.CreateSubKey(Constants.ClientPath);
                key.SetValue("ServerPatchStatus", "cancel");
                key.Close();
                g_serverSocket.Dispose();
                
            }
            catch
            {
                g_serverSocket.Close();
            }
        }

        private void ClientFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_clientFileDialog.ShowDialog() == true)
            {
                g_clientFileName = m_clientFileDialog.FileName;
                safe_clientFileName = m_clientFileDialog.SafeFileName;
                ClientPath.Text = g_clientFileName;
            }
        }

        private void SetClient()
        {
            
            g_clientVersion = ClientVersion.Text;
            
            if (g_clientFileName == "" || g_clientVersion == "")
            {
                CustomMsg msg = new CustomMsg("Please Fill the Client Fields.");
                return;
            }
            if (System.IO.Path.GetFileName(g_clientFileName) == "ServerSetup.exe")
            {
                CustomMsg msg = new CustomMsg("Please Select the Correct File.");
                return;
            }
            ClientPatchStatus.Content = "Started:  " + DateTime.Now.ToString();
            IPEndPoint endPoint = new IPEndPoint(IP, Constants.ClientAutoPort);
            g_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClietnCancel.IsEnabled = true;
            //ServerCancel.IsEnabled = true;
            ClietnSet.IsEnabled = false;
            ClientBorder.Background = new SolidColorBrush(Color.FromRgb(0, 139, 139));
            ClientVersion.IsEnabled = false;
            ClientFileButton.IsEnabled = false;
            ClientPath.IsEnabled = false;
            //ServerSet.IsEnabled = false;
            try
            {


                //Set Register_Key.......
                //Set Register Current Version...
                //Set File Version.........
                //Set Patch Status...........
                var key = clientKey.CreateSubKey(Constants.ClientPath);
                key.SetValue("ClientCurrentVersion", g_clientVersion);
                key.SetValue("ClientFilePath", g_clientFileName);
                key.SetValue("ClientPatchStatus", "set");
                
                ClientPatchList = Constants.PatchPath + "client_" + g_clientVersion + ".txt";
                g_clientBuffer = File.ReadAllBytes(g_clientFileName);
                g_clientSocket.Bind(endPoint);
                g_clientSocket.Listen(1000);
                Thread client = new Thread(new ThreadStart(ClientThread));
                client.Start();
            }
            catch (Exception ex)
            {
                g_clientSocket.Close();
            }
        }
        private void ClietnSet_Click(object sender, RoutedEventArgs e)
        {
            ClientPatchGrid.Items.Clear();
            SetClient();


        }
        private void ClientThread()
        {
            while (true)
            {
                try
                {
                    Socket socket = g_clientSocket.Accept();

                    if (bClient == true)
                    {
                        socket.Dispose();
                        continue;
                    }
                    if (IPRange.Contains(IPAddress.Parse(GetIPAddressOfClient(socket))))
                    {
                        bClient = true;
                        ClientFile(socket);
                    }
                    else
                    {
                        socket.Dispose();
                    }

                }
                catch
                {
                    g_clientSocket.Dispose();
                    break;
                }
            }

        }
        private void ClientFile(Socket socket)
        {
            byte[] dataReceive = new byte[100];

            try
            {
                int size = socket.Receive(dataReceive);
                string data = Encoding.UTF8.GetString(dataReceive, 0, size);

                string userIP = data.Split('*')[0];
                string version = data.Split('*')[1];

                if (version == g_clientVersion)
                {
                    socket.Send(Encoding.UTF8.GetBytes(Constants.Se_Version + "same"));
                }
                else
                {
                    socket.Send(Encoding.UTF8.GetBytes(Constants.Se_AutoVersion + g_clientVersion));
                    Thread.Sleep(500);
                    socket.Send(g_clientBuffer);
                    Thread.Sleep(1000);
                    Version client = new Version
                    {
                        UserIP = GetIPAddressOfClient(socket),
                        Status = g_clientVersion,
                        CreatedAt = DateTime.Now.ToString()
                    };
                    SaveClientList(client);
                    ShowClientDic(client);
                    Thread.Sleep(500);
                }
                socket.Dispose();
            }
            catch
            {
                socket.Dispose();
            }
            bClient = false;
        }
        public string GetIPAddressOfClient(Socket socket)
        {
            return ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
        }
        public void ShowClientDic(Version item)
        {
            if (!clientDictionary.ContainsKey(item.UserIP))
            {
                //Add to write the file IPs.
                clientDictionary.Add(item.UserIP, item);
            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ClientPatchCount++;
                ClientPatchResult.Content = "Client Patch Result : " + clientDictionary.ToList().Count.ToString();
                ClientPatchGrid.Items.Add(item);

            });
        }

        public void ShowServerDic(Version item)
        {
            if (!serverDictionary.ContainsKey(item.UserIP))
            {
                //Add to write the file IPs.
                serverDictionary.Add(item.UserIP, item);
            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ServerPatchCount++;
                ServerPatchResult.Content = "Server Patch Result : " + serverDictionary.ToList().Count.ToString();
                ServerPatchGrid.Items.Add(item);
              

            });

        }
        private void ClietnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClietnCancel.IsEnabled = false;
            //ServerCancel.IsEnabled = false;
            ClietnSet.IsEnabled = true;
            ClientPatchStatus.Content = "Started: ";
            ClientBorder.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ClientVersion.IsEnabled = true;
            ClientFileButton.IsEnabled = true;
            ClientPath.IsEnabled = true;

           


            //ServerCancel.IsEnabled = true;
            try
            {
                var key = clientKey.CreateSubKey(Constants.ClientPath);
                key.SetValue("ClientPatchStatus", "cancel");
                key.Close();
                g_clientSocket.Dispose();
            }
            catch
            {
                g_clientSocket.Close();
            }

        }

        private void PatchBrowserFile_Click(object sender, RoutedEventArgs e)
        {
            if (m_openFileDialog.ShowDialog() == true)
            {
                g_FileName = m_openFileDialog.FileName;
                safe_FIleName = m_openFileDialog.SafeFileName;
                PatchFile.Text = g_FileName;

            }
        }

        private void Ping_Click(object sender, RoutedEventArgs e)
        {
            myList.Clear();
            flag = false;
            //allList.Clear();
            ValidList.ItemsSource = null;
            CheckButton.IsEnabled = true;
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            myList.Clear();
            CheckButton.IsEnabled = false;
            flag = true;
            //allList.Clear();
            ValidList.ItemsSource = null;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            string str_startIP = StartIP.Text;
            string str_endIP = EndIP.Text;

            if (str_startIP == "")
            {
                CustomMsg msg = new CustomMsg("Enter Start IP Address");
                return;
            }
            if (str_endIP == "")
            {
                CustomMsg msg = new CustomMsg("Enter End IP Address");
                return;
            }

            IPAddress start = IPAddress.Parse(str_startIP);
            IPAddress end = IPAddress.Parse(str_endIP);
            if (start.Address > end.Address)
            {
                CustomMsg msg = new CustomMsg("Please Insert the IP Address Correctly.");
                return;
            }
            CheckButton.IsEnabled = false;
            ValidList.ItemsSource = null;
            var item = new IPList(str_startIP, str_endIP);
            item.RunPingSweep_Async();
            Thread.Sleep(1000);
            myList.Clear();
            myList = item.GetRunIPAddress();

            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ValidList.ItemsSource = null;
                ValidList.ItemsSource = myList;
            });


            CheckButton.IsEnabled = true;
        }

        private void PatchButton_Click(object sender, RoutedEventArgs e)
        {
            g_Version = Version.Text;
            g_startIP = StartIP.Text;
            g_endIP = EndIP.Text;
            if (g_FileName == "")
            {
                CustomMsg msg = new CustomMsg("Please Select File");
                return;
            }
            if (g_startIP == "" || g_startIP == null)
            {
                CustomMsg msg = new CustomMsg("Enter Start IP Address here.");
                return;
            }

            if (g_endIP == "" || g_endIP == null)
            {
                CustomMsg msg = new CustomMsg("Enter End IP Address here.");
                return;
            }
            if (g_Version == "" || g_Version == "Enter Version here...")
            {
                CustomMsg msg = new CustomMsg("Please Enter Version");
                return;
            }
            if (flag == true)
            {
                myList.Clear();
                IPAddress start = IPAddress.Parse(g_startIP);
                IPAddress end = IPAddress.Parse(g_endIP);
                if (start.Address > end.Address)
                {
                    CustomMsg msg = new CustomMsg("Please Insert the IP Address Correctly.");
                    return;
                }
                var range = new IPAddressRange(start, end);
                foreach (var ip in range)
                {
                    IPItem temp = new IPItem { IPAddress = ip.ToString(), IsChecked = true, Status = "DisConnected", During = "00:00:00", IconImage = "pack://application:,,,/Resource/connect.png" };
                    myList.Add(temp);
                }
            }
            if (myList.Count == 0)
            {
                CustomMsg msg = new CustomMsg("Please Check IP");
                return;
            }
            string path = g_FileName.Replace("\\", "/");
            //ZeroPatchProc zeroPatch = new ZeroPatchProc(path, myList, nPatchPort,g_Version);
            //zeroPatch.StartUp();
            Thread thread = new Thread(new ThreadStart(AllocThread));
            thread.Start();
        }
        public byte[] byteSend;
        private void AllocThread()
        {
            try
            {
                string path = g_FileName.Replace("\\", "/");
                byteSend = File.ReadAllBytes(path);

                Stopwatch total = new Stopwatch();
                total.Start();
                foreach (var item in myList)
                {
                    if (item.IsChecked == true)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(SendFile), item);
                        //Thread thread = new Thread(new ParameterizedThreadStart(SendFile));
                        //thread.Start(item);
                        Thread.Sleep(3000);
                    }
                }
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    ValidList.ItemsSource = null;
                    ValidList.ItemsSource = myList;
                });

                Thread.Sleep(1000);
                total.Stop();
                var count = myList.Where(m => m.Status == "Patch Success").Count();
                IPItem total_time = new IPItem { IPAddress = "Total count : " + myList.Count.ToString(), During = "Total time : " + (total.ElapsedMilliseconds / 1000).ToString() + "s", IsChecked = true, Status = "Success : " + count.ToString(), IconImage = "pack://application:,,,/Resource/success.png" };
                myList.Add(total_time);
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    ValidList.ItemsSource = null;
                    ValidList.ItemsSource = myList;
                });
            }
            catch
            {

            }
        }

        private void SendFile(object state)
        {
            IPItem item = (IPItem)state;
            var ip = IPAddress.Parse(item.IPAddress);
            byte[] recvBuff = new byte[100];
            IPEndPoint endPoint = new IPEndPoint(ip, nPatchPort);
            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Stopwatch time = new Stopwatch();
            time.Start();
            try
            {


                clientSock.Connect(endPoint);

                byte[] sendVersion = Encoding.ASCII.GetBytes(Constants.Se_Version + g_Version.Trim());
                clientSock.Send(sendVersion);
                while (true)
                {
                    int dataByte = clientSock.Receive(recvBuff, SocketFlags.None);
                    string vcheck = Encoding.UTF8.GetString(recvBuff, 0, dataByte);
                    if (vcheck == Constants.Re_VersionSame)
                    {

                        item.Status = "Installed";
                        time.Stop();
                        var druation = time.ElapsedMilliseconds;
                        if (druation > 1000)
                        {
                            druation = druation / 1000;
                            item.During = druation.ToString() + "s";
                        }
                        item.During = druation.ToString() + "ms";
                        item.IconImage = "pack://application:,,,/Resource/same.png";
                        //ipList.Add(item);
                        clientSock.Close();

                        Console.WriteLine(ip + ":Same");
                        break;
                    }
                    else
                    {

                        clientSock.Send(byteSend);
                        clientSock.Close();

                        item.Status = "Patch Success";
                        time.Stop();
                        var druation = time.ElapsedMilliseconds;
                        if (druation > 1000)
                        {
                            druation = druation / 1000;
                            item.During = druation.ToString() + "s";
                        }
                        item.During = druation.ToString() + "ms";
                        item.IconImage = "pack://application:,,,/Resource/success.png";
                        Console.WriteLine(ip + ":Success");
                        break;
                        //ipList.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {

                clientSock.Close();
                item.Status = "Patch Failure";
                time.Stop();
                var druation = time.ElapsedMilliseconds;
                if (druation > 1000)
                {
                    druation = druation / 1000;
                    item.During = druation.ToString() + "s";
                }
                item.During = druation.ToString() + "ms";
                item.IconImage = "pack://application:,,,/Resource/fail.png";
                Console.WriteLine(ip + ":Fail");
                Console.WriteLine(ip + ":" + ex.Message);
            }
            Thread.Sleep(500);
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ValidList.ItemsSource = null;
                ValidList.ItemsSource = myList;
            });
            Thread.Sleep(300);
        }

        private void Port_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Port.SelectedIndex == 0)
            {
                nPatchPort = Constants.ClientZeroPort;
            }
            else
            {
                nPatchPort = Constants.ServerZeroPort;
            }
        }
        public DateTime startDate, endDate;
        
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }
        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }
        

        private void FileDownLoad_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void FileCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void Thread_Stop(Thread thread)
        {
            if (thread.IsAlive)
            {
                try
                {

                    thread.Abort();

                }
                catch (Exception ex)
                {
                    thread.Abort();

                }

            }
        }

        public bool flagFloor = false;
        private void cbFitlerClick(object sender, SelectionChangedEventArgs e)
        {
            string temp = ((ComboBoxItem)filter.SelectedItem).Content.ToString().ToLower();
            if (temp == "all") opState = "";
            else opState = temp;
            if (flagFloor)
            {
            h1: Thread.Sleep(500);
                if (flagFloor) goto h1;
            }
            flagFloor = true;
            ShowTableToUI();
            flagFloor = false;
        }

        private void cbFloorClick(object sender, SelectionChangedEventArgs e)
        {
            string temp = ((ComboBoxItem)floor.SelectedItem).Content.ToString().ToLower();
            if (temp == "all") opFloor = "";
            if (temp == "2th3 floor") opFloor = "103";
            if (temp == "2th4 floor") opFloor = "104";
            if (temp == "2th5 floor") opFloor = "105";
            if (temp == "1th7 floor") opFloor = "107";
            if (temp == "1th8 floor") opFloor = "108";
            if (temp == "1th9 floor") opFloor = "109";
            if (temp == "3th1 floor") opFloor = "111";
            if (temp == "3th2 floor") opFloor = "112";
            if (temp == "3th3 floor") opFloor = "113";
            if (temp == "3th4 floor") opFloor = "114";
            if (temp == "3th5 floor") opFloor = "115";

            if (flagFloor)
            {
            h2: Thread.Sleep(500);
                if (flagFloor) goto h2;
            }
            flagFloor = true;
            ShowTableToUI();
            flagFloor = false;
        }

        private void OnSelectedDateChaged(object sender, SelectionChangedEventArgs e)
        {
            DateTime tempDate = (DateTime)date_Picker.SelectedDate;

            string strDate = string.Format("{0}-{1}-{2}", tempDate.Year, tempDate.Month, tempDate.Day);
            DisplayInspect(strDate);

        }

        private void DisplayInspect(string strDate)
        {

            inspectData = LoadInspect(strDate);

            Dispatcher.Invoke(() =>
            {
                InspectDataGrid.Items.Clear();

                int cntServer = 0;

                foreach (InspectInfo element in inspectData)
                {
                    int imgId = Int32.Parse(element.Percentage.Substring(0, element.Percentage.Length - 1)) / 10 * 10;

                    if (imgId > 100)
                    {
                        imgId = 100;
                    }

                    InspectDataGrid.Items.Add(new InspectInfo()
                    {
                        IP = element.IP,
                        ImgPath = "pack://application:,,,/Resource/" + imgId + ".png",
                        Percentage = element.Percentage,
                        NumberImgPath = element.NumberImgPath,
                        Rate = element.Rate
                    });

                    cntServer++;
                }

                strCntServer.Content = "Servers : " + cntServer;
            }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        private void ClickInspectTab(object sender, MouseButtonEventArgs e)
        {
            if (date_Picker.SelectedDate == null)
            {
                DateTime curTime = DateTime.Now;
                DateTime yesterday = curTime.AddDays(-1);
                string strDate = string.Format("{0}-{1}-{2}", yesterday.Year, yesterday.Month, yesterday.Day);

                LoadInspect(strDate, true);
                SaveIPList();

                DisplayTodayData();
            }

        }

        private void DisplayTodayData()
        {

            Dispatcher.Invoke(() =>
            {
                DateTime curDate = DateTime.Now;
                date_Picker.SelectedDate = curDate;

                string strdate = string.Format("{0}-{1}-{2}", curDate.Year, curDate.Month, curDate.Day);

                prevDate = strdate;

                DisplayInspect(strdate);
            }, System.Windows.Threading.DispatcherPriority.Normal);

        }

        private void OnClickToday(object sender, RoutedEventArgs e)
        {
            DisplayTodayData();
        }

        private void OnClickPrevDayButton(object sender, MouseButtonEventArgs e)
        {
            DateTime selectedDate = (DateTime)date_Picker.SelectedDate;

            date_Picker.SelectedDate = selectedDate = selectedDate.AddDays(-1);

            string strdate = string.Format("{0}-{1}-{2}", selectedDate.Year, selectedDate.Month, selectedDate.Day);
            DisplayInspect(strdate);
        }

        private void OnClickForwardDayButton(object sender, MouseButtonEventArgs e)
        {
            DateTime selectedDate = (DateTime)date_Picker.SelectedDate;

            date_Picker.SelectedDate = selectedDate = selectedDate.AddDays(1);

            string strdate = string.Format("{0}-{1}-{2}", selectedDate.Year, selectedDate.Month, selectedDate.Day);
            DisplayInspect(strdate);
        }

        private void Row_Click(object sender, RoutedEventArgs e)
        {
            var item = (USBData)sender;
            nSeleted = item.No.ToString();
        }

        private void USBDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string str = "";
            if (USBDataGrid.SelectedItems.Count > 0)
            {
                foreach (var item in USBDataGrid.SelectedItems)
                {
                    var item_USB = item as USBData;
                    nSeleted = item_USB.No.ToString();
                }
            }
            else
            {

            }
        }

        private void USBDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = "";
            if (USBDataGrid.SelectedItems.Count > 0)
            {
                foreach (var item in USBDataGrid.SelectedItems)
                {
                    var item_USB = item as USBData;
                    nSeleted = item_USB.No.ToString();
                }
            }
            else
            {

            }
        }

        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            message_body.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
        }

        

        private void OnSelectedUSBDateChaged(object sender, SelectionChangedEventArgs e)
        {
            DateTime USBCreatedDate = (DateTime)datePickerUSB.SelectedDate;
            string selectedDate = USBCreatedDate.ToString("MM/dd/yyyy");

            try
            {
                USBList.Clear();
                string[] datas = File.ReadAllLines(Path.Combine(Constants.USBFileName));
                foreach (var dataline in datas)
                {
                    string[] data = dataline.Split('*');
                    if (!data[0].Contains("."))
                    {
                        USBData item = new USBData
                        {
                            UsbType = data[0],
                            Status = data[1],
                            UserIP = data[2],
                            Device = data[3],
                            Guid = data[4],
                            CreatedAt = data[5]

                        };

                        if (item.CreatedAt.Contains(selectedDate))
                            USBList.Add(item);
                    }

                }


                USBDataGrid.Items.Clear();
                int nNo = 1;
                foreach (var usb in USBList.OrderByDescending(x => x.CreatedAt))
                {
                    if (nNo > 499) break;

                    if (usb.UsbType != "KeyBoard" && usb.CreatedAt.Contains(selectedDate))
                        USBDataGrid.Items.Add(new USBData { No = nNo++, ImagePath = "pack://application:,,,/Resource/" + usb.UsbType + ".png", UsbType = usb.UsbType, Status = usb.Status, UserIP = usb.UserIP, Device = usb.Device, Guid = usb.Guid, CreatedAt = usb.CreatedAt });
                }
            }
            catch (Exception ex)
            {

            }
        }

        private List<string> InsertUSBHistory(List<USBData> usbData)
        {
            List<string> list = new List<string>();
            USBDataGrid.Items.Clear();
            int nNo = 1;
            foreach (var usb in usbData.OrderByDescending(x => x.CreatedAt))
            {
                if (nNo > 499) break;
                if (usb.UsbType.Trim() != "KeyBoard")
                {
                    USBDataGrid.Items.Add(new USBData { No = nNo++, ImagePath = "pack://application:,,,/Resource/" + usb.UsbType.Trim() + ".png", UsbType = usb.UsbType, Status = usb.Status, UserIP = usb.UserIP, Device = usb.Device, Guid = usb.Guid, CreatedAt = usb.CreatedAt });

                    var strTemp = usb.UsbType.Trim() + "*" + usb.Status.Trim() + "*" + usb.UserIP.Trim() + "*" + usb.Device.Trim() + "*" + usb.Guid.Trim() + "*" + usb.CreatedAt.Trim();
                    list.Add(strTemp);
                }
            }

            return list;
        }
    }
    public class StateData
    {
        public string State { get; set; }
        public string UserName { get; set; }
        public string UserIp { get; set; }
        public string OSCreated { get; set; }
    }
    public class USBData
    {
        public int No { get; set; }
        public string UsbType { get; set; }
        public string Status { get; set; }
        public string UserIP { get; set; }
        public string Guid { get; set; }
        public string Device { get; set; }
        public string CreatedAt { get; set; }
        public string ImagePath { get; set; }
    }

    public class FileData
    {
        public string Guid { get; set; }
        public string UserIP { get; set; }
        public string Speed { get; set; }
        public string CreatedAt { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public Thread thread { get; set; }
    }

    public class Version
    {
        public string Guid { get; set; }
        public string UserIP { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
    }


    public class ArpClientInfo
    {
        public string State { get; set; }
        public string Ip { get; set; }
        public string Server { get; set; }
        public string SysCreated { get; set; }
        public string BootOs { get; set; }
        public string Partition { get; set; }
        public string Hidden { get; set; }
        public string ComputerName { get; set; }
        public string Color { get; set; }
    }

    public class InspectInfo
    {
        public string IP { get; set; }
        public string ImgPath { get; set; }
        public string Percentage { get; set; }

        public string NumberImgPath { get; set; }
        public string Rate { get; set; }
    }
}
