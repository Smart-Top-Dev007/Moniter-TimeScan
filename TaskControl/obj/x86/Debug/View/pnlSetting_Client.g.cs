﻿#pragma checksum "..\..\..\..\View\pnlSetting_Client.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1B482A1CB88601CEF006333C3051C7E9C79E2CBA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// pnlSetting_Client
    /// </summary>
    public partial class pnlSetting_Client : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel setting_client;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse bConnect;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblClientName;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblName;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblOS;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblWork;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\View\pnlSetting_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
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
            System.Uri resourceLocater = new System.Uri("/Monitor.TaskControl;component/view/pnlsetting_client.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\pnlSetting_Client.xaml"
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
            this.setting_client = ((System.Windows.Controls.StackPanel)(target));
            
            #line 15 "..\..\..\..\View\pnlSetting_Client.xaml"
            this.setting_client.MouseLeave += new System.Windows.Input.MouseEventHandler(this.stackpanel_MouseLeave);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\View\pnlSetting_Client.xaml"
            this.setting_client.MouseMove += new System.Windows.Input.MouseEventHandler(this.stackpanel_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bConnect = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.lblClientName = ((System.Windows.Controls.Label)(target));
            
            #line 18 "..\..\..\..\View\pnlSetting_Client.xaml"
            this.lblClientName.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.lblClientName_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblName = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblOS = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblWork = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\View\pnlSetting_Client.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

