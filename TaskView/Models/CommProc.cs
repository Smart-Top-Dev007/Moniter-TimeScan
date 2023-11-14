using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Connect;
using System.IO;
using Monitor.TaskView.View;
using System.Windows.Threading;
using Monitor.TaskView.Utils;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net.Sockets;
using System.Threading;

namespace Monitor.TaskView.Models
{
    class CommProc
    {
        private static CommProc _instance;
        private RemoteProc remoteProc = new RemoteProc();
        public static CommProc Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommProc();
                }

                return _instance;
            }
        }

        CommProc()
        {

        }

        public void Start()
        {
            //Communications.StartUpSocket();
        }
        public void RecDataAnalysis(byte[] buf, int length)
        {
            string prefix = Encoding.UTF8.GetString(buf, 0, 4);
            byte[] temp = new byte[length - 4];
            Array.Copy(buf, 4, temp, 0, length - 4);
            if (prefix == Constants.Se_ClientInfo)
            {
                string strClientData = Encoding.UTF8.GetString(temp);
                string[] clientData = strClientData.Split(':');
                Settings.Instance.RegValue.Version = clientData[0];
                Settings.Instance.RegValue.Password = clientData[1];
                Settings.Instance.RegValue.SessionTime = Convert.ToInt32(clientData[2]);
                Settings.Instance.RegValue.SlideHeight = Convert.ToInt32(clientData[4]);
                Settings.Instance.RegValue.SlideWidth = Convert.ToInt32(clientData[3]);
                Settings.Instance.RegValue.CaptureHeight = Convert.ToInt32(clientData[6]);
                Settings.Instance.RegValue.CaptureWidth = Convert.ToInt32(clientData[5]);

                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                //var c = CultureInfo.CreateSpecificCulture("pt-BR");

                // Defining Format and Testing it via "DateTime.ToString(format)"
                string format = "MM/dd/yyyy HH:mm:ss tt";
                //string dtNow = "2/21/2021 8:10:20 AM";//DateTime.Now.ToString (format);
                //Console.WriteLine("Date Time Now : " + clientData[8]);

                // Trying to Parse DateTime on the same Format defined Above
                Settings.Instance.RegValue.isClientServer_Span = false;
                DateTime ServerTime;
                string _ServerTime = clientData[8].Replace('-', ':');
                if (DateTime.TryParseExact(_ServerTime, format, provider, System.Globalization.DateTimeStyles.None, out ServerTime))
                {
                    Settings.Instance.RegValue.ClientServer_Span = ServerTime.Subtract(DateTime.Now);
                    if (ServerTime.Year != DateTime.Now.Year || ServerTime.Month != DateTime.Now.Month || ServerTime.Day != DateTime.Now.Day || ServerTime.Hour != DateTime.Now.Hour)
                    {
                        Settings.Instance.RegValue.isClientServer_Span = true;
                        Settings.Instance.RegValue.ClientServer_Span = ServerTime.Subtract(DateTime.Now);
                    }
                    //if (ServerTime.Subtract(DateTime.Now).TotalSeconds > 0)
                    //{
                    //    Settings.Instance.RegValue.isClientServer_Span = true;
                    //    Settings.Instance.RegValue.ClientServer_Span = (ServerTime.Subtract(DateTime.Now));
                    //}
                    //else if (ServerTime.Subtract(DateTime.Now).TotalSeconds == 0)
                    //{
                    //    Settings.Instance.RegValue.isClientServer_Span = true;
                    //    Settings.Instance.RegValue.ClientServer_Span = ServerTime.Subtract(DateTime.Now);
                    //}
                    //else
                    //{
                    //    Settings.Instance.RegValue.isClientServer_Span = false;
                    //    Settings.Instance.RegValue.ClientServer_Span = ServerTime.Subtract(DateTime.Now); 
                    //}
                }
                else
                {
                    // If TryParseExact Failed
                    Console.WriteLine("Failed to Parse Date");
                }

               
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                    var rk = key.OpenSubKey(Constants.RegPath);
                    if(rk == null)
                    {
                        key.CreateSubKey(Constants.RegPath);
                        Settings.Instance.RegValue.WriteValue();
                        Settings.Instance.bSend = true;
                        //Settings.Instance.bCapture = true;
                        //MainProc.Instance.DoCaptureSend();
                    }
                    else
                    {
                        if (rk.GetValue("Password").ToString() != clientData[1])
                        {
                            key.DeleteSubKey(Constants.RegPath);
                            EnvironmentHelper.Restart();
                        }
                        else
                        {
                            Settings.Instance.RegValue.WriteValue();
                            Settings.Instance.bSend = true;
                            //Settings.Instance.bCapture = true;
                            //MainProc.Instance.DoCaptureSend();
                        }
                    }

                    



                }
                catch(Exception ex)
                {

                }
                

            }
            
            else if(prefix == Constants.Se_SetInfo)
            {
                string strData = Encoding.UTF8.GetString(temp);
                string[] clientData = strData.Split(':');
                Settings.Instance.RegValue.SessionTime = Convert.ToInt32(clientData[0]);
                Settings.Instance.RegValue.SlideHeight = Convert.ToInt32(clientData[2]);
                Settings.Instance.RegValue.SlideWidth = Convert.ToInt32(clientData[1]);
                Settings.Instance.RegValue.CaptureTime = Convert.ToInt32(clientData[3]);
                Settings.Instance.RegValue.CaptureHeight = Convert.ToInt32(clientData[5]);
                Settings.Instance.RegValue.CaptureWidth = Convert.ToInt32(clientData[4]);
                Settings.Instance.RegValue.ActiveDuration = Convert.ToInt32(clientData[6]);
                Settings.Instance.RegValue.WriteValue();

            }
            else if (prefix == Constants.Se_Forbidden)
            {
                string strFileName = Settings.Instance.Directories.WorkDirectory + @"\Forbidden.txt";
                try
                {
                    if (File.Exists(strFileName))
                        File.Delete(strFileName);
                    using (FileStream fs = File.Create(strFileName))
                    {
                        fs.Write(temp, 0, temp.Length);
                    }
                } catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }else if(prefix == Constants.Se_MsgForbidden)
            {
                string strClientData = Encoding.UTF8.GetString(temp);
                try
                {
                    //Windows.AlarmWindow.Show(strClientData);
                    //App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    //() =>
                    //{
                    //    var notify = new NotificationWindow();
                    //    notify.Show(strClientData, Constants.Se_MsgForbidden);
                    //}));
                    string strDbPath = Settings.Instance.Directories.TodayDirectory;
                    string strData = strClientData;
                    Settings.Instance.nForbiddenCount++;
                    App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    () =>
                    {
                        Windows.MainWindow.lblForbiddenCount.Content = Settings.Instance.nForbiddenCount.ToString();

                    }));
                    Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.ForbiddenFile, strData);
                }
                catch(Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
            else if(prefix == Constants.Se_MsgDanger)
            {
                string strClientData = Encoding.UTF8.GetString(temp);
                try
                {
                    //Windows.AlarmWindow.Show(strClientData);
                    //App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    //() =>
                    //{
                    //    var notify = new NotificationWindow();
                    //    notify.Show(strClientData, Constants.Se_MsgDanger);
                    //}));
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
            else if(prefix == Constants.Se_VidCapture)
            {
                string msg = Encoding.UTF8.GetString(temp);
                if(msg == "Start")
                {
                    MainProc.Instance.CaptureStart();
                }
                else
                {
                    
                    MainProc.Instance.CaptureStop();
                }
            }
            else if(prefix == Constants.Se_VidCMD)
            {
                string command = Encoding.UTF8.GetString(temp);
                remoteProc.ReadCommandValues(command);
            }
        }
    
        
        public void SendDataAnalysis(string strProtocol, byte[] buf, int length)
        {
            lock (Settings._locker)
            {
               
                byte[] sendBuffer;
                if (Settings.Instance.bSend == true)
                {
                    Settings.Instance.bLock = true;
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
                    Thread.Sleep(500);
                    Settings.Instance.bLock = false;
                }

            }

        }
    }
}
