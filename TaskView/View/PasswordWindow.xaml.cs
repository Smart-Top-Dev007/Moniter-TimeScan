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
using System.Windows.Shell;


using Monitor.TaskView.Globals;
using Monitor.TaskView.myEvents;
using Monitor.TaskView.Connect;
using Monitor.TaskView.Utils;

namespace Monitor.TaskView.View
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : INotifyPropertyChanged
    {
        private bool _isAvailable = true;
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
        public event PropertyChangedEventHandler PropertyChanged;
        //        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }

            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public PasswordWindow()
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
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            //Writing for set UI and Auto login
            // Set UI
            PasswordTextBox.Focus();
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
        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown && e.Key == Key.Enter)
            {
                PasswordButton_OnClick(sender, e);
            }
        }
        private void PasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password == "")
            {
                CustomMsg message = new CustomMsg("Please enter Password. You can get a password by contacting the administrator.");
                //MessageBox.Show(" Please enter Password. You can get a password by contacting the administrator.  ", "Alert");
                PasswordTextBox.Focus();
                return;
            }
            if (PasswordTextBox.Password.Trim() != Settings.Instance.RegValue.Password.Trim())
            {
                CustomMsg msg = new CustomMsg("Please input the correct password! ");
                return;
            }
            

            Events.RaiseOnPassword();
        }

    }
}
