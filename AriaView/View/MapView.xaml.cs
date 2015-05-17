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
using AriaView.GoogleMap;
using System.Xml.Linq;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using AriaView.Common;
using System.Reflection;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page http://go.microsoft.com/fwlink/?LinkId=234236

namespace AriaView.Model
{
    public sealed partial class MapView : UserControl
    {
        private StorageFile kmlStorageFile;
        private string webServiceUrl;

        public AriaViewDateTerm CurrentTerm
        {
            get
            {
                return ViewModel["currentTerm"] as AriaViewDateTerm;
            }
            set
            {
                ViewModel["currentTerm"] = value;
                viewModel["currentTermImage"] = ((AriaViewDateTerm)ViewModel["currentTerm"]).ImgName;
            }
        }

        private ObservableDictionary viewModel = new ObservableDictionary();

        public ObservableDictionary ViewModel
        {
            get
            {
                return viewModel;
            }
        }
        


        public MapView()
        {
            this.InitializeComponent();
            DataContext = viewModel;
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


        private void mapView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            switch(e.Value)
            {
                case "SetScriptVariables":
                    SetScriptVariables();
                    break;
            }
        }

        private async void mapView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //var parentPage = ((Grid)Parent).Parent as MapPage;
            //kmlStorageFile = parentPage.ViewModel["localkmlfile"] as StorageFile;
            //webServiceUrl = parentPage.ViewModel["siteInfoUrl"] as string;
            //await GetMapDataAsync();
            //var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            //var currentTerm = ViewModel["currentTerm"] as AriaViewDateTerm;
            //await webView.InvokeScriptAsync("changeCenter", new String[]
            //{ 
            //    ariaViewDate.North.ToString(),
            //    ariaViewDate.East.ToString(),
            //    ariaViewDate.South.ToString(),
            //    ariaViewDate.West.ToString(),
            //    currentTerm.ImgName
            //});
        }

        private async Task GetMapDataAsync()
        {
            var xmlString = await FileIO.ReadTextAsync(kmlStorageFile);
            var kmlReader = new KmlDataReader(XDocument.Parse(xmlString),webServiceUrl);
            viewModel["AriaViewDate"] = kmlReader.CreateDate();
            SetCurrentTerm(0);
        }

        public void SetCurrentTerm(int i)
        {
            var ariaViewDate = viewModel["AriaViewDate"] as AriaViewDate;
            CurrentTerm = ariaViewDate.DateTerms[i];
        }

        public void LoadMapAsync()
        {
            var uri = webView.BuildLocalStreamUri("AriaView", "/GoogleMap/Map.html");
            webView.NavigateToLocalStreamUri(uri, new UriToStreamResolver());
        }


        public async void SetScriptVariables()
        {
            var parentPage = ((Grid)Parent).Parent as MapPage;
            var ariaViewDate = parentPage.ViewModel["AriaViewDate"] as AriaViewDate;
            await webView.InvokeScriptAsync("setValues", new List<String> {
                    ariaViewDate.North.ToString(),
                    ariaViewDate.East.ToString(),
                    ariaViewDate.South.ToString(),
                    ariaViewDate.West.ToString(),
                    CurrentTerm.ImgName
            });
        }

    }
}
