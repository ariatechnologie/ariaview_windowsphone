﻿using AriaView.Common;
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
using AriaView.ViewModel;
using Windows.Data.Xml.Dom;
using AriaView.Model;
using Windows.Storage;


// Pour en savoir plus sur le modèle d'élément Page de base, consultez la page http://go.microsoft.com/fwlink/?LinkId=234237

namespace AriaView
{
    /// <summary>
    /// Page de base qui inclut des caractéristiques communes à la plupart des applications.
    /// </summary>
    public sealed partial class SiteSelectionPage : Page
    {
        private NavigationHelper navigationHelper;
        private SiteSelectionViewModel viewModel;

        /// <summary>
        /// NavigationHelper est utilisé sur chaque page pour faciliter la navigation et 
        /// gestion de la durée de vie des processus
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public SiteSelectionPage()
        {
            this.InitializeComponent();
            if (siteCB.Items.Count > 0)
                siteCB.Items.Clear();
            viewModel = new SiteSelectionViewModel();
            DataContext = viewModel;
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
            var viewModelBase = e.Parameter as ObservableDictionary;
            viewModel.SetDictionary(viewModelBase);
            viewModel.ParseResponse((string)viewModel["xml"]);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (siteCB.SelectedItem == null)
                return;
            
           await viewModel.GetSiteInfoAsync(siteCB.SelectedItem as Site);
           var defaultSite = siteCB.SelectedItem as Site;
           viewModel["defaultSite"] = defaultSite;
           if ((bool)viewModel["saveSite"] == true)
           {
               ApplicationData.Current.LocalSettings.Values["lastSite"] = defaultSite.Name;
           }  
           Frame.Navigate(typeof(MapPage),viewModel);
        }

      
    }
}
