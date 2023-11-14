using Monitor.TaskView.Globals;
using Monitor.TaskView.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Monitor.TaskView.Models
{
    public class RemoteProc
    {
        private Thread THEvents = null;             //Used to listen for commands coming in
        private Thread THServer = null;
        private bool HadEvent = false;
        private DateTime LastEventTime = DateTime.Now;
        private int ScreenClientX = 1920;           //Sizes of the client and our screen
        private int ScreenClientY = 1080;
        private int ScreenServerX = 1920;
        private int ScreenServerY = 1080;
        private static bool Closing = false;
        public bool Running = false;
        private bool Sleep = false;
        private bool IsMetro = false;
        private bool Scale = false;
        public int imageDelay = 2000;
        private PixelFormat ImageResoloution = PixelFormat.Format16bppRgb555;
        private bool Lock = false;
        private int SleepCount = 0;
        private bool Encrypted = false;
        private Brush BrushWait = Brushes.Yellow;
        private Stream CStream;
        public RemoteProc()
        {
            
        }
        public void SleepDelay()
        {//Sleep a bit so the network and the local CPU does not get over worked
            for (int f = 0; f <= this.imageDelay / 100; f++)
            {
                Thread.Sleep(100);
                if (HadEvent)//The client sent a mouse move or something so wake up and send the desktop back a bit early
                {
                    HadEvent = false;
                    return;
                }
            }
        }
        public void Start()
        {
            this.Scale = false;
            this.imageDelay = 2000;
            this.Sleep = false;
            
            this.ScreenClientX = Screen.PrimaryScreen.Bounds.Width;  //Windows seems to get this wrong sometimes but if it was made too easy for us
            this.ScreenClientY = Screen.PrimaryScreen.Bounds.Height; //then microsoft would not be selling us upgrades just so we can use windows remote desktop
            this.ImageResoloution = PixelFormat.Format16bppRgb555;   //We use 16 or 32 bit but not 64, it crashes and was too big anyway
            this.Running = true;

            this.SleepCount = 0;
            //this.CStream = new NetworkStream(Settings.Instance.SockCom);
            //THEvents = new Thread(new ThreadStart(WaitForCommands));
            //THEvents.Start();
            //Thread.Sleep(300);  //Give the command thread a chance to start
            //THServer = new Thread(new ThreadStart(PushTheDesktopToClients));
            //THServer.Start();
        }


        private void WriteScreenSize()
        {
            SendScreenInfo("SCREEN_" + Screen.PrimaryScreen.Bounds.Width + "_" + Screen.PrimaryScreen.Bounds.Height);
            this.ScreenClientX = Screen.PrimaryScreen.Bounds.Width;
            this.ScreenClientY = Screen.PrimaryScreen.Bounds.Height;
            this.ScreenServerX = Screen.PrimaryScreen.Bounds.Width;
            this.ScreenServerY = Screen.PrimaryScreen.Bounds.Height;
        }
        public void ReadCommandValues(string temp)
        {//Could be a key-stroke or a mouse move/click or a cammon to do something else
            try
            {
                if (temp.StartsWith("CDELAY"))
                {

                    //this.imageDelay = int.Parse(temp.Substring(6, temp.Length - 6));
                }
                else if (temp.StartsWith("CMD "))
                {
                    bool Force = false;
                    string Cmd = temp.Substring(3).Trim();
                    if (Cmd == "OK") return;

                    if (Cmd.StartsWith("KEYSYNC ")) Helper.SyncKeys(Cmd);
                    if (Cmd == "SUP" && this.IsMetro) Helper.ScrollVertical(-50);
                    if (Cmd == "SDOWN" && this.IsMetro) Helper.ScrollVertical(50);
                    if (Cmd == "SLEFT" && this.IsMetro) Helper.ScrollHorizontal(-50);
                    if (Cmd == "SRIGHT" && this.IsMetro) Helper.ScrollHorizontal(50);
                    if (Cmd == "SHOWSTART") Helper.ShowStart();
                    if (Cmd.StartsWith("SLEEP TRUE")) this.Sleep = true;
                    if (Cmd.StartsWith("SLEEP FALSE")) this.Sleep = false;
                    if (Cmd.StartsWith("SCALE ")) this.Scale = bool.Parse(Cmd.ToLower().Replace("scale ", ""));
                    if (Cmd.StartsWith("RESOLUTION TRUE")) this.ImageResoloution = PixelFormat.Format32bppArgb;
                    if (Cmd.StartsWith("RESOLUTION FALSE")) this.ImageResoloution = PixelFormat.Format16bppRgb555;
                    if (Cmd.StartsWith("METRO")) Helper.ShowMetro();
                    if (Cmd.StartsWith("CTRLALTDELETE")) Helper.ShowTaskmanager();
                    if (Cmd.StartsWith("CLOSE")) this.Stop();
                }
                else if (temp.StartsWith("SCREEN "))
                {
                    this.ScreenServerX = int.Parse(temp.Split(' ')[1]);
                    this.ScreenServerY = int.Parse(temp.Split(' ')[2]);
                }
                else if (temp.StartsWith("UNLOCK"))
                {//Can send more data
                    this.Lock = false;
                }
                else if (temp.StartsWith("LOCK"))
                {//Don't send more data
                    this.Lock = true;
                }
                else if (temp.StartsWith("BEEP"))
                {
                    SystemSounds.Asterisk.Play();
                }
                else if (temp.StartsWith("SHUTDOWN"))
                {
                    //ListenStop(true);
                }
                else if (temp.StartsWith("{CAPSLOCK}"))
                {
                    Helper.CapsLock();
                    return;
                }
                else if (temp.StartsWith("{NUMLOCK}"))
                {
                    Helper.NumLock();
                    return;
                }
                else if (temp.StartsWith("LCLICK"))
                {
                    //mouse_event(MOUSE_LEFTDOWN | MOUSE_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    //printDebug(temp, false);
                }
                else if (temp.StartsWith("RCLICK"))
                {
                    //mouse_event(MOUSE_LEFTDOWN | MOUSE_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    //printDebug(temp, false);
                }
                else if (temp.StartsWith("LDOWN"))
                {
                    App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    () =>
                    {
                        int x = Cursor.Position.X;
                        int y = Cursor.Position.Y;
                        Helper.SetCursorPos(x, y);
                        Helper.mouse_event(Helper.MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    }));
                }
                else if (temp.StartsWith("LUP"))
                {
                    App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    () =>
                    {
                        int x = Cursor.Position.X;
                        int y = Cursor.Position.Y;
                        Helper.SetCursorPos(x, y);
                        Helper.mouse_event(Helper.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    }));
                }
                else if (temp.StartsWith("RDOWN"))
                {
                    App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    () =>
                    {
                        int x = Cursor.Position.X;
                        int y = Cursor.Position.Y;
                        Helper.SetCursorPos(x, y);
                        Helper.mouse_event(Helper.MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    }));
                }
                else if (temp.StartsWith("RUP"))
                {
                    App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                    () =>
                    {
                        int x = Cursor.Position.X;
                        int y = Cursor.Position.Y;
                        Helper.SetCursorPos(x, y);
                        Helper.mouse_event(Helper.MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    }));
                }
                else if (temp.StartsWith("M"))
                {
                    int xPos = 0, yPos = 0;
                    try
                    {
                        xPos = int.Parse(temp.Substring(1, temp.IndexOf(' ')));
                        yPos = int.Parse(temp.Substring(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')));
                        Cursor.Position = new Point(xPos, yPos);
                    }
                    catch (Exception Ex)
                    {

                    }
                }
                else if (temp.Substring(0, 3).StartsWith("KEY"))
                {
                    if (temp.Substring(4) == "Ctrl C") Console.WriteLine("Copy Event");
                    else if (temp.Substring(4) == "Ctrl V") Console.WriteLine("Paste Event");
                    //else if (temp.Length > 1) printDebug(temp, true);
                    SendKeys.SendWait(temp.Substring(4));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public void Stop()
        {
            if (this.THEvents != null) this.THEvents.Abort();//Don't know they say don't use Abort but what else can you do!
            if (this.THServer != null) this.THServer.Abort();
        }

        private Bitmap ResizeImage(Bitmap B, int Width, int Height)
        {
            Bitmap BNew = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(BNew);
            float sx = (float)Width / (float)B.Width;
            float sy = (float)Height / (float)B.Height;

            G.ScaleTransform(sx, sy);
            G.DrawImage(B, 0, 0);
            return BNew;
        }

        private void SendScreenInfo(string Data)
        {//The screen sizes need sending to the client
            Data = "#INFO#" + Data;
            byte[] data = Encoding.UTF8.GetBytes(Data);
            CommProc.Instance.SendDataAnalysis("", data, data.Length);
        }

        public static MemoryStream Encrypt(Bitmap Img)//Tried using Rijndael but it thrashed the CPU on the server and the client flickered, look at using SSL if you need it
        {//We just remove the start of the PNG and put some junk in
            string Seed = DateTime.Now.Millisecond.ToString() + DateTime.Now.Hour + DateTime.Now.Second + "aqf";//"All queer fuckers" if you wanted to know
            byte[] Dummey = UTF8Encoding.UTF8.GetBytes(Seed.Substring(0, 6));
            MemoryStream MSin = new MemoryStream();
            Img.Save(MSin, ImageFormat.Png);
            MSin.Position = 0;
            MSin.Position = 0;
            MSin.Write(Dummey, 0, 6);
            return MSin;
        }
        private void KeepMachineAwake()
        {//Fake key-move to keep the machine awake
            LastEventTime = DateTime.Now;
            SendKeys.SendWait("{DOWN}");
            Thread.Sleep(50);
            SendKeys.SendWait("{UP}");
        }

    }
}
