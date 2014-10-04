using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using RSSAgregator.Mobile.ViewModel;
using Windows.Storage;
using Windows.Web.Syndication;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

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
                string[] value = feed.Split('.');
                KeyValuePair<string, object> newFeed = new KeyValuePair<string, object>(value[1], feed);
                if (!localSettings.Values.Contains(newFeed))
                {
                    localSettings.Values[value[1]] = feed;
                } 
            }
        }
    }
}
