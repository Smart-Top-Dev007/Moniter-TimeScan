using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace CSVHost.Model
{
    [Flags()]
    internal enum SetupDiGetClassDevsFlags
    {
        Default = 1,
        Present = 2,
        AllClasses = 4,
        Profile = 8,
        DeviceInterface = (int)0x10
    }

    internal enum DiFunction
    {
        SelectDevice = 1,
        InstallDevice = 2,
        AssignResources = 3,
        Properties = 4,
        Remove = 5,
        FirstTimeSetup = 6,
        FoundDevice = 7,
        SelectClassDrivers = 8,
        ValidateClassDrivers = 9,
        InstallClassDrivers = (int)0xa,
        CalcDiskSpace = (int)0xb,
        DestroyPrivateData = (int)0xc,
        ValidateDriver = (int)0xd,
        Detect = (int)0xf,
        InstallWizard = (int)0x10,
        DestroyWizardData = (int)0x11,
        PropertyChange = (int)0x12,
        EnableClass = (int)0x13,
        DetectVerify = (int)0x14,
        InstallDeviceFiles = (int)0x15,
        UnRemove = (int)0x16,
        SelectBestCompatDrv = (int)0x17,
        AllowInstall = (int)0x18,
        RegisterDevice = (int)0x19,
        NewDeviceWizardPreSelect = (int)0x1a,
        NewDeviceWizardSelect = (int)0x1b,
        NewDeviceWizardPreAnalyze = (int)0x1c,
        NewDeviceWizardPostAnalyze = (int)0x1d,
        NewDeviceWizardFinishInstall = (int)0x1e,
        Unused1 = (int)0x1f,
        InstallInterfaces = (int)0x20,
        DetectCancel = (int)0x21,
        RegisterCoInstallers = (int)0x22,
        AddPropertyPageAdvanced = (int)0x23,
        AddPropertyPageBasic = (int)0x24,
        Reserved1 = (int)0x25,
        Troubleshooter = (int)0x26,
        PowerMessageWake = (int)0x27,
        AddRemotePropertyPageAdvanced = (int)0x28,
        UpdateDriverUI = (int)0x29,
        Reserved2 = (int)0x30
    }

    internal enum StateChangeAction
    {
        Enable = 1,
        Disable = 2,
        PropChange = 3,
        Start = 4,
        Stop = 5
    }

    [Flags()]
    internal enum Scopes
    {
        Global = 1,
        ConfigSpecific = 2,
        ConfigGeneral = 4
    }

    internal enum SetupApiError
    {
        NoAssociatedClass = unchecked((int)0xe0000200),
        ClassMismatch = unchecked((int)0xe0000201),
        DuplicateFound = unchecked((int)0xe0000202),
        NoDriverSelected = unchecked((int)0xe0000203),
        KeyDoesNotExist = unchecked((int)0xe0000204),
        InvalidDevinstName = unchecked((int)0xe0000205),
        InvalidClass = unchecked((int)0xe0000206),
        DevinstAlreadyExists = unchecked((int)0xe0000207),
        DevinfoNotRegistered = unchecked((int)0xe0000208),
        InvalidRegProperty = unchecked((int)0xe0000209),
        NoInf = unchecked((int)0xe000020a),
        NoSuchHDevinst = unchecked((int)0xe000020b),
        CantLoadClassIcon = unchecked((int)0xe000020c),
        InvalidClassInstaller = unchecked((int)0xe000020d),
        DiDoDefault = unchecked((int)0xe000020e),
        DiNoFileCopy = unchecked((int)0xe000020f),
        InvalidHwProfile = unchecked((int)0xe0000210),
        NoDeviceSelected = unchecked((int)0xe0000211),
        DevinfolistLocked = unchecked((int)0xe0000212),
        DevinfodataLocked = unchecked((int)0xe0000213),
        DiBadPath = unchecked((int)0xe0000214),
        NoClassInstallParams = unchecked((int)0xe0000215),
        FileQueueLocked = unchecked((int)0xe0000216),
        BadServiceInstallSect = unchecked((int)0xe0000217),
        NoClassDriverList = unchecked((int)0xe0000218),
        NoAssociatedService = unchecked((int)0xe0000219),
        NoDefaultDeviceInterface = unchecked((int)0xe000021a),
        DeviceInterfaceActive = unchecked((int)0xe000021b),
        DeviceInterfaceRemoved = unchecked((int)0xe000021c),
        BadInterfaceInstallSect = unchecked((int)0xe000021d),
        NoSuchInterfaceClass = unchecked((int)0xe000021e),
        InvalidReferenceString = unchecked((int)0xe000021f),
        InvalidMachineName = unchecked((int)0xe0000220),
        RemoteCommFailure = unchecked((int)0xe0000221),
        MachineUnavailable = unchecked((int)0xe0000222),
        NoConfigMgrServices = unchecked((int)0xe0000223),
        InvalidPropPageProvider = unchecked((int)0xe0000224),
        NoSuchDeviceInterface = unchecked((int)0xe0000225),
        DiPostProcessingRequired = unchecked((int)0xe0000226),
        InvalidCOInstaller = unchecked((int)0xe0000227),
        NoCompatDrivers = unchecked((int)0xe0000228),
        NoDeviceIcon = unchecked((int)0xe0000229),
        InvalidInfLogConfig = unchecked((int)0xe000022a),
        DiDontInstall = unchecked((int)0xe000022b),
        InvalidFilterDriver = unchecked((int)0xe000022c),
        NonWindowsNTDriver = unchecked((int)0xe000022d),
        NonWindowsDriver = unchecked((int)0xe000022e),
        NoCatalogForOemInf = unchecked((int)0xe000022f),
        DevInstallQueueNonNative = unchecked((int)0xe0000230),
        NotDisableable = unchecked((int)0xe0000231),
        CantRemoveDevinst = unchecked((int)0xe0000232),
        InvalidTarget = unchecked((int)0xe0000233),
        DriverNonNative = unchecked((int)0xe0000234),
        InWow64 = unchecked((int)0xe0000235),
        SetSystemRestorePoint = unchecked((int)0xe0000236),
        IncorrectlyCopiedInf = unchecked((int)0xe0000237),
        SceDisabled = unchecked((int)0xe0000238),
        UnknownException = unchecked((int)0xe0000239),
        PnpRegistryError = unchecked((int)0xe000023a),
        RemoteRequestUnsupported = unchecked((int)0xe000023b),
        NotAnInstalledOemInf = unchecked((int)0xe000023c),
        InfInUseByDevices = unchecked((int)0xe000023d),
        DiFunctionObsolete = unchecked((int)0xe000023e),
        NoAuthenticodeCatalog = unchecked((int)0xe000023f),
        AuthenticodeDisallowed = unchecked((int)0xe0000240),
        AuthenticodeTrustedPublisher = unchecked((int)0xe0000241),
        AuthenticodeTrustNotEstablished = unchecked((int)0xe0000242),
        AuthenticodePublisherNotTrusted = unchecked((int)0xe0000243),
        SignatureOSAttributeMismatch = unchecked((int)0xe0000244),
        OnlyValidateViaAuthenticode = unchecked((int)0xe0000245)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DeviceInfoData
    {
        public int Size;
        public Guid ClassGuid;
        public int DevInst;
        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PropertyChangeParameters
    {
        public int Size;
        // part of header. It's flattened out into 1 structure.
        public DiFunction DiFunction;
        public StateChangeAction StateChange;
        public Scopes Scope;
        public int HwProfile;
    }

    internal class NativeMethods
    {

        private const string setupapi = "setupapi.dll";

        private NativeMethods()
        {
        }

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiCallClassInstaller(DiFunction installFunction, SafeDeviceInfoSetHandle deviceInfoSet, [In()]
ref DeviceInfoData deviceInfoData);

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiEnumDeviceInfo(SafeDeviceInfoSetHandle deviceInfoSet, int memberIndex, ref DeviceInfoData deviceInfoData);

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeDeviceInfoSetHandle SetupDiGetClassDevs([In()]
ref Guid classGuid, [MarshalAs(UnmanagedType.LPWStr)]
string enumerator, IntPtr hwndParent, SetupDiGetClassDevsFlags flags);

        /*
        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiGetDeviceInstanceId(SafeDeviceInfoSetHandle deviceInfoSet, [In()]
ref DeviceInfoData did, [MarshalAs(UnmanagedType.LPTStr)]
StringBuilder deviceInstanceId, int deviceInstanceIdSize, [Out()]
ref int requiredSize);
        */
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiGetDeviceInstanceId(
           IntPtr DeviceInfoSet,
           ref DeviceInfoData did,
           [MarshalAs(UnmanagedType.LPTStr)] StringBuilder DeviceInstanceId,
           int DeviceInstanceIdSize,
           out int RequiredSize
        );

        [SuppressUnmanagedCodeSecurity()]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiSetClassInstallParams(SafeDeviceInfoSetHandle deviceInfoSet, [In()]
ref DeviceInfoData deviceInfoData, [In()]
ref PropertyChangeParameters classInstallParams, int classInstallParamsSize);

    }

    internal class SafeDeviceInfoSetHandle : SafeHandleZeroOrMinusOneIsInvalid
    {

        public SafeDeviceInfoSetHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.SetupDiDestroyDeviceInfoList(this.handle);
        }

    }


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
        static bool bPhone, isFirst, isTime;
        static DateTime PreDt = DateTime.Now;
        static int DiffEvent = 600;
        static string DeviceType = "Unknown";
        public DateTime dt;
        public int nTime = 0;

        public static string strPass;
        public static string Md5Key = "A!9HHhi%XjjYY4YP2@Nob009X";
        public bool isPhone;
        public Guid mouseGuid_, m_mouseGuid;
        public string instancePath_, m_instancePath;

        public UsbState()
        {
            isPhone = false;
            isFirst = false;
            isTime = false;

            Thread PhoneStopThread = new Thread(new ThreadStart(PhoneSThread));
            PhoneStopThread.Start();
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
                    if (property.Value.ToString().Contains("Composite") && !isFirst)   // KeyBorad                    
                    {  // KeyBorad                    
                        Type = "KeyBoard";
                        var usbDevices = GetLogicalDevices();
                        foreach (var usbDevice in usbDevices)
                        {

                            string strName = usbDevice.GetPropertyValue("Name").ToString();
                            string strDeviceID = usbDevice.GetPropertyValue("DeviceID").ToString();
                            string strClassGuid = usbDevice.GetPropertyValue("ClassGuid").ToString();

                            Guid mouseGuid = new Guid(usbDevice.GetPropertyValue("ClassGuid").ToString());
                            string instancePath = usbDevice.GetPropertyValue("DeviceID").ToString();

                            if (usbDevice.GetPropertyValue("Name").ToString().Contains("Composite"))
                            {
                                SetDeviceEnabled(mouseGuid, instancePath, true);

                                mouseGuid_ = mouseGuid;
                                instancePath_ = instancePath;
                            }
                        }
                    }

                    if (property.Value.ToString().Contains("Storage"))     // USB
                    {
                        Type = "Memory";

                        var usbDevices = GetLogicalDevices();
                        foreach (var usbDevice in usbDevices)
                        {

                            if (usbDevice.GetPropertyValue("DeviceID").ToString().Contains("USB") &&
                                usbDevice.GetPropertyValue("Status").ToString().Contains("OK"))
                            {
                                string strName = usbDevice.GetPropertyValue("Name").ToString();
                                string strDeviceID = usbDevice.GetPropertyValue("DeviceID").ToString();
                                string strClassGuid = usbDevice.GetPropertyValue("ClassGuid").ToString();

                                Guid mouseGuid = new Guid(usbDevice.GetPropertyValue("ClassGuid").ToString());
                                string instancePath = usbDevice.GetPropertyValue("DeviceID").ToString();

                                if (usbDevice.GetPropertyValue("Name").ToString().Contains("ADB"))
                                //if (usbDevice.GetPropertyValue("Name").ToString().Contains("Composite"))
                                {
                                    isTime = true;
                                    m_mouseGuid = mouseGuid;
                                    m_instancePath = instancePath;

                                    Form1 phonePermission = new Form1();
                                    Application.EnableVisualStyles();
                                    Application.Run(phonePermission);

                                    if (!isTime)
                                    {
                                        break;
                                    }

                                    strPass = phonePermission.strPass;

                                    if (string.IsNullOrEmpty(strPass))
                                    {
                                        MessageBox.Show("Please input the password correctly.");
                                        //return;
                                    }

                                    isTime = false;

                                    bPhone = true;
                                    //if (Type != DeviceType)
                                    //{
                                    Type = "Mobile Phone";
                                    string SendInfo = Type + "   *  " + "INSERT" + "  * " + GetIPAddress().ToString() + " * " + Device1;
                                    SendUsbData(SendInfo);
                                    //}
                                    DeviceType = "Unknown";


                                    string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                                    Console.WriteLine(hostName);
                                    // Get the IP  
                                    
                                    string strPassword = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "-" + GetIPAddress().ToString();

                                    if (strPass == Crypto(strPassword))
                                    {
                                        isPhone = true;
                                    }
                                    else
                                    {
                                        isPhone = false;
                                    }
                                }

                                

                                if (usbDevice.GetPropertyValue("Name").ToString().Contains("Composite"))
                                {
                                    mouseGuid_ = mouseGuid;
                                    instancePath_ = instancePath;
                                }

                                if (usbDevice.GetPropertyValue("Name").ToString().Contains("Storage") || usbDevice.GetPropertyValue("Name").ToString().Contains("Composite"))
                                {
                                    if (isPhone)
                                        SetDeviceEnabled(mouseGuid, instancePath, true);
                                    else
                                        SetDeviceEnabled(mouseGuid, instancePath, false);

                                    isFirst = true;
                                }

                            }
                        }
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
                    //bPhone = true;
                    //if (Type != DeviceType)
                    //{
                    //    Type = "Mobile Phone";
                    //    string SendInfo = Type + "   *  " + "INSERT" + "  * " + GetIPAddress().ToString() + " * ID1 : " + Device1 + " ID2 : " + tempDevice;
                    //    SendUsbData(SendInfo);
                    //}
                    //DeviceType = "Unknown";
                }

            }
        }

        private void UsbRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            
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
                    {
                        Type = "Memory";

                        var usbDevices = GetLogicalDevices();
                        foreach (var usbDevice in usbDevices)
                        {
                            if (usbDevice.GetPropertyValue("DeviceID").ToString().Contains("USB") &&
                                usbDevice.GetPropertyValue("Status").ToString().Contains("OK"))
                            {
                                if (usbDevice.GetPropertyValue("Name").ToString().Contains("Storage"))
                                {
                                    string strName = usbDevice.GetPropertyValue("Name").ToString();
                                    string strDeviceID = usbDevice.GetPropertyValue("DeviceID").ToString();
                                    string strClassGuid = usbDevice.GetPropertyValue("ClassGuid").ToString();

                                    Guid mouseGuid = new Guid(usbDevice.GetPropertyValue("ClassGuid").ToString());
                                    string instancePath = usbDevice.GetPropertyValue("DeviceID").ToString();
                                    SetDeviceEnabled(mouseGuid, instancePath, true);
                                }
                            }
                        }

                        isFirst = false;
                    }
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

            //WqlEventQuery MouseInsertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PointingDevice'");

            //ManagementEventWatcher MouseInsertWatcher = new ManagementEventWatcher(MouseInsertQuery);
            //MouseInsertWatcher.EventArrived += new EventArrivedEventHandler(MouseInsertedEvent);
            //MouseInsertWatcher.Start();

            //WqlEventQuery MouseRemoveQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PointingDevice'");
            //ManagementEventWatcher MouseRemoveWatcher = new ManagementEventWatcher(MouseRemoveQuery);
            //MouseRemoveWatcher.EventArrived += new EventArrivedEventHandler(MouseRemovedEvent);
            //MouseRemoveWatcher.Start();
            
            System.Threading.Thread.Sleep(500);
        }

        public static List<ManagementBaseObject> GetLogicalDevices()
        {
            List<ManagementBaseObject> devices = new List<ManagementBaseObject>();
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'USB%'"))
                collection = searcher.Get();
            foreach (var device in collection)
            {
                devices.Add(device);
            }
            collection.Dispose();
            return devices;
        }


        public static void SetDeviceEnabled(Guid classGuid, string instanceId, bool enable)
        {
            SafeDeviceInfoSetHandle diSetHandle = null;
            try
            {
                diSetHandle = NativeMethods.SetupDiGetClassDevs(ref classGuid, null,
                              IntPtr.Zero, SetupDiGetClassDevsFlags.Present);
                DeviceInfoData[] diData = GetDeviceInfoData(diSetHandle);
                int index = GetIndexOfInstance(diSetHandle, diData, instanceId);
                EnableDevice(diSetHandle, diData[index], enable);
            }
            finally
            {
                if (diSetHandle != null)
                {
                    if (diSetHandle.IsClosed == false)
                    {
                        diSetHandle.Close();
                    }
                    diSetHandle.Dispose();
                }
            }
        }
        private static void EnableDevice(SafeDeviceInfoSetHandle handle,
                                 DeviceInfoData diData, bool enable)
        {
            PropertyChangeParameters @params = new PropertyChangeParameters();
            @params.Size = 8;
            @params.DiFunction = DiFunction.PropertyChange;
            @params.Scope = Scopes.Global;
            if (enable)
            {
                @params.StateChange = StateChangeAction.Enable;
            }
            else
            {
                @params.StateChange = StateChangeAction.Disable;
            }
            bool result = NativeMethods.SetupDiSetClassInstallParams(handle, ref diData, ref @params, Marshal.SizeOf(@params));
            if (result == false) throw new Win32Exception();
            result = NativeMethods.SetupDiCallClassInstaller(DiFunction.PropertyChange, handle, ref diData);
            if (result == false)
            {
                int err = Marshal.GetLastWin32Error();
                //if (err == (int)SetupApiError.NotDisableable)
                //    throw new ArgumentException("Device can't be disabled (programmatically).");
                //else if (err >= (int)SetupApiError.NoAssociatedClass &&
                //        err <= (int)SetupApiError.OnlyValidateViaAuthenticode)
                //    throw new Win32Exception("SetupAPI error: " + ((SetupApiError)err).ToString());
                //else
                //    throw new Win32Exception();
            }
        }

        private static DeviceInfoData[] GetDeviceInfoData(SafeDeviceInfoSetHandle handle)
        {
            List<DeviceInfoData> data = new List<DeviceInfoData>();
            DeviceInfoData did = new DeviceInfoData();
            int didSize = Marshal.SizeOf(did);
            did.Size = didSize;
            int index = 0;
            while (NativeMethods.SetupDiEnumDeviceInfo(handle, index, ref did))
            {
                data.Add(did);
                index += 1;
                did = new DeviceInfoData();
                did.Size = didSize;
            }
            return data.ToArray();
        }

        // Find the index of the particular DeviceInfoData for the instanceId.
        private static int GetIndexOfInstance(SafeDeviceInfoSetHandle handle, DeviceInfoData[] diData, string instanceId)
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            for (int index = 0; index <= diData.Length - 1; index++)
            {
                StringBuilder sb = new StringBuilder(1);
                int requiredSize = 0;
                bool result = NativeMethods.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb, sb.Capacity, out requiredSize);
                if (result == false && Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                {
                    sb.Capacity = requiredSize;
                    result = NativeMethods.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb, sb.Capacity, out requiredSize);
                }
                if (result == false)
                    throw new Win32Exception();
                if (instanceId.Equals(sb.ToString()))
                {
                    return index;
                }
            }
            // not found
            return -1;
        }

        public static string Decrypto(string strpass)
        {
            string strDecrypto = "";
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Md5Key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;
                    string line = "";

                    using (var transform = tdes.CreateDecryptor())
                    {
                        try
                        {
                            byte[] cipherBytes = Convert.FromBase64String(line);
                            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                            strDecrypto = UTF8Encoding.UTF8.GetString(bytes);
                        }
                        catch
                        {

                        }
                    }

                }
            }

            return strDecrypto;
        }

        public static string Crypto(string strPass)
        {
            string strDecryptoPass = "";
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Md5Key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {

                        try
                        {
                            byte[] textBytes = UTF8Encoding.UTF8.GetBytes(strPass);
                            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);

                            strDecryptoPass = Convert.ToBase64String(bytes, 0, bytes.Length);

                            strDecryptoPass = strDecryptoPass[0].ToString() + strDecryptoPass[2].ToString() + strDecryptoPass[4].ToString() + strDecryptoPass[6].ToString() + strDecryptoPass[7].ToString() + strDecryptoPass[8].ToString() + strDecryptoPass[9].ToString() + strDecryptoPass[10].ToString();

                        }
                        catch
                        {

                        }

                    }
                }
            }

            return strDecryptoPass;
        }

        private void PhoneSThread()
        {
            while (true)
            {
                if (isTime)
                {
                    //Thread.Sleep(1000);
                    nTime++;

                    if (nTime >= 60)
                    {
                        var usbDevices = GetLogicalDevices();

                        foreach (var usbDevice in usbDevices)
                        {

                            if (usbDevice.GetPropertyValue("DeviceID").ToString().Contains("USB") &&
                                usbDevice.GetPropertyValue("Status").ToString().Contains("OK"))
                            {
                                string strName = usbDevice.GetPropertyValue("Name").ToString();
                                string strDeviceID = usbDevice.GetPropertyValue("DeviceID").ToString();
                                string strClassGuid = usbDevice.GetPropertyValue("ClassGuid").ToString();

                                Guid mouseGuid = new Guid(usbDevice.GetPropertyValue("ClassGuid").ToString());
                                string instancePath = usbDevice.GetPropertyValue("DeviceID").ToString();

                                if (usbDevice.GetPropertyValue("Name").ToString().Contains("Composite"))
                                {
                                    mouseGuid_ = mouseGuid;
                                    instancePath_ = instancePath;

                                    SetDeviceEnabled(mouseGuid, instancePath, false);
                                    isFirst = true;
                                }


                            }
                        }
                        nTime = 0;
                        isTime = false;
                    }
                }
                else
                    nTime = 0;

                Thread.Sleep(1000);
            }
        }

    }

    public class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(48, 93);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(198, 20);
            this.txtPassword.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please input the password";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(200, 19);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(46, 13);
            this.lblToday.TabIndex = 2;
            this.lblToday.Text = "Today : ";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(266, 91);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Confirm";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(252, 19);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 13);
            this.lblDate.TabIndex = 4;

            string strDate = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
            this.lblDate.Text = strDate;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 171);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Phone Permission";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strPass = this.txtPassword.Text;
            this.Close();
        }

        #endregion

        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblToday;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Label lblDate;

        public string strPass = "";
    }

}

