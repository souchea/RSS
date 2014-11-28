using Ninject;
using RSSAgregator.Mobile.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page de base, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.ViewModel;

namespace RSSAgregator.Mobile.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SourceListPage : Page
    {
        private NavigationHelper navigationHelper;
        public SourcePageViewModel DefaultViewModel { get; set; }

        public SourceListPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            DefaultViewModel = App.Kernel.Get<SourcePageViewModel>();
            DataContext = DefaultViewModel;
        }

        /// <summary>
        /// Obtient le <see cref="NavigationHelper"/> associé à ce <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
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
        /// antérieure.  L'état n'aura pas la valeur Null lors de la première visite de la page.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }

        /// <summary>
        /// Conserve l'état associé à cette page en cas de suspension de l'application ou de
        /// suppression de la page du cache de navigation.  Les valeurs doivent être conformes aux
        /// exigences en matière de sérialisation de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">La source de l'événement ; en général <see cref="NavigationHelper"/></param>
        /// <param name="e">Données d'événement qui fournissent un dictionnaire vide à remplir à l'aide de l'
        /// état sérialisable.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region Inscription de NavigationHelper

        /// <summary>
        /// Les méthodes fournies dans cette section sont utilisées simplement pour permettre
        /// NavigationHelper pour répondre aux méthodes de navigation de la page.
        /// <para>
        /// La logique spécifique à la page doit être placée dans les gestionnaires d'événements pour  
        /// <see cref="NavigationHelper.LoadState"/>
        /// et <see cref="NavigationHelper.SaveState"/>.
        /// Le paramètre de navigation est disponible dans la méthode LoadState 
        /// en plus de l'état de page conservé durant une session antérieure.
        /// </para>
        /// </summary>
        /// <param name="e">Fournit des données pour les méthodes de navigation et
        /// les gestionnaires d'événements qui ne peuvent pas annuler la requête de navigation.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            var text = new TextBlock();
            text = e.Parameter as TextBlock;
            if (text != null) DefaultViewModel.SetCategoryList(text.Text);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Source_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FeedsPage), sender);
        }

        private void SelectList_SelectionModeChanged(object sender, RoutedEventArgs e)
        {
            if (SelectSourceList.SelectionMode != ListViewSelectionMode.Multiple)
            {
                SelectAppBarButton.Visibility = Visibility.Visible;
                DeleteAppBarButton.Visibility = Visibility.Collapsed;
                CommandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
            }
            else
            {
                SelectAppBarButton.Visibility = Visibility.Collapsed;
                DeleteAppBarButton.Visibility = Visibility.Visible;
                CommandBar.Visibility = Visibility.Visible;
                CommandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
            }
        }
        private void SelectAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            SelectSourceList.SelectionMode = ListViewSelectionMode.Multiple;
        }

        private void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> catList = SelectSourceList.SelectedItems.ToList();

            var list = catList.OfType<SourceDTO>();
            DefaultViewModel.DeleteSources(list.ToList());
            SelectSourceList.SelectionMode = ListViewSelectionMode.None;
            CommandBar.Visibility = Visibility.Collapsed;
        }
    }

        
}
