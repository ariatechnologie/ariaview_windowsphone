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

namespace AriaView.Model
{
    /// <summary>
    /// Page de base qui inclut des caractéristiques communes à la plupart des applications.
    /// </summary>
    public sealed partial class MapPage : Page
    {

        private NavigationHelper navigationHelper;
        private MapPageViewModel viewModel = new MapPageViewModel();
        

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
            var previousPageViewModel = e.Parameter as ObservableDictionary;
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
            //Chargement de la map
            mapView.LoadMapAsync();
        }


        /// <summary>
        /// Insere les objet necessaire à l'affichage des informations de la map
        /// dans le viewModel
        /// </summary>
        /// <returns></returns>
        void InitMapAsync()
        {
            //Insertion des objet a binder dans le dictionnaire
            var user = ViewModel["user"] as User;
            ViewModel["sites"] = user.Sites;
            var datesList = ViewModel["datesList"] as List<String>;

            //creation de l'objet ariaviewdate
            //var xmlString = await FileIO.ReadTextAsync(ViewModel["localkmlfile"] as StorageFile);
            var xmlString = ViewModel["kmlString"] as String;
            var kmlReader = new KmlDataReader(XDocument.Parse(xmlString), ViewModel["siteInfoUrl"] as String
                , user.Sites
                , datesList);
            ViewModel["AriaViewDate"] = kmlReader.CreateDate();
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;

            //observablecollection pour l'affichage des echeances
            ViewModel["dateTerms"] = new ObservableCollection<AriaViewDateTerm>(ariaViewDate.DateTerms);
            ViewModel["currentTermName"] = ariaViewDate.CurrentTerm.StartDate;

            //Valeurs par defaut des combobox
            datesCB.SelectedValue = datesList.Last();
            var defaultSite = ViewModel["defaultSite"] as Site;
            sitesCB.SelectedValue = defaultSite;
            dateTermsCB.SelectedIndex = 0;
        }

        /// <summary>
        /// Actualise les comboBox permetant l'affichage des échéances, et de la date en courante
        /// </summary>
        public void UpdateUI()
        {
            var ariaViewDate = viewModel["AriaViewDate"] as AriaViewDate;
            dateTermsCB.SelectedIndex = ariaViewDate.CurrentTermIndex;
        }


        private void nextTermBtn_Click(object sender, RoutedEventArgs e)
        {
           mapView.NextTerm();
        }

        private void previousTermBtn_Click(object sender, RoutedEventArgs e)
        {
           mapView.PreviousTerm();
        }

        private async void dateTermsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si le viewModel du mapView est vide
            if (mapView.ViewModel.Count == 0 || ((ObservableCollection<AriaViewDateTerm>)ViewModel["dateTerms"]).Count == 0)
                return;
            var source = sender as ComboBox;
            await mapView.ChangeTerm(source.SelectedIndex);
        }

        private async void datesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si le viewModel du mapView est vide
            if (mapView.ViewModel.Count == 0)
                return;
            var currentSite = sitesCB.SelectedValue as Site;
            var date = datesCB.SelectedValue as string;
            var url = GetUrl(currentSite.Name) + date + "/";
            await ChangeDateAsync(url);
        }

        public void SetSelectedDateTerm(int i)
        {
            dateTermsCB.SelectedIndex = i;
        }

        public ComboBox GetDateTermsComboBox()
        {
            return dateTermsCB;
        }


        private async Task ChangeDateAsync(string url)
        {

                var kmlString = await new AriaView.WebService.AriaViewWS().GetKmlAsync(url + datesCB.SelectedValue + ".kml");
                var dates = ViewModel["datesList"] as List<string>;
                var user = ViewModel["user"] as User;
                var kmlReader = new KmlDataReader(XDocument.Parse(kmlString), url
                    , user.Sites
                    , dates);

                //Mise à jour l'objet ariaviewdate avec la nouvelle date
                var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
                ariaViewDate.DateTerms = kmlReader.CreateDate().DateTerms;
                ViewModel["currentTermName"] = ariaViewDate.DateTerms[0].StartDate;
                 var dateTermCBContent = ViewModel["dateTerms"] as ObservableCollection<AriaViewDateTerm>;
                 dateTermCBContent.Clear();
                 foreach(AriaViewDateTerm t in ariaViewDate.DateTerms)
                 {
                     dateTermCBContent.Add(t);
                 }
                dateTermsCB.SelectedIndex = 0;
        }

  
        private async Task ChangeSiteAsync(String url)
        {
            var newSite = sitesCB.SelectedValue as Site;
            var siteInfoUrl = GetUrl(newSite.Name);

            //Recupere la liste des date disponible pour le nouveau site
            //et la stocke dans le dictionnaire
            var datesList = await GetDates(siteInfoUrl);
            ViewModel["datesList"] = datesList;

            //Recupere les donnees du Kml du site
            var kmlString = await new AriaView.WebService.AriaViewWS().GetKmlAsync(url + datesList.Last() + ".kml");
            //TODO
            //Creer le nouvel ariaViewDate correspondant au nouveau site avec un KmlDataReader

        }

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

        public String GetUrl(string siteName)
        {
            var urlParts = ViewModel["urlParts"] as Dictionary<string, string>;
            return  String.Format("{0}/{1}/{2}/GEARTH/{3}_{4}/", urlParts["host"], urlParts["url"], siteName, urlParts["model"], urlParts["nest"]);
        }

        private async void sitesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si le viewModel du mapView est vide
            if (mapView.ViewModel.Count == 0)
                return;
            var newSite = sitesCB.SelectedItem as Site;
            await ChangeSiteAsync(GetUrl(newSite.Name));
        }

        private void pollutantsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
