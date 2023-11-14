using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor.TaskView.View;
using Monitor.TaskView.Logger;
using Monitor.TaskView.myEvents;

namespace Monitor.TaskView.Connect
{
    public static class LoaderConnect
    {
        public static bool IsRunning { get; private set; }
        public static bool UpToDate { get; set; }
        internal static string LatestUpdateJson { get; private set; }
        internal static string LatestCoreJson { get; private set; }

        static LoaderConnect()
        {

        }

        public static void ConnectionSystem()
        {
            IsRunning = true;

            var connectWindow = new ConnectionWindow();
            connectWindow.BeginConnect(
                new ConnectionWindow.ConnectionWindowDelegate[]
                {
                    ConnectionRoution.InitializeUpdateRoutine, ConnectionRoution.ConnectionRoutine, ConnectionRoution.DownloadRoutine/**/
                }, null);

            
            IsRunning = false;

            Events.RaiseOnConnectedFinished(EventArgs.Empty);
        }
    }
}
