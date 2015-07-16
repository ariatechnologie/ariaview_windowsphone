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
using AriaView.WebService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AriaView.View;
using Windows.UI.Popups;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page http://go.microsoft.com/fwlink/?LinkId=234236

namespace AriaView.Model
{
    public sealed partial class MapView : UserControl
    {

        private ViewModelBase viewModel = new ViewModelBase();
        private MapPage parentView;
        public Site CurrentSite { get; set; }
        public String FirstDayDate { get; set; }
        public String LastDayDate { get; set; }
        public Pollutant CurrentPollutant { get; set; }
        public AriaViewDateTerm CurrentTerm { get; set; }
        public ViewModelBase ViewModel
        {
            get
            {
                return viewModel;
            }
        }
        

        public WebView GetWebView()
        {
            return webView;
        }


        public MapView()
        {
            this.InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>
        /// Converts an URI into a stream
        /// </summary>
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

        /// <summary>
        ///receive the javascript's notifications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void mapView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            //the notification is a String that begins by the method name to call 
            //and the parameters separated by commas

            var methodName = e.Value.Split(',')[0];
            switch(methodName)
            {
                case "SetScriptVariables":
                    SetScriptVariables();
                    break;
                case "GetExtractionData":
                    var splittedValue = e.Value.Split(',');
                    await GetExtractionData(splittedValue[1],splittedValue[2]);
                    break;
                case "UnsetPinMode":
                    await parentView.UnsetPinMode();
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
            ariaViewDate.CurrentPollutant.CurrentTermIndex = i;
            parentView.ViewModel["currentTermName"] = ariaViewDate.CurrentPollutant.CurrentTerm.StartDate;
            parentView.ViewModel["InitialSelectedDateTerm"] = ariaViewDate.CurrentPollutant.CurrentTermIndex;
        }

        /// <summary>
        /// Loading of the web content in the webview
        /// </summary>
        public void LoadMapAsync()
        {
            var uri = webView.BuildLocalStreamUri("AriaView", "/GoogleMap/Map.html");
            webView.NavigateToLocalStreamUri(uri, new UriToStreamResolver());
        }


        /// <summary>
        ///initialize variables with values
        ///to build the googlemap object
        /// </summary>
        public async void SetScriptVariables()
        {
            parentView = ((Grid)Parent).Parent as MapPage;
            var mapPageViewModel = parentView.ViewModel as ViewModelBase;
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
                    ariaViewDate.CurrentPollutant.CurrentTerm.ImgName,
                    centerLat.ToString(),
                    centerlong.ToString()
            });
        }

        /// <summary>
        /// Displays the next term
        /// </summary>
        public void NextTerm()
        {
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            var i = ariaViewDate.CurrentPollutant.CurrentTermIndex + 1;
            if (i < 0 || i == ariaViewDate.CurrentPollutant.DateTerms.Count)
                return;
            parentView.GetDateTermsComboBox().SelectedIndex = i;
        }

        /// <summary>
        /// Displays the previous term
        /// </summary>
        public void PreviousTerm()
        {
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            var i = ariaViewDate.CurrentPollutant.CurrentTermIndex - 1;
            if (i < 0 || i == ariaViewDate.CurrentPollutant.DateTerms.Count)
                return;
            parentView.GetDateTermsComboBox().SelectedIndex = i;
        }

        /// <summary>
        /// Displays the term according to the specified index
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public async Task ChangeTerm(int i)
        {
            SetCurrentTerm(i);
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            await webView.InvokeScriptAsync("changeOverlay", new String[] { ariaViewDate.CurrentPollutant.CurrentTerm.ImgName });
            parentView.UpdateUI();
        }

        /// <summary>
        /// Enables the pinMode in the webView
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetPinModeValue(string value)
        {
            await webView.InvokeScriptAsync("setPinMode", new List<String> {value});
        }

        /// <summary>
        /// Call the javascript function that send the marker's position
        /// </summary>
        /// <returns></returns>
        public async Task ExtractMarkerData()
        {
            await webView.InvokeScriptAsync("ExtractMarkerData", new List<String> {});
        }

        /// <summary>
        /// Get the datas according to the marker's position from the webservice
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public async Task GetExtractionData(String lat, String lng)
        {
            parentView.GetProgressRing().IsActive = true;
            var urlParts = parentView.ViewModel["urlParts"] as Dictionary<String, String>;
            var webServiceUrl = urlParts["host"] + "/OpenDapServicesRESTAT/GridGetTimeSerieByPointDomainVariablePeriod";
            var requestContent = new List<KeyValuePair<String,String>>();
            requestContent.Add(new KeyValuePair<string,string>("longitude",lng.ToString()));
            requestContent.Add(new KeyValuePair<string,string>("latitude",lat.ToString()));
            requestContent.Add(new KeyValuePair<string,string>("domainid","_LENVIS_"+  urlParts["model"] + "_" + CurrentSite.Name + "_reference_"
            + urlParts["nest"] + "_dataset"));
            requestContent.Add(new KeyValuePair<string,string>("variableid",CurrentPollutant.ScientificName));
            requestContent.Add(new KeyValuePair<string,string>("startdate",FirstDayDate));
             requestContent.Add(new KeyValuePair<string,string>("enddate",LastDayDate));
            var extractDatas = await AriaViewWS.GetExtractionData(webServiceUrl,requestContent);
            if(extractDatas == "Exception null\n")
            {
               await  new MessageDialog("No data found for this location").ShowAsync();
               parentView.GetProgressRing().IsActive = false;
               return;
            }
            parentView.ViewModel["chartPoints"]  = ParseExtratedData(extractDatas);
            parentView.Frame.Navigate(typeof(ChartPage),parentView.ViewModel);
        }

        /// <summary>
        /// Parse the json content downloaded from the webservice
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        List<ChartPoint> ParseExtratedData(String json)
        {
            var dataValuesField = JObject.Parse(json)["GetDataResult"]["dataValuesField"];
            var values = new List<ChartPoint>();
            parentView.ViewModel["pollutantScientificName"] = CurrentPollutant.ScientificName;
            ViewModel["currentSite"] = CurrentSite;
            ViewModel["day"] = String.Format("{0:MM/dd/yyyy}", ConvertFirstDayDate());
            foreach (var value in dataValuesField)
            {
                var date =  System.Text.RegularExpressions.Regex.Match(value["dateTimeField"].ToString(),@"\d+").Value;
                var millisecondes = Double.Parse(date);
                var hour =  (millisecondes / (1000 * 60 * 60)) % 24;
                var val = Math.Round(Double.Parse(value["valueField"].ToString()),2);
                if(val < 0)
                val = 0;
                values.Add(new ChartPoint(
                       hour
                    , Math.Round(Math.Abs(val),4))
                    );
            }
            return values;
        }

        /// <summary>
        /// Format the dates to be compatible with webservice date format
        /// </summary>
        /// <returns></returns>
        private DateTime ConvertFirstDayDate()
        {
            var year = int.Parse(FirstDayDate.Substring(0, 4));
            var month = int.Parse(FirstDayDate.Substring(5, 2));
            var day = int.Parse(FirstDayDate.Substring(8, 2));
            var hour = int.Parse(FirstDayDate.Substring(11, 2));
            var minute = int.Parse(FirstDayDate.Substring(14, 2));
            return new DateTime(year, month, day, hour, minute, 0);
        }
    }
}
