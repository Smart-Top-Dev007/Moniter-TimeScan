using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Monitor.TaskView.Utils
{
    public static class NativeImports
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr processHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool wow64Process);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("coredll.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool EnumProcessModules(IntPtr hProcess,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] uint[] lphModule, uint cb, [MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded);
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    }

    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VirtualMemoryOperation = 0x00000008,
        VirtualMemoryRead = 0x00000010,
        VirtualMemoryWrite = 0x00000020,
        DuplicateHandle = 0x00000040,
        CreateProcess = 0x000000080,
        SetQuota = 0x00000100,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        QueryLimitedInformation = 0x00001000,
        Synchronize = 0x00100000
    }

    public enum Win32Error
    {
        ERROR_SUCCESS = 0,
        ERROR_INVALID_FUNCTION = 1,
        ERROR_FILE_NOT_FOUND = 2,
        ERROR_PATH_NOT_FOUND = 3,
        ERROR_TOO_MANY_OPEN_FILES = 4,
        ERROR_ACCESS_DENIED = 5,
        ERROR_INVALID_HANDLE = 6,
        ERROR_ARENA_TRASHED = 7,
        ERROR_NOT_ENOUGH_MEMORY = 8,
        ERROR_INVALID_BLOCK = 9,
        ERROR_BAD_ENVIRONMENT = 10,
        ERROR_BAD_FORMAT = 11,
        ERROR_INVALID_ACCESS = 12,
        ERROR_INVALID_DATA = 13,
        ERROR_OUTOFMEMORY = 14,
        ERROR_INVALID_DRIVE = 15,
        ERROR_CURRENT_DIRECTORY = 16,
        ERROR_NOT_SAME_DEVICE = 17,
        ERROR_NO_MORE_FILES = 18,
        ERROR_WRITE_PROTECT = 19,
        ERROR_BAD_UNIT = 20,
        ERROR_NOT_READY = 21,
        ERROR_BAD_COMMAND = 22,
        ERROR_CRC = 23,
        ERROR_BAD_LENGTH = 24,
        ERROR_SEEK = 25,
        ERROR_NOT_DOS_DISK = 26,
        ERROR_SECTOR_NOT_FOUND = 27,
        ERROR_OUT_OF_PAPER = 28,
        ERROR_WRITE_FAULT = 29,
        ERROR_READ_FAULT = 30,
        ERROR_GEN_FAILURE = 31,
        ERROR_SHARING_VIOLATION = 32,
        ERROR_LOCK_VIOLATION = 33,
        ERROR_WRONG_DISK = 34,
        ERROR_SHARING_BUFFER_EXCEEDED = 36,
        ERROR_HANDLE_EOF = 38,
        ERROR_HANDLE_DISK_FULL = 39,
        ERROR_NOT_SUPPORTED = 50,
        ERROR_REM_NOT_LIST = 51,
        ERROR_DUP_NAME = 52,
        ERROR_BAD_NETPATH = 53,
        ERROR_NETWORK_BUSY = 54,
        ERROR_DEV_NOT_EXIST = 55,
        ERROR_TOO_MANY_CMDS = 56,
        ERROR_ADAP_HDW_ERR = 57,
        ERROR_BAD_NET_RESP = 58,
        ERROR_UNEXP_NET_ERR = 59,
        ERROR_BAD_REM_ADAP = 60,
        ERROR_PRINTQ_FULL = 61,
        ERROR_NO_SPOOL_SPACE = 62,
        ERROR_PRINT_CANCELLED = 63,
        ERROR_NETNAME_DELETED = 64,
        ERROR_NETWORK_ACCESS_DENIED = 65,
        ERROR_BAD_DEV_TYPE = 66,
        ERROR_BAD_NET_NAME = 67,
        ERROR_TOO_MANY_NAMES = 68,
        ERROR_TOO_MANY_SESS = 69,
        ERROR_SHARING_PAUSED = 70,
        ERROR_REQ_NOT_ACCEP = 71,
        ERROR_REDIR_PAUSED = 72,
        ERROR_FILE_EXISTS = 80,
        ERROR_CANNOT_MAKE = 82,
        ERROR_FAIL_I24 = 83,
        ERROR_OUT_OF_STRUCTURES = 84,
        ERROR_ALREADY_ASSIGNED = 85,
        ERROR_INVALID_PASSWORD = 86,
        ERROR_INVALID_PARAMETER = 87,
        ERROR_NET_WRITE_FAULT = 88,
        ERROR_NO_PROC_SLOTS = 89,
        ERROR_TOO_MANY_SEMAPHORES = 100,
        ERROR_EXCL_SEM_ALREADY_OWNED = 101,
        ERROR_SEM_IS_SET = 102,
        ERROR_TOO_MANY_SEM_REQUESTS = 103,
        ERROR_INVALID_AT_INTERRUPT_TIME = 104,
        ERROR_SEM_OWNER_DIED = 105,
        ERROR_SEM_USER_LIMIT = 106,
        ERROR_DISK_CHANGE = 107,
        ERROR_DRIVE_LOCKED = 108,
        ERROR_BROKEN_PIPE = 109,
        ERROR_OPEN_FAILED = 110,
        ERROR_BUFFER_OVERFLOW = 111,
        ERROR_DISK_FULL = 112,
        ERROR_NO_MORE_SEARCH_HANDLES = 113,
        ERROR_INVALID_TARGET_HANDLE = 114,
        ERROR_INVALID_CATEGORY = 117,
        ERROR_INVALID_VERIFY_SWITCH = 118,
        ERROR_BAD_DRIVER_LEVEL = 119,
        ERROR_CALL_NOT_IMPLEMENTED = 120,
        ERROR_SEM_TIMEOUT = 121,
        ERROR_INSUFFICIENT_BUFFER = 122,
        ERROR_INVALID_NAME = 123,
        ERROR_INVALID_LEVEL = 124,
        ERROR_NO_VOLUME_LABEL = 125,
        ERROR_MOD_NOT_FOUND = 126,
        ERROR_PROC_NOT_FOUND = 127,
        ERROR_WAIT_NO_CHILDREN = 128,
        ERROR_CHILD_NOT_COMPLETE = 129,
        ERROR_DIRECT_ACCESS_HANDLE = 130,
        ERROR_NEGATIVE_SEEK = 131,
        ERROR_SEEK_ON_DEVICE = 132,
        ERROR_IS_JOIN_TARGET = 133,
        ERROR_IS_JOINED = 134,
        ERROR_IS_SUBSTED = 135,
        ERROR_NOT_JOINED = 136,
        ERROR_NOT_SUBSTED = 137,
        ERROR_JOIN_TO_JOIN = 138,
        ERROR_SUBST_TO_SUBST = 139,
        ERROR_JOIN_TO_SUBST = 140,
        ERROR_SUBST_TO_JOIN = 141,
        ERROR_BUSY_DRIVE = 142,
        ERROR_SAME_DRIVE = 143,
        ERROR_DIR_NOT_ROOT = 144,
        ERROR_DIR_NOT_EMPTY = 145,
        ERROR_IS_SUBST_PATH = 146,
        ERROR_IS_JOIN_PATH = 147,
        ERROR_PATH_BUSY = 148,
        ERROR_IS_SUBST_TARGET = 149,
        ERROR_SYSTEM_TRACE = 150,
        ERROR_INVALID_EVENT_COUNT = 151,
        ERROR_TOO_MANY_MUXWAITERS = 152,
        ERROR_INVALID_LIST_FORMAT = 153,
        ERROR_LABEL_TOO_LONG = 154,
        ERROR_TOO_MANY_TCBS = 155,
        ERROR_SIGNAL_REFUSED = 156,
        ERROR_DISCARDED = 157,
        ERROR_NOT_LOCKED = 158,
        ERROR_BAD_THREADID_ADDR = 159,
        ERROR_BAD_ARGUMENTS = 160,
        ERROR_BAD_PATHNAME = 161,
        ERROR_SIGNAL_PENDING = 162,
        ERROR_MAX_THRDS_REACHED = 164,
        ERROR_LOCK_FAILED = 167,
        ERROR_BUSY = 170,
        ERROR_DEVICE_SUPPORT_IN_PROGRESS = 171,
        ERROR_CANCEL_VIOLATION = 173,
        ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174,
        ERROR_INVALID_SEGMENT_NUMBER = 180,
        ERROR_INVALID_ORDINAL = 182,
        ERROR_ALREADY_EXISTS = 183,
        ERROR_INVALID_FLAG_NUMBER = 186,
        ERROR_SEM_NOT_FOUND = 187,
        ERROR_INVALID_STARTING_CODESEG = 188,
        ERROR_INVALID_STACKSEG = 189,
        ERROR_INVALID_MODULETYPE = 190,
        ERROR_INVALID_EXE_SIGNATURE = 191,
        ERROR_EXE_MARKED_INVALID = 192,
        ERROR_BAD_EXE_FORMAT = 193,
        ERROR_ITERATED_DATA_EXCEEDS_64k = 194,
        ERROR_INVALID_MINALLOCSIZE = 195,
        ERROR_DYNLINK_FROM_INVALID_RING = 196,
        ERROR_IOPL_NOT_ENABLED = 197,
        ERROR_INVALID_SEGDPL = 198,
        ERROR_AUTODATASEG_EXCEEDS_64k = 199,
        ERROR_RING2SEG_MUST_BE_MOVABLE = 200,
        ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201,
        ERROR_INFLOOP_IN_RELOC_CHAIN = 202,
        ERROR_ENVVAR_NOT_FOUND = 203,
        ERROR_NO_SIGNAL_SENT = 205,
        ERROR_FILENAME_EXCED_RANGE = 206,
        ERROR_RING2_STACK_IN_USE = 207,
        ERROR_META_EXPANSION_TOO_LONG = 208,
        ERROR_INVALID_SIGNAL_NUMBER = 209,
        ERROR_THREAD_1_INACTIVE = 210,
        ERROR_LOCKED = 212,
        ERROR_TOO_MANY_MODULES = 214,
        ERROR_NESTING_NOT_ALLOWED = 215,
        ERROR_EXE_MACHINE_TYPE_MISMATCH = 216,
        ERROR_EXE_CANNOT_MODIFY_SIGNED_BINARY = 217,
        ERROR_EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY = 218,
        ERROR_FILE_CHECKED_OUT = 220,
        ERROR_CHECKOUT_REQUIRED = 221,
        ERROR_BAD_FILE_TYPE = 222,
        ERROR_FILE_TOO_LARGE = 223,
        ERROR_FORMS_AUTH_REQUIRED = 224,
        ERROR_VIRUS_INFECTED = 225,
        ERROR_VIRUS_DELETED = 226,
        ERROR_PIPE_LOCAL = 229,
        ERROR_BAD_PIPE = 230,
        ERROR_PIPE_BUSY = 231,
        ERROR_NO_DATA = 232,
        ERROR_PIPE_NOT_CONNECTED = 233,
        ERROR_MORE_DATA = 234,
        ERROR_VC_DISCONNECTED = 240,
        ERROR_INVALID_EA_NAME = 254,
        ERROR_EA_LIST_INCONSISTENT = 255,
        ERROR_NO_MORE_ITEMS = 259,
        ERROR_CANNOT_COPY = 266,
        ERROR_DIRECTORY = 267,
        ERROR_EAS_DIDNT_FIT = 275,
        ERROR_EA_FILE_CORRUPT = 276,
        ERROR_EA_TABLE_FULL = 277,
        ERROR_INVALID_EA_HANDLE = 278,
        ERROR_EAS_NOT_SUPPORTED = 282,
        ERROR_NOT_OWNER = 288,
        ERROR_TOO_MANY_POSTS = 298,
        ERROR_PARTIAL_COPY = 299,
        ERROR_OPLOCK_NOT_GRANTED = 300,
        ERROR_INVALID_OPLOCK_PROTOCOL = 301,
        ERROR_DISK_TOO_FRAGMENTED = 302,
        ERROR_DELETE_PENDING = 303,
        ERROR_INCOMPATIBLE_WITH_GLOBAL_SHORT_NAME_REGISTRY_SETTING = 304,
        ERROR_SHORT_NAMES_NOT_ENABLED_ON_VOLUME = 305,
        ERROR_SECURITY_STREAM_IS_INCONSISTENT = 306,
        ERROR_INVALID_LOCK_RANGE = 307,
        ERROR_IMAGE_SUBSYSTEM_NOT_PRESENT = 308,
        ERROR_NOTIFICATION_GUID_ALREADY_DEFINED = 309,
        ERROR_INVALID_EXCEPTION_HANDLER = 310,
        ERROR_DUPLICATE_PRIVILEGES = 311,
        ERROR_NO_RANGES_PROCESSED = 312,
        ERROR_NOT_ALLOWED_ON_SYSTEM_FILE = 313,
        ERROR_DISK_RESOURCES_EXHAUSTED = 314,
        ERROR_INVALID_TOKEN = 315,
        ERROR_DEVICE_FEATURE_NOT_SUPPORTED = 316,
        ERROR_MR_MID_NOT_FOUND = 317,
        ERROR_SCOPE_NOT_FOUND = 318,
        ERROR_UNDEFINED_SCOPE = 319,
        ERROR_INVALID_CAP = 320,
        ERROR_DEVICE_UNREACHABLE = 321,
        ERROR_DEVICE_NO_RESOURCES = 322,
        ERROR_DATA_CHECKSUM_ERROR = 323,
        ERROR_INTERMIXED_KERNEL_EA_OPERATION = 324,
        ERROR_FILE_LEVEL_TRIM_NOT_SUPPORTED = 326,
        ERROR_OFFSET_ALIGNMENT_VIOLATION = 327,
        ERROR_INVALID_FIELD_IN_PARAMETER_LIST = 328,
        ERROR_OPERATION_IN_PROGRESS = 329,
        ERROR_BAD_DEVICE_PATH = 330,
        ERROR_TOO_MANY_DESCRIPTORS = 331,
        ERROR_SCRUB_DATA_DISABLED = 332,
        ERROR_NOT_REDUNDANT_STORAGE = 333,
        ERROR_RESIDENT_FILE_NOT_SUPPORTED = 334,
        ERROR_COMPRESSED_FILE_NOT_SUPPORTED = 335,
        ERROR_DIRECTORY_NOT_SUPPORTED = 336,
        ERROR_NOT_READ_FROM_COPY = 337,
        ERROR_FAIL_NOACTION_REBOOT = 350,
        ERROR_FAIL_SHUTDOWN = 351,
        ERROR_FAIL_RESTART = 352,
        ERROR_MAX_SESSIONS_REACHED = 353,
        ERROR_THREAD_MODE_ALREADY_BACKGROUND = 400,
        ERROR_THREAD_MODE_NOT_BACKGROUND = 401,
        ERROR_PROCESS_MODE_ALREADY_BACKGROUND = 402,
        ERROR_PROCESS_MODE_NOT_BACKGROUND = 403,
        ERROR_INVALID_ADDRESS = 487,
    }
    public class WinCursor
    {

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        private const Int32 CURSOR_SHOWING = 0x0001;
        private const Int32 DI_NORMAL = 0x0003;
        //With all these hacks i keep needing to do I need to learn C++ and go back to COM that microdoft kicked us all off with .NET about 15 years ago and still uses at its core

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);
        public static Color CaptureCursor(ref int X, ref int Y, Graphics theShot, int ScreenServerX, int ScreenServerY)
        {//We return a color so that it can be embeded in a bitmap to be returned to the client program
            IntPtr C = Cursors.Arrow.Handle;
            CURSORINFO pci;
            pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));
            if (GetCursorInfo(out pci))
            {
                X = pci.ptScreenPos.x;
                Y = pci.ptScreenPos.y;
                if (pci.hCursor == Cursors.Default.Handle) return Color.Red;
                else if (pci.hCursor == Cursors.WaitCursor.Handle) return Color.Green;
                else if (pci.hCursor == Cursors.Arrow.Handle) return Color.Blue;
                else if (pci.hCursor == Cursors.IBeam.Handle) return Color.White;
                else if (pci.hCursor == Cursors.Hand.Handle) return Color.Violet;
                else if (pci.hCursor == Cursors.SizeNS.Handle) return Color.Yellow;
                else if (pci.hCursor == Cursors.SizeWE.Handle) return Color.Orange;
                else if (pci.hCursor == Cursors.SizeNESW.Handle) return Color.Aqua;
                else if (pci.hCursor == Cursors.SizeNWSE.Handle) return Color.Pink;
                else if (pci.hCursor == Cursors.PanEast.Handle) return Color.BlueViolet;
                else if (pci.hCursor == Cursors.HSplit.Handle) return Color.Cyan;
                else if (pci.hCursor == Cursors.VSplit.Handle) return Color.DarkGray;
                else if (pci.hCursor == Cursors.Help.Handle) return Color.DarkGreen;
                else if (pci.hCursor == Cursors.AppStarting.Handle) return Color.SlateGray;
                if (pci.flags == CURSOR_SHOWING)
                {//We need to caputer the mouse image and embed tha in the screen shot of the desktop because it's a custom mouse cursor
                    float XReal = pci.ptScreenPos.x * (float)ScreenServerX / (float)Screen.PrimaryScreen.Bounds.Width - 11;
                    float YReal = pci.ptScreenPos.y * (float)ScreenServerY / (float)Screen.PrimaryScreen.Bounds.Height - 11;
                    int x = Screen.PrimaryScreen.Bounds.X;
                    var hdc = theShot.GetHdc();
                    DrawIconEx(hdc, (int)XReal, (int)YReal, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                    theShot.ReleaseHdc();
                }
                return Color.Black;
            }
            X = 0;
            Y = 0;
            return Color.Black;
        }
        public static Cursor ColorToCursor(Color C)
        {//Code for the client that pulls a pixel from the picture and converts it to a cursor
            if (C.ToArgb() == Color.Red.ToArgb()) return Cursors.Default;
            if (C.ToArgb() == Color.Green.ToArgb()) return Cursors.WaitCursor;
            if (C.ToArgb() == Color.Blue.ToArgb()) return Cursors.Arrow;
            if (C.ToArgb() == Color.White.ToArgb()) return Cursors.IBeam;
            if (C.ToArgb() == Color.Violet.ToArgb()) return Cursors.Hand;
            if (C.ToArgb() == Color.Yellow.ToArgb()) return Cursors.SizeNS;
            if (C.ToArgb() == Color.Orange.ToArgb()) return Cursors.SizeWE;
            if (C.ToArgb() == Color.Aqua.ToArgb()) return Cursors.SizeNESW;
            if (C.ToArgb() == Color.Pink.ToArgb() || C.B == 206) return Cursors.SizeNWSE;
            if (C.ToArgb() == Color.BlueViolet.ToArgb()) return Cursors.PanEast;
            if (C.ToArgb() == Color.Cyan.ToArgb()) return Cursors.HSplit;
            if (C.ToArgb() == Color.DarkGray.ToArgb()) return Cursors.VSplit;
            if (C.ToArgb() == Color.DarkGreen.ToArgb()) return Cursors.Help;
            if (C.ToArgb() == Color.SlateGray.ToArgb()) return Cursors.AppStarting;
            if (C.ToArgb() == Color.Fuchsia.ToArgb()) return Cursors.No;
            return Cursors.Default;
        }
    }

}
