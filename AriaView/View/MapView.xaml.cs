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

        private ObservableDictionary viewModel = new ObservableDictionary();
        private MapPage parentView;
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

       /// <summary>
       /// Crée l'objet AriaViewDate a partir du kml
       /// </summary>
       /// <returns></returns>
        //private async Task GetMapDataAsync()
        //{
        //    var xmlString = await FileIO.ReadTextAsync(kmlStorageFile);
        //    var kmlReader = new KmlDataReader(XDocument.Parse(xmlString),webServiceUrl);
        //    viewModel["AriaViewDate"] = kmlReader.CreateDate();
        //    SetCurrentTerm(0);
        //}

        /// <summary>
        /// Change l'échéance courante en fonction de l'indice passé en paramètre
        /// </summary>
        /// <param name="i"></param>
        public void SetCurrentTerm(int i)
        {
            var ariaViewDate = viewModel["AriaViewDate"] as AriaViewDate;
            ariaViewDate.CurrentTermIndex = i;
            parentView.ViewModel["currentTermName"] = ariaViewDate.CurrentTerm.StartDate;
            parentView.ViewModel["InitialSelectedDateTerm"] = ariaViewDate.CurrentTermIndex;
        }

        /// <summary>
        /// Chargement des fichiers web dans la webView
        /// </summary>
        public void LoadMapAsync()
        {
            var uri = webView.BuildLocalStreamUri("AriaView", "/GoogleMap/Map.html");
            webView.NavigateToLocalStreamUri(uri, new UriToStreamResolver());
        }


        /// <summary>
        /// Envoie les données nécessaire à la creation
        /// de la map au fichier javascript
        /// </summary>
        public async void SetScriptVariables()
        {
            parentView = ((Grid)Parent).Parent as MapPage;
            var mapPageViewModel = parentView.ViewModel as ObservableDictionary;
            viewModel.SetDictionary(mapPageViewModel);
            var ariaViewDate = this.viewModel["AriaViewDate"] as AriaViewDate;
            var n = ariaViewDate.North;
            var e = ariaViewDate.East;
            var s = ariaViewDate.South;
            var w = ariaViewDate.West;
            var centerLat = (n + s) / 2;
            var centerlong = (e + w) / 2;
            await webView.InvokeScriptAsync("setValues", new List<String> {
                    ariaViewDate.North.ToString(),
                    ariaViewDate.East.ToString(),
                    ariaViewDate.South.ToString(),
                    ariaViewDate.West.ToString(),
                    ariaViewDate.CurrentTerm.ImgName,
                    centerLat.ToString(),
                    centerlong.ToString()
            });
        }

        public void NextTermAsync()
        {
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            var i = ariaViewDate.CurrentTermIndex + 1;
            if (i < 0 || i == ariaViewDate.DateTerms.Count)
                return;
            //await ChangeTerm(i);
            parentView.GetDateTermsComboBox().SelectedIndex = i;
        }

        public void PreviousTermAsync()
        {
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            var i = ariaViewDate.CurrentTermIndex - 1;
            if (i < 0 || i == ariaViewDate.DateTerms.Count)
                return;
            //await ChangeTerm(i);
            parentView.GetDateTermsComboBox().SelectedIndex = i;
        }

        public async Task ChangeTerm(int i)
        {
            SetCurrentTerm(i);
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            await webView.InvokeScriptAsync("changeOverlay", new String[] { ariaViewDate.CurrentTerm.ImgName });
            parentView.UpdateUI();
        }

    }
}
