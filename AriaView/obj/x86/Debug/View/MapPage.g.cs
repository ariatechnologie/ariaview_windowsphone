﻿

#pragma checksum "C:\Users\jerome\Source\Repos\ariaview_windowsphone\AriaView\View\MapPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "78A75DC74E35DBAA662119D4A57F23FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AriaView.Model
{
    partial class MapPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 11 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.pageRoot_Loaded;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 46 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.previousTermBtn_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 49 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.dateTermsCB_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 59 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.nextTermBtn_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 68 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.sitesCB_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 86 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.playBtn_Click;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 75 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.pollutantsCB_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 62 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.datesCB_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 101 "..\..\..\View\MapPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


