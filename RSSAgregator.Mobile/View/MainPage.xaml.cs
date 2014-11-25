using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using RSSAgregator.Mobile.Common;
using RSSAgregator.Mobile.Model;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.ViewModel;
using Windows.Storage;
using Windows.Web.Syndication;
using Ninject;

namespace RSSAgregator.Mobile.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPageViewModel DefaultViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            DefaultViewModel = App.Kernel.Get<MainPageViewModel>();
            DataContext = DefaultViewModel;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d’événement décrivant la manière dont l’utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }

        private async void AddFeed_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await AddNewFeed.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                string feed = RssFeedAddTextBox.Text;
                GetFeedAsync(feed);
                /*int i = DefaultViewModel.Feeds.Count;
                DefaultViewModel.Feeds.Add(await newFeed);*/
            }
        }

        private async void GetFeedAsync(string feedUriString)
        {
            Frame.Navigate(typeof (Categories), feedUriString);
            /*SyndicationClient client = new SyndicationClient();
            Uri feedUri = new Uri(feedUriString);

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                StorageManager.SaveFileAsync(feed.Title.Text, feed);
            }
            catch (Exception)
            {
            }*/
        }

        private async void Click_Account(object sender, RoutedEventArgs e)
        {
            Button accountButton = new Button();
            accountButton = sender as Button;
            if (accountButton.Content.Equals("Login"))
            {
                Frame.Navigate(typeof(ConnectionPage));
            }
        }

        private void Feed_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private async void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SourceListPage), sender);
        }

        private void Source_OnTapped(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var selected = selectionChangedEventArgs.AddedItems[0] as Shared.Model.SourceDTO;
            //int selected = DefaultViewModel.SelectedSourceIndex;
            Frame.Navigate(typeof(FeedsPage), selected);
        }
    }
}
