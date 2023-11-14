﻿#pragma checksum "..\..\..\View\LoginWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0AC3E5D6947462BBBFD751BDA3F93AB2266FE6D5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Monitor.TaskControl.Resource;
using Monitor.TaskControl.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Monitor.TaskControl.View {
    
    
    /// <summary>
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AVARTA;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WorkDirectory;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox RememberCheckBox;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Monitor.TaskControl;component/view/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\LoginWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\View\LoginWindow.xaml"
            ((Monitor.TaskControl.View.LoginWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\View\LoginWindow.xaml"
            ((Monitor.TaskControl.View.LoginWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\View\LoginWindow.xaml"
            ((Monitor.TaskControl.View.LoginWindow)(target)).Initialized += new System.EventHandler(this.Window_Initialized);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 29 "..\..\..\View\LoginWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Grid_MouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AVARTA = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            
            #line 54 "..\..\..\View\LoginWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.WorkDirectory = ((System.Windows.Controls.TextBox)(target));
            
            #line 78 "..\..\..\View\LoginWindow.xaml"
            this.WorkDirectory.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.WorkDirectory1_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 82 "..\..\..\View\LoginWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.WorkDirectory_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 89 "..\..\..\View\LoginWindow.xaml"
            this.PasswordTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.PasswordTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RememberCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\..\View\LoginWindow.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.PasswordButton_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

