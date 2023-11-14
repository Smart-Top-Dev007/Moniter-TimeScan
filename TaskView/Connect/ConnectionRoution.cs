using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor.TaskView.View;
using Monitor.TaskView.Globals;
using System.Threading;

namespace Monitor.TaskView.Connect
{
    internal static class ConnectionRoution
    {

        internal static void InitializeUpdateRoutine(ConnectionWindow ui, Dictionary<string, object> args)
        {
            var currentProgress = 0;
            ui.Status = "Chrcking initialize file ...";
            ui.Details = "";           

            for (currentProgress = 0; currentProgress < 20; currentProgress++)
            {
                currentProgress += 1;
                Thread.Sleep(5);
                ui.CurrentProgress = currentProgress * 5;
                ui.OveralCurrentProgress = currentProgress;
                ui.Details = string.Format("    {0}% ", currentProgress);
            }

            return;
        }

        internal static void ConnectionRoutine(ConnectionWindow ui, Dictionary<string, object> args)
        {
            var currentProgress = 0;
            ui.Status = "Connecting ...";
            ui.Details = "";            

            for (currentProgress = 21; currentProgress < 70; currentProgress++)
            {
                currentProgress += 1;
                Thread.Sleep(50);
                ui.CurrentProgress = (currentProgress -21) * 2;
                ui.OveralCurrentProgress = currentProgress;
                ui.Details = string.Format("    {0}% ", currentProgress);
            }

            return;
        }
        internal static void DownloadRoutine(ConnectionWindow ui, Dictionary<string, object> args)
        {
            var currentProgress = 0;
            ui.Status = "Downloading ...";
            ui.Details = "";
            var OveralProgress = ui.OveralCurrentProgress;

            for (currentProgress = 71; currentProgress < 100; currentProgress++)
            {
                currentProgress += 1;
                Thread.Sleep(20);
                ui.CurrentProgress = (currentProgress -70) * 2;
                ui.OveralCurrentProgress = currentProgress;
                ui.Details = string.Format("    {0}% ", currentProgress);
            }

            if ( (Windows.LoginWindow != null ) || !Settings.Instance.RegValue.ExistsGetValue())
            {
                Settings.Instance.RegValue.WriteValue();
            }
            
            return;
        }
    }
}
