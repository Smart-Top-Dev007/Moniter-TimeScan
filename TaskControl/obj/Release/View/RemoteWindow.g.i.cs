﻿#pragma checksum "..\..\..\View\RemoteWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "374A1ED4CBCC3E425ED887C6BD57E3D5203AA74E"
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
    /// RemoteWindow
    /// </summary>
    public partial class RemoteWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\View\RemoteWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox viewBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\View\RemoteWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MyCanvas;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\View\RemoteWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image myImage;
        
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
            System.Uri resourceLocater = new System.Uri("/Monitor.TaskControl;component/view/remotewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\RemoteWindow.xaml"
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
            
            #line 10 "..\..\..\View\RemoteWindow.xaml"
            ((Monitor.TaskControl.View.RemoteWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\View\RemoteWindow.xaml"
            ((Monitor.TaskControl.View.RemoteWindow)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.MyCanvas_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.viewBox = ((System.Windows.Controls.Viewbox)(target));
            return;
            case 3:
            this.MyCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 19 "..\..\..\View\RemoteWindow.xaml"
            this.MyCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.MyCanvas_MouseMove);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\View\RemoteWindow.xaml"
            this.MyCanvas.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MyImage_MouseUp);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\View\RemoteWindow.xaml"
            this.MyCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MyCanvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\View\RemoteWindow.xaml"
            this.MyCanvas.KeyUp += new System.Windows.Input.KeyEventHandler(this.MyCanvas_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.myImage = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            
            #line 29 "..\..\..\View\RemoteWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Click += new System.Windows.RoutedEventHandler(this.CheckBox_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

