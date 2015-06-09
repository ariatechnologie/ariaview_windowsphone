using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using Windows.Storage;
using System.Security;
using Windows.Security.Credentials;
using AriaView.ViewModel;


namespace AriaView.Model
{
    public sealed partial class AuthentificationForm : UserControl
    {

        public Page Caller { get; set; }
        private AuthentificationViewModel viewModel = new AuthentificationViewModel();
        public AuthentificationViewModel ViewModel
        {
            get
            {
                return viewModel;
            }

            set
            {
                viewModel = value;
            }
        }

        public AuthentificationForm()
        {
            this.InitializeComponent();
            DataContext = viewModel;
        }


       async private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = await viewModel.AuthentificationAsync();
            if (result != null)
            {
                viewModel["user"] = new User
                {
                    Login = (string)viewModel["Login"],
                    Password = (string)viewModel["Password"],
                };
                viewModel["xml"] = result;
                if (cbMemoriser.IsChecked == true)
                    viewModel.SaveCredentials();
                else
                    viewModel.RemoveCredentials();
                Caller.Frame.Navigate(typeof(SiteSelectionPage), viewModel);
            }
            else
            {
                MsgError.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
           
        }


       
       

}
}
