using System;
using System.Collections.Generic;
using System.IO;
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
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Streams;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page http://go.microsoft.com/fwlink/?LinkId=234236

namespace AriaView.Model
{
    public sealed partial class MapView : UserControl
    {
        private StorageFile kmlStorageFile;

        public MapView()
        {
            this.InitializeComponent();
            var uri = webView.BuildLocalStreamUri("AriaView", "/GoogleMap/Map.html");
            webView.NavigateToLocalStreamUri(uri, new UriToStreamResolver());
        }


        public sealed class UriToStreamResolver : Windows.Web.IUriToStreamResolver
        {

            public IAsyncOperation<Windows.Storage.Streams.IInputStream> UriToStreamAsync(Uri uri)
            {
                var path = uri.AbsolutePath;
                return GetStreamAsync(path).AsAsyncOperation();
            }

            async private Task<Windows.Storage.Streams.IInputStream> GetStreamAsync(string path)
            {
                try
                {
                    Uri localUri = new Uri("ms-appx://" + path);
                    StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(localUri);
                    IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);
                    return stream;
                }
                catch(Exception)
                {
                    throw new Exception("File not found");
                }
            }


        }

        private async Task ChangeMapLocationAsync()
        {
            var result = await webView.InvokeScriptAsync("changeCenter",null);
        }

        private void mapView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var test = e.Value;
        }

        private async void mapView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //await ChangeMapLocationAsync();
            var parentPage = ((Grid)Parent).Parent as MapPage;
            kmlStorageFile = parentPage.ViewModel["localkmlfile"] as StorageFile;
            await webView.InvokeScriptAsync("changeCenter", new String[] { kmlStorageFile.Path });
        }
    }
}
