﻿

#pragma checksum "C:\Users\jerome\Source\Repos\ariaview_windowsphone\AriaView\View\MapView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "73648230880344C3E6EA2FA4A86EC85D"
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
    partial class MapView : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 14 "..\..\..\View\MapView.xaml"
                ((global::Windows.UI.Xaml.Controls.WebView)(target)).ScriptNotify += this.mapView_ScriptNotify;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


