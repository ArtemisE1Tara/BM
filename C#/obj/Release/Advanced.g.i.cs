﻿#pragma checksum "..\..\Advanced.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "92FB9D6BF85ED1FDE0C7DD8AD2A4128F784A8E5D61C03F6A62E6CCE9D4FB705D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using BMBF_Manager;
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


namespace BMBF_Manager {
    
    
    /// <summary>
    /// Advanced
    /// </summary>
    public partial class Advanced : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\Advanced.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbox;
        
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
            System.Uri resourceLocater = new System.Uri("/BMBF Manager;component/advanced.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Advanced.xaml"
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
            
            #line 10 "..\..\Advanced.xaml"
            ((BMBF_Manager.Advanced)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Drag);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 13 "..\..\Advanced.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.noDrag);
            
            #line default
            #line hidden
            
            #line 13 "..\..\Advanced.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.doDrag);
            
            #line default
            #line hidden
            
            #line 13 "..\..\Advanced.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 14 "..\..\Advanced.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.noDrag);
            
            #line default
            #line hidden
            
            #line 14 "..\..\Advanced.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.doDrag);
            
            #line default
            #line hidden
            
            #line 14 "..\..\Advanced.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Mini);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\Advanced.xaml"
            this.txtbox.MouseEnter += new System.Windows.Input.MouseEventHandler(this.noDrag);
            
            #line default
            #line hidden
            
            #line 24 "..\..\Advanced.xaml"
            this.txtbox.MouseLeave += new System.Windows.Input.MouseEventHandler(this.doDrag);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

