﻿#pragma checksum "..\..\..\..\View\PasswordWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84B01A0F34BDB147508BDEA6C05182ADB27DE089"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Monitor.TaskView.Globals;
using Monitor.TaskView.Resource;
using Monitor.TaskView.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Monitor.TaskView.View {
    
    
    /// <summary>
    /// PasswordWindow
    /// </summary>
    public partial class PasswordWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\..\View\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AVARTA;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\View\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\View\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox RememberCheckBox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\View\PasswordWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Monitor.TaskView;component/view/passwordwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PasswordWindow.xaml"
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
            
            #line 12 "..\..\..\..\View\PasswordWindow.xaml"
            ((Monitor.TaskView.View.PasswordWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\View\PasswordWindow.xaml"
            ((Monitor.TaskView.View.PasswordWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\View\PasswordWindow.xaml"
            ((Monitor.TaskView.View.PasswordWindow)(target)).Initialized += new System.EventHandler(this.Window_Initialized);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 31 "..\..\..\..\View\PasswordWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Grid_MouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AVARTA = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            
            #line 65 "..\..\..\..\View\PasswordWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 85 "..\..\..\..\View\PasswordWindow.xaml"
            this.PasswordTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.PasswordTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RememberCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\View\PasswordWindow.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.PasswordButton_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

