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
using AriaView.Model;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.ApplicationModel.DataTransfer;

// Pour en savoir plus sur le modèle d'élément Page de base, consultez la page http://go.microsoft.com/fwlink/?LinkId=234237

namespace AriaView.View
{
    /// <summary>
    /// Page that display the extracted datas in a chart
    /// </summary>
    public sealed partial class ChartPage : Page
    {

        private NavigationHelper navigationHelper;
        private ViewModelBase defaultViewModel = new ViewModelBase();
        private DataTransferManager dataTransfer;

        /// <summary>
        /// Cela peut être remplacé par un modèle d'affichage fortement typé.
        /// </summary>
        public ViewModelBase ViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper allows to easily navigate between pages and
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public ChartPage()
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

        /// <summary>
        /// When navigate on this page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            RegisterForShare();
            var vm = e.Parameter as ViewModelBase;
            ViewModel.SetDictionary(vm);
        }

        /// <summary>
        /// When navigate from this page to another
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        
        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //Generate the chart
            var charPoints = (ViewModel as ViewModelBase)["chartPoints"] as List<ChartPoint>;
            (lineChart.Series[0] as LineSeries).ItemsSource = charPoints;
            lineChart.Title = ViewModel["currentSite"] + " " + ViewModel["day"];
            var legenditem = (lineChart.Series[0] as LineSeries).LegendItems[0] as LegendItem;
            legenditem.Content = ViewModel["pollutantScientificName"] as string;
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            //display the share menu
            DataTransferManager.ShowShareUI();
        }

        void dataTransfert_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
        }


        void RegisterForShare()
        {
            var dataTransfert = DataTransferManager.GetForCurrentView();
            dataTransfert.DataRequested += dataTransfert_DataRequested;
        }


    }
}
