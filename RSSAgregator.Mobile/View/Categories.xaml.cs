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

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556
using Ninject;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.ViewModel;

namespace RSSAgregator.Mobile.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Categories : Page
    {
        public CategoriesViewModel DefaultViewModel { get; set; }
        public string Url;

        public Categories()
        {
            this.InitializeComponent();

            DefaultViewModel = App.Kernel.Get<CategoriesViewModel>();
            DataContext = DefaultViewModel;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Url = e.Parameter as string;

        }

        private void ListViewBase_OnItemClick(object sender, RoutedEventArgs e)
        {
            TextBlock cat = sender as TextBlock;
            if (cat != null) DefaultViewModel.SetNewSource(DefaultViewModel.GetCompleteUrl(Url), DefaultViewModel.GetCatId(cat.Text));
            Frame.GoBack();
        }
    }
}
