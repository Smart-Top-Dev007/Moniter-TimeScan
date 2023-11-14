using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
//using System.Windows.Forms;
//using System.Drawing;
using System.IO;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using Monitor.TaskView.Globals;
using Monitor.TaskView.myEvents;
using Monitor.TaskView.Logger;
using Monitor.TaskView.Utils;

using LiveCharts;
using LiveCharts.Wpf;

using System.Resources;
using System.Reflection;
using System.Windows.Interop;
using System.Drawing;
using Rectangle = System.Windows.Shapes.Rectangle;
using Color = System.Windows.Media.Color;
using System.Text;
using Monitor.TaskView.Models;
using System.Data;
using Path = System.IO.Path;
using Microsoft.Win32;

namespace Monitor.TaskView.View
{
    public partial class MainWindow : Window
    {
        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;

        const uint SC_CLOSE = 0xF060;



        public bool m_isFirstTimeRect, m_isFirstProcessRect, m_isFirstAudioRect;


        public DateTime m_DeltaComTime;
        public DateTime m_DeltaProcessTime;
        public List<ListOfProcessByOrder> m_OldProcessList = new List<ListOfProcessByOrder>();
        public List<ListOfAudio> m_OldAudioList = new List<ListOfAudio>();

        public bool m_DataPickerFlag;
        public bool m_bWindowStatus;
        public bool m_JPGPSL;
        public bool fVersion = false;
        bool bDatePicker;

        //private readonly IList<Item> shapes;
        private Popup_Screen currentMovingShape = new Popup_Screen();
        private Random rnd = new Random();

        private int m_TimeCount;
        private List<ProcessDetailItem> m_ProcessDetailItemList = new List<ProcessDetailItem>();

        /// <summary>
        private double m_dRedLine_Pos = 0;

        private double m_LastItem_Width = 0;

        private double m_TotalRestTime;

        private double m_previousItemWidth, m_previousItemEnd, m_previousAppWidth, m_previousAppEnd, m_previousAudioEnd;
        private string m_strPreviousProcess, m_strPreviousApp;

        private bool m_ProcessFilter, m_URLFilter;
        private bool m_isResize;
        /// <summary>
        /// /////////////////////////  ICON GET COLOR
        /// </summary>
        /// 
        public static List<System.Drawing.Color> TenMostUsedColors { get; private set; }
        public static List<int> TenMostUsedColorIncidences { get; private set; }

        public static System.Drawing.Color MostUsedColor { get; private set; }
        public static int MostUsedColorIncidence { get; private set; }

        private static int pixelColor;

        private static Dictionary<int, int> dctColorIncidence;

        private List<string> iconPath = new List<string>();
        private Random _random = new Random();
        public PatchProc pathcProc;
        ProcessInfos m_processInfo;
        /// <summary>
        /// ////////////////////////////
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            m_JPGPSL = false;
            bDatePicker = false;
            SetTrayIcon();
            Events.RaiseOnMainProc();

            iconPath.Add(Path.GetPathRoot(Environment.SystemDirectory) + "Windows\\System32\\winver.exe");
            iconPath.Add(Path.GetPathRoot(Environment.SystemDirectory) + "Windows\\System32\\mshta.exe");
            iconPath.Add(Path.GetPathRoot(Environment.SystemDirectory) + "Windows\\System32\\isoburn.exe");
            iconPath.Add(Path.GetPathRoot(Environment.SystemDirectory) + "Windows\\System32\\Magnify.exe");

            m_TotalRestTime = 0;
            m_previousItemWidth = 0;
            m_previousItemEnd = 0;
            m_previousAppWidth = 0;
            m_previousAppEnd = 0;
            m_previousAudioEnd = 0;
            m_strPreviousProcess = "";
            m_strPreviousApp = "";

            m_ProcessFilter = false;
            m_URLFilter = false;
            m_isResize = false;

            date_Picker.SelectedDate = DateTime.Now;

            pathcProc = new PatchProc();
            grd_Popup.Children.Add(currentMovingShape);
            currentMovingShape.Visibility = Visibility.Hidden;



            //Log.Instance.DoLog("--------  Init MainWindow ----", Log.LogType.Info);

            DispatcherTimer dtClockTime = new DispatcherTimer();
            dtClockTime.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            dtClockTime.Tick += TimerTick;
            dtClockTime.Start();
            m_TimeCount = 1;



            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            var rk = key.OpenSubKey(Constants.RegPath);
            if (rk != null)
            {
                //var nRestart = (int)rk.GetValue("Restart");
                if (rk.GetValue("Restart") != null)
                {
                    try
                    {
                        if ((int)rk.GetValue("Restart") == 1)
                        {
                            rk.SetValue("Restart", 0);
                            WindowState = WindowState.Minimized;
                            this.Hide();
                        }
                    }
                    catch { }
                }

            }
        }

        public void ShowAlarm()
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/Resource/alarm.png");
            logo.EndInit();
            fVersion = true;

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Disable close button
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            IntPtr hMenu = NativeImports.GetSystemMenu(hwnd, false);
            if (hMenu != IntPtr.Zero)
            {
                NativeImports.EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (Settings.Instance.bSend)
            {
                this.AVARTA.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/net-on.png"));
                //serverIP.Foreground = System.Windows.Media.Brushes.White;
                //Title.Foreground = System.Windows.Media.Brushes.White;
            }
            else
            {
                this.AVARTA.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/net-off.png"));
                //serverIP.Foreground = System.Windows.Media.Brushes.Gray;
                //Title.Foreground = System.Windows.Media.Brushes.Gray;
            }

            if (!m_DataPickerFlag)
            {
                if (m_TimeCount % 60 == 0)
                {
                    RefreshData();

                }
                if (m_TimeCount % 73 == 0)
                {
                    TimeStack.Children.Clear();
                    ProcessStack.Children.Clear();
                    VoiceStack.Children.Clear();
                    m_dRedLine_Pos = 0;
                    m_isFirstTimeRect = false;
                    m_isFirstProcessRect = false;
                    m_isFirstAudioRect = false;
                    m_previousAudioEnd = 0;

                    SetComputerUsageStatus(Settings.Instance.ProcessList.ToList());
                    SetProcessStatus(Settings.Instance.ProcessList.ToList());
                    m_OldProcessList = Settings.Instance.ProcessList.ToList();
                    SetVoiceStatus(Settings.Instance.AudioList.ToList());
                    m_OldAudioList = Settings.Instance.AudioList.ToList();

                    //ShowProcessDetail(Settings.Instance.ProcessList.ToList());
                    //ShowProcessTotal(Settings.Instance.ProcessList.ToList());
                    SetRedLinePos();

                }
                if (m_TimeCount == 1200) m_TimeCount = 0;
            }

            GC.Collect();
            m_TimeCount++;
        }

        public void SetTrayIcon()
        {

            Settings.Instance.ni.Icon = Properties.Resources_.TaskView;
            Settings.Instance.ni.Visible = true;
            Settings.Instance.ni.Text = "Task View";
            Settings.Instance.ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {

                    this.Show();
                    this.WindowState = WindowState.Normal;

                    TimeStack.Children.Clear();
                    ProcessStack.Children.Clear();
                    VoiceStack.Children.Clear();
                    m_dRedLine_Pos = 0;
                    m_isFirstTimeRect = false;
                    m_isFirstProcessRect = false;
                    m_isFirstAudioRect = false;
                    m_previousAudioEnd = 0;

                    //Init();

                    if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                    {
                        SetComputerUsageStatus(Settings.Instance.ProcessList.ToList());
                        SetProcessStatus(Settings.Instance.ProcessList.ToList());
                        m_OldProcessList = Settings.Instance.ProcessList.ToList();
                        SetVoiceStatus(Settings.Instance.AudioList.ToList());
                        m_OldAudioList = Settings.Instance.AudioList.ToList();
                        SetRedLinePos();
                    }
                    else
                    {
                        SetComputerUsageStatus(Settings.Instance.ProcessList_Day.ToList());
                        SetProcessStatus(Settings.Instance.ProcessList_Day.ToList());
                        m_OldProcessList = Settings.Instance.ProcessList_Day.ToList();
                        SetVoiceStatus(Settings.Instance.AudioList_Day.ToList());
                        m_OldAudioList = Settings.Instance.AudioList_Day.ToList();
                        SetRedLinePos();
                    }
                };

        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Instance.ni.Dispose();
            //e.Cancel = true;            
        }

        public void SetComputerUsageStatus(List<ListOfProcessByOrder> processLists)
        {

            double bWidth = this.Width - 22;

            //if (this.TimeStack.Children.Count > 1) {
            //    m_dRedLine_Pos -= m_LastItem_Width;
            //    this.TimeStack.Children.RemoveAt(this.TimeStack.Children.Count - 1);               
            //}

            foreach (var workRect in processLists)
            {

                double hourS = workRect.ProcessStartTime.Hour * 3600;
                double minS = workRect.ProcessStartTime.Minute * 60;
                double secondS = workRect.ProcessStartTime.Second;
                double StartSecond = hourS + minS + secondS;

                double hourE = workRect.ProcessEndTime.Hour * 3600;
                double minE = workRect.ProcessEndTime.Minute * 60;
                double secondE = workRect.ProcessEndTime.Second;
                double EndSecond = hourE + minE + secondE;

                if (EndSecond - StartSecond < 0)
                    continue;


                if (m_isFirstTimeRect == false && workRect.ProcessStartTime.ToShortTimeString() != "00:00:00")
                {
                    Rectangle rectWorkRest1 = new Rectangle();
                    rectWorkRest1.Width = (StartSecond) / (60 * 60 * 24) * bWidth;
                    rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    TimeStack.Children.Add(rectWorkRest1);
                    m_isFirstTimeRect = true;
                    m_dRedLine_Pos += rectWorkRest1.Width;
                    m_LastItem_Width = rectWorkRest1.Width;


                    Rectangle _rectWorkRest1 = new Rectangle();
                    _rectWorkRest1.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    if (workRect.ProcessName == Constants.RestProcess)
                    {
                        _rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(180, 0, 0));
                        m_strPreviousProcess = Constants.RestProcess;

                        //m_SumRestTime += EndSecond - StartSecond;
                    }
                    //else if (workRect.ProcessName == Constants.DisConnect)
                    //{
                    //    _rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                    //    m_strPreviousProcess = Constants.DisConnect;

                    //    m_SumDisconnectTime += EndSecond - StartSecond;
                    //}
                    else
                    {
                        _rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(0, 180, 0));
                        m_strPreviousProcess = "Work";
                    }

                    TimeStack.Children.Add(_rectWorkRest1);
                    m_dRedLine_Pos += _rectWorkRest1.Width;
                    m_LastItem_Width = _rectWorkRest1.Width;

                    m_previousItemWidth = _rectWorkRest1.Width;
                    m_previousItemEnd = EndSecond;
                    Console.WriteLine(" ########### Start ############");

                    continue;
                }

                if (StartSecond - m_previousItemEnd < 0)
                {
                    Console.WriteLine(" ########### Sub ############");
                    TimeStack.Children.RemoveAt(TimeStack.Children.Count - 1);



                    Rectangle rectWorkRestSub = new Rectangle();
                    rectWorkRestSub.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousItemWidth;

                    if (m_strPreviousProcess == Constants.RestProcess)
                    {
                        rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(180, 0, 0));
                        m_strPreviousProcess = Constants.RestProcess;
                    }
                    //else if (workRect.ProcessName == Constants.DisConnect)
                    //{
                    //    rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                    //    m_strPreviousProcess = Constants.DisConnect;
                    //}
                    else
                    {
                        rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(0, 180, 0));
                        m_strPreviousProcess = "Work";
                    }


                    TimeStack.Children.Add(rectWorkRestSub);
                    m_dRedLine_Pos += rectWorkRestSub.Width;
                    m_LastItem_Width = rectWorkRestSub.Width;

                    m_previousItemWidth = rectWorkRestSub.Width;
                    m_previousItemEnd = EndSecond;

                    //m_SumRestTime += EndSecond - StartSecond;
                    continue;
                }
                else if (StartSecond - m_previousItemEnd > 0/* && StartSecond - m_previousItemEnd != 0*/)
                {
                    Console.WriteLine(" ########### Sub ############");
                    Rectangle rectWorkRestSub = new Rectangle();
                    rectWorkRestSub.Width = (StartSecond - m_previousItemEnd) / (60 * 60 * 24) * bWidth;

                    if (workRect.ProcessName == Constants.RestProcess)
                    {
                        rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(180, 0, 0));
                        m_strPreviousProcess = Constants.RestProcess;
                    }
                    //else if (workRect.ProcessName == Constants.DisConnect)
                    //{
                    //    rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                    //    m_strPreviousProcess = Constants.DisConnect;
                    //}
                    else
                    {

                        if (m_strPreviousProcess == "Work")
                        {
                            rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(0, 180, 0));
                            m_strPreviousProcess = "Work";
                        }
                        else if (m_strPreviousProcess == Constants.RestProcess)
                        {
                            rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(180, 0, 0));
                            m_strPreviousProcess = Constants.RestProcess;
                            //m_SumRestTime += StartSecond - m_previousItemEnd;
                        }
                        //else if (m_strPreviousProcess == Constants.DisConnect)
                        //{
                        //    rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                        //    m_strPreviousProcess = Constants.DisConnect;
                        //    //m_SumDisconnectTime += StartSecond - m_previousItemEnd;
                        //}
                    }

                    //rectWorkRestSub.Fill = new SolidColorBrush(Color.FromRgb(180, 0, 0));
                    //m_strPreviousProcess = Constants.RestProcess;

                    TimeStack.Children.Add(rectWorkRestSub);
                    m_dRedLine_Pos += rectWorkRestSub.Width;
                    m_LastItem_Width = rectWorkRestSub.Width;

                    m_previousItemWidth = rectWorkRestSub.Width;
                    m_previousItemEnd = StartSecond;
                    //m_SumRestTime += EndSecond - StartSecond;
                }

                Rectangle rectWorkRest = new Rectangle();
                if (workRect.ProcessName == Constants.RestProcess)
                {
                    Console.WriteLine(" ########### RestProcess ############");
                    if (workRect.ProcessName == m_strPreviousProcess)
                    {
                        this.TimeStack.Children.RemoveAt(this.TimeStack.Children.Count - 1);
                        rectWorkRest.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousItemWidth;
                    }
                    else
                    {
                        rectWorkRest.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    }

                    rectWorkRest.Fill = new SolidColorBrush(Color.FromRgb(180, 0, 0));
                    this.TimeStack.Children.Add(rectWorkRest);
                    m_dRedLine_Pos += rectWorkRest.Width;
                    m_LastItem_Width = rectWorkRest.Width;

                    m_previousItemWidth = rectWorkRest.Width;

                    m_previousItemEnd = EndSecond;
                    m_strPreviousProcess = Constants.RestProcess;
                    m_dRedLine_Pos = EndSecond / (60 * 60 * 24) * bWidth;// + m_previousItemWidth;

                    //m_SumRestTime += EndSecond - StartSecond;
                }
                //else if (workRect.ProcessName == Constants.DisConnect)
                //{
                //    Console.WriteLine(" ########### Disconnect ############");
                //    if (workRect.ProcessName == m_strPreviousProcess)
                //    {
                //        this.TimeStack.Children.RemoveAt(this.TimeStack.Children.Count - 1);
                //        rectWorkRest.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousItemWidth;
                //    }
                //    else
                //    {
                //        rectWorkRest.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                //    }

                //    rectWorkRest.Fill = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                //    this.TimeStack.Children.Add(rectWorkRest);
                //    m_dRedLine_Pos += rectWorkRest.Width;
                //    m_LastItem_Width = rectWorkRest.Width;

                //    m_previousItemWidth = rectWorkRest.Width;
                //    m_previousItemEnd = EndSecond;
                //    m_strPreviousProcess = Constants.DisConnect;

                //    m_SumDisconnectTime += EndSecond - StartSecond;

                //}
                else
                {
                    Console.WriteLine(" ########### Work ############");
                    if (m_strPreviousProcess == "Work")
                    {
                        this.TimeStack.Children.RemoveAt(this.TimeStack.Children.Count - 1);
                        rectWorkRest.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousItemWidth;
                    }
                    else
                    {
                        rectWorkRest.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    }

                    rectWorkRest.Fill = new SolidColorBrush(Color.FromRgb(0, 200, 0));
                    this.TimeStack.Children.Add(rectWorkRest);
                    m_dRedLine_Pos += rectWorkRest.Width;
                    m_LastItem_Width = rectWorkRest.Width;

                    m_previousItemWidth = rectWorkRest.Width;
                    m_previousItemEnd = EndSecond;

                    m_dRedLine_Pos = EndSecond / (60 * 60 * 24) * bWidth;// + m_previousItemWidth;
                    m_strPreviousProcess = "Work";

                }
            }

            //TimeSpan totalTime = TimeSpan.FromSeconds(m_TotalRestTime);
            //string strTotalTime = "";

            //strTotalTime = totalTime.ToString(@"hh\:mm\:ss");
            //lblRestTime.Content = strTotalTime;
            //Log.Instance.DoLog("--------  ComputerUsage Status ----", Log.LogType.Info);
        }

        public void SetProcessStatus(List<ListOfProcessByOrder> processLists)
        {

            double bWidth = this.Width - 22;

            //if (this.ProcessStack.Children.Count > 1)
            //{
            //    this.ProcessStack.Children.RemoveAt(this.ProcessStack.Children.Count - 1);
            //}
            foreach (var app in processLists)
            {
                double hourS = app.ProcessStartTime.Hour * 3600;
                double minS = app.ProcessStartTime.Minute * 60;
                double secondS = app.ProcessStartTime.Second;
                double StartSecond = hourS + minS + secondS;

                double hourE = app.ProcessEndTime.Hour * 3600;
                double minE = app.ProcessEndTime.Minute * 60;
                double secondE = app.ProcessEndTime.Second;
                double EndSecond = hourE + minE + secondE;

                if (EndSecond - StartSecond < 0)
                    continue;


                if (m_isFirstProcessRect == false && app.ProcessStartTime.ToShortTimeString() != "00:00:00")
                {
                    //double hour0 = app.ProcessStartTime.Hour * 3600;
                    //double min0 = app.ProcessStartTime.Minute * 60;
                    //double second0 = app.ProcessStartTime.Second;
                    //double StartSecond0 = hour0 + min0 + second0;

                    Canvas rectProcess1 = new Canvas();
                    rectProcess1.Width = StartSecond / (60 * 60 * 24) * bWidth;
                    rectProcess1.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    this.ProcessStack.Children.Add(rectProcess1);

                    m_isFirstProcessRect = true;

                    Canvas rectProcess2 = new Canvas();
                    rectProcess2.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    if (app.ProcessName == Constants.RestProcess)
                    {
                        rectProcess2.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                        m_strPreviousApp = Constants.RestProcess;
                    }
                    //else if (app.ProcessName == Constants.DisConnect)
                    //{
                    //    rectProcess2.Background = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                    //    m_strPreviousApp = Constants.DisConnect;
                    //}
                    else
                    {
                        if (app.ProcessColor == Constants.Default)
                        {
                            string path = app.ProcessPath;
                            if (path == "Unknown")
                            {
                                path = iconPath[rnd.Next(0, 4)];
                            }
                            if (!File.Exists(path))
                            {
                                path = iconPath[rnd.Next(0, 4)];
                            }
                            System.Drawing.Color tmpColor = GetColor(path);
                            //rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_LastItem_Width;
                            rectProcess2.Background = new SolidColorBrush(Color.FromRgb(tmpColor.R, tmpColor.G, tmpColor.B));
                        }
                        else if (app.ProcessColor != "")
                        {
                            string[] strRGB = app.ProcessColor.Split(',');
                            //Int16.Parse(strRGB[0]);
                            rectProcess2.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(strRGB[0]), Convert.ToByte(strRGB[1]), Convert.ToByte(strRGB[2])));

                        }

                        m_strPreviousApp = app.ProcessName;
                    }
                    this.ProcessStack.Children.Add(rectProcess2);

                    m_previousAppWidth = rectProcess2.Width;
                    m_previousAppEnd = EndSecond;
                    // m_processStart = (StartSecond0) / (60 * 60 * 24) * bWidth;
                    continue;
                }

                //if (StartSecond - m_previousAppEnd < 0) continue;

                if (StartSecond - m_previousAppEnd < 0)
                {
                    Console.WriteLine(" ########### Sub ############");
                    ProcessStack.Children.RemoveAt(ProcessStack.Children.Count - 1);
                    Canvas rectProcessSub = new Canvas();
                    rectProcessSub.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousAppWidth;

                    if (app.ProcessName == Constants.RestProcess)
                    {
                        rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                        m_strPreviousApp = Constants.RestProcess;
                    }
                    //else if (app.ProcessName == Constants.DisConnect)
                    //{
                    //    rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                    //    m_strPreviousApp = Constants.DisConnect;
                    //}
                    else
                    {
                        //rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                        m_strPreviousApp = app.ProcessName;

                        if (app.ProcessColor == Constants.Default)
                        {
                            string path = app.ProcessPath;
                            if (path == "Unknown")
                            {
                                path = iconPath[rnd.Next(0, 4)];
                            }
                            if (!File.Exists(path))
                            {
                                path = iconPath[rnd.Next(0, 4)];
                            }
                            System.Drawing.Color tmpColor = GetColor(path);
                            //rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_LastItem_Width;
                            rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(tmpColor.R, tmpColor.G, tmpColor.B));
                        }
                        else if (app.ProcessColor != "")
                        {
                            string[] strRGB = app.ProcessColor.Split(',');
                            Int16.Parse(strRGB[0]);
                            rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(strRGB[0]), Convert.ToByte(strRGB[1]), Convert.ToByte(strRGB[2])));

                        }
                    }

                    //rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    //m_strPreviousApp = Constants.RestProcess;

                    ProcessStack.Children.Add(rectProcessSub);

                    m_previousAppWidth = rectProcessSub.Width;
                    m_previousAppEnd = EndSecond;

                    //m_SumRestTime += EndSecond - StartSecond;
                    continue;
                }
                else if (StartSecond - m_previousAppEnd > 0/* && StartSecond - m_previousItemEnd != 0*/)
                {
                    Console.WriteLine(" ########### Sub ############");
                    Canvas rectProcessSub = new Canvas();
                    rectProcessSub.Width = (StartSecond - m_previousAppEnd) / (60 * 60 * 24) * bWidth;

                    if (app.ProcessName == Constants.RestProcess)
                    {
                        rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                        m_strPreviousApp = Constants.RestProcess;
                    }
                    //else if (app.ProcessName == Constants.DisConnect)
                    //{
                    //    rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                    //    m_strPreviousApp = Constants.DisConnect;
                    //}
                    else
                    {
                        if (m_strPreviousApp == Constants.RestProcess)
                        {
                            rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                            m_strPreviousApp = Constants.RestProcess;
                        }
                        //else if (m_strPreviousApp == Constants.DisConnect)
                        //{
                        //    rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(255, 215, 0));
                        //    m_strPreviousApp = Constants.DisConnect;
                        //}
                        else
                        {
                            m_strPreviousApp = app.ProcessName;

                            if (app.ProcessColor == Constants.Default)
                            {
                                string path = app.ProcessPath;
                                if (path == "Unknown")
                                {
                                    path = iconPath[rnd.Next(0, 4)];
                                }
                                if (!File.Exists(path))
                                {
                                    path = iconPath[rnd.Next(0, 4)];
                                }
                                System.Drawing.Color tmpColor = GetColor(path);
                                //rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_LastItem_Width;
                                rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(tmpColor.R, tmpColor.G, tmpColor.B));
                            }
                            else if (app.ProcessColor != "")
                            {
                                string[] strRGB = app.ProcessColor.Split(',');
                                Int16.Parse(strRGB[0]);
                                rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(strRGB[0]), Convert.ToByte(strRGB[1]), Convert.ToByte(strRGB[2])));

                            }
                        }
                    }


                    //rectProcessSub.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    //m_strPreviousApp = Constants.RestProcess;

                    ProcessStack.Children.Add(rectProcessSub);

                    m_previousAppWidth = rectProcessSub.Width;
                    m_previousAppEnd = StartSecond;
                    //m_SumRestTime += EndSecond - StartSecond;
                }


                Canvas rectProcess = new Canvas();
                //rectProcess.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rnd.Next(255)), Convert.ToByte(rnd.Next(256)), Convert.ToByte(rnd.Next(256))));
                if (app.ProcessName == Constants.RestProcess)
                {
                    if (app.ProcessName == m_strPreviousApp)
                    {
                        this.ProcessStack.Children.RemoveAt(this.ProcessStack.Children.Count - 1);
                        rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousAppWidth;
                    }
                    else
                    {
                        rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    }

                    rectProcess.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));

                    m_previousAppWidth = rectProcess.Width;
                    m_previousAppEnd = EndSecond;

                    m_strPreviousApp = Constants.RestProcess;
                }
                //else if (app.ProcessName == Constants.DisConnect)
                //{
                //    if (app.ProcessName == m_strPreviousApp)
                //    {
                //        this.ProcessStack.Children.RemoveAt(this.ProcessStack.Children.Count - 1);
                //        rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousAppWidth;
                //    }
                //    else
                //    {
                //        rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                //    }

                //    rectProcess.Background = new SolidColorBrush(Color.FromRgb(255, 215, 0));

                //    m_previousAppWidth = rectProcess.Width;
                //    m_previousAppEnd = EndSecond;

                //    m_strPreviousApp = Constants.DisConnect;
                //}
                else
                {
                    if (app.ProcessName == m_strPreviousApp)
                    {
                        this.ProcessStack.Children.RemoveAt(this.ProcessStack.Children.Count - 1);
                        rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_previousAppWidth;
                    }
                    else
                    {
                        rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    }


                    if (app.ProcessColor == Constants.Default)
                    {
                        string path = app.ProcessPath;
                        if (path == "Unknown")
                        {
                            path = iconPath[rnd.Next(0, 4)];
                        }
                        if (!File.Exists(path))
                        {
                            path = iconPath[rnd.Next(0, 4)];
                        }
                        System.Drawing.Color tmpColor = GetColor(path);
                        //rectProcess.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth + m_LastItem_Width;
                        rectProcess.Background = new SolidColorBrush(Color.FromRgb(tmpColor.R, tmpColor.G, tmpColor.B));
                    }
                    else if (app.ProcessColor != "")
                    {
                        string[] strRGB = app.ProcessColor.Split(',');
                        Int16.Parse(strRGB[0]);
                        rectProcess.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(strRGB[0]), Convert.ToByte(strRGB[1]), Convert.ToByte(strRGB[2])));

                    }

                    m_previousAppWidth = rectProcess.Width;
                    m_previousAppEnd = EndSecond;
                    m_strPreviousApp = app.ProcessName;

                }
                try { ProcessStack.Children.Add(rectProcess); }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }

            }
            //Log.Instance.DoLog("--------  Progress status ----", Log.LogType.Info);
        }

        public void SetVoiceStatus(List<ListOfAudio> audioLists)
        {
            double bWidth = this.Width - 22;
            double m_dDelta = 300;

            foreach (var audioList in audioLists)
            {
                double hourS = audioList.ProcessStartTime.Hour * 3600;
                double minS = audioList.ProcessStartTime.Minute * 60;
                double secondS = audioList.ProcessStartTime.Second;
                double StartSecond = hourS + minS + secondS;

                double hourE = audioList.ProcessEndTime.Hour * 3600;
                double minE = audioList.ProcessEndTime.Minute * 60;
                double secondE = audioList.ProcessEndTime.Second;
                double EndSecond = hourE + minE + secondE;

                if (EndSecond - StartSecond < 0)
                    continue;

                if (m_isFirstAudioRect == false && audioList.ProcessStartTime.ToShortTimeString() != "00:00:00")
                {
                    VoiceStack.Children.Clear();
                    m_isFirstAudioRect = true;

                    Rectangle rectWorkRest1 = new Rectangle();
                    rectWorkRest1.Width = (StartSecond) / (60 * 60 * 24) * bWidth;
                    rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    VoiceStack.Children.Add(rectWorkRest1);

                    Rectangle _rectWorkRest1 = new Rectangle();
                    if ((EndSecond - StartSecond) / (60 * 60 * 24) * bWidth < 0) continue;

                    if (EndSecond - StartSecond < m_dDelta)
                    {
                        _rectWorkRest1.Width = m_dDelta / (60 * 60 * 24) * bWidth;
                        m_previousAudioEnd = StartSecond + m_dDelta;
                        _rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(22, 86, 224));
                    }
                    else
                    {
                        _rectWorkRest1.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                        _rectWorkRest1.Fill = new SolidColorBrush(Color.FromRgb(27, 157, 195));
                        m_previousAudioEnd = EndSecond;
                    }

                    VoiceStack.Children.Add(_rectWorkRest1);


                    continue;
                }

                Rectangle rectWorkRest2 = new Rectangle();
                if ((StartSecond - m_previousAudioEnd) / (60 * 60 * 24) * bWidth < 0) continue;
                rectWorkRest2.Width = (StartSecond - m_previousAudioEnd) / (60 * 60 * 24) * bWidth;
                rectWorkRest2.Fill = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                VoiceStack.Children.Add(rectWorkRest2);


                Rectangle _rectWorkRest2 = new Rectangle();
                if ((EndSecond - StartSecond) / (60 * 60 * 24) * bWidth < 0) continue;

                if (EndSecond - StartSecond < m_dDelta)
                {
                    _rectWorkRest2.Width = m_dDelta / (60 * 60 * 24) * bWidth;
                    m_previousAudioEnd = StartSecond + m_dDelta;
                    _rectWorkRest2.Fill = new SolidColorBrush(Color.FromRgb(22, 86, 224));
                }
                else
                {
                    _rectWorkRest2.Width = (EndSecond - StartSecond) / (60 * 60 * 24) * bWidth;
                    m_previousAudioEnd = EndSecond;
                    _rectWorkRest2.Fill = new SolidColorBrush(Color.FromRgb(27, 157, 195));
                }


                VoiceStack.Children.Add(_rectWorkRest2);

            }

            Rectangle spaceRect = new Rectangle();

            if (m_dRedLine_Pos - m_previousAudioEnd / (60 * 60 * 24) * bWidth < 0) return;

            spaceRect.Width = m_dRedLine_Pos - m_previousAudioEnd / (60 * 60 * 24) * bWidth;
            m_previousAudioEnd += spaceRect.Width / bWidth * 60 * 60 * 24;
            spaceRect.Fill = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            VoiceStack.Children.Add(spaceRect);

            //if (audioLists.Count == 0)
            //{
            //    Rectangle spaceRect = new Rectangle();
            //    spaceRect.Width = m_dRedLine_Pos - m_previousAudioEnd / (60 * 60 * 24) * bWidth + nVirtualWidth;
            //    spaceRect.Fill = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            //    VoiceStack.Children.Add(spaceRect);

            //}


        }

        public void ShowProcessDetail(List<ListOfProcessByOrder> processLists)
        {
            if (m_ProcessFilter) return;

            list1View.Items.Clear();
            var processes = processLists.ToList().OrderByDescending(x => x.ProcessEndTime);
            foreach (var process in processes)
            {
                if (process.ProcessName == Constants.RestProcess || process.ProcessName == Constants.HideProcess_IDLE || process.ProcessName == Constants.HideProcess_APH || process.ProcessName == Constants.HideProcess_LockApp)
                    continue;

                TimeSpan duration = process.ProcessEndTime.Subtract(process.ProcessStartTime);

                if (duration.ToString(@"hh\:mm\:ss") == "00:00:00")
                    continue;

                Icon icon = null;
                if (process.ProcessPath == "Unknown")
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                }
                else if (!File.Exists(process.ProcessPath))
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                }
                else
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(process.ProcessPath);
                }
                BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                string strState = "";
                foreach (string strFP in Constants.strForbiddenProcess)
                {
                    if (process.ProcessName.ToLower().Contains(strFP.ToLower()))
                    {
                        strState = "Danger";
                        break;
                    }
                }

                string strWindowTemp = process.ProcessWindow;
                if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                {
                    strWindowTemp = process.ProcessName;
                }
                list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = process.ProcessStartTime.ToLongTimeString(), EndTime = process.ProcessEndTime.ToLongTimeString(), Duration = duration.ToString(@"hh\:mm\:ss"), State = strState });

            }
            //Log.Instance.DoLog("--------   ShowProcessDetail----", Log.LogType.Info);

        }

        public void ShowURL(List<ListOfUrl> urlLists)
        {
            if (m_URLFilter) return;

            string ChromePath32 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
            string ChromePath64 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Google\\Chrome\\Application\\chrome.exe";

            string FirefoxPath32 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files (x86)\\Mozilla Firefox\\firefox.exe";
            string FirefoxPath64 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Mozilla Firefox\\firefox.exe";

            string IEPath = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Internet Explorer\\iexplore.exe";
            string Edge = "";
            Icon icon = null;
            URLView.Items.Clear();
            try
            {
                var urlList = urlLists.OrderByDescending(x => x.URLEndTime).ToList();
                foreach (var urlInfo in urlList)
                {
                    if (urlInfo.BrowserType == 1)
                    {
                        if (File.Exists(ChromePath32))
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath32);
                        }
                        else
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath64);
                        }
                    }
                    else if (urlInfo.BrowserType == 2)
                    {
                        if (File.Exists(FirefoxPath32))
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath32);
                        }
                        else
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath64);
                        }

                    }
                    else if (urlInfo.BrowserType == 3)
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(Edge);
                    }
                    else if (urlInfo.BrowserType == 4)
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(IEPath);
                    }

                    BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());
                    string strState = "";
                    foreach (string strFURL in Constants.strForbiddenURL)
                    {
                        if (urlInfo.strURL.ToLower().Contains(strFURL.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                        {
                            strState = "Danger";
                            break;
                        }
                    }
                    URLView.Items.Add(new URL { List2Icon = bitmapSource, strURL = urlInfo.strURL, StartTime = urlInfo.URLStartTime.ToLongTimeString(), EndTime = urlInfo.URLEndTime.ToLongTimeString(), State = strState });


                }
            }
            catch (Exception ex)
            {

            }

            //Log.Instance.DoLog("  ShowURL ====> ", Log.LogType.Info);

        }

        public void ShowProcessTotal(List<ListOfProcessByOrder> processLists)
        {
            list2View.Items.Clear();
            //var processTotalLists = from proTotalList in Settings.Instance.ProcessList select proTotalList;
            var processTemp = processLists.Select(x => new { ProcessName = x.ProcessName, Duration = x.ProcessEndTime.Subtract(x.ProcessStartTime).TotalSeconds, Path = x.ProcessPath }).ToList();
            var processTotal = processTemp.GroupBy(t => t.ProcessName).Select(g => new { ProcessName = g.Key, Duration = g.Sum(u => u.Duration), Path = g.First().Path });

            foreach (var process in processTotal)
            {
                if (process.ProcessName == Constants.RestProcess || process.ProcessName == Constants.HideProcess_IDLE || process.ProcessName == Constants.HideProcess_APH || process.ProcessName == Constants.HideProcess_LockApp)
                    continue;

                TimeSpan duration = TimeSpan.FromSeconds(process.Duration);
                string strDuration = duration.ToString(@"hh\:mm\:ss");

                if (strDuration == "00:00:00")
                    continue;
                Icon icon = null;
                if (process.Path == "Unknown")
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                }
                else if (!File.Exists(process.Path))
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                }
                else
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(process.Path);
                }
                BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());
                list2View.Items.Add(new ProcessTotalItem { List2Icon = bitmapSource, Process = process.ProcessName, Time = strDuration });
            }

            TimeSpan totalTime = new TimeSpan();
            string strTotalTime = "";
            for (int i = 0; i < list2View.Items.Count; i++)
            {
                totalTime += TimeSpan.Parse((list2View.Items[i] as ProcessTotalItem).Time);
            }
            strTotalTime = totalTime.ToString(@"hh\:mm");
            lblTotalTime.Content = strTotalTime;

            if (ProcessStack.Children.Count == 0) m_previousAppEnd = 0;
            double dRestTime = m_previousAppEnd - totalTime.TotalSeconds;
            if (dRestTime < 0) dRestTime = 0;

            lblRestTime.Content = TimeSpan.FromSeconds(dRestTime).ToString(@"hh\:mm");

            double dFree = 86400 - totalTime.TotalSeconds - dRestTime;

            if (dFree < 0) dFree = 0;

            WorkPie.Values = new ChartValues<double> { totalTime.TotalSeconds };
            RestPie.Values = new ChartValues<double> { dRestTime };
            //DisconnectPie.Values = new ChartValues<double> { m_SumDisconnectTime };
            FreePie.Values = new ChartValues<double> { dFree };


            WorkRect.Width = 115 * totalTime.TotalSeconds / 86400;
            RestRect.Width = 115 * dRestTime / 86400;
            LeftRect.Width = 115 * dFree / 86400;

            _WorkPie.Text = Convert.ToInt32(WorkRect.Width / 115 * 100).ToString() + "%";
            _RestPie.Text = Convert.ToInt32(RestRect.Width / 115 * 100).ToString() + "%";
            _LeftPie.Text = Convert.ToInt32(LeftRect.Width / 115 * 100).ToString() + "%";

        }

        public void ShowDownload()
        {
            try
            {
                DownloadView.Items.Clear();

                List<string> strDownloadListTemp = new List<string>();
                strDownloadListTemp.Clear();

                DateTime tempDate = (DateTime)Windows.MainWindow.date_Picker.SelectedDate;
                string strDate = tempDate.Year.ToString() + "-" + tempDate.Month.ToString() + "-" + tempDate.Day.ToString();
                string strPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.DownloadFile;
                strDownloadListTemp = Md5Crypto.ReadCryptoFile(strPath);

                Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(Path.GetPathRoot(Environment.SystemDirectory) + "Windows\\System32\\rundll32.exe");

                BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                String[] spearator = { Constants.filePattern };

                for (int i = strDownloadListTemp.Count - 1; i >= 0; i--)
                {
                    string[] strArray = strDownloadListTemp[i].Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    DownloadView.Items.Add(new DownloadInfo { List2Icon = bitmapSource, FileName = "  " + strArray[0], DownloadTime = strArray[3].Replace('-', ':'), FileSize = strArray[1] });

                }
            }
            catch { }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Content = Constants.version;
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0, UseAeroCaptionButtons = false });

            Init();

            LoadForbiddenData();
        }

        private void LoadForbiddenData()
        {
            try
            {


                var ListDownloadFile = Md5Crypto.ReadCryptoFile(Settings.Instance.Directories.TodayDirectory + "\\" + Constants.DownloadFile);
                Settings.Instance.nDownloadCount = ListDownloadFile.Count;
                lblDownloadCount.Content = ListDownloadFile.Count.ToString();

                var ListForbiddenFile = Md5Crypto.ReadCryptoFile(Settings.Instance.Directories.TodayDirectory + "\\" + Constants.ForbiddenFile);
                Settings.Instance.nForbiddenCount = ListForbiddenFile.Count;
                lblForbiddenCount.Content = ListForbiddenFile.Count.ToString();

                var ListUrlFile = Md5Crypto.ReadCryptoFile(Settings.Instance.Directories.TodayDirectory + "\\" + Constants.urlFileName);
                Settings.Instance.nUrlCount = ListUrlFile.Count;
                lblURL.Content = ListUrlFile.Count.ToString();
            }
            catch { }

            int nDangerURL = 0;
            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList.Count();

                    foreach (var strFU in Constants.strForbiddenURL)
                    {
                        foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.ToLower().Contains(strFU.ToLower()) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList_Day.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.ToLower().Contains(strFU.ToLower()) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }

            lblDangerURLCount.Content = nDangerURL.ToString();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            m_bWindowStatus = true;
            m_isFirstTimeRect = false;
            m_isFirstProcessRect = false;
            m_isFirstAudioRect = false;
            m_DataPickerFlag = false;
            m_processInfo = new ProcessInfos();
        }

        public void Init()
        {
            serverIP.Foreground = System.Windows.Media.Brushes.White;
            Title.Foreground = System.Windows.Media.Brushes.White;
            if (Settings.Instance.bSend)
            {
                this.AVARTA.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/net-on.png"));
                //serverIP.Foreground = System.Windows.Media.Brushes.White;
                //Title.Foreground = System.Windows.Media.Brushes.White;
            }
            else
            {
                this.AVARTA.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/net-off.png"));
                //serverIP.Foreground = System.Windows.Media.Brushes.Gray;
                //Title.Foreground = System.Windows.Media.Brushes.Gray;
            }
            serverIP.Content = "Server IP : " + Settings.Instance.RegValue.ServerIP;

            SetComputerUsageStatus(Settings.Instance.ProcessList.ToList());
            SetProcessStatus(Settings.Instance.ProcessList.ToList());
            m_OldProcessList = Settings.Instance.ProcessList.ToList();
            SetVoiceStatus(Settings.Instance.AudioList.ToList());
            m_OldAudioList = Settings.Instance.AudioList.ToList();
            m_isResize = false;

            ShowProcessDetail(Settings.Instance.ProcessList.ToList());
            ShowProcessTotal(Settings.Instance.ProcessList.ToList());
            //ShowURL(Settings.Instance.URLList.ToList());
            ShowDownload();
            SetRedLinePos();



        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SettingButton_OnClick(object sender, RoutedEventArgs e)
        {
            Windows.SettingWindow = new SettingWindow();
            Windows.SettingWindow.ShowDialog();
        }

        private void HideButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        //////////////////////////////////////////////////////////////       

        private void SetMovingShapePosition(MouseEventArgs e, double width)
        {
            var window = e.GetPosition(this);
            //Log.Instance.DoLog("XYX ====>" + window.X, Log.LogType.Info);
            if ((Width - window.X) > width)
            {
                Canvas.SetLeft(grd_Popup, window.X);
                Canvas.SetTop(grd_Popup, window.Y + 25);
            }
            else if ((Width - window.X - 10) < width)
            {
                Canvas.SetLeft(grd_Popup, window.X - width);
                Canvas.SetTop(grd_Popup, window.Y + 25);
            }
        }

        private void VoiceStack_MouseMove(object sender, MouseEventArgs e)
        {
            grd_GrayLine.Visibility = Visibility.Visible;
            Canvas.SetLeft(grd_GrayLine, e.GetPosition(this).X);
            currentMovingShape.Visibility = Visibility.Hidden;


            if (DateTime.Now.Subtract(m_DeltaProcessTime).TotalMilliseconds > 50)
            {
                GetAudioInfo(e);
                m_DeltaProcessTime = DateTime.Now;
            }
            Console.WriteLine("+++6666666+++ {0}", e.GetPosition(this).X);
            Console.WriteLine("+++4444444+++ {0}", m_previousAppEnd);
            //if (e.GetPosition(this).Y > 232 || e.GetPosition(this).Y < 205 || e.GetPosition(this).X > m_previousAppEnd)
            //{
            //    currentMovingShape.Visibility = Visibility.Hidden;
            //}
        }

        private void VoiceStack_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void imgBackButton_Click(object sender, MouseEventArgs e)
        {
            bDatePicker = true;
            DateTime tempDate = (DateTime)date_Picker.SelectedDate;
            date_Picker.SelectedDate = tempDate.AddDays(-1);
            DateTime tempDate1 = (DateTime)date_Picker.SelectedDate;

            string strDate = tempDate1.Year.ToString() + "-" + tempDate1.Month.ToString() + "-" + tempDate1.Day.ToString();

            string strDownloadPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.DownloadFile;
            var DownloadTemp = Md5Crypto.ReadCryptoFile(strDownloadPath);
            lblDownloadCount.Content = DownloadTemp.Count.ToString();

            string strForbiddenPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.ForbiddenFile;
            var ForbiddenTemp = Md5Crypto.ReadCryptoFile(strForbiddenPath);
            lblForbiddenCount.Content = ForbiddenTemp.Count.ToString();

            string strUrlPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.urlFileName;
            var UrlTemp = Md5Crypto.ReadCryptoFile(strUrlPath);
            lblURL.Content = UrlTemp.Count.ToString();

            int nDangerURL = 0;
            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {
                        foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList_Day.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }

            lblDangerURLCount.Content = nDangerURL.ToString();

            //tempDate = (DateTime)date_Picker_Client.SelectedDate;
            return;


        }

        void imgBackMouseMove(object sender, MouseEventArgs e)
        {
            imgBack.Opacity = 0.6;
        }

        void imgBackMouseLeave(object sender, MouseEventArgs e)
        {
            imgBack.Opacity = 1;
        }

        void imgForwardMouseMove(object sender, MouseEventArgs e)
        {
            imgForward.Opacity = 0.6;
        }

        void imgForwardMouseLeave(object sender, MouseEventArgs e)
        {
            imgForward.Opacity = 1;
        }

        private void PnlMessages_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
        }

        private void imgForwardButton_Click(object sender, MouseEventArgs e)
        {
            m_previousAudioEnd = 0;
            if (date_Picker.SelectedDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                return;
            }
            bDatePicker = true;
            DateTime tempDate = (DateTime)date_Picker.SelectedDate;
            date_Picker.SelectedDate = tempDate.AddDays(1);

            DateTime tempDate1 = (DateTime)date_Picker.SelectedDate;
            string strDate = tempDate1.Year.ToString() + "-" + tempDate1.Month.ToString() + "-" + tempDate1.Day.ToString();
            string strDownloadPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.DownloadFile;
            var DownloadTemp = Md5Crypto.ReadCryptoFile(strDownloadPath);
            lblDownloadCount.Content = DownloadTemp.Count.ToString();

            string strForbiddenPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.ForbiddenFile;
            var ForbiddenTemp = Md5Crypto.ReadCryptoFile(strForbiddenPath);
            lblForbiddenCount.Content = ForbiddenTemp.Count.ToString();

            string strUrlPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.urlFileName;
            var UrlTemp = Md5Crypto.ReadCryptoFile(strUrlPath);
            lblURL.Content = UrlTemp.Count.ToString();
            // tempDate = (DateTime)date_Picker_Client.SelectedDate;

            int nDangerURL = 0;
            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {
                        foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList_Day.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }

            lblDangerURLCount.Content = nDangerURL.ToString();

            return;

        }

        private void imgTodayButton_Click(object sender, RoutedEventArgs e)
        {
            m_previousAudioEnd = 0;
            if (date_Picker.SelectedDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                return;
            }
            bDatePicker = true;
            DateTime tempDate = (DateTime)date_Picker.SelectedDate;
            tempDate = (DateTime)date_Picker.SelectedDate;
            //if (date_Picker_Client.SelectedDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            //{
            //    return;
            //}
            date_Picker.SelectedDate = DateTime.Now;
            DateTime tempDate1 = (DateTime)date_Picker.SelectedDate;
            string strDate = tempDate1.Year.ToString() + "-" + tempDate1.Month.ToString() + "-" + tempDate1.Day.ToString();
            string strDownloadPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.DownloadFile;
            var DownloadTemp = Md5Crypto.ReadCryptoFile(strDownloadPath);
            lblDownloadCount.Content = DownloadTemp.Count.ToString();

            string strForbiddenPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.ForbiddenFile;
            var ForbiddenTemp = Md5Crypto.ReadCryptoFile(strForbiddenPath);
            lblForbiddenCount.Content = ForbiddenTemp.Count.ToString();

            string strUrlPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.urlFileName;
            var UrlTemp = Md5Crypto.ReadCryptoFile(strUrlPath);
            lblURL.Content = UrlTemp.Count.ToString();

            lblAudioCount.Content = Settings.Instance.AudioList.Count();

            int nDangerURL = 0;

            nDangerURL = 0;
            try
            {
                foreach (var strFU in Constants.strForbiddenURL)
                {
                    foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                    {
                        if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                            nDangerURL++;
                    }
                }
            }
            catch (Exception ex) { }

            lblDangerURLCount.Content = nDangerURL.ToString();

            return;

        }

        private void OnShowData(string strDate)
        {
            TimeStack.Children.Clear();
            ProcessStack.Children.Clear();
            VoiceStack.Children.Clear();
            m_isFirstTimeRect = false;
            m_isFirstProcessRect = false;
            m_isFirstAudioRect = false;
            m_previousAudioEnd = 0;

            list1View.Items.Clear();
            list2View.Items.Clear();
            URLView.Items.Clear();
            m_dRedLine_Pos = 0;
            SetRedLinePos();

            string strDBFilePath = Settings.Instance.RegValue.BaseDirectory + "\\" + strDate + "\\";
            strDBFilePath += Constants.DbFileName;

            Settings.Instance.ProcessList_Day = new List<ListOfProcessByOrder>();
            Settings.Instance.URLList_Day = new List<ListOfUrl>();
            Settings.Instance.AudioList_Day = new List<ListOfAudio>();

            List<string> strTempList = new List<string>();
            strTempList = Md5Crypto.ReadCryptoFile(strDBFilePath);

            string strURLFilePath = Settings.Instance.RegValue.BaseDirectory + "\\" + strDate + "\\" + Constants.urlFileName;
            List<string> strTempURLList = new List<string>();
            strTempURLList = Md5Crypto.ReadCryptoFile(strURLFilePath);

            string strAudioFilePath = Settings.Instance.RegValue.BaseDirectory + "\\" + strDate + "\\" + Constants.AudioFileName;
            List<string> strTempAudioList = new List<string>();
            strTempAudioList = Md5Crypto.ReadCryptoFile(strAudioFilePath);

            String[] spearator = { Constants.filePattern };
            foreach (var line in strTempList)
            {
                try
                {
                    String[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOPBO_Day.ProcessName = strArray[0];
                    Settings.Instance.LOPBO_Day.ProcessWindow = strArray[1];
                    Settings.Instance.LOPBO_Day.ProcessPath = strArray[2];
                    Settings.Instance.LOPBO_Day.ProcessStartTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOPBO_Day.ProcessEndTime = DateTime.Parse(strArray[4]);
                    try
                    {
                        Settings.Instance.LOPBO_Day.ProcessColor = strArray[5];
                    }
                    catch
                    {
                        Settings.Instance.LOPBO_Day.ProcessColor = Constants.Default;
                    }
                    Settings.Instance.ProcessList_Day.Add(Settings.Instance.LOPBO_Day);
                }
                catch
                {

                }
            }

            foreach (var line in strTempAudioList)
            {
                try
                {
                    String[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOA_Day.ProcessName = strArray[0];
                    Settings.Instance.LOA_Day.ProcessWindow = strArray[1];
                    Settings.Instance.LOA_Day.ProcessPath = strArray[2];
                    Settings.Instance.LOA_Day.ProcessStartTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOA_Day.ProcessEndTime = DateTime.Parse(strArray[4]);
                    Settings.Instance.LOA_Day.FileName = strArray[5];
                    Settings.Instance.LOA_Day.FileSize = strArray[6];
                    Settings.Instance.AudioList_Day.Add(Settings.Instance.LOA_Day);
                }
                catch
                {

                }
            }

            foreach (var line in strTempURLList)
            {
                try
                {
                    String[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOU_Day.strWindow = strArray[0];
                    Settings.Instance.LOU_Day.strURL = strArray[1];
                    Settings.Instance.LOU_Day.URLStartTime = DateTime.Parse(strArray[2]);
                    Settings.Instance.LOU_Day.URLEndTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOU_Day.BrowserType = (byte)Int32.Parse(strArray[4]);
                    Settings.Instance.URLList_Day.Add(Settings.Instance.LOU_Day);
                }
                catch
                {

                }
            }


            SetComputerUsageStatus(Settings.Instance.ProcessList_Day.ToList());
            SetProcessStatus(Settings.Instance.ProcessList_Day.ToList());
            m_OldProcessList = Settings.Instance.ProcessList_Day.ToList();
            SetVoiceStatus(Settings.Instance.AudioList_Day.ToList());
            m_OldAudioList = Settings.Instance.AudioList_Day.ToList();

            ShowProcessDetail(Settings.Instance.ProcessList_Day.ToList());
            ShowProcessTotal(Settings.Instance.ProcessList_Day.ToList());
            //ShowURL(Settings.Instance.URLList_Day.ToList());
            ShowDownload();
            SetRedLinePos();

            int nDangerURL = 0;
            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                nDangerURL = 0;
                try
                {
                    foreach (var strFU in Constants.strForbiddenURL)
                    {
                        foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                nDangerURL = 0;
                try
                {
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }

            lblDangerURLCount.Content = nDangerURL.ToString();
        }

        private void ProcessStack_MouseMove(object sender, MouseEventArgs e)
        {
            grd_GrayLine.Visibility = Visibility.Visible;
            Canvas.SetLeft(grd_GrayLine, e.GetPosition(this).X);
            //GetProcessScreen(e);
            if (DateTime.Now.Subtract(m_DeltaProcessTime).TotalMilliseconds > 50)
            {
                GetProcessScreen(e);
                m_DeltaProcessTime = DateTime.Now;
            }

            if (e.GetPosition(this).Y > 232 || e.GetPosition(this).Y < 205 || e.GetPosition(this).X > m_previousAppEnd)
            {
                currentMovingShape.Visibility = Visibility.Hidden;
            }

        }

        private void ProcessStack_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        public void GetFilePath()
        {
            string strDate = string.Format("{0}-{1}-{2}", Windows.MainWindow.date_Picker.SelectedDate.Value.Year, Windows.MainWindow.date_Picker.SelectedDate.Value.Month, Windows.MainWindow.date_Picker.SelectedDate.Value.Day);
            string strTempPath = System.IO.Path.Combine(Settings.Instance.RegValue.BaseDirectory, strDate, "Slide");
            string[] files = Directory.GetFiles(strTempPath);
            if (files.Length == 0) return;

            foreach (var file in files)
            {
                if (file.Contains("psl"))
                {
                    m_JPGPSL = false;
                    //strSlideFileName = strFileName2;
                    break;
                }

                else if (file.Contains("jpg"))
                {
                    m_JPGPSL = true;
                    break;
                }

            }
        }

        public void GetAudioInfo(MouseEventArgs e)
        {
            double bAudioStackWidth = this.Width - 30;
            double totalSecond = (e.GetPosition(this).X - 15) / bAudioStackWidth * 60 * 60 * 24;

            if (double.IsNaN(totalSecond)) return;

            int nCount = -300;
            while (nCount <= 300)
            {
                if (totalSecond + nCount < 0)
                {
                    nCount += 60;
                    continue;
                }

                TimeSpan tTime = TimeSpan.FromSeconds(totalSecond + nCount);

                //string strHour = tTime.Hours.ToString();
                //string strMinute = tTime.Minutes.ToString();
                //string strSecond = tTime.Seconds.ToString();

                //string strDate = string.Format("{0}-{1}-{2}", Windows.MainWindow.date_Picker.SelectedDate.Value.Year, Windows.MainWindow.date_Picker.SelectedDate.Value.Month, Windows.MainWindow.date_Picker.SelectedDate.Value.Day);

                tTime = TimeSpan.FromSeconds(totalSecond + nCount);

                string strTime = tTime.ToString(@"hh\-mm\-ss");
                string strSTime = tTime.ToString(@"hh\:mm\:ss");

                if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                {
                    var _strAudioInfo = from item in Settings.Instance.AudioList where item.ProcessStartTime.ToString("HH:mm:ss").StartsWith(strSTime.Remove(5, 3)) select item;


                    if (_strAudioInfo.Count() > 0)
                    {
                        foreach (var audioInfo in _strAudioInfo)
                        {
                            currentMovingShape.TimeText.Text = "Time : " + strTime.Replace('-', ':') + "  ( " + audioInfo.ProcessStartTime.ToString("HH:mm") + "  ~  " + audioInfo.ProcessEndTime.ToString("HH:mm") + "  )        ";
                            currentMovingShape.ProcessText.Text = "Process Name : " + audioInfo.ProcessName + "        ";
                            currentMovingShape.PathText.Text = "Window Title : " + audioInfo.ProcessWindow + "        ";
                            currentMovingShape.WindowText.Text = "Size : " + audioInfo.FileSize;

                            currentMovingShape.SetScale(240, 70);
                            currentMovingShape.img_Screenshot.Source = null;
                            SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                            currentMovingShape.Visibility = Visibility.Visible;

                            break;
                        }

                        break;
                    }
                    else
                    {
                        DateTime dateTime;
                        DateTime.TryParse(Windows.MainWindow.date_Picker.Text + " " + strSTime, out dateTime);
                        var audioInfos = from item in Settings.Instance.AudioList where item.ProcessStartTime <= dateTime && item.ProcessEndTime >= dateTime select item;

                        if (audioInfos.Count() > 0)
                        {
                            foreach (var audioInfo in audioInfos)
                            {

                                if (string.IsNullOrWhiteSpace(audioInfo.ProcessStartTime.ToString("HH:mm:ss")))
                                {
                                    currentMovingShape.Visibility = Visibility.Hidden;
                                    return;
                                }
                                currentMovingShape.TimeText.Text = "Time : " + strTime.Replace('-', ':') + "  ( " + audioInfo.ProcessStartTime.ToString("HH:mm") + "  ~  " + audioInfo.ProcessEndTime.ToString("HH:mm") + "  )        ";
                                currentMovingShape.ProcessText.Text = "Process Name : " + audioInfo.ProcessName + "        ";
                                currentMovingShape.PathText.Text = "Window Title : " + audioInfo.ProcessWindow + "        ";
                                currentMovingShape.WindowText.Text = "Size : " + audioInfo.FileSize;

                                currentMovingShape.SetScale(240, 70);
                                currentMovingShape.img_Screenshot.Source = null;
                                SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                                currentMovingShape.Visibility = Visibility.Visible;

                                break;
                            }

                        }
                        else
                        {
                            currentMovingShape.Visibility = Visibility.Hidden;
                            //return;
                        }

                    }
                }
                else
                {
                    var _strAudioInfo = from item in Settings.Instance.AudioList_Day where item.ProcessStartTime.ToString("HH:mm:ss").StartsWith(strTime.Remove(5, 3)) select item;


                    if (_strAudioInfo.Count() > 0)
                    {
                        foreach (var audioInfo in _strAudioInfo)
                        {
                            currentMovingShape.TimeText.Text = "Time : " + strTime.Replace('-', ':') + "( " + audioInfo.ProcessStartTime.ToString("HH:mm") + "  ~  " + audioInfo.ProcessEndTime.ToString("HH:mm") + "  )        ";
                            currentMovingShape.ProcessText.Text = "Process Name : " + audioInfo.ProcessName + "        ";
                            currentMovingShape.PathText.Text = "Window Title : " + audioInfo.ProcessWindow + "        ";
                            currentMovingShape.WindowText.Text = "Size : " + audioInfo.FileSize;

                            currentMovingShape.SetScale(240, 70);
                            currentMovingShape.img_Screenshot.Source = null;
                            SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                            currentMovingShape.Visibility = Visibility.Visible;

                            break;
                        }

                        break;
                    }
                    else
                    {
                        DateTime dateTime;
                        DateTime.TryParse(Windows.MainWindow.date_Picker.Text + " " + strSTime, out dateTime);
                        var audioInfos = from item in Settings.Instance.AudioList_Day where item.ProcessStartTime <= dateTime && item.ProcessEndTime >= dateTime select item;

                        if (audioInfos.Count() > 0)
                        {
                            foreach (var audioInfo in audioInfos)
                            {

                                if (string.IsNullOrWhiteSpace(audioInfo.ProcessStartTime.ToString("HH:mm:ss")))
                                {
                                    currentMovingShape.Visibility = Visibility.Hidden;
                                    return;
                                }
                                currentMovingShape.TimeText.Text = "Time : " + strTime.Replace('-', ':') + "  ( " + audioInfo.ProcessStartTime.ToString("HH:mm") + "  ~  " + audioInfo.ProcessEndTime.ToString("HH:mm") + "  )        ";
                                currentMovingShape.ProcessText.Text = "Process Name : " + audioInfo.ProcessName + "        ";
                                currentMovingShape.PathText.Text = "Window Title : " + audioInfo.ProcessWindow + "        ";
                                currentMovingShape.WindowText.Text = "Size : " + audioInfo.FileSize;

                                currentMovingShape.SetScale(240, 70);
                                currentMovingShape.img_Screenshot.Source = null;
                                SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                                currentMovingShape.Visibility = Visibility.Visible;

                                break;
                            }

                        }
                        else
                        {
                            currentMovingShape.Visibility = Visibility.Hidden;
                            //return;
                        }

                    }
                }

                nCount += 60;
            }
        }

        public void GetProcessScreen(MouseEventArgs e)
        {
            double bTimeStackWidth = this.Width - 30;
            double totalSecond = (e.GetPosition(this).X - 15) / bTimeStackWidth * 60 * 60 * 24;


            TimeSpan tTime = TimeSpan.FromSeconds(totalSecond);

            string strHour = tTime.Hours.ToString();
            string strMinute = tTime.Minutes.ToString();
            string strSecond = tTime.Seconds.ToString();

            string strDate = string.Format("{0}-{1}-{2}", Windows.MainWindow.date_Picker.SelectedDate.Value.Year, Windows.MainWindow.date_Picker.SelectedDate.Value.Month, Windows.MainWindow.date_Picker.SelectedDate.Value.Day);

            string strSlideFileName = "";

            tTime = TimeSpan.FromSeconds(totalSecond);

            string strTime = tTime.ToString(@"hh\-mm\-ss");
            string strSTime = tTime.ToString(@"hh\:mm\:ss");
            DateTime dateTime;
            DateTime.TryParse(Windows.MainWindow.date_Picker.Text + " " + strSTime, out dateTime);

            strHour = tTime.Hours.ToString();
            strMinute = tTime.Minutes.ToString();
            strSecond = tTime.Seconds.ToString();

            string strFileName1 = System.IO.Path.Combine(Settings.Instance.RegValue.BaseDirectory, strDate, "Slide", strHour + "-" + strMinute + "-" + strSecond + ".jpg");

            string strTempPath = System.IO.Path.Combine(Settings.Instance.RegValue.BaseDirectory, strDate, "Slide");
            string strTempTime = strHour + "-" + strMinute + "-" + strSecond + "." + Constants.strImgExtension;

            string strFileName2 = Md5Crypto.OnImageFileNameChange(strTempPath, strTime);
            //strFileName2 = strFileName2 + "." + Constants.strImgExtension;

            string _strFileName = strFileName2.Remove(0, strFileName2.IndexOf("Slide") + 6);
            _strFileName = _strFileName.Remove(5, 3);
            //List<string> strFiles = Directory.EnumerateFiles(strTempPath, "*.psl", SearchOption.AllDirectories);
            if (!Directory.Exists(strTempPath)) return;

            var _strRealFiles = from file in Directory.EnumerateFiles(strTempPath, "*.psl", SearchOption.AllDirectories) where file.StartsWith(System.IO.Path.Combine(strTempPath, _strFileName)) select file;

            try
            {
                if (_strRealFiles.Count() > 0)
                {
                    string[] strPSLFile = _strRealFiles.ToArray();

                    byte[] temp = Md5Crypto.OnReadImgFile(strPSLFile[0]);

                    using (MemoryStream ms = new MemoryStream(temp))
                    {

                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms))
                        {

                            string strProcessName = "", strPath = "", strTimeDuration = "", strWindow = "";
                            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                            {
                                foreach (var Process in (from Processes in Settings.Instance.ProcessList.ToList() where Processes.ProcessStartTime <= dateTime && Processes.ProcessEndTime >= dateTime select Processes))
                                {

                                    //if (Process.ProcessName == Constants.RestProcess)
                                    //    return;

                                    if (Process.ProcessName == Constants.RestProcess) continue;


                                    strProcessName = Process.ProcessName;
                                    strPath = "Path : " + Process.ProcessPath;
                                    strTimeDuration = "  ( " + Process.ProcessStartTime.ToLongTimeString() + " ~ " + Process.ProcessEndTime.ToLongTimeString() + " )";
                                    strWindow = Process.ProcessWindow;


                                    if (Process.ProcessName.Trim() == "chrome.exe" || Process.ProcessName.Trim() == "firefox.exe")
                                    {
                                        foreach (var strURLs in from urlList in Settings.Instance.URLList where urlList.URLStartTime <= dateTime || urlList.URLEndTime >= dateTime select urlList.strURL)
                                        {
                                            if (strURLs == "") continue;

                                            strPath = "URL : " + strURLs;
                                            break;
                                        }

                                    }

                                    break;

                                }

                            }
                            else
                            {
                                foreach (var Process in (from Processes in Settings.Instance.ProcessList_Day.ToList() where Processes.ProcessStartTime <= dateTime && Processes.ProcessEndTime >= dateTime select Processes))
                                {

                                    //if (Process.ProcessName == Constants.RestProcess)
                                    //    return;

                                    if (Process.ProcessName == Constants.RestProcess) continue;


                                    strProcessName = Process.ProcessName;
                                    strPath = "Path : " + Process.ProcessPath;
                                    strTimeDuration = "  ( " + Process.ProcessStartTime.ToLongTimeString() + " ~ " + Process.ProcessEndTime.ToLongTimeString() + " )";
                                    strWindow = Process.ProcessWindow;


                                    if (Process.ProcessName.Trim() == "chrome.exe" || Process.ProcessName.Trim() == "firefox.exe")
                                    {
                                        foreach (var strURLs in from urlList in Settings.Instance.URLList_Day where urlList.URLStartTime <= dateTime || urlList.URLEndTime >= dateTime select urlList.strURL)
                                        {
                                            if (strURLs == "") continue;

                                            strPath = "URL : " + strURLs;
                                            break;
                                        }

                                    }

                                    break;

                                }
                            }

                            if (strProcessName == "") return;

                            currentMovingShape.TimeText.Text = "Time : " + strTime.Replace('-', ':') + strTimeDuration;
                            currentMovingShape.ProcessText.Text = "Process : " + strProcessName;
                            currentMovingShape.PathText.Text = strPath;
                            currentMovingShape.WindowText.Text = "Window : " + strWindow;
                            try
                            {
                                BitmapImage imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                MemoryStream ms1 = new MemoryStream(temp);
                                imageSource.StreamSource = ms1;
                                imageSource.EndInit();
                                currentMovingShape.img_Screenshot.Source = imageSource;
                            }
                            catch (Exception ex)
                            {

                            }

                            //if (img.Width > img.Height * 2)
                            //    currentMovingShape.SetScale(710, 200);
                            //else
                            //    currentMovingShape.SetScale(355, 200/*img.Width, img.Height*/);
                            currentMovingShape.SetScale(img.Width, img.Height);
                            SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                            currentMovingShape.Visibility = Visibility.Visible;
                            //Log.Instance.DoLog("Screen Path OK =======>>>>" + strSlideFileName, Log.LogType.Info);
                        }
                    }
                }
                else
                {
                    currentMovingShape.Visibility = Visibility.Hidden;
                    return;
                    //strMinute = (tTime.Minutes + 1).ToString();
                    //goto AA;
                }
            }
            catch (Exception ex)
            {
                return;
                //strMinute = (tTime.Minutes + 1).ToString();
                //goto AA;
            }


        }

        public void GetWorkRestInfo(MouseEventArgs e)
        {
            double bTimeStackWidth = this.Width - 30;
            double totalSecond = (e.GetPosition(this).X - 15) / bTimeStackWidth * 60 * 60 * 24;

            if (double.IsNaN(totalSecond)) return;

            TimeSpan tTime = TimeSpan.FromSeconds(totalSecond);

            string strTime = tTime.ToString(@"hh\-mm\-ss");
            string strSTime = tTime.ToString(@"hh\:mm\:ss");

            DateTime dateTime;
            DateTime.TryParse(Windows.MainWindow.date_Picker.Text + " " + strSTime, out dateTime);

            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                var _itemInfos = from item in Settings.Instance.ProcessList.ToList() where item.ProcessStartTime <= dateTime && item.ProcessEndTime >= dateTime select item;

                string strStatus = "";
                //string strFirstTime = "";
                //string strEndTime = "";
                DateTime FirstTime = DateTime.Now, EndTime = DateTime.Now;

                if (_itemInfos.ToList().Count < 0)
                {
                    currentMovingShape.Visibility = Visibility.Hidden;
                    return;
                }


                foreach (var itemInfos in _itemInfos)
                {
                    if (itemInfos.ProcessName == "RestProcess")
                    {
                        foreach (var item in from workInfos in Settings.Instance.ProcessList.ToList().OrderByDescending(x => x.ProcessEndTime) where workInfos.ProcessStartTime <= dateTime select workInfos)
                        {
                            //strFirstTime = item.ProcessEndTime.ToString("HH:mm:ss");
                            FirstTime = item.ProcessEndTime;
                            if (item.ProcessName == "RestProcess") continue;

                            strStatus = "Rest";

                            break;
                        }

                        foreach (var item in from workInfos in Settings.Instance.ProcessList.ToList().OrderBy(x => x.ProcessStartTime) where workInfos.ProcessStartTime >= dateTime select workInfos)
                        {
                            //strEndTime = item.ProcessStartTime.ToString("HH:mm:ss");
                            EndTime = item.ProcessStartTime;
                            if (item.ProcessName == "RestProcess") continue;

                            strStatus = "Rest";

                            break;
                        }
                    }
                    else
                    {
                        foreach (var item in from workInfos in Settings.Instance.ProcessList.ToList().OrderByDescending(x => x.ProcessEndTime) where workInfos.ProcessStartTime <= dateTime select workInfos)
                        {
                            //strFirstTime = item.ProcessEndTime.ToString("HH:mm:ss");
                            FirstTime = item.ProcessEndTime;
                            if (item.ProcessName == "RestProcess")
                            {

                                strStatus = "Work";

                                break;
                            }

                        }

                        foreach (var item in from workInfos in Settings.Instance.ProcessList.ToList().OrderBy(x => x.ProcessStartTime) where workInfos.ProcessStartTime >= dateTime select workInfos)
                        {
                            //strEndTime = item.ProcessStartTime.ToString("HH:mm:ss");
                            EndTime = item.ProcessStartTime;
                            if (item.ProcessName == "RestProcess")
                            {

                                strStatus = "Work";

                                break;
                            }
                        }
                    }

                    currentMovingShape.TimeText.Text = "Time : " + strSTime;
                    currentMovingShape.ProcessText.Text = /*"End Time : " + */ "Duration : " + TimeSpan.FromSeconds(EndTime.Subtract(FirstTime).TotalSeconds).ToString(@"hh\:mm\:ss");
                    currentMovingShape.PathText.Text = "Status : " + strStatus;
                    currentMovingShape.WindowText.Text = /*"Start Time : " + */"From To : " + FirstTime.ToLongTimeString() + " ~ " + EndTime.ToLongTimeString();

                    currentMovingShape.SetScale(240, 50);
                    currentMovingShape.img_Screenshot.Source = null;
                    SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                    currentMovingShape.Visibility = Visibility.Visible;
                    //break;
                    return;
                }

            }

            else
            {
                var _itemInfos = from item in Settings.Instance.ProcessList_Day.ToList() where item.ProcessStartTime <= dateTime && item.ProcessEndTime >= dateTime select item;

                string strStatus = "";
                //string strFirstTime = "";
                //string strEndTime = "";
                DateTime FirstTime = DateTime.Now, EndTime = DateTime.Now;

                if (_itemInfos.ToList().Count < 0)
                {
                    currentMovingShape.Visibility = Visibility.Hidden;
                    return;
                }


                foreach (var itemInfos in _itemInfos)
                {
                    if (itemInfos.ProcessName == "RestProcess")
                    {
                        foreach (var item in from workInfos in Settings.Instance.ProcessList_Day.ToList().OrderByDescending(x => x.ProcessEndTime) where workInfos.ProcessStartTime <= dateTime select workInfos)
                        {
                            //strFirstTime = item.ProcessEndTime.ToString("HH:mm:ss");
                            FirstTime = item.ProcessEndTime;
                            if (item.ProcessName == "RestProcess") continue;

                            strStatus = "Rest";

                            break;
                        }

                        foreach (var item in from workInfos in Settings.Instance.ProcessList_Day.ToList().OrderBy(x => x.ProcessStartTime) where workInfos.ProcessStartTime >= dateTime select workInfos)
                        {
                            //strEndTime = item.ProcessStartTime.ToString("HH:mm:ss");
                            EndTime = item.ProcessStartTime;
                            if (item.ProcessName == "RestProcess") continue;

                            strStatus = "Rest";

                            break;
                        }
                    }
                    else
                    {
                        foreach (var item in from workInfos in Settings.Instance.ProcessList_Day.ToList().OrderByDescending(x => x.ProcessEndTime) where workInfos.ProcessStartTime <= dateTime select workInfos)
                        {
                            //strFirstTime = item.ProcessEndTime.ToString("HH:mm:ss");
                            FirstTime = item.ProcessEndTime;
                            if (item.ProcessName == "RestProcess")
                            {

                                strStatus = "Work";

                                break;
                            }

                        }

                        foreach (var item in from workInfos in Settings.Instance.ProcessList_Day.ToList().OrderBy(x => x.ProcessStartTime) where workInfos.ProcessStartTime >= dateTime select workInfos)
                        {
                            //strEndTime = item.ProcessStartTime.ToString("HH:mm:ss");
                            EndTime = item.ProcessStartTime;
                            if (item.ProcessName == "RestProcess")
                            {

                                strStatus = "Work";

                                break;
                            }
                        }
                    }

                    currentMovingShape.TimeText.Text = "Time : " + strSTime;
                    currentMovingShape.ProcessText.Text = /*"End Time : " + */ "Duration : " + TimeSpan.FromSeconds(EndTime.Subtract(FirstTime).TotalSeconds).ToString(@"hh\:mm\:ss");
                    currentMovingShape.PathText.Text = "Status : " + strStatus;
                    currentMovingShape.WindowText.Text = /*"Start Time : " + */"From To : " + FirstTime.ToLongTimeString() + " ~ " + EndTime.ToLongTimeString();

                    currentMovingShape.SetScale(240, 50);
                    currentMovingShape.img_Screenshot.Source = null;
                    SetMovingShapePosition(e, currentMovingShape.ActualWidth);
                    currentMovingShape.Visibility = Visibility.Visible;
                    //break;
                    return;
                }
            }

        }

        public void RefreshData()
        {
            List<ListOfProcessByOrder> tmpProcessList = new List<ListOfProcessByOrder>();

            if (Settings.Instance.ProcessList.Count == 0)
                return;
            if (m_OldProcessList.Count == 0)
            {
                tmpProcessList = Settings.Instance.ProcessList.ToList();
            }
            else
            {
                for (int i = m_OldProcessList.Count; i < Settings.Instance.ProcessList.Count; i++)
                {
                    tmpProcessList.Add(Settings.Instance.ProcessList[i]);
                }
            }

            List<ListOfAudio> tmpAudioList = new List<ListOfAudio>();

            //if (Settings.Instance.AudioList.Count == 0)
            //    return;
            if (m_OldAudioList.Count == 0)
            {
                tmpAudioList = Settings.Instance.AudioList.ToList();
            }
            else
            {
                for (int i = m_OldAudioList.Count; i < Settings.Instance.AudioList.Count; i++)
                {
                    tmpAudioList.Add(Settings.Instance.AudioList[i]);
                }
            }

            // Log.Instance.DoLog("--------  RefreshData ----", Log.LogType.Info);
            SetComputerUsageStatus(tmpProcessList);
            SetProcessStatus(tmpProcessList);
            SetVoiceStatus(tmpAudioList);

            ShowProcessDetail(Settings.Instance.ProcessList.ToList());
            ShowProcessTotal(Settings.Instance.ProcessList.ToList());
            //ShowURL(Settings.Instance.URLList.ToList());
            ShowDownload();
            SetRedLinePos();
            m_OldProcessList = Settings.Instance.ProcessList.ToList();
            m_OldAudioList = Settings.Instance.AudioList.ToList();

            lblDownloadCount.Content = Settings.Instance.nDownloadCount.ToString();
            lblForbiddenCount.Content = Settings.Instance.nForbiddenCount.ToString();
            lblURL.Content = Settings.Instance.URLList.Count.ToString();

            int nDangerURL = 0;
            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {
                        foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                nDangerURL = 0;
                try
                {
                    lblAudioCount.Content = Settings.Instance.AudioList_Day.Count();
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                        {
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                nDangerURL++;
                        }
                    }
                }
                catch (Exception ex) { }
            }

            lblDangerURLCount.Content = nDangerURL.ToString();
        }

        private void TimeStack_MouseMove(object sender, MouseEventArgs e)
        {
            if (DateTime.Now.Subtract(m_DeltaProcessTime).TotalMilliseconds > 50)
            {
                GetWorkRestInfo(e);
                m_DeltaProcessTime = DateTime.Now;
            }

            grd_GrayLine.Visibility = Visibility.Visible;
            Canvas.SetLeft(grd_GrayLine, e.GetPosition(this).X);

            Console.WriteLine("%%%%%%%%%% {0}", e.GetPosition(this).Y);

            if (e.GetPosition(this).Y > 195 || e.GetPosition(this).Y < 165)
            {
                try
                {
                    currentMovingShape.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
        }

        private void TimeStack_MouseLeave(object sender, MouseEventArgs e)
        {
            //currentMovingShape.Visibility = Visibility.Hidden;
        }

        public void NewDayClearList()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                Thread.Sleep(100);
                list1View.Items.Clear();
                list2View.Items.Clear();
                URLView.Items.Clear();

                TimeStack.Children.Clear();
                ProcessStack.Children.Clear();
                VoiceStack.Children.Clear();
                m_isFirstTimeRect = false;
                m_isFirstProcessRect = false;
                m_isFirstAudioRect = false;
                m_dRedLine_Pos = 0;
                SetRedLinePos();

                date_Picker.SelectedDate = DateTime.Now;
                Thread.Sleep(100);
            });

        }

        public void RefreshProcessDetail(List<ListOfProcessByOrder> lstProcess)
        {
            //if (list1View.Items.Count > 0) {
            //    list1View.Items.RemoveAt(list1View.Items.Count - 1);
            //}
            for (int index = 0; index < lstProcess.Count; index++)
            {
                if (lstProcess[index].ProcessName == Constants.RestProcess)
                    continue;
                Icon icon = null;
                if (lstProcess[index].ProcessPath == "Unknown")
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                }
                else if (!File.Exists(lstProcess[index].ProcessPath))
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                }
                else
                {
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(lstProcess[index].ProcessPath);
                }
                BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                if (lstProcess[index].ProcessEndTime.Subtract(lstProcess[index].ProcessStartTime).ToString(@"hh\:mm\:ss") == "00:00:00")
                    continue;

                string strWindowTemp = lstProcess[index].ProcessWindow;
                if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                {
                    strWindowTemp = lstProcess[index].ProcessName;
                }
                list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = lstProcess[index].ProcessStartTime.ToLongTimeString(), EndTime = lstProcess[index].ProcessEndTime.ToLongTimeString(), Duration = lstProcess[index].ProcessEndTime.Subtract(lstProcess[index].ProcessStartTime).ToString(@"hh\:mm\:ss") });
            }
        }

        private void SetRedLinePos()
        {
            //Canvas.SetLeft(grd_RedLineTop, m_dRedLine_Pos + 11);
            Canvas.SetLeft(TimeLine, m_dRedLine_Pos + 11);
            Canvas.SetLeft(grd_RedLine, m_dRedLine_Pos + 11);
            //Canvas.SetLeft(grd_RedLineBottom, m_dRedLine_Pos + 11);
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            grd_GrayLine.Visibility = Visibility.Visible;
            Canvas.SetLeft(grd_GrayLine, e.GetPosition(this).X);
        }

        private void StackPanel_MouseMove_App(object sender, MouseEventArgs e)
        {
            grd_GrayLine.Visibility = Visibility.Visible;
            Canvas.SetLeft(grd_GrayLine, e.GetPosition(this).X);
            if (DateTime.Now.Subtract(m_DeltaProcessTime).TotalMilliseconds > 50)
            {
                GetProcessScreen(e);
                m_DeltaProcessTime = DateTime.Now;
            }

            if (e.GetPosition(this).Y > 169 || e.GetPosition(this).Y < 138)
            {
                try
                {
                    currentMovingShape.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
        }

        public static BitmapSource ConvertBitmap(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Pbgra32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }

        public static void GetMostUsedColor(Bitmap theBitMap)
        {
            TenMostUsedColors = new List<System.Drawing.Color>();
            TenMostUsedColorIncidences = new List<int>();

            MostUsedColor = System.Drawing.Color.Empty;
            MostUsedColorIncidence = 0;

            // does using Dictionary<int,int> here
            // really pay-off compared to using
            // Dictionary<Color, int> ?

            // would using a SortedDictionary be much slower, or ?

            dctColorIncidence = new Dictionary<int, int>();

            // this is what you want to speed up with unmanaged code
            for (int row = 0; row < theBitMap.Size.Width; row++)
            {
                for (int col = 0; col < theBitMap.Size.Height; col++)
                {
                    pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                    if (dctColorIncidence.Keys.Contains(pixelColor))
                    {
                        dctColorIncidence[pixelColor]++;
                    }
                    else
                    {
                        dctColorIncidence.Add(pixelColor, 1);
                    }
                }
            }

            // note that there are those who argue that a
            // .NET Generic Dictionary is never guaranteed
            // to be sorted by methods like this
            var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // this should be replaced with some elegant Linq ?
            foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
            {
                TenMostUsedColors.Add(System.Drawing.Color.FromArgb(kvp.Key));
                TenMostUsedColorIncidences.Add(kvp.Value);
            }

            MostUsedColor = System.Drawing.Color.FromArgb(dctSortedByValueHighToLow.First().Key);
            MostUsedColorIncidence = dctSortedByValueHighToLow.First().Value;
        }

        private System.Drawing.Color GetColor(string Path)
        {
            Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(Path);
            Bitmap bMap = ico.ToBitmap();
            //   GetMostUsedColor(bMap);
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int x = 0; x < bMap.Width; x++)
            {
                for (int y = 0; y < bMap.Height; y++)
                {
                    System.Drawing.Color clr = bMap.GetPixel(x, y);
                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;
            System.Drawing.Color color = bMap.GetPixel(bMap.Width / 2, bMap.Height / 2);
            //   System.Drawing.Color color = MostUsedColorIncidence;
            return color;
            //return System.Drawing.Color.FromArgb(r, g, b);
        }

        private void Grd_Popup_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(this).Y > 170 || e.GetPosition(this).Y < 138)
            {
                currentMovingShape.Visibility = Visibility.Hidden;
            }
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            bDatePicker = true;
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            bDatePicker = false;
        }

        private void OnSelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bDatePicker == true)
            {
                DateTime tempDate = (DateTime)date_Picker.SelectedDate;

                string strDate = tempDate.Year.ToString() + "-" + tempDate.Month.ToString() + "-" + tempDate.Day.ToString();

                string strDownloadPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.DownloadFile;
                var DownloadTemp = Md5Crypto.ReadCryptoFile(strDownloadPath);
                lblDownloadCount.Content = DownloadTemp.Count.ToString();

                string strForbiddenPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.ForbiddenFile;
                var ForbiddenTemp = Md5Crypto.ReadCryptoFile(strForbiddenPath);
                lblForbiddenCount.Content = ForbiddenTemp.Count.ToString();

                string strUrlPath = Settings.Instance.Directories.WorkDirectory + "\\" + strDate + "\\" + Constants.urlFileName;
                var UrlTemp = Md5Crypto.ReadCryptoFile(strUrlPath);
                lblURL.Content = UrlTemp.Count.ToString();

                int nDangerURL = 0;
                if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                {
                    try
                    {
                        lblAudioCount.Content = Settings.Instance.AudioList.Count();
                        foreach (var strFU in Constants.strForbiddenURL)
                        {
                            foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                            {
                                if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                    nDangerURL++;
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    try
                    {
                        lblAudioCount.Content = Settings.Instance.AudioList_Day.Count();
                        foreach (var strFU in Constants.strForbiddenURL)
                        {

                            foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.Contains(strFU) select urlList)
                            {
                                if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                    nDangerURL++;
                            }
                        }
                    }
                    catch (Exception ex) { }
                }

                lblDangerURLCount.Content = nDangerURL.ToString();


                if (tempDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    m_DataPickerFlag = false;

                    TimeStack.Children.Clear();
                    ProcessStack.Children.Clear();
                    VoiceStack.Children.Clear();

                    m_isFirstTimeRect = false;
                    m_isFirstProcessRect = false;
                    m_isFirstAudioRect = false;

                    list1View.Items.Clear();
                    list2View.Items.Clear();
                    URLView.Items.Clear();
                    m_dRedLine_Pos = 0;
                    SetRedLinePos();
                    Init();
                }
                else
                {
                    m_DataPickerFlag = true;
                    string _strDate = string.Format("{0}-{1}-{2}", tempDate.Year, tempDate.Month, tempDate.Day);
                    OnShowData(_strDate);
                }
                bDatePicker = false;
            }
        }

        private void CopyURL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get the clicked MenuItem
                var menuItem = (MenuItem)sender;

                //Get the ContextMenu to which the menuItem belongs
                var contextMenu = (ContextMenu)menuItem.Parent;

                //Find the placementTarget
                var item = (DataGrid)contextMenu.PlacementTarget;
                var urlItem = item.CurrentItem as URL;

                //if (urlItem == null) return;

                string strURL = urlItem.strURL;


                Clipboard.SetText(strURL.Trim());
            }
            catch (Exception ex)
            { }

        }

        private void GoToURL_Click(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            try
            {
                var menuItem = (MenuItem)sender;

                //Get the ContextMenu to which the menuItem belongs
                var contextMenu = (ContextMenu)menuItem.Parent;

                //Find the placementTarget
                var item = (DataGrid)contextMenu.PlacementTarget;
                var urlItem = item.CurrentItem as URL;

                //if (urlItem == null) return;

                string strURL = urlItem.strURL;

                System.Diagnostics.Process.Start(strURL.Trim());
            }
            catch (Exception ex)
            {

            }

        }

        private void URLView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //if (URLView.Items.Count == 0)
            //    menu.Visibility = Visibility.Hidden;
            //else
            //    menu.Visibility = Visibility.Visible;
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            if (fVersion == false)
            {
                return;
            }
            else
            {
                pathcProc.DownloadPatch();
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ALLProcessCheckBox.IsChecked = false;
                FilterByProcess();
            }
        }

        private void FilterProcessButton_Click(object sender, RoutedEventArgs e)
        {
            FilterByProcess();
        }

        private void ProcessFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (processFilter.Text == "Filter by process")
            {

                processFilter.Text = "";
            }
        }

        private void ProcessFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(processFilter.Text))
                processFilter.Text = "Filter by process";
        }

        private void ALLProcessCheckBox_Click(object sender, RoutedEventArgs e)
        {
            processFilter.Text = "Filter by process";

            if (ALLProcessCheckBox.IsChecked == true)
            {
                FilterAllProcess();
            }
            else
            {
                list1View.Items.Clear();
                m_ProcessFilter = false;

                if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                    ShowProcessDetail(Settings.Instance.ProcessList.ToList());
                else
                    ShowProcessDetail(Settings.Instance.ProcessList_Day.ToList());
            }
        }

        private void FilterByProcess()
        {
            list1View.Items.Clear();

            if (processFilter.Text.Trim() == "")
            {
                m_ProcessFilter = false;

                if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                    ShowProcessDetail(Settings.Instance.ProcessList.ToList());
                else
                    ShowProcessDetail(Settings.Instance.ProcessList_Day.ToList());

                return;
            }

            m_ProcessFilter = true;

            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                var processes = from processList in Settings.Instance.ProcessList.ToList().OrderByDescending(x => x.ProcessEndTime) where processList.ProcessName.ToLower().Contains(processFilter.Text.ToLower().Trim()) select processList;
                foreach (var process in processes)
                {
                    if (process.ProcessName == Constants.RestProcess || process.ProcessName == Constants.HideProcess_IDLE || process.ProcessName == Constants.HideProcess_APH || process.ProcessName == Constants.HideProcess_LockApp)
                        continue;

                    TimeSpan duration = process.ProcessEndTime.Subtract(process.ProcessStartTime);

                    if (duration.ToString(@"hh\:mm\:ss") == "00:00:00")
                        continue;

                    Icon icon = null;
                    if (process.ProcessPath == "Unknown")
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                    }
                    else if (!File.Exists(process.ProcessPath))
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                    }
                    else
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(process.ProcessPath);
                    }
                    BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                    string strState = "";
                    foreach (string strFP in Constants.strForbiddenProcess)
                    {
                        if (process.ProcessName.ToLower().Contains(strFP.ToLower()))
                        {
                            strState = "Danger";
                            break;
                        }
                    }
                    string strWindowTemp = process.ProcessWindow;
                    if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                    {
                        strWindowTemp = process.ProcessName;
                    }
                    list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = process.ProcessStartTime.ToLongTimeString(), EndTime = process.ProcessEndTime.ToLongTimeString(), Duration = duration.ToString(@"hh\:mm\:ss"), State = strState });


                }
            }
            else
            {
                var processes = from processList in Settings.Instance.ProcessList_Day.ToList().OrderByDescending(x => x.ProcessEndTime) where processList.ProcessName.ToLower().Contains(processFilter.Text.ToLower().Trim()) select processList;
                foreach (var process in processes)
                {
                    if (process.ProcessName == Constants.RestProcess || process.ProcessName == Constants.HideProcess_IDLE || process.ProcessName == Constants.HideProcess_APH || process.ProcessName == Constants.HideProcess_LockApp)
                        continue;

                    TimeSpan duration = process.ProcessEndTime.Subtract(process.ProcessStartTime);

                    if (duration.ToString(@"hh\:mm\:ss") == "00:00:00")
                        continue;

                    Icon icon = null;
                    if (process.ProcessPath == "Unknown")
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                    }
                    else if (!File.Exists(process.ProcessPath))
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                    }
                    else
                    {
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(process.ProcessPath);
                    }
                    BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                    string strWindowTemp = process.ProcessWindow;
                    if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                    {
                        strWindowTemp = process.ProcessName;
                    }

                    list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = process.ProcessStartTime.ToLongTimeString(), EndTime = process.ProcessEndTime.ToLongTimeString(), Duration = duration.ToString(@"hh\:mm\:ss") });

                    string strState = "";
                    foreach (string strFP in Constants.strForbiddenProcess)
                    {
                        if (process.ProcessName.ToLower().Contains(strFP.ToLower()))
                        {
                            strState = "Danger";
                            break;
                        }
                    }

                    strWindowTemp = process.ProcessWindow;
                    if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                    {
                        strWindowTemp = process.ProcessName;
                    }
                    list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = process.ProcessStartTime.ToLongTimeString(), EndTime = process.ProcessEndTime.ToLongTimeString(), Duration = duration.ToString(@"hh\:mm\:ss"), State = strState });

                }
            }
        }

        private void FilterAllProcess()
        {
            m_ProcessFilter = true;
            list1View.Items.Clear();

            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                foreach (var strFP in Constants.strForbiddenProcess)
                {

                    foreach (var process in from processList in Settings.Instance.ProcessList.ToList() where processList.ProcessName.ToLower().Contains(strFP.ToLower()) select processList)
                    {
                        if (process.ProcessName == Constants.RestProcess || process.ProcessName == Constants.HideProcess_IDLE || process.ProcessName == Constants.HideProcess_APH || process.ProcessName == Constants.HideProcess_LockApp)
                            continue;

                        TimeSpan duration = process.ProcessEndTime.Subtract(process.ProcessStartTime);

                        if (duration.ToString(@"hh\:mm\:ss") == "00:00:00")
                            continue;

                        Icon icon = null;
                        if (process.ProcessPath == "Unknown")
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                        }
                        else if (!File.Exists(process.ProcessPath))
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                        }
                        else
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(process.ProcessPath);
                        }
                        BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                        string strWindowTemp = process.ProcessWindow;
                        if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                        {
                            strWindowTemp = process.ProcessName;
                        }

                        list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = process.ProcessStartTime.ToLongTimeString(), EndTime = process.ProcessEndTime.ToLongTimeString(), Duration = duration.ToString(@"hh\:mm\:ss"), State = "Danger" });
                    }
                }

            }
            else
            {
                foreach (var strFP in Constants.strForbiddenProcess)
                {

                    foreach (var process in from processList in Settings.Instance.ProcessList_Day.ToList() where processList.ProcessName.ToLower().Contains(strFP.ToLower()) select processList)
                    {
                        if (process.ProcessName == Constants.RestProcess || process.ProcessName == Constants.HideProcess_IDLE || process.ProcessName == Constants.HideProcess_APH || process.ProcessName == Constants.HideProcess_LockApp)
                            continue;

                        TimeSpan duration = process.ProcessEndTime.Subtract(process.ProcessStartTime);

                        if (duration.ToString(@"hh\:mm\:ss") == "00:00:00")
                            continue;

                        Icon icon = null;
                        if (process.ProcessPath == "Unknown")
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                        }
                        else if (!File.Exists(process.ProcessPath))
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath[_random.Next(0, 4)]);
                        }
                        else
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(process.ProcessPath);
                        }
                        BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                        string strWindowTemp = process.ProcessWindow;
                        if (strWindowTemp == "" || strWindowTemp == Constants.Unknown)
                        {
                            strWindowTemp = process.ProcessName;
                        }

                        list1View.Items.Add(new ProcessDetailItem { List1Icon = bitmapSource, Process = strWindowTemp, StartTime = process.ProcessStartTime.ToLongTimeString(), EndTime = process.ProcessEndTime.ToLongTimeString(), Duration = duration.ToString(@"hh\:mm\:ss"), State = "Danger" });
                    }
                }
            }

        }

        private void UrlFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (urlFilter.Text == "Filter by URL")
            {
                urlFilter.Text = "";
            }
        }

        private void UrlFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ALLURLCheckBox.IsChecked = false;
                FilterByURL();
            }
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            m_processInfo.GetPieChartProcess();
        }

        private void RestartButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] endData = Encoding.UTF8.GetBytes(Constants.Re_End);
                Settings.Instance.SockCom.Send(endData);
            }
            catch
            {

            }
            // DeleteTryIcon();

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.CreateSubKey(Constants.RegPath);
            key.SetValue("Restart", 1);

            EnvironmentHelper.Restart(true);
        }

        private void UrlFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(urlFilter.Text))
                urlFilter.Text = "Filter by URL";
        }

        private void ALLURLCheckBox_Click(object sender, RoutedEventArgs e)
        {

            urlFilter.Text = "Filter by URL";

            if (ALLURLCheckBox.IsChecked == true)
            {
                FilterAllURL();
            }
            else
            {
                m_URLFilter = false;

                if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                    ShowURL(Settings.Instance.URLList.ToList());
                else
                    ShowURL(Settings.Instance.URLList_Day.ToList());
            }
        }

        private void FilterAllURL()
        {
            m_URLFilter = true;

            string ChromePath32 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
            string ChromePath64 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Google\\Chrome\\Application\\chrome.exe";

            string FirefoxPath32 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files (x86)\\Mozilla Firefox\\firefox.exe";
            string FirefoxPath64 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Mozilla Firefox\\firefox.exe";

            string IEPath = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Internet Explorer\\iexplore.exe";
            string Edge = "";
            Icon icon = null;
            URLView.Items.Clear();

            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                try
                {
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.ToLower().Contains(strFU.ToLower()) select urlList)
                        {
                            if (urlInfo.BrowserType == 1)
                            {
                                if (File.Exists(ChromePath32))
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath32);
                                }
                                else
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath64);
                                }
                            }
                            else if (urlInfo.BrowserType == 2)
                            {
                                if (File.Exists(FirefoxPath32))
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath32);
                                }
                                else
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath64);
                                }

                            }
                            else if (urlInfo.BrowserType == 3)
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(Edge);
                            }
                            else if (urlInfo.BrowserType == 4)
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(IEPath);
                            }

                            BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());
                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                URLView.Items.Add(new URL { List2Icon = bitmapSource, strURL = urlInfo.strURL, StartTime = urlInfo.URLStartTime.ToLongTimeString(), EndTime = urlInfo.URLEndTime.ToLongTimeString(), State = "Danger" });

                        }
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                try
                {
                    foreach (var strFU in Constants.strForbiddenURL)
                    {

                        foreach (var urlInfo in from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.ToLower().Contains(strFU.ToLower()) select urlList)
                        {
                            if (urlInfo.BrowserType == 1)
                            {
                                if (File.Exists(ChromePath32))
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath32);
                                }
                                else
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath64);
                                }
                            }
                            else if (urlInfo.BrowserType == 2)
                            {
                                if (File.Exists(FirefoxPath32))
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath32);
                                }
                                else
                                {
                                    icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath64);
                                }

                            }
                            else if (urlInfo.BrowserType == 3)
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(Edge);
                            }
                            else if (urlInfo.BrowserType == 4)
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(IEPath);
                            }

                            BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());

                            if (!urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                                URLView.Items.Add(new URL { List2Icon = bitmapSource, strURL = urlInfo.strURL, StartTime = urlInfo.URLStartTime.ToLongTimeString(), EndTime = urlInfo.URLEndTime.ToLongTimeString(), State = "Danger" });
                        }
                    }
                }
                catch (Exception ex) { }
            }

        }

        private void FilterByURL()
        {
            string ChromePath32 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
            string ChromePath64 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Google\\Chrome\\Application\\chrome.exe";

            string FirefoxPath32 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files (x86)\\Mozilla Firefox\\firefox.exe";
            string FirefoxPath64 = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Mozilla Firefox\\firefox.exe";

            string IEPath = Path.GetPathRoot(Environment.SystemDirectory) + "Program Files\\Internet Explorer\\iexplore.exe";
            string Edge = "";
            Icon icon = null;
            URLView.Items.Clear();

            if (urlFilter.Text.Trim() == "")
            {
                m_URLFilter = false;

                if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
                    ShowURL(Settings.Instance.URLList.ToList());
                else
                    ShowURL(Settings.Instance.URLList_Day.ToList());

                return;
            }

            m_URLFilter = true;

            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                try
                {
                    var urlLists = from urlList in Settings.Instance.URLList.OrderByDescending(x => x.URLEndTime) where urlList.strURL.ToLower().Contains(urlFilter.Text.ToLower().Trim()) select urlList;
                    foreach (var urlInfo in urlLists)
                    {
                        if (urlInfo.BrowserType == 1)
                        {
                            if (File.Exists(ChromePath32))
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath32);
                            }
                            else
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath64);
                            }
                        }
                        else if (urlInfo.BrowserType == 2)
                        {
                            if (File.Exists(FirefoxPath32))
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath32);
                            }
                            else
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath64);
                            }

                        }
                        else if (urlInfo.BrowserType == 3)
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(Edge);
                        }
                        else if (urlInfo.BrowserType == 4)
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(IEPath);
                        }

                        BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());
                        string strState = "";
                        foreach (string strFURL in Constants.strForbiddenURL)
                        {
                            if (urlInfo.strURL.ToLower().Contains(strFURL.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                            {
                                strState = "Danger";
                                break;
                            }
                        }
                        URLView.Items.Add(new URL { List2Icon = bitmapSource, strURL = urlInfo.strURL, StartTime = urlInfo.URLStartTime.ToLongTimeString(), EndTime = urlInfo.URLEndTime.ToLongTimeString(), State = strState });

                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    var urlLists = from urlList in Settings.Instance.URLList_Day.OrderByDescending(x => x.URLEndTime) where urlList.strURL.ToLower().Contains(urlFilter.Text.ToLower().Trim()) select urlList;
                    foreach (var urlInfo in urlLists)
                    {
                        if (urlInfo.BrowserType == 1)
                        {
                            if (File.Exists(ChromePath32))
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath32);
                            }
                            else
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(ChromePath64);
                            }
                        }
                        else if (urlInfo.BrowserType == 2)
                        {
                            if (File.Exists(FirefoxPath32))
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath32);
                            }
                            else
                            {
                                icon = System.Drawing.Icon.ExtractAssociatedIcon(FirefoxPath64);
                            }

                        }
                        else if (urlInfo.BrowserType == 3)
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(Edge);
                        }
                        else if (urlInfo.BrowserType == 4)
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(IEPath);
                        }

                        BitmapSource bitmapSource = ConvertBitmap(icon.ToBitmap());
                        string strState = "";
                        foreach (string strFURL in Constants.strForbiddenURL)
                        {
                            if (urlInfo.strURL.ToLower().Contains(strFURL.ToLower()) && !urlInfo.strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !urlInfo.strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                            {
                                strState = "Danger";
                                break;
                            }
                        }

                        URLView.Items.Add(new URL { List2Icon = bitmapSource, strURL = urlInfo.strURL, StartTime = urlInfo.URLStartTime.ToLongTimeString(), EndTime = urlInfo.URLEndTime.ToLongTimeString(), State = strState });

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!m_isResize)
            {
                m_isResize = true;
                return;
            }

            TimeStack.Children.Clear();
            ProcessStack.Children.Clear();
            VoiceStack.Children.Clear();
            m_dRedLine_Pos = 0;
            m_isFirstTimeRect = false;
            m_isFirstProcessRect = false;
            m_isFirstAudioRect = false;
            m_previousAudioEnd = 0;

            SetComputerUsageStatus(Settings.Instance.ProcessList.ToList());
            SetProcessStatus(Settings.Instance.ProcessList.ToList());
            m_OldProcessList = Settings.Instance.ProcessList.ToList();
            SetVoiceStatus(Settings.Instance.AudioList.ToList());
            m_OldAudioList = Settings.Instance.AudioList.ToList();

            //ShowProcessDetail(Settings.Instance.ProcessList.ToList());
            //ShowProcessTotal(Settings.Instance.ProcessList.ToList());
            SetRedLinePos();

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



            TimeStack.Children.Clear();
            ProcessStack.Children.Clear();
            VoiceStack.Children.Clear();
            m_dRedLine_Pos = 0;
            m_isFirstTimeRect = false;
            m_isFirstProcessRect = false;
            m_isFirstAudioRect = false;
            m_previousAudioEnd = 0;

            if (date_Picker.SelectedDate.Value.Year == DateTime.Now.Year && date_Picker.SelectedDate.Value.Month == DateTime.Now.Month && date_Picker.SelectedDate.Value.Day == DateTime.Now.Day)
            {
                SetComputerUsageStatus(Settings.Instance.ProcessList.ToList());
                SetProcessStatus(Settings.Instance.ProcessList.ToList());
                m_OldProcessList = Settings.Instance.ProcessList.ToList();
                SetVoiceStatus(Settings.Instance.AudioList.ToList());
                m_OldAudioList = Settings.Instance.AudioList.ToList();
                SetRedLinePos();
            }
            else
            {
                SetComputerUsageStatus(Settings.Instance.ProcessList_Day.ToList());
                SetProcessStatus(Settings.Instance.ProcessList_Day.ToList());
                m_OldProcessList = Settings.Instance.ProcessList_Day.ToList();
                SetVoiceStatus(Settings.Instance.AudioList_Day.ToList());
                m_OldAudioList = Settings.Instance.AudioList_Day.ToList();
                SetRedLinePos();
            }


        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] endData = Encoding.UTF8.GetBytes(Constants.Re_End);
                Settings.Instance.SockCom.Send(endData);
            }
            catch
            {

            }
            //DeleteTryIcon();
            EnvironmentHelper.Restart(true);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                byte[] endData = Encoding.UTF8.GetBytes(Constants.Re_End);
                Settings.Instance.SockCom.Send(endData);
            }
            catch
            {

            }
            //DeleteTryIcon();
            EnvironmentHelper.Restart(true);
        }
    }

    public class ProcessDetailItem
    {
        public BitmapSource List1Icon { get; set; }
        public string Process { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string State { get; set; }
    }

    public class ProcessTotalItem
    {
        public BitmapSource List2Icon { get; set; }
        public string Process { get; set; }
        public string Time { get; set; }
    }

    public class URL
    {
        public BitmapSource List2Icon { get; set; }
        public string strURL { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string State { get; set; }
    }

    public class DownloadInfo
    {
        public BitmapSource List2Icon { get; set; }
        public string FileName { get; set; }
        public string DownloadTime { get; set; }
        public string FileSize { get; set; }
    }

}
