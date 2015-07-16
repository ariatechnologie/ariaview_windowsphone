using AriaView.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AriaView.ViewModel;
using AriaView.GoogleMap;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

namespace AriaView.Model
{
    /// <summary>
    /// Page de base qui inclut des caractéristiques communes à la plupart des applications.
    /// </summary>
    public sealed partial class MapPage : Page
    {

        private NavigationHelper navigationHelper;
        private MapPageViewModel viewModel = new MapPageViewModel();
        private DispatcherTimer timer;
        private int termScrollingIndex, termScrollingLimit;
        private bool IstermScrollingEnable;
        public bool PinMode { get; private set; }


        /// <summary>
        /// Cela peut être remplacé par un modèle d'affichage fortement typé.
        /// </summary>
        public MapPageViewModel ViewModel
        {
            get { return viewModel; }
        }

        /// <summary>
        /// NavigationHelper est utilisé sur chaque page pour faciliter la navigation et 
        /// gestion de la durée de vie des processus
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MapPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += TermScrolling;
            IstermScrollingEnable = false;
            PinMode = false;
        }

        /// <summary>
        /// display the day terms one after thr other
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void TermScrolling(object sender, object e)
        {
            if (++termScrollingIndex == termScrollingLimit - 1)
                await mapView.ChangeTerm(0);
            else
                mapView.NextTerm();

        }

        /// <summary>
        /// begin the term scrolling and disable the other buttons
        /// </summary>
        async void StartTermScrolling()
        {
            sitesCB.IsEnabled = false;
            dateTermsCB.IsEnabled = false;
            datesCB.IsEnabled = false;
            nextTermBtn.IsEnabled = false;
            previousTermBtn.IsEnabled = false;
            pollutantsCB.IsEnabled = false;
            pinBtn.IsEnabled = false;
            extractionBtn.IsEnabled = false;
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            termScrollingLimit = ariaViewDate.CurrentPollutant.DateTerms.Count;
            await mapView.ChangeTerm(0);
            btnStartStop.Source = new BitmapImage(new Uri("ms-appx:///Assets/stop.png"));
            timer.Start();
        }

        /// <summary>
        /// stop the term scrolling adnd nables the other buttons
        /// </summary>
        async void StopTermScrolling()
        {
            timer.Stop();
            await mapView.ChangeTerm(0);
            btnStartStop.Source = new BitmapImage(new Uri("ms-appx:///Assets/play.png"));
            termScrollingIndex = 0;
            sitesCB.IsEnabled = true;
            dateTermsCB.IsEnabled = true;
            datesCB.IsEnabled = true;
            nextTermBtn.IsEnabled = true;
            previousTermBtn.IsEnabled = true;
            pollutantsCB.IsEnabled = true;
            pinBtn.IsEnabled = true;
            extractionBtn.IsEnabled = true;
        }



        /// <summary>
        /// Remplit la page à l'aide du contenu passé lors de la navigation. Tout état enregistré est également
        /// fourni lorsqu'une page est recréée à partir d'une session antérieure.
        /// </summary>
        /// <param name="sender">
        /// La source de l'événement ; en général <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Données d'événement qui fournissent le paramètre de navigation transmis à
        /// <see cref="Frame.Navigate(Type, Object)"/> lors de la requête initiale de cette page et
        /// un dictionnaire d'état conservé par cette page durant une session
        /// antérieure. L'état n'aura pas la valeur Null lors de la première visite de la page.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Conserve l'état associé à cette page en cas de suspension de l'application ou de la
        /// suppression de la page du cache de navigation.  Les valeurs doivent être conformes aux
        /// exigences en matière de sérialisation de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">La source de l'événement ; en général <see cref="NavigationHelper"/></param>
        /// <param name="e">Données d'événement qui fournissent un dictionnaire vide à remplir à l'aide de
        /// état sérialisable.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region Inscription de NavigationHelper

        /// Les méthodes fournies dans cette section sont utilisées simplement pour permettre
        /// NavigationHelper pour répondre aux méthodes de navigation de la page.
        /// 
        /// La logique spécifique à la page doit être placée dans les gestionnaires d'événements pour  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// et <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// Le paramètre de navigation est disponible dans la méthode LoadState 
        /// en plus de l'état de page conservé durant une session antérieure.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            var previousPageViewModel = e.Parameter as ViewModelBase;
            ViewModel.SetDictionary(previousPageViewModel);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            InitMapAsync();
            //map loading
            mapView.LoadMapAsync();
        }


        /// <summary>
        /// Insert all necessary objects to display the map's informations in the viewModel
        /// </summary>
        /// <returns></returns>
        void InitMapAsync()
        {
            progressRing.IsActive = true;
            //Inserts the objects to bind in the dictionary
            var user = ViewModel["user"] as User;
            ViewModel["sites"] = user.Sites;
            var datesList = ViewModel["datesList"] as List<String>;

            //ariaviewdate object's creation
            var xmlString = ViewModel["kmlString"] as String;
            var kmlReader = new KmlDataReader(XDocument.Parse(xmlString), ViewModel["siteInfoUrl"] as String
                , user.Sites
                , datesList);
            ViewModel["AriaViewDate"] = kmlReader.CreateAriaViewDate();
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;

            //observablecollection to display the terms
            ViewModel["dateTerms"] = new ObservableCollection<AriaViewDateTerm>(ariaViewDate.CurrentPollutant.DateTerms);
            ViewModel["pollutantsList"] = ariaViewDate.PollutantsList;
            ViewModel["currentTermName"] = ariaViewDate.CurrentPollutant.CurrentTerm.StartDate;

            //Default values for the comboboxes
            datesCB.SelectedValue = datesList.Last();
            var defaultSite = ViewModel["defaultSite"] as Site;
            sitesCB.SelectedValue = defaultSite;
            dateTermsCB.SelectedIndex = 0;
            pollutantsCB.SelectedIndex = 0;

            //Inserts the legend image's url in the dictionary
            var currentSite = sitesCB.SelectedValue as Site;
            ViewModel["legendImage"] = GetUrl(currentSite.Name) + "/" + datesList.Last() + "/" + ariaViewDate.CurrentPollutant.LegendImage;
            progressRing.IsActive = false;
        }

        /// <summary>
        /// Actualise  comboBoxes
        /// </summary>
        public void UpdateUI()
        {
            var ariaViewDate = viewModel["AriaViewDate"] as AriaViewDate;
            dateTermsCB.SelectedIndex = ariaViewDate.CurrentPollutant.CurrentTermIndex;
        }

       /// <summary>
       /// display the next term on the map
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private async void nextTermBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!AriaView.WebService.AriaViewWS.IsConnectedToInternet())
            {
                await new MessageDialog("Network error").ShowAsync();
                return;
            }
            mapView.NextTerm();
        }

        /// <summary>
        /// display the previous term in the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void previousTermBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!AriaView.WebService.AriaViewWS.IsConnectedToInternet())
            {
                await new MessageDialog("Network error").ShowAsync();
                return;
            }
            mapView.PreviousTerm();
        }


        private async void dateTermsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = sender as ComboBox;
            //if viewModel is empty
            if (mapView.ViewModel.Count == 0 || ((ObservableCollection<AriaViewDateTerm>)ViewModel["dateTerms"]).Count == 0
                || source.SelectedIndex < 0)
                return;
            await mapView.ChangeTerm(source.SelectedIndex);
        }

        private async void datesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!AriaView.WebService.AriaViewWS.IsConnectedToInternet())
            {
                await new MessageDialog("Network error").ShowAsync();
                return;
            }
            //if viewModel is empty
            if (mapView.ViewModel.Count == 0 || datesCB.SelectedValue == null)
                return;
            var currentSite = sitesCB.SelectedValue as Site;
            var date = datesCB.SelectedValue as string;
            var url = GetUrl(currentSite.Name) + date + "/";
            await ChangeDateAsync(url);
        }

        /// <summary>
        /// set the current with the sepocified index
        /// </summary>
        /// <param name="i"></param>
        public void SetSelectedDateTerm(int i)
        {
            dateTermsCB.SelectedIndex = i;
        }

        public ComboBox GetDateTermsComboBox()
        {
            return dateTermsCB;
        }

        /// <summary>
        /// Change the date
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task ChangeDateAsync(string url)
        {
            progressRing.IsActive = true;
            var kmlString = await new AriaView.WebService.AriaViewWS().GetKmlAsync(url + datesCB.SelectedValue + ".kml");
            var dates = ViewModel["datesList"] as List<string>;
            var user = ViewModel["user"] as User;
            var kmlReader = new KmlDataReader(XDocument.Parse(kmlString), url
                , user.Sites
                , dates);

            //Mise à jour l'objet ariaviewdate avec la nouvelle date
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            ariaViewDate.PollutantsList = kmlReader.CreateAriaViewDate().PollutantsList;
            ViewModel["currentTermName"] = ariaViewDate.PollutantsList[0].DateTerms[0].StartDate;
            var dateTermCBContent = ViewModel["dateTerms"] as ObservableCollection<AriaViewDateTerm>;
            dateTermCBContent.Clear();
            foreach (AriaViewDateTerm t in ariaViewDate.PollutantsList[0].DateTerms)
            {
                dateTermCBContent.Add(t);
            }
            dateTermsCB.SelectedIndex = 0;
            progressRing.IsActive = false;
        }

        /// <summary>
        /// Change the site
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task ChangeSiteAsync(String url)
        {
            progressRing.IsActive = true;
            var newSite = sitesCB.SelectedValue as Site;
            var siteInfoUrl = GetUrl(newSite.Name);
            var user = ViewModel["user"] as User;

            //Recupere la liste des date disponible pour le nouveau site
            //et la stocke dans le dictionnaire
            var datesList = await GetDates(siteInfoUrl);
            ViewModel["datesList"] = datesList;

            //Recupere les donnees du Kml du site
            var kmlString = await new AriaView.WebService.AriaViewWS().GetKmlAsync(url + "/" + datesList.Last() + "/" + datesList.Last() + ".kml");

            //Creation du nouvel ariaViewDate correspondant au nouveau site avec un KmlDataReader
            var kmlReader = new KmlDataReader(XDocument.Parse(kmlString)
            , siteInfoUrl + "/" + datesList.Last()
            , user.Sites
            , datesList);
            var newAriaViewDate = kmlReader.CreateAriaViewDate();
            ViewModel["AriaViewDate"] = newAriaViewDate;

            //observablecollection pour l'affichage des echeances
            ViewModel["dateTerms"] = new ObservableCollection<AriaViewDateTerm>(newAriaViewDate.CurrentPollutant.DateTerms);
            ViewModel["currentTermName"] = newAriaViewDate.CurrentPollutant.CurrentTerm.StartDate;
            ViewModel["pollutantsList"] = newAriaViewDate.PollutantsList;

            //Mise a jour de la legende des polluants
            //ViewModel["legendImage"] = newAriaViewDate.CurrentPollutant.LegendImage;

            //Valeurs par defaut des combobox
            datesCB.SelectedValue = datesList.Last();
            var defaultSite = ViewModel["defaultSite"] as Site;
            dateTermsCB.SelectedIndex = 0;
            pollutantsCB.SelectedIndex = 0;

            //relocalisation le centre de la carte et le overlay sur le nouveau site
            var ariaViewDate = this.viewModel["AriaViewDate"] as AriaViewDate;
            var n = ariaViewDate.North;
            var e = ariaViewDate.East;
            var s = ariaViewDate.South;
            var w = ariaViewDate.West;
            var centerLat = (n + s) / 2;
            var centerlong = (e + w) / 2;
            await mapView.GetWebView().InvokeScriptAsync("changeLocationInfos", new List<String> {
                    ariaViewDate.North.ToString(),
                    ariaViewDate.East.ToString(),
                    ariaViewDate.South.ToString(),
                    ariaViewDate.West.ToString(),
                    centerLat.ToString(),
                    centerlong.ToString()
            });

            //Mise a jour de la legend du polluant
            var currentSite = sitesCB.SelectedValue as Site;
            ViewModel["legendImage"] = GetUrl(currentSite.Name) + "/" + datesList.Last() + "/" + ariaViewDate.CurrentPollutant.LegendImage;

            await mapView.ChangeTerm(0);
            progressRing.IsActive = false;
        }

        /// <summary>
        /// Get the availables dates for the current site from the webservice
        /// </summary>
        /// <param name="siteRootUrl"></param>
        /// <returns></returns>
        private async Task<List<String>> GetDates(string siteRootUrl)
        {
            var ws = new AriaView.WebService.AriaViewWS();
            var siteInfoXml = await ws.GetSitesInfosAsync(sitesCB.SelectedValue as Site, ViewModel["user"] as User);
            var dateFilename = XDocument.Parse(siteInfoXml).Descendants("datefile").ElementAt(0).Value;
            var datesXml = await ws.GetDatesAsync(siteRootUrl + dateFilename);
            var datesList = new List<String>();
            foreach (var date in XDocument.Parse(datesXml).Descendants("Folder").Descendants("name"))
                datesList.Add(date.Value);
            return datesList;
        }

        /// <summary>
        /// returns the webservice url according to the current site
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public String GetUrl(string siteName)
        {
            var urlParts = ViewModel["urlParts"] as Dictionary<string, string>;
            return String.Format("{0}/{1}/{2}/GEARTH/{3}_{4}/", urlParts["host"], urlParts["url"], siteName, urlParts["type"], urlParts["scale"]);
        }

        private async void sitesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!AriaView.WebService.AriaViewWS.IsConnectedToInternet())
            {
                await new MessageDialog("Network error").ShowAsync();
                return;
            }
            //Si le viewModel du mapView est vide
            if (mapView.ViewModel.Count == 0)
                return;
            var newSite = sitesCB.SelectedItem as Site;
            await ChangeSiteAsync(GetUrl(newSite.Name));
        }

        private void pollutantsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Si le viewModel du mapView est vide
            if (mapView.ViewModel.Count == 0
                || pollutantsCB.SelectedIndex < 0)
                return;
            var newPollutantIndex = pollutantsCB.SelectedIndex;
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            ariaViewDate.CurrentPollutantIndex = newPollutantIndex;
            ViewModel["currentTermName"] = ariaViewDate.PollutantsList[newPollutantIndex].DateTerms[0].StartDate;
            var dateTermCBContent = ViewModel["dateTerms"] as ObservableCollection<AriaViewDateTerm>;
            dateTermCBContent.Clear();
            foreach (AriaViewDateTerm t in ariaViewDate.PollutantsList[newPollutantIndex].DateTerms)
            {
                dateTermCBContent.Add(t);
            }
            dateTermsCB.SelectedIndex = 0;

            //Mise a jour de la legende du polluant
            var currentSite = sitesCB.SelectedValue as Site;
            var datesList = ViewModel["datesList"] as List<string>;
            ViewModel["legendImage"] = GetUrl(currentSite.Name) + "/" + datesList.Last() + "/" + ariaViewDate.CurrentPollutant.LegendImage;
        }



        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IstermScrollingEnable)
            {
                StartTermScrolling();
                IstermScrollingEnable = true;
            }
            else
            {
                StopTermScrolling();
                IstermScrollingEnable = false;
            }
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

        private async void extractionBtn_Click(object sender, RoutedEventArgs e)
        {
            mapView.CurrentSite = sitesCB.SelectedItem as Site;
            var dateTermsList = dateTermsCB.Items.ToList();
            var firstDate = dateTermsList.First() as AriaViewDateTerm;
            var lastDate = dateTermsList.Last() as AriaViewDateTerm;
            mapView.FirstDayDate = firstDate.StartDate.Split('+')[0].Replace("T", " ");
            mapView.LastDayDate = lastDate.EndDate.Split('+')[0].Replace("T", " ");
            mapView.CurrentPollutant = pollutantsCB.SelectedItem as Pollutant;
            mapView.CurrentTerm = dateTermsCB.SelectedItem as AriaViewDateTerm;
            await mapView.ExtractMarkerData();
        }

        public ProgressRing GetProgressRing()
        {
            return progressRing;
        }

        /// <summary>
        /// enbles the pinMode and disables the other buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void pinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!PinMode)
            {
                PinMode = true;
                //Enable addMarker on tje map
                await mapView.SetPinModeValue("true");
                sitesCB.IsEnabled = false;
                dateTermsCB.IsEnabled = false;
                datesCB.IsEnabled = false;
                nextTermBtn.IsEnabled = false;
                previousTermBtn.IsEnabled = false;
                pollutantsCB.IsEnabled = false;
                playBtn.IsEnabled = false;
            }
            else
            {
                PinMode = false;
                //Disable addMarker on the map
                await mapView.SetPinModeValue("false");
                sitesCB.IsEnabled = true;
                dateTermsCB.IsEnabled = true;
                datesCB.IsEnabled = true;
                nextTermBtn.IsEnabled = true;
                previousTermBtn.IsEnabled = true;
                pollutantsCB.IsEnabled = true;
                playBtn.IsEnabled = true;
            }
        }

        /// <summary>
        /// Disable the pinMode and enable the other buttons
        /// </summary>
        /// <returns></returns>
        public async Task UnsetPinMode()
        {
            PinMode = false;
            //Disable addMarker on the map
            await mapView.SetPinModeValue("false");
            sitesCB.IsEnabled = true;
            dateTermsCB.IsEnabled = true;
            datesCB.IsEnabled = true;
            nextTermBtn.IsEnabled = true;
            previousTermBtn.IsEnabled = true;
            pollutantsCB.IsEnabled = true;
            playBtn.IsEnabled = true;
        }

    }
}
