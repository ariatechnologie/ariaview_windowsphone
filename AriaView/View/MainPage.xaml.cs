using System;
using Windows.Data.Json;
using System.Net;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Net.Http;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace AriaView
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            AuthForm.Caller = this;
            ApplicationData.Current.LocalSettings.Values["wsurl"] = "http://web.aria.fr";
        }

        private void btnPortugese_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "pt-PT";
            throw new Exception();
        }

        private void btnFrench_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "fr-FR";
            throw new Exception();
        }

        private void btnSpanish_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "es-ES";
            throw new Exception();
        }

        private void btnChinese_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "zh-Hans-CN";
            throw new Exception();
        }

        private void btnEnglish_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "en-US";
            throw new Exception();
        }


        
      

    }
}
