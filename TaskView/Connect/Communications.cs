using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Models;
using Monitor.TaskView.myEvents;
using Monitor.TaskView.Logger;
using System.IO;
using System.Diagnostics;
using Monitor.TaskView.Utils;
using System.Globalization;
using System.Windows.Threading;

namespace Monitor.TaskView.Connect
{
    public class Communications
    {
        //private static int bytePerSize = 1024;
        //private static byte[] byteData = new byte[bytePerSize];
        private static bool flag = false;
        public Communications()
        {
            //StartUpSocket();
        }
        public static void StartUpSocket()
        {
            flag = false;
            Settings.Instance.bStart = true;
            Settings.Instance.bSend = false;
            Settings.Instance.bConnect = false;
            string strIPAddress = "";
            if (Settings.Instance.RegValue.ServerIP == "")
            {
                strIPAddress = "127.0.0.1";
            }
            else
            {
                strIPAddress = Settings.Instance.RegValue.ServerIP;
            }
            IPAddress ipAddress = IPAddress.Parse(Settings.Instance.RegValue.ServerIP);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Constants.Port);
            Settings.Instance.SockCom = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (Settings.Instance.SockCom.Connected == true)
                {
                    return;
                }
                Settings.Instance.SockCom.Connect(localEndPoint);
                Settings.Instance.bConnect = true;


                string strComName = Environment.MachineName;
                string strOSDate = Directory.GetCreationTime(@"C:/Windows/system").ToString();

                //.Replace(",", "").Trim(); //+ Directory.GetCreationTime(Path.GetPathRoot(Environment.SystemDirectory)+@"Windows").ToLongTimeString();
                //DateTime date = DateTime.Parse(strOSDate);
                //strOSDate = date.ToString("MM/dd/yyyy HH:mm:ss");
                string strTemp = Settings.Instance.RegValue.UserName + ";" + Settings.Instance.RegValue.Password + ";" + Settings.Instance.RegValue.Company + ";" + Settings.Instance.RegValue.SessionTime + ";" + Settings.Instance.RegValue.CaptureTime + ";" + Settings.Instance.RegValue.SlideWidth + ";" + Settings.Instance.RegValue.SlideHeight + ";" + Settings.Instance.RegValue.CaptureWidth + ";" + Settings.Instance.RegValue.CaptureHeight + ";" + strComName + ";" + strOSDate;

                string strClientInfo = Constants.Re_ClientInfo + strTemp;
                byte[] buffer = Encoding.UTF8.GetBytes(strClientInfo);
                Thread.Sleep(2000);
                Settings.Instance.SockCom.Send(buffer);

                Thread receiveThread = new Thread(new ThreadStart(Receive));
                receiveThread.Start();
                Settings.Instance.bStart = false;
                //Thread checkThread = new Thread(new ThreadStart(Restart));
                //checkThread.Start();


            }
            catch (Exception e)
            {
                Settings.Instance.bConnect = false;
                Settings.Instance.bStart = false;
                CustomEx.DoExecption(Constants.exRepair, e);
                Thread disconnectThread = new Thread(new ThreadStart(Check));
                disconnectThread.Start();

            }

        }

        public static void Check()
        {
            Disconnect();
        }

        public static void Restart()
        {
            while (true)
            {
                if (flag == false)
                {
                    try
                    {
                        byte[] test = Encoding.UTF8.GetBytes("AAAAAA");
                        Settings.Instance.SockCom.Send(test);
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000 * 30);
                        Disconnect();
                    }
                }
                else
                {
                    break;
                }

                Thread.Sleep(1000 * 60 * 10);
            }
        }




        public static void Receive()
        {
            try
            {
                byte[] buffer = new byte[1024 * 5 * 1000];
                while (true)
                {
                    int byteData = Settings.Instance.SockCom.Receive(buffer, SocketFlags.None);
                    if (byteData > 0)
                    {
                        CommProc.Instance.RecDataAnalysis(buffer, byteData);
                    }
                }

            }
            catch (Exception e)
            {
                CustomEx.DoExecption(Constants.exRepair, e);
                Disconnect();
            }

        }

        public static void Disconnect()
        {
            try
            {
                if (Settings.Instance.bStart == false)
                {
                    Settings.Instance.bConnect = false;
                    Settings.Instance.bSend = false;
                    Settings.Instance.SockCom.Dispose();
                    flag = true;
                    Thread.Sleep(1000 * 10);
                    StartUpSocket();
                }

            }
            catch (Exception e)
            {
                CustomEx.DoExecption(Constants.exRepair, e);
            }

        }
    }
}
