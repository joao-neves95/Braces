﻿#pragma checksum "..\..\..\UserControls\TextEditorControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "545911BABDEC8C8345ECD7B8E0931898516612CF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Braces.UI.UserControls;
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


namespace Braces.UI.UserControls {
    
    
    /// <summary>
    /// TextEditorControl
    /// </summary>
    public partial class TextEditorControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\UserControls\TextEditorControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Braces.UI.UserControls.TextEditorControl textEditorControl;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\UserControls\TextEditorControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Input.KeyBinding CutBinding;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\UserControls\TextEditorControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer lineNumbersScroller;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\UserControls\TextEditorControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lineNumersListBlock;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\UserControls\TextEditorControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox richTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Braces;component/usercontrols/texteditorcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\TextEditorControl.xaml"
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
            this.textEditorControl = ((Braces.UI.UserControls.TextEditorControl)(target));
            return;
            case 2:
            
            #line 21 "..\..\..\UserControls\TextEditorControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MoveUp_Handler);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 25 "..\..\..\UserControls\TextEditorControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MoveDown_Handler);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 29 "..\..\..\UserControls\TextEditorControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Duplicate_Handler);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 33 "..\..\..\UserControls\TextEditorControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Duplicate_Handler);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CutBinding = ((System.Windows.Input.KeyBinding)(target));
            return;
            case 7:
            this.lineNumbersScroller = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 8:
            this.lineNumersListBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.richTextBox = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 81 "..\..\..\UserControls\TextEditorControl.xaml"
            this.richTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.OnPreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 81 "..\..\..\UserControls\TextEditorControl.xaml"
            this.richTextBox.KeyUp += new System.Windows.Input.KeyEventHandler(this.OnKeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

