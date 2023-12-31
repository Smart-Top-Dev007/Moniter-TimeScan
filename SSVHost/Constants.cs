﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSVHost
{
    internal static class Constants
    {

        internal static readonly string AutoPatchServerIP = "192.168.109.250";
        //internal static readonly string AutoPatchServerIP = "192.168.104.85";
        internal static readonly int AutoPatchLoop  = 1000 * 60 * 60;
        internal static readonly int ArpInfoLoop  = 999 * 60 * 10;///****

        internal static readonly int ZeroPatchPort = 9997;
        internal static readonly int PatchPort = 19995;
        internal static readonly int ArpInfoPort = 19990;
        internal static readonly int FilePort = 19993;
        internal static readonly int USBPort = 9994;
        internal static readonly string Se_AutoVersion = "RS27";
        internal static readonly string Se_FileEnd = "RS28";
        internal static readonly string Se_AutoFileInfo = "RS23";
        internal static readonly string Se_AutoFileEnd = "RS24";
        internal static readonly string Se_Version = "RS25";


        internal static readonly string Re_UsbDetect = "RR21";
        internal static readonly string Re_UsbEnd = "RR22";
        internal static readonly string Re_FileAlready = "RR23";
        internal static readonly string Re_FileName = "RR24";
        internal static readonly string Re_FileEnd = "RR25";
        internal static readonly string Re_End = "RR26";
        internal static readonly string Re_ARP = "RR27";

        internal static readonly string Re_DrectoryName = "RR27";
        internal static readonly string Re_VersionSame = "same";
        internal static readonly string Re_VersionOther = "other";

        internal static readonly string RegPath = "Ryonbong\\Server";
        //internal static readonly string DbFileName = "Contents.lib";
        //internal static readonly string DbFileName = "Contents.lib";
    }
}
