using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSVHost.Model
{
    public class UsbDetect
    {
        public UsbDetect()
        {
            Thread UsbThread = new Thread(new ThreadStart(UsbDetectThread));
            UsbThread.Start();
        }
        private void UsbDetectThread()
        {
            UsbState usb = new UsbState();
            BackgroundWorker bgwDriveDetector = new BackgroundWorker();
            bgwDriveDetector.DoWork += usb.bgwDriveDetector_DoWork;
            bgwDriveDetector.RunWorkerAsync();
            bgwDriveDetector.WorkerReportsProgress = true;
            bgwDriveDetector.WorkerSupportsCancellation = true;
        }
    }
    public class UsbState
    {
        static string Device1 = "";
        static bool bPhone = false;
        static DateTime PreDt = DateTime.Now;
        static int DiffEvent = 600;
        static string DeviceType = "Unknown";
        public UsbState()
        {
        }
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
        private void SendDataNoPhone(object sendInfo)
        {
            Thread.Sleep(600);
            if (!bPhone)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    IPAddress serverIP = IPAddress.Parse(Constants.AutoPatchServerIP);
                    IPEndPoint localEndPoint = new IPEndPoint(serverIP, Constants.USBPort);
                    socket.Connect(localEndPoint);
                    Thread.Sleep(50);
                    byte[] data = Encoding.UTF8.GetBytes(Constants.Re_UsbDetect + sendInfo.ToString());
                    socket.Send(data);
                    Thread.Sleep(50);
                    DeviceType = "Unknown";
                    socket.Close();
                }
                catch
                {
                    DeviceType = "Unknown";
                    socket.Close();
                }
                Console.WriteLine(sendInfo.ToString());
            }
        }
        private void SendUsbData(string sdata)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPAddress serverIP = IPAddress.Parse(Constants.AutoPatchServerIP);
                IPEndPoint localEndPoint = new IPEndPoint(serverIP, Constants.USBPort);
                socket.Connect(localEndPoint);
                byte[] data = Encoding.UTF8.GetBytes(Constants.Re_UsbDetect + sdata);
                socket.Send(data);
                Thread.Sleep(50);
                socket.Close();
            }
            catch { socket.Close(); }

            Console.WriteLine(sdata);
        }

        private void UsbInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string tempDevice = "";
            string Type = "Unknown";
            bPhone = false;
            System.TimeSpan diffResult = dt.Subtract(PreDt);
            PreDt = dt;

            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            foreach (var property in instance.Properties)
            {
                if (property.Name == "Name")
                {
                    if (property.Value.ToString().Contains("Composite"))   // KeyBorad                    
                        Type = "KeyBoard";
                    if (property.Value.ToString().Contains("Storage"))     // USB
                        Type = "Memory";
                }
                if (property.Name == "DeviceID")
                {
                    tempDevice = property.Value.ToString();
                }
            }

            if (diffResult.TotalMilliseconds > DiffEvent)   //first event.
            {
                Device1 = tempDevice;
                DeviceType = Type;
                string sendInfo = Type + "   *  " + "INSERT" + "  *  " + GetIPAddress().ToString() + " * " + Device1;
                Thread sendThread = new Thread(new ParameterizedThreadStart(SendDataNoPhone));
                sendThread.Start(sendInfo);
            }
            else   //Second event
            {
                bPhone = true;
                if (Type != DeviceType)
                {
                    Type = "Mobile Phone";
                    string SendInfo = Type + "   *  " + "INSERT" + "  * " + GetIPAddress().ToString() + " * ID1 : " + Device1 + " ID2 : " + tempDevice;
                    SendUsbData(SendInfo);
                }
                DeviceType = "Unknow";
            }

        }

        private void UsbRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string tempDevice = "";
            string Type = "Unknown";
            bPhone = false;
            System.TimeSpan diffResult = dt.Subtract(PreDt);
            PreDt = dt;

            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            foreach (var property in instance.Properties)
            {
                if (property.Name == "Name")
                {
                    if (property.Value.ToString().Contains("Composite"))   // KeyBorad                    
                        Type = "KeyBoard";
                    if (property.Value.ToString().Contains("Storage"))     // USB
                        Type = "Memory";
                }
                if (property.Name == "DeviceID")
                {
                    tempDevice = property.Value.ToString();
                }
            }

            if (diffResult.TotalMilliseconds > DiffEvent)   //first event.
            {
                Device1 = tempDevice;
                DeviceType = Type;
                string sendInfo = Type + "   *  " + "REMOVE" + "  *  " + GetIPAddress().ToString() + " * " + Device1;
                Thread sendThread = new Thread(new ParameterizedThreadStart(SendDataNoPhone));
                sendThread.Start(sendInfo);

            }
            else   //Second event
            {
                bPhone = true;
                if (Type != DeviceType)
                {
                    Type = "Mobile Phone";
                    string SendInfo = Type + "   *  " + "REMOVE" + "  * " + GetIPAddress().ToString() + " * ID1 : " + Device1 + " ID2 : " + tempDevice;
                    SendUsbData(SendInfo);
                }
                DeviceType = "Unknow";
            }

        }
        private void MouseInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            string DeviceID = "";
            try
            {
                ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                foreach (var property in instance.Properties)
                {
                    if (property.Name == "Name")
                    {
                        if (property.Value.ToString().Contains("Input"))   // Mouse
                        {
                            DeviceType = "Mouse";
                        }
                    }

                    if (property.Name == "DeviceID")
                    {
                        DeviceID = property.Value.ToString();
                    }
                }
                if (DeviceType != "Unknown")
                {
                    string SendInfo = DeviceType.ToString() + "   *  " + "INSERT" + "  *  " + GetIPAddress().ToString() + " * " + DeviceID;
                    SendUsbData(SendInfo);
                }
            }
            catch { }
        }

        private void MouseRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            string DeviceID = "";
            try
            {
                ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                foreach (var property in instance.Properties)
                {
                    if (property.Name == "Name")
                    {
                        if (property.Value.ToString().Contains("Input"))   // Mouse
                        {
                            DeviceType = "Mouse";
                        }
                    }

                    if (property.Name == "DeviceID")
                    {
                        DeviceID = property.Value.ToString();
                    }
                }
                if (DeviceType != "Unknown")
                {
                    string SendInfo = DeviceType.ToString() + "   *  " + "REMOVE" + "  *  " + GetIPAddress().ToString() + " * " + DeviceID;
                    SendUsbData(SendInfo);
                }
            }
            catch { }
        }
        public void bgwDriveDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            WqlEventQuery UsbInsertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");

            ManagementEventWatcher USBInsertWatcher = new ManagementEventWatcher(UsbInsertQuery);
            USBInsertWatcher.EventArrived += new EventArrivedEventHandler(UsbInsertedEvent);
            USBInsertWatcher.Start();

            WqlEventQuery UsbRemoveQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            ManagementEventWatcher USBRemoveWatcher = new ManagementEventWatcher(UsbRemoveQuery);
            USBRemoveWatcher.EventArrived += new EventArrivedEventHandler(UsbRemovedEvent);
            USBRemoveWatcher.Start();

            WqlEventQuery MouseInsertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PointingDevice'");

            ManagementEventWatcher MouseInsertWatcher = new ManagementEventWatcher(MouseInsertQuery);
            MouseInsertWatcher.EventArrived += new EventArrivedEventHandler(MouseInsertedEvent);
            MouseInsertWatcher.Start();

            WqlEventQuery MouseRemoveQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PointingDevice'");
            ManagementEventWatcher MouseRemoveWatcher = new ManagementEventWatcher(MouseRemoveQuery);
            MouseRemoveWatcher.EventArrived += new EventArrivedEventHandler(MouseRemovedEvent);
            MouseRemoveWatcher.Start();

            System.Threading.Thread.Sleep(500);
        }
    }
}