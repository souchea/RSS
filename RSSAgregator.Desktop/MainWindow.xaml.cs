using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.ViewModel;

namespace RSSAgregator.Desktop
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum AppState
        {
            None,
            Login,
            Register,
            Category,
            Flux,
            Item,
            ItemContent,
            AddFlux,
            DelFlux,
            EditFlux,
            AddCategory,
            DelCategory,
            EditCategory
        }

        public ViewModel.ViewModelContainer DefaultViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<CategoryDTO> categories = new ObservableCollection<CategoryDTO>();
            ObservableCollection<SourceDTO> sources;
            categories.Add(new CategoryDTO { Id = 0, Feeds = null, Name = "Test"});
            categories.Add(new CategoryDTO { Id = 1, Feeds = null, Name = "Test2" });
        }

        private void SMLogin_Click(object sender, RoutedEventArgs e)
        {
            SubMainGrid.IsEnabled = false;
            Credentials.Background = Brushes.Green;

            Credentials.Visibility = System.Windows.Visibility.Visible;
        }

        private void QuitApp_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Close();
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t.Foreground == Brushes.Gray)
            {
                t.Foreground = Brushes.Black;
                t.Text = String.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t.Text == String.Empty)
            {
                string defaultText;
                if (t.Name.ToString() == "EMail")
                {
                    defaultText = "Please enter your e-mail here";
                }
                else
                    defaultText = "Please enter your password here";
                t.Foreground = Brushes.Gray;
                t.Text = defaultText;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            EMail.Text = "Please enter your e-mail here";
            EMail.Foreground = Brushes.Gray;
            Password.Text = "Please enter your password here";
            Password.Foreground = Brushes.Gray;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                SubMainGrid.IsEnabled = true;
                ButtonReset_Click(null, null);
                Credentials.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                ButtonReset_Click(null, null);
                CredentialsInfo.Foreground = Brushes.Red;
                CredentialsInfo.Content = "Login failed.";
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonReset_Click(null, null);
            Credentials.Visibility = System.Windows.Visibility.Hidden;
            SubMainGrid.IsEnabled = true;
        }
    }
}
