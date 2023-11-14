using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shell;


using Monitor.TaskView.Globals;
using Monitor.TaskView.myEvents;
using Monitor.TaskView.Utils;

namespace Monitor.TaskView.View
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : INotifyPropertyChanged
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
        public SignInWindow()
        {

            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome {  CaptionHeight = 0, UseAeroCaptionButtons = false });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //Settings.Instance.RememberCredentials = RememberCheckBox.IsChecked ?? false;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            //Writing for set UI and Auto login
            // Set UI
            WorkDirectory.Text = Constants.BaseDirectory;
            ServerIP.Focus();
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
            EnvironmentHelper.ShutDown();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
//        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RegistryButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ( ServerIP.Text == "")
            {
                message = new CustomMsg("The ServerIP is required for registration. Please enter the ServerIP ");
                //MessageBox.Show("The ServerIP is required for registration. Please enter the ServerIP ", "Alert");
                ServerIP.Focus();
                return;
            }
            if (UserName.Text == "")
            {
                message = new CustomMsg("The Username is required for registration. Please enter it ");
                //MessageBox.Show("The Username is required for registration. Please enter it", "Alert");
                UserName.Focus();
                return;
            }
            if (Company.Text == "")
            {
                message = new CustomMsg("The Compay name is required for registration. Please enter it ");
                //MessageBox.Show("The Compay name is required for registration. Please enter it", "Alert");
                Company.Focus();
                return;
            }
            if (WorkDirectory.Text == "")
            {
                message = new CustomMsg("The workDirectory is required for registration. Please enter it ");
                //MessageBox.Show("The workDirectory is required for registration. Please enter it", "Alert");
                WorkDirectory.Focus();
                return;
            }

            Events.RaiseOnRegister(e);
        }
        private void WorkDirectory_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                WorkDirectory.Text = openFileDlg.SelectedPath;
            }
            if ( WorkDirectory.Text == ""){
                WorkDirectory.Text = Constants.BaseDirectory;
            }
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
                WorkDirectory.Text = Constants.BaseDirectory;
            }
        }
    }
}
