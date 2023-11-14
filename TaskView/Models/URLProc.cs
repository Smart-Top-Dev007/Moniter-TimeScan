using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Automation;
using System.Text.RegularExpressions;
using Monitor.TaskView.Utils;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Connect;

namespace Monitor.TaskView.Models
{
    public class URLProc
    {
        static bool bFirst;
        public URLProc()
        {
            bFirst = true;
            LoadURLInfo(Settings.Instance.Directories.TodayDirectory);
        }
        public string GetChromeURL()
        {
            string outURL = "";
            AutomationElement elm = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                        new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1"));
            if (elm == null) return outURL;

            AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
            if (!(bool)elmUrlBar.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty))
            {
                AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                if (patterns.Length == 1)
                {
                    try
                    {
                        outURL = ((ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0])).Current.Value;
                    }
                    catch { }
                    if (outURL != "")
                    {
                        // must match a domain name (and possibly "https://" in front)
                        if (Regex.IsMatch(outURL, @"^(https:\/\/)?[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,4}).*$"))
                        {
                            // prepend http:// to the url, because Chrome hides it if it's not SSL
                            if (!outURL.StartsWith("http"))
                            {
                                outURL = "http://" + outURL;
                            }
                            return outURL;
                        }
                    }
                }
            }

            return outURL;
        }
        public string GetFireFoxURL()
        {
            string outURL = "";
            try
            {
                AutomationElement root = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                        new PropertyCondition(AutomationElement.ClassNameProperty, "MozillaWindowClass"));
                if (root == null) return outURL;

                Condition toolBar = new AndCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar),
                new PropertyCondition(AutomationElement.NameProperty, "Browser tabs"));
                if (toolBar == null) return outURL;

                var tool = root.FindFirst(TreeScope.Children, toolBar);
                if (tool == null) return outURL;

                var tool2 = TreeWalker.ControlViewWalker.GetNextSibling(tool);
                if (tool2 == null) return outURL;

                var children = tool2.FindAll(TreeScope.Children, Condition.TrueCondition);
                if (children == null) return outURL;

                foreach (AutomationElement item in children)
                {
                    foreach (AutomationElement i in item.FindAll(TreeScope.Children, Condition.TrueCondition))
                    {
                        foreach (AutomationElement ii in i.FindAll(TreeScope.Element, Condition.TrueCondition))
                        {
                            if (ii.Current.LocalizedControlType == "edit")
                            {
                                //   if (!ii.Current.BoundingRectangle.X.ToString().Contains("empty"))
                                {
                                    ValuePattern activeTab = ii.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                                    outURL = activeTab.Current.Value;
                                    return outURL;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return outURL;
            }
            return outURL;

        }
        public string GetIEURL()
        {
            string outURL = "";


            return outURL;
        }
        public string GetMicrosoftEdge()
        {
            string outURL = "";


            return outURL;
        }

        public void LoadURLInfo(string strPath)
        {
            strPath = strPath + "\\" + Constants.urlFileName;
            List<string> strTempList = new List<string>();
            strTempList = Md5Crypto.ReadCryptoFile(strPath);
            if (strTempList.Count == 0)
            {
                return;
            }
            Settings.Instance.URLList = new List<ListOfUrl>();
            String[] spearator = { Constants.filePattern };
            foreach (var line in strTempList)
            {
                try
                {
                    string[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOU.strWindow = strArray[0];
                    Settings.Instance.LOU.strURL = strArray[1];
                    Settings.Instance.LOU.URLStartTime = DateTime.Parse(strArray[2]);
                    Settings.Instance.LOU.URLEndTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOU.BrowserType = (byte)Int32.Parse(strArray[4]);
                    Settings.Instance.URLList.Add(Settings.Instance.LOU);
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
        }

        //public void SaveURLList()
        //{
        //    string strDbPath = Settings.Instance.Directories.TodayDirectory;
        //    List<string> strTempList = new List<string>();
        //    foreach (ListOfUrl list in Settings.Instance.URLList.ToList())
        //    {
        //        strTempList.Add(list.strWindow + Constants.filePattern + list.strURL + Constants.filePattern + list.URLStartTime.ToString() + Constants.filePattern + list.URLEndTime.ToString() + Constants.filePattern + list.BrowserType.ToString());
        //    }
        //    Md5Crypto.WriteCryptoFile(strDbPath, Constants.urlFileName, strTempList);

        //}

        public void URLProcFunc()
        {
            IntPtr hWnd = NativeImports.GetForegroundWindow();
            uint procId = 0;
            NativeImports.GetWindowThreadProcessId(hWnd, out procId);
            var proc = Process.GetProcessById((int)procId);
            string strProName = proc.ProcessName + ".exe";
            string strProWindow = proc.MainWindowTitle;
            DateTime _dateTime = DateTime.Now;
            string strURL = "";

            if (strProName == "chrome.exe")
            {
                strURL = GetChromeURL();
                Settings.Instance.LOU.BrowserType = (int)Browser.Chrome;
            }
            else if (strProName == "firefox.exe")
            {
                strURL = GetFireFoxURL();
                Settings.Instance.LOU.BrowserType = (int)Browser.Firefox;
            }
            else if (strProName == "Edge")
            {
                strURL = GetMicrosoftEdge();
                Settings.Instance.LOU.BrowserType = (int)Browser.Edge;
            }
            else if (strProName == "IE")
            {
                strURL = GetIEURL();
                Settings.Instance.LOU.BrowserType = (int)Browser.IE;
            }

            // save to list
            if (strURL != "")
            {

                byte nUrlType = Settings.Instance.LOU.BrowserType;
                if (bFirst == true)
                {
                    Settings.Instance.LOU.strWindow = strProWindow;
                    Settings.Instance.LOU.strURL = strURL;
                    Settings.Instance.LOU.URLStartTime = _dateTime;
                    Settings.Instance.LOU.URLEndTime = _dateTime.AddSeconds(Constants.urlSessionTime);
                    Settings.Instance.URLList.Add(Settings.Instance.LOU);
                }
                else
                {
                    if (strURL != Settings.Instance.URLList[Settings.Instance.URLList.Count - 1].strURL)
                    {
                        Settings.Instance.LOU.strWindow = strProWindow;
                        Settings.Instance.LOU.strURL = strURL;
                        Settings.Instance.LOU.BrowserType = nUrlType;
                        Settings.Instance.LOU.URLStartTime = _dateTime;
                        Settings.Instance.LOU.URLEndTime = _dateTime.AddSeconds(Constants.urlSessionTime);
                        Settings.Instance.URLList.Add(Settings.Instance.LOU);

                        if (Settings.Instance.URLList.Count > 2)
                        {
                            Settings.Instance.nUrlCount++;
                            string strDbPath = Settings.Instance.Directories.TodayDirectory;
                            string strData = Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].strWindow + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].strURL + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].URLStartTime.ToString() + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].URLEndTime.ToString() + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].BrowserType.ToString();
                            Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.urlFileName, strData);
                        }
                    }
                    else
                    {
                        if (_dateTime.Subtract(Settings.Instance.URLList[Settings.Instance.URLList.Count - 1].URLEndTime).TotalSeconds < Constants.urlActiveTime + 4)
                        {
                            DateTime startTemp = Settings.Instance.URLList[Settings.Instance.URLList.Count - 1].URLStartTime;
                            Settings.Instance.URLList.RemoveAt(Settings.Instance.URLList.Count - 1);

                            Settings.Instance.LOU.strWindow = strProWindow;
                            Settings.Instance.LOU.strURL = strURL;
                            Settings.Instance.LOU.BrowserType = nUrlType;
                            Settings.Instance.LOU.URLStartTime = startTemp;
                            Settings.Instance.LOU.URLEndTime = _dateTime.AddSeconds(Constants.urlSessionTime);
                            Settings.Instance.URLList.Add(Settings.Instance.LOU);
                        }
                        else
                        {
                            Settings.Instance.LOU.strWindow = strProWindow;
                            Settings.Instance.LOU.strURL = strURL;
                            Settings.Instance.LOU.BrowserType = nUrlType;
                            Settings.Instance.LOU.URLStartTime = _dateTime;
                            Settings.Instance.LOU.URLEndTime = _dateTime.AddSeconds(Constants.urlSessionTime);
                            Settings.Instance.URLList.Add(Settings.Instance.LOU);

                            if (Settings.Instance.URLList.Count > 2)
                            {
                                Settings.Instance.nUrlCount++;
                                string strDbPath = Settings.Instance.Directories.TodayDirectory;
                                string strData = Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].strWindow + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].strURL + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].URLStartTime.ToString() + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].URLEndTime.ToString() + Constants.filePattern + Settings.Instance.URLList[Settings.Instance.URLList.Count - 2].BrowserType.ToString();
                                Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.urlFileName, strData);
                            }
                        }
                    }
                }
                bFirst = false;
                string sendData = strProWindow + Constants.filePattern + strURL + Constants.filePattern + _dateTime + Constants.filePattern + nUrlType.ToString();

                foreach (var strFU in Constants.strForbiddenURL)
                {
                    if (strURL.ToLower().Contains(strFU.ToLower()) && !strURL.ToLower().Contains(Constants.TranslateCom.ToLower().Trim()) && !strURL.ToLower().Contains(Constants.Updating.ToLower().Trim()))
                        Md5Crypto.WriteCryptoFileAppend(Settings.Instance.Directories.TodayDirectory, Constants.DangerURLFile, strURL);
                }

                int DangerURL_Count = 0;
                DangerURL_Count  = Md5Crypto.ReadCryptoFile(Path.Combine(Settings.Instance.Directories.TodayDirectory, Constants.DangerURLFile)).Count();

                //if (DangerURL_Count)
                sendData = Constants.filePattern + DangerURL_Count.ToString();

                byte[] bytes = Encoding.UTF8.GetBytes(sendData);
                SendDataAnalysis(Constants.Re_DataURL, bytes, bytes.Length);
            }
        }

        public void SendDataAnalysis(string strProtocol, byte[] buf, int length)
        {
            byte[] sendBuffer;
            if (Settings.Instance.bSend == true)
            {
                byte[] bytePrefix = Encoding.UTF8.GetBytes(strProtocol);
                sendBuffer = new byte[length + 4];
                bytePrefix.CopyTo(sendBuffer, 0);
                buf.CopyTo(sendBuffer, 4);
                try
                {
                    Settings.Instance.SockCom.Send(sendBuffer, SocketFlags.None);
                }
                catch (Exception e)
                {
                    Array.Clear(sendBuffer, 0, sendBuffer.Length);
                    CustomEx.DoExecption(Constants.exRepair, e);
                    Communications.Disconnect();
                }
                Array.Clear(sendBuffer, 0, sendBuffer.Length);
            }

        }
    }
}
