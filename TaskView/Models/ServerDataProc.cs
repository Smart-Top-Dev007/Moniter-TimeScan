using Monitor.TaskView.Connect;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor.TaskView.Models
{
    public class ServerDataProc
    {
        public byte[] sendData = new byte[1024 * 1024];
        public ServerDataProc()
        {

        }

        public void SendData(string FilePath, string FileName, string FileType)
        {
            //try
            //{
            if (FileType == "Slide")
            {
                if (File.Exists(FilePath))
                {
                    sendData = File.ReadAllBytes(FilePath);

                    byte[] sendBuffer;
                    byte[] bytePrefix = Encoding.UTF8.GetBytes(FileName.Substring(0, 8));
                    sendBuffer = new byte[sendData.Length + 8];
                    bytePrefix.CopyTo(sendBuffer, 0);
                    sendData.CopyTo(sendBuffer, 8);

                    SendDataAnalysis(Constants.Re_ServerSlideData, sendBuffer, sendBuffer.Length, FilePath);
                    //Settings.Instance.SockCapture.Send(sendData, 0, sendData.Length, SocketFlags.None);
                }
            }
            else if (FileType == "Capture")
            {
                if (File.Exists(FilePath))
                {
                    sendData = File.ReadAllBytes(FilePath);

                    byte[] sendBuffer;
                    byte[] bytePrefix = Encoding.UTF8.GetBytes(FileName.Substring(0, 8));
                    sendBuffer = new byte[sendData.Length + 8];
                    bytePrefix.CopyTo(sendBuffer, 0);
                    sendData.CopyTo(sendBuffer, 8);

                    SendDataAnalysis(Constants.Re_ServerCaptureData, sendBuffer, sendBuffer.Length, FilePath);
                    //Settings.Instance.SockCapture.Send(sendData, 0, sendData.Length, SocketFlags.None);

                }
            }
            else if (FileType == "Process")
            {
                if (File.Exists(FilePath))
                {
                    sendData = File.ReadAllBytes(FilePath);
                    SendDataAnalysis(Constants.Re_ServerProcessData, sendData, sendData.Length, FilePath);
                    //Settings.Instance.SockCapture.Send(sendData, 0, sendData.Length, SocketFlags.None);

                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    CustomEx.DoExecption(Constants.exResume, ex);
            //}


        }

        public void SendDataAnalysis(string strProtocol, byte[] buf, int length, string strPath)
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
                    Thread.Sleep(1500);

                    File.Delete(strPath);
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
