using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using RSSAgregator.Mobile.Model;
using RSSAgregator.Mobile.ViewModel;
using Windows.Storage;
using Windows.Web.Syndication;

namespace RSSAgregator.Mobile.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel defaultViewModel = new MainPageViewModel();

        public MainPageViewModel DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
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
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings; ;

                string feed = RssFeedAddTextBox.Text;
                Task<FeedDto.FeedData> newFeed = defaultViewModel.GetFeedAsync(feed);
                int i = defaultViewModel.Feeds.Count + 1;
                defaultViewModel.Feeds.Add(await newFeed);
                KeyValuePair<string, object> newEntry = new KeyValuePair<string, object>(defaultViewModel.Feeds[i].Title, defaultViewModel.Feeds[i]);
                if (!localSettings.Values.Contains(newEntry))
                {
                    localSettings.Values[defaultViewModel.Feeds[i].Title] = defaultViewModel.Feeds[i];
                }
            }
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
    }
}
