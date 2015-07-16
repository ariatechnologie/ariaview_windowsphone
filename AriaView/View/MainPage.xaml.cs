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

        /// <summary>
        /// Change app language to portuguese
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPortugese_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "pt-PT";
            restartMsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Change app language to french
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFrench_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "fr-FR";
            restartMsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Change app language to spanish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpanish_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "es-ES";
            restartMsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }


        /// <summary>
        /// Change app language to chinese
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChinese_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "zh-Hans-CN";
            restartMsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Change app language to english
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnglish_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["lang"] = "en-US";
            restartMsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Quit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }


        
      

    }
}
