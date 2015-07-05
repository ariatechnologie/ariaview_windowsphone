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
using Windows.UI.Popups;


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

            if (!AriaView.WebService.AriaViewWS.IsConnectedToInternet())
            {
                await new MessageDialog("Network error").ShowAsync();
                return;
            }
                

            var result = await viewModel.AuthentificationAsync();
            ViewModel["lastSiteName"] = ApplicationData.Current.LocalSettings.Values["lastSite"];
            
            if (result != null)
            {
                viewModel["user"] = new User
                {
                    Login = (string)viewModel["Login"],
                    Password = (string)viewModel["Password"],
                };
                viewModel["xml"] = result;
                if (cbMemoriser.IsChecked == true)
                {
                    viewModel.SaveCredentials();
                    ViewModel["saveSite"] = true;
                }
                else
                {
                    ViewModel["saveSite"] = false;
                    viewModel.RemoveCredentials();
                    ApplicationData.Current.LocalSettings.Values["lastSite"] = string.Empty;
                }

                ViewModel.ParseResponse((string)ViewModel["xml"]);
                var user = viewModel["user"] as User;

                //Teste l'existance du conteneur de site par defaut sur le terminal
                //et le cree s'il n'existe pas
                try
                {
                   var temp = ApplicationData.Current.LocalSettings.Values["lastSite"].ToString();
                }
                catch
                {
                    ApplicationData.Current.LocalSettings.Values["lastSite"] = string.Empty;
                }


                if (ApplicationData.Current.LocalSettings.Values["lastSite"].ToString() != string.Empty
                    && user.Sites.Where(X => X.Name == ApplicationData.Current.LocalSettings.Values["lastSite"].ToString()).Count() > 0)
                {
                    var site = user.Sites.First(X => X.Name == ApplicationData.Current.LocalSettings.Values["lastSite"].ToString());
                    viewModel["defaultSite"] = site;
                    await viewModel.GetSiteInfoAsync(site);
                    if ((bool)viewModel["saveSite"] == true)
                    {
                        ApplicationData.Current.LocalSettings.Values["lastSite"] = site.Name;
                    }
                    Caller.Frame.Navigate(typeof(MapPage), viewModel);
                }
                else
                    Caller.Frame.Navigate(typeof(SiteSelectionPage), viewModel);
            }
            else
            {
                MsgError.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
           
        }

     


       
       

}
}
