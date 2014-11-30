using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Ninject;

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


        private string emailInfo = "Please enter your login";
        private string passwordInfo = "Please enter your password";
        private AppState curAppState = AppState.None;
        private AppState prevAppState = AppState.None;
        private List<AppState> stateChain = new List<AppState> { AppState.None, AppState.Login, AppState.Category, AppState.Flux, AppState.Item, AppState.ItemContent };
        public ViewModel.ViewModelContainer DefaultViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //LoadLists();
            ObservableCollection<CategoryDTO> categories = new ObservableCollection<CategoryDTO>();
            ObservableCollection<SourceDTO> sources;
            DefaultViewModel = App.Kernel.Get<ViewModel.ViewModelContainer>();
            DefaultViewModel.MainPageVM = App.Kernel.Get<MainPageViewModel>();
            DefaultViewModel.LoginPageVM = App.Kernel.Get<LoginPageViewModel>();

            DataContext = DefaultViewModel;
            categories.Add(new CategoryDTO { Id = 0, Feeds = null, Name = "Test"});
            categories.Add(new CategoryDTO { Id = 1, Feeds = null, Name = "Test2" });
        }

        private void SMLogin_Click(object sender, RoutedEventArgs e)
        {
            SubMainGrid.IsEnabled = false;
            Credentials.Visibility = System.Windows.Visibility.Visible;
        }

        private void QuitApp_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //SaveLists();
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
            EMail.Text = String.Empty;
            Password.Password = String.Empty;
            EmailInfo.Content = emailInfo;
            PasswordInfo.Content = passwordInfo;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidEmail(EMail.Text))
            {
                CredentialsInfo.Content = "The email address you provided is not valid";
                CredentialsInfo.Foreground = Brushes.Red;
                return;
            }
            if ((ButtonRegister.IsChecked == true ? DefaultViewModel.LoginPageVM.RegisterAsync(Password.Password) : DefaultViewModel.LoginPageVM.LoginAsync(Password.Password)).Result == true)
            {
                SubMainGrid.IsEnabled = true;
                ButtonReset_Click(null, null);
                Credentials.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                ButtonReset_Click(null, null);
                CredentialsInfo.Foreground = Brushes.Red;
                CredentialsInfo.Content =  "Login failed.";
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonReset_Click(null, null);
            Credentials.Visibility = System.Windows.Visibility.Hidden;
            SubMainGrid.IsEnabled = true;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ChangeState(AppState state)
        {
            switch (state)
            {
                case AppState.None:
                    return false;
                case AppState.Login:
                    SubMainGrid.IsEnabled = false;
                    Credentials.Visibility = System.Windows.Visibility.Visible;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                case AppState.Register:
                    SubMainGrid.IsEnabled = false;
                    Credentials.Visibility = System.Windows.Visibility.Visible;
                    ButtonRegister.IsChecked = true;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                case AppState.Category:
                    CategoryList.Visibility = System.Windows.Visibility.Visible;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    return true;
                default:
                    return false;
            }
        }
        private bool NextState()
        {
            if (curAppState == AppState.ItemContent)
                return false;
            int i;
            if ((i = stateChain.IndexOf(curAppState)) != -1)
                return ChangeState(stateChain[i + 1]);
            if (curAppState == AppState.Register 
                || curAppState == AppState.AddCategory || curAppState == AppState.EditCategory || curAppState == AppState.DelCategory 
                || curAppState == AppState.AddFlux || curAppState == AppState.EditFlux || curAppState == AppState.DelFlux)
                return ChangeState(prevAppState);
            return false;
        }
    }
}
