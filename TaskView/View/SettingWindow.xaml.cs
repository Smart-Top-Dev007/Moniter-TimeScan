using Monitor.TaskView.Globals;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using Monitor.TaskView.Connect;
using Monitor.TaskView.Utils;
using Monitor.TaskView.Models;

using Monitor.TaskView.myEvents;

namespace Monitor.TaskView.View
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : INotifyPropertyChanged
    {
        private bool _isAvailable = true;
        public CustomMsg message;
        public bool IsAvailable
        {
            get { return _isAvailable; }

            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }

            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public SettingWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0, UseAeroCaptionButtons = false });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //Settings.Instance.RememberCredentials = RememberCheckBox.IsChecked ?? false;

            Close();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            //Writing for set UI and Auto login
            // Set UI
            ServerIP.Text = Settings.Instance.RegValue.ServerIP;
            Username.Text = Settings.Instance.RegValue.UserName;
            Company.Text = Settings.Instance.RegValue.Company;
            WorkDirectory.Text = Settings.Instance.RegValue.BaseDirectory;
            Version.Text = Constants.version;
            Width.Text = Settings.Instance.RegValue.CaptureWidth.ToString();
            Height.Text = Settings.Instance.RegValue.CaptureHeight.ToString();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //        Process.Start(new ProcessStartInfo(Constants.WeScriptLostPasswordURL));
        }       

        private void TextBlock2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //        Process.Start(new ProcessStartInfo(Constants.WeScriptRegisterURL));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            if ( ServerIP.Text == "")
            {
                message = new CustomMsg("Please enter ServerIP.");
                //MessageBox.Show(" Please enter ServerIP.", "ServerIP");
                ServerIP.Focus();
                return;
            }
            if (Username.Text == "")
            {
                message = new CustomMsg("Please enter Username.");
                //MessageBox.Show(" Please enter Username.", "Alert");
                Username.Focus();
                return;
            }
            if (Company.Text == "")
            {
                message = new CustomMsg("Please enter Company.");
                //MessageBox.Show(" Please enter Company.", "Alert");
                Company.Focus();
                return;
            }
            if (WorkDirectory.Text == "")
            {
                message = new CustomMsg("Please enter WorkDirectory.");
                //MessageBox.Show(" Please enter WorkDirectory.", "Alert");
                WorkDirectory.Focus();
                return;
            }

            if(ServerIP.Text != Settings.Instance.RegValue.ServerIP)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
              
                try
                {
                    key.DeleteSubKey(Constants.RegPath);
                    EnvironmentHelper.Restart();
                }
                catch
                {

                }
            }

            Hide();

            Settings.Instance.RegValue.UserName = Username.Text;
            Settings.Instance.RegValue.Company = Company.Text;
            if (Settings.Instance.RegValue.BaseDirectory != WorkDirectory.Text)
            {
                try
                {
                    MainProc.MainWorkThread.Suspend();
                    MainProc.UrlThread.Suspend();
                    MainProc.AudioThread.Suspend();

                    FileHelper.SafeMoveFolder(Settings.Instance.RegValue.BaseDirectory, WorkDirectory.Text, true);

                    MainProc.MainWorkThread.Resume();
                    MainProc.UrlThread.Resume();
                    MainProc.AudioThread.Resume();
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
                Settings.Instance.RegValue.BaseDirectory = WorkDirectory.Text;
            }

            bool bFlag = true;
            if (Settings.Instance.RegValue.ServerIP != ServerIP.Text)
            {
                bFlag = false;
            }
            Settings.Instance.RegValue.ServerIP = ServerIP.Text;
            Settings.Instance.RegValue.WriteValue();
            if (bFlag == false)
            {
                Events.RaiseOnChangeServer();
                Close();
                return;
            }



            string strTemp = Settings.Instance.RegValue.UserName + ":" + Settings.Instance.RegValue.Password + ":" + Settings.Instance.RegValue.Company + ":" + Settings.Instance.RegValue.SessionTime + ":" + Settings.Instance.RegValue.CaptureTime + ":" + Settings.Instance.RegValue.SlideWidth + ":" + Settings.Instance.RegValue.SlideHeight + ":" + Settings.Instance.RegValue.CaptureWidth + ":" + Settings.Instance.RegValue.CaptureHeight + ":" + "" + ":" + "" + ":" + Settings.Instance.RegValue.ActiveDuration;
            //


            string strClientInfo = Constants.Re_SetInfo + strTemp;
            byte[] buffer = Encoding.UTF8.GetBytes(strClientInfo);
            try
            {
                Settings.Instance.SockCom.Send(buffer);
            }
            catch (Exception ex)
            {
                Communications.Disconnect();
            }



            Windows.MainWindow.Show();
            Close();


        }

        private void WorkDirectory1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                WorkDirectory.Text = openFileDlg.SelectedPath;
            }
            if (WorkDirectory.Text == "")
            {
                WorkDirectory.Text = Settings.Instance.Directories.WorkDirectory;
            }
        }

        private void WorkDirectory_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                WorkDirectory.Text = openFileDlg.SelectedPath;
            }
            if (WorkDirectory.Text == "")
            {
                WorkDirectory.Text = Settings.Instance.Directories.WorkDirectory;
            }
        }
    }
}
