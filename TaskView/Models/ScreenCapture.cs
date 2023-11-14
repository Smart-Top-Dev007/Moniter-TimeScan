using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Logger;
using Monitor.TaskView.Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Sockets;
using Monitor.TaskView.Connect;

//using DlibDotNet;
//using DlibDotNet.Extensions;
//using Dlib = DlibDotNet.Dlib;


namespace Monitor.TaskView.Models
{
    public class ScreenCaptures
    {
        public static int screenLeft;
        public static int screenTop;
        public static int screenWidth;
        public static int screenHeight;
        public static int nScrCount;
        public bool bCapture = false;
        public bool bSlide = false;
        public ScreenCaptures()
        {
            screenLeft = SystemInformation.VirtualScreen.Left;
            screenTop = SystemInformation.VirtualScreen.Top;
            screenWidth = SystemInformation.VirtualScreen.Width;
            screenHeight = SystemInformation.VirtualScreen.Height;
            nScrCount = GetScreenCount();
        }
        private int GetScreenCount()
        {
            return Screen.AllScreens.Length;
        }

        public void SaveImage(string type)
        {
            try
            {
                if(Settings.Instance.bPhoto == true)
                {
                    Thread.Sleep(591);
                }
                string strToday = Settings.Instance.Directories.TodayDirectory;
                if (!Directory.Exists(strToday))
                {
                    return;
                }
                DateTime localDate = DateTime.Now;
                string strFilePath = Settings.Instance.Directories.SlideDirectory;
                float fWidth = Settings.Instance.RegValue.SlideWidth;
                int nHeight = Settings.Instance.RegValue.SlideHeight;

                if (type == "capture")
                {
                    strFilePath = Settings.Instance.Directories.CaptureDirectory;
                    fWidth = Settings.Instance.RegValue.CaptureWidth;
                    nHeight = Settings.Instance.RegValue.CaptureHeight;
                }

                string strFileName = localDate.ToString("HH-mm-ss").Replace(":", "-") + "." + Constants.strImgExtension;
                Settings.Instance.bPhoto = true;
                //string strFileName = localDate.Hour.ToString() + "-" + localDate.Minute.ToString() + "-" + localDate.Second.ToString() + ".jpg";
                using (Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb))
                {
                    using (Graphics captureGraphics = Graphics.FromImage(captureBitmap))
                    {
                        captureGraphics.CopyFromScreen(screenLeft, screenTop, 0, 0, captureBitmap.Size);
                        Size resize;
                        
                        if (nScrCount > 1)
                            fWidth *= 1.5f;
                        resize = new Size((int)fWidth, nHeight);

                        using (Bitmap resizeImage = new Bitmap(captureBitmap, resize))
                        {
                            string FileName = Md5Crypto.OnImageFileNameChange(strFilePath, strFileName);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                resizeImage.Save(ms, ImageFormat.Jpeg);
                                ms.Position = 0;
                                byte[] data = ms.ToArray();
                                byte[] bytePrefix = Encoding.UTF8.GetBytes(Constants.Re_DataCapture);
                                byte[] sendBuffer = new byte[data.Length + 4];
                                bytePrefix.CopyTo(sendBuffer, 0);
                                data.CopyTo(sendBuffer, 4);
                                File.WriteAllBytes(FileName, sendBuffer);
                                
                            }
                        }

                    }

                }

                Settings.Instance.bPhoto = false;
                //MemoryStream ms1 = new MemoryStream(sendBuffer);
                //Bitmap bitmap = new Bitmap(ms1);
                //bitmap.Save(FileName, ImageFormat.Jpeg);

                //Md5Crypto.WriteImageFile(strFilePath, strFileName, resizeImage);
                //resizeImage.Save(strFileName, ImageFormat.Jpeg);                       
            }
            catch (Exception ex)
            {
                CustomEx.DoExecption(Constants.exResume, ex);
                //Log.Instance.DoLog(string.Format("Unhandled Exception.\r\nException: {0}\r\n", ex.Message), Log.LogType.Error);
            }
        }

        public void SaveServerImage(string type)
        {
            try
            {
                if(Settings.Instance.bPhoto == true)
                {
                    Thread.Sleep(593);
                }
                string strToday = Settings.Instance.Directories.TodayDirectory;
                if (!Directory.Exists(strToday))
                {
                    return;
                }
                DateTime localDate = DateTime.Now.Add(Settings.Instance.RegValue.ClientServer_Span);
                string strFilePath = Settings.Instance.Directories.SlideDirectory + "\\Server";

                if (!Directory.Exists(strFilePath))
                    Directory.CreateDirectory(strFilePath);

                float fWidth = Settings.Instance.RegValue.SlideWidth;
                int nHeight = Settings.Instance.RegValue.SlideHeight;

                if (type == "capture")
                {
                    strFilePath = Settings.Instance.Directories.CaptureDirectory;
                    fWidth = Settings.Instance.RegValue.CaptureWidth;
                    nHeight = Settings.Instance.RegValue.CaptureHeight;
                }

                string strFileName = localDate.ToString("HH-mm-ss").Replace(":", "-") + "." + Constants.strImgExtension;
                Settings.Instance.bPhoto = true;
                //string strFileName = localDate.Hour.ToString() + "-" + localDate.Minute.ToString() + "-" + localDate.Second.ToString() + ".jpg";
                using (Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb))
                {
                    using (Graphics captureGraphics = Graphics.FromImage(captureBitmap))
                    {
                        captureGraphics.CopyFromScreen(screenLeft, screenTop, 0, 0, captureBitmap.Size);
                        Size resize;

                        if (nScrCount > 1)
                            fWidth *= 1.5f;
                        resize = new Size((int)fWidth, nHeight);

                        using (Bitmap resizeImage = new Bitmap(captureBitmap, resize))
                        {
                            string FileName = Md5Crypto.OnImageFileNameChange(strFilePath, strFileName);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                resizeImage.Save(ms, ImageFormat.Jpeg);
                                ms.Position = 0;
                                byte[] data = ms.ToArray();
                                byte[] bytePrefix = Encoding.UTF8.GetBytes(Constants.Re_DataCapture);
                                byte[] sendBuffer = new byte[data.Length + 4];
                                bytePrefix.CopyTo(sendBuffer, 0);
                                data.CopyTo(sendBuffer, 4);


                                File.WriteAllBytes(FileName, sendBuffer);
                                
                            }
                        }

                    }

                }

                Settings.Instance.bPhoto = false;
                //MemoryStream ms1 = new MemoryStream(sendBuffer);
                //Bitmap bitmap = new Bitmap(ms1);
                //bitmap.Save(FileName, ImageFormat.Jpeg);

                //Md5Crypto.WriteImageFile(strFilePath, strFileName, resizeImage);
                //resizeImage.Save(strFileName, ImageFormat.Jpeg);                       
            }
            catch (Exception ex)
            {
                CustomEx.DoExecption(Constants.exResume, ex);
                //Log.Instance.DoLog(string.Format("Unhandled Exception.\r\nException: {0}\r\n", ex.Message), Log.LogType.Error);
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
        public void GetStream(string type)
        {
            byte[] data;
            int nFlag = 0;
            if(Settings.Instance.bPhoto == true)
            {
                Thread.Sleep(592);
            }
            try
            {
                Settings.Instance.bPhoto = true;
                using (Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb))
                {
                    using (Graphics captureGraphics = Graphics.FromImage(captureBitmap))
                    {
                        captureGraphics.CopyFromScreen(screenLeft, screenTop, 0, 0, captureBitmap.Size);

                        Size resize, resize_Human;
                        float fWidth = Settings.Instance.RegValue.SlideWidth;
                        int nHeight = Settings.Instance.RegValue.SlideHeight;

                        if (type == "capture")
                        {
                            nFlag = 1;

                            /*********************************************************************/
                            //using (var fd = Dlib.GetFrontalFaceDetector())
                            //{
                            //    resize_Human = new Size((int)captureBitmap.Width * 1, captureBitmap.Height * 1);

                            //    using (Bitmap detectionImage = new Bitmap(captureBitmap, resize_Human))
                            //    {


                            //        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, detectionImage.Width, detectionImage.Height);

                            //        System.Drawing.Imaging.BitmapData _bitmapdata = detectionImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, detectionImage.PixelFormat);

                            //        var array = new byte[_bitmapdata.Stride * _bitmapdata.Height];

                            //        Marshal.Copy(_bitmapdata.Scan0, array, 0, array.Length);

                            //        using (Array2D<BgrPixel> img = Dlib.LoadImageData<BgrPixel>(array, (uint)detectionImage.Height, (uint)detectionImage.Width, (uint)_bitmapdata.Stride))
                            //        {
                            //            // find all faces in the image
                            //            var faces = fd.Operator(img);

                            //                if (faces.Length > 0)
                            //                {
                            //                    // please define...
                            //                    nFlag = 2;
                            //                }
                            //                else
                            //                {
                            //                    nFlag = 1;
                            //                }
                            //        }

                            //    }

                            //    //fd.Dispose();
                            //    //
                            //}
                            /******************************************************************/
                            fWidth = Settings.Instance.RegValue.CaptureWidth;
                            nHeight = Settings.Instance.RegValue.CaptureHeight;
                        }
                        else if (type == "video")
                        {
                            nFlag = 3;
                            fWidth = 1366;
                            nHeight = 768;
                        }
                        if (nScrCount > 1)
                            fWidth *= 1.5f;
                        resize = new Size((int)fWidth, nHeight);

                        using (Bitmap resizeImage = new Bitmap(captureBitmap, resize))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                resizeImage.Save(ms, ImageFormat.Jpeg);
                                ms.Position = 0;
                                data = ms.ToArray();
                                if (nFlag == 0)
                                {
                                    SendDataAnalysis(Constants.Re_DataSlide, data, data.Length);
                                    Thread.Sleep(200);
                                }
                                else if (nFlag == 1)
                                {
                                    SendDataAnalysis(Constants.Re_DataCapture, data, data.Length);
                                    Thread.Sleep(500);

                                }
                                else if (nFlag == 2)
                                {
                                    SendDataAnalysis(Constants.Re_DataHuman, data, data.Length);
                                }
                                else if (nFlag == 3)
                                {
                                    SendDataAnalysis(Constants.Re_VidCapture, data, data.Length);
                                    Thread.Sleep(1500);
                                }
                            }

                        }

                    }
                }
                Settings.Instance.bPhoto = false;
            }
            catch(Exception ex)
            {

            }
            
        }

       
    }
}
