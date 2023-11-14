using Monitor.TaskView.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Monitor.TaskView.View
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public NotificationWindow()
        : base()
        {
            this.InitializeComponent();
            this.Closed += this.NotificationWindowClosed;
        }

        private void NotificationWindowClosed(object sender, EventArgs e)
        {
            
        }

        public new void Show(string Message, string strType)
        {
            this.Topmost = true;
            base.Show();

            //this.Owner = System.Windows.Application.Current.MainWindow;
            //this.lbl_message.Content = Message;
            this.alarm_date.Content = DateTime.Now.ToString();
            if (strType == Constants.Se_MsgForbidden)
            {
                this.lbl_message.Content = "You are running the Forbbiden Program.";
                lbl_information.Content = "Forbbiden Program: " + Message;
            }
            else if (strType == Constants.Se_MsgDownLoading)
            {
                this.lbl_message.Content = "You are downloading the file.";
                lbl_information.Content = "File Name : " + Message;
            }
            else if (strType == Constants.Se_MsgDownload)
            {
                this.lbl_message.Content = "You have downloaded the file.";
                lbl_information.Content = "File Name : " + Message;
            }
            else if (strType == Constants.Se_MsgDanger)
            {
                this.lbl_message.Content = "You use the danger URL.";
                lbl_information.Content = "Danger URL : " + Message;
            }
            else if (strType == Constants.Se_MsgAudio)
            {
                this.lbl_message.Content = "You are listening music data.";
                lbl_information.Content = "Process Name : " + Message;
            }
            this.Closed += this.NotificationWindowClosed;
            var workingArea = Screen.PrimaryScreen.WorkingArea;

            this.Left = workingArea.Right - this.ActualWidth;
            double top = workingArea.Bottom - this.ActualHeight;

           

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                string windowName = window.GetType().Name;

                if (windowName.Equals("NotificationWindow") && window != this)
                {
                    // Adjust any windows that were above this one to drop down
                    if (window.Top < this.Top)
                    {
                        window.Top = window.Top + this.ActualHeight;
                    }
                }
            }

            this.Top = top;
        }
        private void ImageMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void DoubleAnimationCompleted(object sender, EventArgs e)
        {
            if (!this.IsMouseOver)
            {
                //Settings.Instance.ni.Dispose();
                this.Close();
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Settings.Instance.ni.Dispose();
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Settings.Instance.ni.Dispose();
            this.Close();
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Settings.Instance.ni.Dispose();
            this.Close();
        }
    }
}
