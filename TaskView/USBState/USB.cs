using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.TaskView.USBState
{
    public class USB
    {
        public USB()
        {
            UsbState usb = new UsbState();
            BackgroundWorker bgwDriveDetector = new BackgroundWorker();
            bgwDriveDetector.DoWork += usb.bgwDriveDetector_DoWork;
            bgwDriveDetector.RunWorkerAsync();
            bgwDriveDetector.WorkerReportsProgress = true;
            bgwDriveDetector.WorkerSupportsCancellation = true;
        }
    }
}
