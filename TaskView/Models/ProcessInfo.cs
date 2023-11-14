using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Logger;
using Monitor.TaskView.Utils;

namespace Monitor.TaskView.Models
{
    public class ProcessInfos
    {
        private static object _syncLock;
        public static bool isFirst, isSecond;
        //static DateTime preTime;

        //private const int WH_KEYBOARD_LL = 13;
        //private const int WH_MOUSE_LL = 14;
        //private static IntPtr _hookIDKeyboard = IntPtr.Zero;
        //private static IntPtr _hookIDMouse = IntPtr.Zero;


        //private static IntPtr SetHookMouse(LowLevelProc proc)
        //{
        //    using (Process curProcess = Process.GetCurrentProcess())
        //    using (ProcessModule curModule = curProcess.MainModule)
        //    {
        //        return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        //    }
        //}

        //private static IntPtr SetHookKeyboard(LowLevelProc proc)
        //{
        //    using (Process curProcess = Process.GetCurrentProcess())
        //    using (ProcessModule curModule = curProcess.MainModule)
        //    {
        //        return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        //    }
        //}

        //private static IntPtr HookCallbackKeyboard(int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //    preTime = DateTime.Now;
        //    return CallNextHookEx(_hookIDKeyboard, nCode, wParam, lParam);
        //}

        //private static IntPtr HookCallbackMouse(int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //    preTime = DateTime.Now;
        //    return CallNextHookEx(_hookIDMouse, nCode, wParam, lParam);
        //}

        //private delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //public void hookStart()
        //{
        //    _hookIDKeyboard = SetHookKeyboard(HookCallbackKeyboard);
        //    _hookIDMouse = SetHookMouse(HookCallbackMouse);
        //    try
        //    {
        //        Application.Run();
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomEx.DoExecption(Constants.exResume, ex);
        //    }
        //    UnhookWindowsHookEx(_hookIDKeyboard);
        //    UnhookWindowsHookEx(_hookIDMouse);
        //}

        public ProcessInfos()
        {
            //Thread thr = new Thread(new ThreadStart(hookStart));
            //thr.Start();
            _syncLock = new object();
            isFirst = true;
            isSecond = true;
            //preTime = DateTime.Now;
            LoadProcessInfo(Settings.Instance.Directories.TodayDirectory);
        }
        ~ProcessInfos()
        {
            //if (_hookIDKeyboard != null)
            //{
            //    UnhookWindowsHookEx(_hookIDKeyboard);
            //}
            //if (_hookIDMouse != null)
            //{
            //    UnhookWindowsHookEx(_hookIDMouse);
            //}
        }

        private static class NativeMethods
        {
            public struct LastInputInfo
            {
                public UInt32 cbSize;
                public UInt32 dwTime;
            }

            [DllImport("user32.dll")]
            public static extern bool GetLastInputInfo(ref LastInputInfo plii);
        }

        public static TimeSpan GetInputIdleTime()
        {
            var plii = new NativeMethods.LastInputInfo();
            plii.cbSize = (UInt32)Marshal.SizeOf(plii);

            if (NativeMethods.GetLastInputInfo(ref plii))
            {
                return TimeSpan.FromMilliseconds(Environment.TickCount - plii.dwTime);
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public void LoadProcessInfo(string strPath)
        {
            string dbOldPath = strPath + "\\" + Constants.DbFileNameTemp;
            strPath = strPath + "\\" + Constants.DbFileName;
            List<string> strTempList = new List<string>();
            strTempList = Md5Crypto.ReadCryptoFile(strPath);
            if (strTempList.Count == 0)
            {
                return;
            }
            //if (!File.Exists(strPath))
            //{
            //    return;
            //}
            Settings.Instance.ProcessList = new List<ListOfProcessByOrder>();
            //string line = "";
            //int nCount = 1;
            String[] spearator = { Constants.filePattern };

            foreach (var line in strTempList)
            {
                try
                {
                    string[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOPBO.ProcessName = strArray[0];
                    Settings.Instance.LOPBO.ProcessWindow = strArray[1];
                    Settings.Instance.LOPBO.ProcessPath = strArray[2];
                    Settings.Instance.LOPBO.ProcessStartTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOPBO.ProcessEndTime = DateTime.Parse(strArray[4]);
                    try
                    {
                        Settings.Instance.LOPBO.ProcessColor = strArray[5];
                    }
                    catch
                    {
                        Settings.Instance.LOPBO.ProcessColor = Constants.Default;
                    }
                    Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }

            var strAddData = Md5Crypto.ReadCryptoFile(dbOldPath);
            if (strAddData.Count == 0)
            {
                return;
            }

            foreach (var line in strAddData)
            {
                try
                {
                    if (line == "")
                    {
                        continue;
                    }
                    string[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOPBO.ProcessName = strArray[0];
                    Settings.Instance.LOPBO.ProcessWindow = strArray[1];
                    Settings.Instance.LOPBO.ProcessPath = strArray[2];
                    Settings.Instance.LOPBO.ProcessStartTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOPBO.ProcessEndTime = DateTime.Parse(strArray[4]);
                    try
                    {
                        Settings.Instance.LOPBO.ProcessColor = strArray[5];
                    }
                    catch
                    {
                        Settings.Instance.LOPBO.ProcessColor = Constants.Default;
                    }
                    Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }

        }

        private int mostFrequent(int[] arr, int n)
        {
            // Sort the array 
            Array.Sort(arr);

            // find the max frequency using  
            // linear traversal 
            int max_count = 1, res = arr[0];
            int curr_count = 1;

            for (int i = 1; i < n; i++)
            {
                if (arr[i] == arr[i - 1])
                    curr_count++;
                else
                {
                    if (curr_count > max_count)
                    {
                        max_count = curr_count;
                        res = arr[i - 1];
                    }
                    curr_count = 1;
                }
            }

            // If last element is most frequent 
            if (curr_count > max_count)
            {
                max_count = curr_count;
                res = arr[n - 1];
            }

            return res;
        }

        public void GetPieChartProcess()
        {
            
        }

        public byte[] GetProcessInfo()
        {
            string strProName = ""; string strProWindow = ""; string strProPath = ""; string strProColor = "";
            try
            {
                IntPtr hWnd = NativeImports.GetForegroundWindow();
                uint procId = 0;
                NativeImports.GetWindowThreadProcessId(hWnd, out procId);
                var proc = Process.GetProcessById((int)procId);
                strProName = proc.ProcessName + ".exe";
                if (strProName == "LockApp.exe")
                {
                    strProName = Constants.RestProcess;
                }
                strProWindow = proc.MainWindowTitle;
                if (strProWindow == "")
                {
                    strProWindow = Constants.Unknown;
                }

                strProPath = Constants.Unknown;
                var buffer = new StringBuilder(1024);
                IntPtr hprocess = NativeImports.OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, (int)procId);
                if (hprocess != IntPtr.Zero)
                {
                    try
                    {
                        int size = buffer.Capacity;
                        if (NativeImports.QueryFullProcessImageName(hprocess, 0, buffer, out size))
                        {
                            strProPath = buffer.ToString();
                        }
                    }
                    finally
                    {
                        NativeImports.CloseHandle(hprocess);
                    }
                }

                Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(strProPath);
                Bitmap bMap = ico.ToBitmap();

                int[] arrGray = new int[16 * 16];
                List<int> arrTemp = new List<int>();
                int nTemp = 0;

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        System.Drawing.Color oc = bMap.GetPixel(x, y);
                        int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                        arrGray[x * 16 + y] = grayScale;
                        if (grayScale != 0 && grayScale != 255)
                        {
                            arrTemp.Add(grayScale);
                            nTemp++;
                        }
                    }
                }
                int nGrayValue = mostFrequent(arrTemp.ToArray(), nTemp);
                int nIndex = Array.IndexOf(arrGray, nGrayValue);
                System.Drawing.Color maxColor = bMap.GetPixel(nIndex / 16, nIndex % 16);
                strProColor = maxColor.R.ToString() + "," + maxColor.G.ToString() + "," + maxColor.B.ToString();
            }
            catch
            {
                strProName = Constants.RestProcess;
                strProWindow = Constants.Unknown;
                strProPath = Constants.Unknown;
                strProColor = "";
            }

            DateTime localDate = DateTime.Now;

            if (isFirst == true)
            {
                try
                {
                    if (Settings.Instance.ProcessList.Count != 0)
                    {
                        DateTime startTemp = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessEndTime;
                        //Settings.Instance.ProcessList.RemoveAt(Settings.Instance.ProcessList.Count - 1);
                        Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                        Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                        Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                        Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate;
                        Settings.Instance.LOPBO.ProcessColor = "";
                        Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);

                        Settings.Instance.LOPBO.ProcessName = strProName;
                        Settings.Instance.LOPBO.ProcessWindow = strProWindow;
                        Settings.Instance.LOPBO.ProcessPath = strProPath;
                        Settings.Instance.LOPBO.ProcessStartTime = localDate;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate;
                        Settings.Instance.LOPBO.ProcessColor = strProColor;
                        Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                        if (Settings.Instance.ProcessList.Count > 1)
                        {
                            string strDbPath = Settings.Instance.Directories.TodayDirectory;
                            string strData = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessColor;
                            Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.DbFileName, strData);
                        }

                        //strProName = Constants.RestProcess;
                        //strProPath = Constants.Unknown;
                        //strProWindow = Constants.Unknown;
                        //strProColor = "";
                        //
                        //if (Settings.Instance.ProcessList.Count > 2)
                        //{
                        //    string strDbPath = Settings.Instance.Directories.TodayDirectory;
                        //    string strData = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessColor;
                        //    Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.DbFileName, strData);
                        //}
                    }
                    else
                    {
                        Settings.Instance.LOPBO.ProcessName = strProName;
                        Settings.Instance.LOPBO.ProcessWindow = strProWindow;
                        Settings.Instance.LOPBO.ProcessPath = strProPath;
                        Settings.Instance.LOPBO.ProcessStartTime = localDate;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate;
                        Settings.Instance.LOPBO.ProcessColor = strProColor;
                        Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                    }
                    //new file
                    string strDbPathTemp = Settings.Instance.Directories.TodayDirectory;
                    string strDataTemp = strProName + Constants.filePattern + strProWindow + Constants.filePattern + strProPath + Constants.filePattern + localDate.ToString() + Constants.filePattern + localDate.ToString() + Constants.filePattern + strProColor;
                    Md5Crypto.WriteFileReplace(strDbPathTemp, Constants.DbFileNameTemp, strDataTemp);
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
            else
            {
                try
                {
                    TimeSpan preTimeSpan = GetInputIdleTime();
                    if ((int)preTimeSpan.Hours * 3600 + preTimeSpan.Minutes * 60 + preTimeSpan.Seconds > (int)Settings.Instance.RegValue.ActiveDuration)
                    {
                        string name = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessName;
                        //string window = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessWindow;
                        //string path = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessPath;
                        //string color = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessColor;
                        DateTime startTemp = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessStartTime;
                        DateTime endTemp = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessEndTime;
                        //Settings.Instance.ProcessList.RemoveAt(Settings.Instance.ProcessList.Count - 1);
                        //Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                        //Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                        //Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                        //Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                        //Settings.Instance.LOPBO.ProcessEndTime = localDate;
                        //Settings.Instance.LOPBO.ProcessColor = "";
                        //Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);

                        if (Constants.RestProcess != name)
                        {
                            Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                            Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessStartTime = endTemp;
                            Settings.Instance.LOPBO.ProcessEndTime = localDate;
                            Settings.Instance.LOPBO.ProcessColor = "";
                            Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                            //
                            if (Settings.Instance.ProcessList.Count > 1)
                            {
                                string strDbPath = Settings.Instance.Directories.TodayDirectory;
                                string strData = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessColor;
                                Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.DbFileName, strData);
                            }
                        }
                        else
                        {
                            Settings.Instance.ProcessList.RemoveAt(Settings.Instance.ProcessList.Count - 1);
                            Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                            Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                            Settings.Instance.LOPBO.ProcessEndTime = localDate;
                            Settings.Instance.LOPBO.ProcessColor = "";
                            Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                        }
                        strProName = Constants.RestProcess;
                        strProPath = Constants.Unknown;
                        strProWindow = Constants.Unknown;
                        strProColor = "";
                    }
                    else
                    {
                        string name = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessName;
                        string window = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessWindow;
                        string path = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessPath;
                        string color = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessColor;
                        DateTime startTemp = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessStartTime;
                        Settings.Instance.ProcessList.RemoveAt(Settings.Instance.ProcessList.Count - 1);
                        Settings.Instance.LOPBO.ProcessName = name;
                        Settings.Instance.LOPBO.ProcessWindow = window;
                        Settings.Instance.LOPBO.ProcessPath = path;
                        Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate;
                        Settings.Instance.LOPBO.ProcessColor = color;
                        Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                        if (strProName != name)
                        {
                            Settings.Instance.LOPBO.ProcessName = strProName;
                            Settings.Instance.LOPBO.ProcessWindow = strProWindow;
                            Settings.Instance.LOPBO.ProcessPath = strProPath;
                            Settings.Instance.LOPBO.ProcessStartTime = localDate;
                            Settings.Instance.LOPBO.ProcessEndTime = localDate;
                            Settings.Instance.LOPBO.ProcessColor = strProColor;
                            Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);
                            //
                            if (Settings.Instance.ProcessList.Count > 1)
                            {
                                string strDbPath = Settings.Instance.Directories.TodayDirectory;
                                string strData = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessColor;
                                Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.DbFileName, strData);
                            }
                            //new file
                            string strDbPathTemp = Settings.Instance.Directories.TodayDirectory;
                            string strDataTemp = strProName + Constants.filePattern + strProWindow + Constants.filePattern + strProPath + Constants.filePattern + localDate.ToString() + Constants.filePattern + localDate.ToString() + Constants.filePattern + strProColor;
                            Md5Crypto.WriteFileReplace(strDbPathTemp, Constants.DbFileNameTemp, strDataTemp);
                        }
                        else
                        {
                            //new file
                            string strDbPathTemp = Settings.Instance.Directories.TodayDirectory;
                            string strDataTemp = strProName + Constants.filePattern + strProWindow + Constants.filePattern + strProPath + Constants.filePattern + startTemp.ToString() + Constants.filePattern + localDate.ToString() + Constants.filePattern + strProColor;
                            Md5Crypto.WriteFileReplace(strDbPathTemp, Constants.DbFileNameTemp, strDataTemp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }

            string sendData = "N" + strProName + Constants.filePattern + "W" + strProWindow + Constants.filePattern + "P" + strProPath + Constants.filePattern + "T" + strProColor;

            byte[] bytes = Encoding.ASCII.GetBytes(sendData);

            isFirst = false;
            return bytes;
        }

        public byte[] GetProcessInfoServer(string strProcessFile)
        {
            //if (!Settings.Instance.RegValue.isClientServer_Span) return null;
            string strProName = ""; string strProWindow = ""; string strProPath = ""; string strProColor = "";
            try
            {
                IntPtr hWnd = NativeImports.GetForegroundWindow();
                uint procId = 0;
                NativeImports.GetWindowThreadProcessId(hWnd, out procId);
                var proc = Process.GetProcessById((int)procId);
                strProName = proc.ProcessName + ".exe";
                if (strProName == "LockApp.exe")
                {
                    strProName = Constants.RestProcess;
                }
                strProWindow = proc.MainWindowTitle;
                if (strProWindow == "")
                {
                    strProWindow = Constants.Unknown;
                }

                strProPath = Constants.Unknown;
                var buffer = new StringBuilder(1024);
                IntPtr hprocess = NativeImports.OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, (int)procId);
                if (hprocess != IntPtr.Zero)
                {
                    try
                    {
                        int size = buffer.Capacity;
                        if (NativeImports.QueryFullProcessImageName(hprocess, 0, buffer, out size))
                        {
                            strProPath = buffer.ToString();
                        }
                    }
                    finally
                    {
                        NativeImports.CloseHandle(hprocess);
                    }
                }

                Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(strProPath);
                Bitmap bMap = ico.ToBitmap();

                int[] arrGray = new int[16 * 16];
                List<int> arrTemp = new List<int>();
                int nTemp = 0;

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        System.Drawing.Color oc = bMap.GetPixel(x, y);
                        int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                        arrGray[x * 16 + y] = grayScale;
                        if (grayScale != 0 && grayScale != 255)
                        {
                            arrTemp.Add(grayScale);
                            nTemp++;
                        }
                    }
                }
                int nGrayValue = mostFrequent(arrTemp.ToArray(), nTemp);
                int nIndex = Array.IndexOf(arrGray, nGrayValue);
                System.Drawing.Color maxColor = bMap.GetPixel(nIndex / 16, nIndex % 16);
                strProColor = maxColor.R.ToString() + "," + maxColor.G.ToString() + "," + maxColor.B.ToString();
            }
            catch
            {
                strProName = Constants.RestProcess;
                strProWindow = Constants.Unknown;
                strProPath = Constants.Unknown;
                strProColor = "";
            }

            DateTime localDate = DateTime.Now;

            if (isSecond == true)
            {
                try
                {
                    if (Settings.Instance.ProcessClientList.Count != 0)
                    {
                        DateTime startTemp = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessEndTime;
                        Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                        Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                        Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                        Settings.Instance.LOPBO.ProcessStartTime = startTemp.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessColor = "";
                        Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);

                        Settings.Instance.LOPBO.ProcessName = strProName;
                        Settings.Instance.LOPBO.ProcessWindow = strProWindow;
                        Settings.Instance.LOPBO.ProcessPath = strProPath;
                        Settings.Instance.LOPBO.ProcessStartTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessColor = strProColor;
                        Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);
                        if (Settings.Instance.ProcessClientList.Count > 1)
                        {
                            string strDbPath = Settings.Instance.Directories.TodayDirectory;
                            string strData = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessColor;
                            Md5Crypto.WriteCryptoFileAppend(strDbPath, strProcessFile, strData);
                        }

                        //strProName = Constants.RestProcess;
                        //strProPath = Constants.Unknown;
                        //strProWindow = Constants.Unknown;
                        //strProColor = "";
                        //
                        //if (Settings.Instance.ProcessList.Count > 2)
                        //{
                        //    string strDbPath = Settings.Instance.Directories.TodayDirectory;
                        //    string strData = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 2].ProcessColor;
                        //    Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.DbFileName, strData);
                        //}
                    }
                    else
                    {
                        Settings.Instance.LOPBO.ProcessName = strProName;
                        Settings.Instance.LOPBO.ProcessWindow = strProWindow;
                        Settings.Instance.LOPBO.ProcessPath = strProPath;
                        Settings.Instance.LOPBO.ProcessStartTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessColor = strProColor;
                        Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);
                    }
                    //new file
                    string strDbPathTemp = Settings.Instance.Directories.TodayDirectory;
                    string strDataTemp = strProName + Constants.filePattern + strProWindow + Constants.filePattern + strProPath + Constants.filePattern + localDate.ToString() + Constants.filePattern + localDate.ToString() + Constants.filePattern + strProColor;
                    Md5Crypto.WriteFileReplace(strDbPathTemp, Constants.DbServerFileNameTemp, strDataTemp);
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
            else
            {
                try
                {
                    TimeSpan preTimeSpan = GetInputIdleTime();
                    if ((int)preTimeSpan.Hours * 3600 + preTimeSpan.Minutes * 60 + preTimeSpan.Seconds > (int)Settings.Instance.RegValue.ActiveDuration)
                    {
                        string name = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessName;
                        //string window = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessWindow;
                        //string path = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessPath;
                        //string color = Settings.Instance.ProcessList[Settings.Instance.ProcessList.Count - 1].ProcessColor;
                        DateTime startTemp = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessStartTime;
                        DateTime endTemp = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessEndTime;
                        //Settings.Instance.ProcessList.RemoveAt(Settings.Instance.ProcessList.Count - 1);
                        //Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                        //Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                        //Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                        //Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                        //Settings.Instance.LOPBO.ProcessEndTime = localDate;
                        //Settings.Instance.LOPBO.ProcessColor = "";
                        //Settings.Instance.ProcessList.Add(Settings.Instance.LOPBO);

                        if (Constants.RestProcess != name)
                        {
                            Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                            Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessStartTime = endTemp;
                            Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span);
                            Settings.Instance.LOPBO.ProcessColor = "";
                            Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);
                            //
                            if (Settings.Instance.ProcessClientList.Count > 1)
                            {
                                string strDbPath = Settings.Instance.Directories.TodayDirectory;
                                string strData = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessColor;
                                Md5Crypto.WriteCryptoFileAppend(strDbPath, strProcessFile, strData);
                            }
                        }
                        else
                        {
                            Settings.Instance.ProcessClientList.RemoveAt(Settings.Instance.ProcessClientList.Count - 1);
                            Settings.Instance.LOPBO.ProcessName = Constants.RestProcess;
                            Settings.Instance.LOPBO.ProcessWindow = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessPath = Constants.Unknown;
                            Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                            Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span);
                            Settings.Instance.LOPBO.ProcessColor = "";
                            Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);
                        }
                        strProName = Constants.RestProcess;
                        strProPath = Constants.Unknown;
                        strProWindow = Constants.Unknown;
                        strProColor = "";
                    }
                    else
                    {
                        string name = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessName;
                        string window = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessWindow;
                        string path = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessPath;
                        string color = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessColor;
                        DateTime startTemp = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 1].ProcessStartTime;
                        Settings.Instance.ProcessClientList.RemoveAt(Settings.Instance.ProcessClientList.Count - 1);
                        Settings.Instance.LOPBO.ProcessName = name;
                        Settings.Instance.LOPBO.ProcessWindow = window;
                        Settings.Instance.LOPBO.ProcessPath = path;
                        Settings.Instance.LOPBO.ProcessStartTime = startTemp;
                        Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                        Settings.Instance.LOPBO.ProcessColor = color;
                        Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);
                        if (strProName != name)
                        {
                            Settings.Instance.LOPBO.ProcessName = strProName;
                            Settings.Instance.LOPBO.ProcessWindow = strProWindow;
                            Settings.Instance.LOPBO.ProcessPath = strProPath;
                            Settings.Instance.LOPBO.ProcessStartTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                            Settings.Instance.LOPBO.ProcessEndTime = localDate.Add(Settings.Instance.RegValue.ClientServer_Span); ;
                            Settings.Instance.LOPBO.ProcessColor = strProColor;
                            Settings.Instance.ProcessClientList.Add(Settings.Instance.LOPBO);
                            //
                            if (Settings.Instance.ProcessClientList.Count > 1)
                            {
                                string strDbPath = Settings.Instance.Directories.TodayDirectory;
                                string strData = Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessName + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessWindow + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessPath + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessStartTime.ToString() + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessEndTime.ToString() + Constants.filePattern + Settings.Instance.ProcessClientList[Settings.Instance.ProcessClientList.Count - 2].ProcessColor;
                                Md5Crypto.WriteCryptoFileAppend(strDbPath, strProcessFile, strData);
                            }
                            //new file
                            string strDbPathTemp = Settings.Instance.Directories.TodayDirectory;
                            string strDataTemp = strProName + Constants.filePattern + strProWindow + Constants.filePattern + strProPath + Constants.filePattern + localDate.Add(Settings.Instance.RegValue.ClientServer_Span).ToString() + Constants.filePattern + localDate.Add(Settings.Instance.RegValue.ClientServer_Span).ToString() + Constants.filePattern + strProColor;
                            Md5Crypto.WriteFileReplace(strDbPathTemp, Constants.DbServerFileNameTemp, strDataTemp);
                        }
                        else
                        {
                            //new file
                            string strDbPathTemp = Settings.Instance.Directories.TodayDirectory;
                            string strDataTemp = strProName + Constants.filePattern + strProWindow + Constants.filePattern + strProPath + Constants.filePattern + startTemp.ToString() + Constants.filePattern + localDate.Add(Settings.Instance.RegValue.ClientServer_Span).ToString() + Constants.filePattern + strProColor;
                            Md5Crypto.WriteFileReplace(strDbPathTemp, Constants.DbServerFileNameTemp, strDataTemp);

                        }
                    }
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }

            string sendData = "N" + strProName + Constants.filePattern + "W" + strProWindow + Constants.filePattern + "P" + strProPath + Constants.filePattern + "T" + strProColor;

            byte[] bytes = Encoding.ASCII.GetBytes(sendData);

            isSecond = false;
            return bytes;
        }

        public void SaveSlideProcess()
        {
            string strDbPath = Settings.Instance.Directories.TodayDirectory;
            //if (!Directory.Exists(strDbPath))
            //{
            //    Directory.CreateDirectory(strDbPath);
            //}
            //strDbPath = Path.Combine(strDbPath, Constants.DbFileName);
            List<string> strTempList = new List<string>();
            foreach (ListOfProcessByOrder list in Settings.Instance.ProcessList.ToList())
            {
                strTempList.Add(list.ProcessName + Constants.filePattern + list.ProcessWindow + Constants.filePattern + list.ProcessPath + Constants.filePattern + list.ProcessStartTime.ToString() + Constants.filePattern + list.ProcessEndTime.ToString());
            }
            Md5Crypto.WriteCryptoFile(strDbPath, Constants.DbFileName, strTempList);

        }

    }

}
