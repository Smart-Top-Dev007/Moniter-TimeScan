using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Monitor.TaskControl.Globals;
using Monitor.TaskControl.Models;
using Monitor.TaskControl.Logger;
using Monitor.TaskControl.Utils;
using System.Windows.Threading;
using Monitor.TaskControl.View;

namespace Monitor.TaskControl.Communication
{

    public class Communications
    {

        public static ManualResetEvent allDone = new ManualResetEvent(false);


        public Communications()
        {

        }
        public static void StartUpSocket()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress m_IPAddress = GetIPAddress();
            IPEndPoint localEndPoint = new IPEndPoint(m_IPAddress, Constants.Port);
            IPEndPoint audioEndPoint = new IPEndPoint(m_IPAddress, Constants.AudioPort);
            IPEndPoint newEndPoint = new IPEndPoint(m_IPAddress, Constants.NewMainPort);
            Socket m_mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket m_audioSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket m_newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket[] socketList = { };
            try
            {
                m_mainSock.Bind(localEndPoint);
                m_mainSock.Listen(100);
                //socketList[0] = m_mainSock;

                //m_audioSock.Bind(audioEndPoint);
                //m_audioSock.Listen(100);
                //socketList[1] = m_audioSock;

                //m_newSock.Bind(newEndPoint);
                //m_newSock.Listen(100);
                //socketList[2] = m_newSock;

                Thread mthread = new Thread(new ParameterizedThreadStart(AcceptThread));
                mthread.Start(m_mainSock);

            }
            catch (Exception e)
            {
                CustomEx.DoExecption(Constants.exRepair, e);

            }

        }

        public static IPAddress GetIPAddress()
        {
            IPAddress m_IPAddress = null;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Log.Instance.DoLog(ni.Name, Log.LogType.Info);
                    if (ni.Name.StartsWith("Ethernet"))
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                Log.Instance.DoLog(ip.Address.ToString(), Log.LogType.Info);
                                string[] strIP = ip.Address.ToString().Split('.');
                                if (Convert.ToInt32(strIP[0]) == 192 && Convert.ToInt32(strIP[1]) == 168)
                                {
                                    if (Convert.ToInt32(strIP[2]) >= 103 && Convert.ToInt32(strIP[2]) <= 105)
                                    {
                                        m_IPAddress = ip.Address;
                                        return m_IPAddress;
                                    }

                                    if (Convert.ToInt32(strIP[2]) >= 107 && Convert.ToInt32(strIP[2]) <= 115)
                                    {
                                        m_IPAddress = ip.Address;
                                        return m_IPAddress;
                                    }


                                }

                            }
                        }
                    }

                }
            }
            return m_IPAddress;
        }


        public static void AcceptThread(Object obj)
        {
            Socket mSocket = (Socket)obj;
            while (true)
            {
                try
                {
                    Socket msocket = mSocket.Accept();
                    //Socket asocket = socketList[1].Accept();
                    //Socket nsocket = socketList[2].Accept();
                    string IP = GetIPAddressOfClient(msocket).ToString();

                    try
                    {
                        //Settings.Instance.ClientSocketDic.Add(IP, socket);
                        handleClient client = new handleClient(msocket);
                        Settings.Instance.SocketDic.Add(IP, client);
                        
                        client.StartClient();
                        // This point UserDisPlay()
                    }
                    catch (Exception e)
                    {
                        //Settings.Instance.ClientSocketDic.Remove(IP);
                        Settings.Instance.SocketDic[IP].Close();
                        Settings.Instance.SocketDic.Remove(IP);
                        //CustomEx.DoExecption(Constants.exRepair, e);

                    }
                }
                catch (Exception ex)
                {


                }


            }


        }

        public static void Send(Socket socket, byte[] data)
        {
            try
            {
                socket.Send(data, SocketFlags.None);
            }
            catch (Exception e)
            {
                CustomEx.DoExecption(Constants.exRepair, e);
            }
        }



        public static string GetIPAddressOfClient(Socket socket)
        {
            return ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
        }

    }
    public class handleClient
    {
        public Socket clientSocket;
        //public Socket aSocket;
        //public Socket nSocket;
        public ClientInfo clientInfo;
        public string IPAddress;

        public handleClient(Socket msocket)
        {
            this.clientSocket = msocket;
            //this.aSocket = asocket;
            //this.nSocket = nsocket;
            this.IPAddress = GetIPAddress(msocket);
        }
        public void StartClient()
        {
            try
            {
                Thread mthread = new Thread(Receive);
                mthread.Start();
            }
            catch (Exception ex)
            {

            }

        }
        public void Receive()
        {

            byte[] buffer = null;
            string prefix = "";

            while (true)
            {
                try
                {
                    if (!clientSocket.Connected)
                    {
                        Settings.Instance.ClientDic[IPAddress].NetworkState = false;
                        Close();
                        break;
                    }

                    buffer = new byte[1024 * 1024 * 2];
                    int byteData = clientSocket.Receive(buffer, SocketFlags.None);

                    if (byteData > 0)
                    {
                        prefix = Encoding.UTF8.GetString(buffer, 0, 4);
                        if (prefix == Constants.Re_ClientInfo)
                        {
                            string strPassword = "";
                            if (Settings.Instance.RegValue.ExistsGetValue())
                            {
                                strPassword = Settings.Instance.RegValue.Password;
                            }
                            else
                            {
                                strPassword = Constants.InitPassword;
                            }
                            string strTemp = Constants.Version + ":" + strPassword + ":" + Constants.SessionTime + ":" + Constants.SlideWidth + ":" + Constants.SlideHeight + ":" + Constants.CaptureWidth + ":" + Constants.CaptureHeight + ":" + Constants.CaptureTime + ":" + DateTime.Now.ToString("MM/dd/yyyy HH-mm-ss tt");
                            byte[] buff = Encoding.UTF8.GetBytes(Constants.Se_ClientInfo + strTemp);
                            clientSocket.Send(buff);

                        }
                        else if (prefix == Constants.Re_End)
                        {
                            Close();
                            break;

                        }
                        CommProc.Instance.RecDataAnalysis(buffer, byteData, IPAddress);
                    }
                }
                catch (Exception ex)
                {
                    Close();
                }

            }


        }

        public void Send(string strMessage)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(strMessage);
                this.clientSocket.Send(data);
            }
            catch (Exception e)
            {
                CustomEx.DoExecption(Constants.exRepair, e);
                Close();
            }

        }
        public string GetIPAddress(Socket socket)
        {
            return ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
        }

        public void Close()
        {
            try
            {
                Settings.Instance.ClientDic[IPAddress].NetworkState = false;
                Settings.Instance.SocketDic.Remove(IPAddress);
                clientSocket.Dispose();
            }
            catch (Exception ex)
            {
                CustomEx.DoExecption(Constants.exRepair, ex);
            }

        }
    }
}
