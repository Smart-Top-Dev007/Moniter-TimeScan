﻿#pragma checksum "..\..\..\View\ExplorerWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "417F561000958D446D071DD2263930C10090AE0B"
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
    /// ExplorerWindow
    /// </summary>
    public partial class ExplorerWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 29 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border UnderBorder;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chSmall;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chMedium;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chLarge;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ImageList;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image backImage;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BackBorder;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image currentImage;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border CurrentBorder;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image forwardImage;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\View\ExplorerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ForwardBorder;
        
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
            System.Uri resourceLocater = new System.Uri("/Monitor.TaskControl;component/view/explorerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ExplorerWindow.xaml"
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
            
            #line 10 "..\..\..\View\ExplorerWindow.xaml"
            ((Monitor.TaskControl.View.ExplorerWindow)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Window_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.UnderBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.chSmall = ((System.Windows.Controls.CheckBox)(target));
            
            #line 40 "..\..\..\View\ExplorerWindow.xaml"
            this.chSmall.Click += new System.Windows.RoutedEventHandler(this.SmallCheckBox_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.chMedium = ((System.Windows.Controls.CheckBox)(target));
            
            #line 64 "..\..\..\View\ExplorerWindow.xaml"
            this.chMedium.Click += new System.Windows.RoutedEventHandler(this.MediumCheckBox_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.chLarge = ((System.Windows.Controls.CheckBox)(target));
            
            #line 88 "..\..\..\View\ExplorerWindow.xaml"
            this.chLarge.Click += new System.Windows.RoutedEventHandler(this.LargeCheckBox_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ImageList = ((System.Windows.Controls.ListView)(target));
            
            #line 111 "..\..\..\View\ExplorerWindow.xaml"
            this.ImageList.AddHandler(System.Windows.Controls.ScrollViewer.ScrollChangedEvent, new System.Windows.Controls.ScrollChangedEventHandler(this.listview_ScrollChanged));
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\View\ExplorerWindow.xaml"
            this.ImageList.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnListViewMouseDown);
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\View\ExplorerWindow.xaml"
            this.ImageList.MouseMove += new System.Windows.Input.MouseEventHandler(this.OnListViewMouseMove);
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\View\ExplorerWindow.xaml"
            this.ImageList.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnPreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\View\ExplorerWindow.xaml"
            this.ImageList.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.OnPreviewMouseUp);
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\View\ExplorerWindow.xaml"
            this.ImageList.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.OnPreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.backImage = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.BackBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.currentImage = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.CurrentBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 12:
            this.forwardImage = ((System.Windows.Controls.Image)(target));
            return;
            case 13:
            this.ForwardBorder = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 7:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseLeftButtonUpEvent;
            
            #line 121 "..\..\..\View\ExplorerWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListView_MouseDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 122 "..\..\..\View\ExplorerWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListView_MouseDoubleDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

