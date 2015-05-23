﻿using AriaView.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private async void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {

            await InitMapAsync();
            //Creation des binding ne pouvant pas etre executer
            //lors de la construction de la page

            var selectedIndexBinding = new Binding { Path = new PropertyPath("initialSelectedDateIndex") };
            var selectedIndexBinding1 = new Binding { Path = new PropertyPath("InitialSelectedDateTerm") };
            datesCB.SetBinding(ComboBox.SelectedIndexProperty, selectedIndexBinding);
            dateTermsCB.SetBinding(ComboBox.SelectedIndexProperty, selectedIndexBinding1);

            //Chargement de la map
            mapView.LoadMapAsync();
        }


        /// <summary>
        /// Insere les objet necessaire à l'affichage des informations de la map
        /// dans le viewModel
        /// </summary>
        /// <returns></returns>
        async Task InitMapAsync()
        {
            //Insertion des objet a binder dans le dictionnaire
            var user = ViewModel["user"] as User;
            ViewModel["sites"] = user.Sites;
            var datesList = ViewModel["datesList"] as List<String>;
            ViewModel["initialSelectedDateIndex"] = 0;
            ViewModel["InitialSelectedDateTerm"] = 0;
            var xmlString = await FileIO.ReadTextAsync(ViewModel["localkmlfile"] as StorageFile);

            //creation de l'objet ariaviewdate
            var kmlReader = new KmlDataReader(XDocument.Parse(xmlString), ViewModel["siteInfoUrl"] as String
                , user.Sites
                , datesList);
            ViewModel["AriaViewDate"] = kmlReader.CreateDate();
            var ariaViewDate = ViewModel["AriaViewDate"] as AriaViewDate;
            ViewModel["currentTermName"] = ariaViewDate.CurrentTerm.StartDate;
        }


        private async void nextTermBtn_Click(object sender, RoutedEventArgs e)
        {
           await mapView.NextTermAsync();
        }

        private async void previousTermBtn_Click(object sender, RoutedEventArgs e)
        {
            await mapView.PreviousTermAsync();
        }

        private async void dateTermsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si le viewModel du mapView est vide
            if (mapView.ViewModel.Count == 0)
                return;
            var source = sender as ComboBox;
            await mapView.ChangeTerm(source.SelectedIndex);
        }

        private void datesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void SetSelectedDateTerm(int i)
        {
            dateTermsCB.SelectedIndex = i;
        }

    }
}
