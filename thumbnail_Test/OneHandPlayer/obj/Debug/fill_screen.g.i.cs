﻿#pragma checksum "..\..\fill_screen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "36F6F7FA181A6990DC4F60D699C2D9862DFB5BDFAE9C6402F78483A0A0C75381"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// fill_screen
    /// </summary>
    public partial class fill_screen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 65 "..\..\fill_screen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mediaMain;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\fill_screen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStop;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\fill_screen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPlayTime;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\fill_screen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sldrPlayTime;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\fill_screen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectFile;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/fill_screen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\fill_screen.xaml"
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
            
            #line 13 "..\..\fill_screen.xaml"
            ((WpfApp2.fill_screen)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mediaMain = ((System.Windows.Controls.MediaElement)(target));
            
            #line 65 "..\..\fill_screen.xaml"
            this.mediaMain.MediaEnded += new System.Windows.RoutedEventHandler(this.mediaMain_MediaEnded);
            
            #line default
            #line hidden
            
            #line 65 "..\..\fill_screen.xaml"
            this.mediaMain.MediaOpened += new System.Windows.RoutedEventHandler(this.mediaMain_MediaOpened);
            
            #line default
            #line hidden
            
            #line 65 "..\..\fill_screen.xaml"
            this.mediaMain.MediaFailed += new System.EventHandler<System.Windows.ExceptionRoutedEventArgs>(this.mediaMain_MediaFailed);
            
            #line default
            #line hidden
            
            #line 65 "..\..\fill_screen.xaml"
            this.mediaMain.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.mediaMain_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnStop = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.lblPlayTime = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.sldrPlayTime = ((System.Windows.Controls.Slider)(target));
            return;
            case 6:
            this.btnSelectFile = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

