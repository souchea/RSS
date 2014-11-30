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
            DefaultViewModel = App.Kernel.Get<ViewModel.ViewModelContainer>();
            DefaultViewModel.MainPageVM = App.Kernel.Get<MainPageViewModel>();
            DefaultViewModel.LoginPageVM = App.Kernel.Get<LoginPageViewModel>();
            DefaultViewModel.CategoryPageVM = App.Kernel.Get<CategoriesViewModel>();
            DefaultViewModel.SourcePageVM = App.Kernel.Get<SourcePageViewModel>();
            DefaultViewModel.FeedPageVM = App.Kernel.Get<FeedPageViewModel>();
            DefaultViewModel.FeedInfoVM = App.Kernel.Get<FeedViewerPageViewModel>();
            DataContext = DefaultViewModel;

            ChangeState(AppState.None);
        }

        private void SMLogin_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(AppState.Login);
        }

        private void QuitApp_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //SaveData();
                Close();
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            EMail.Text = String.Empty;
            Password.Password = String.Empty;
            EmailInfo.Content = emailInfo;
            PasswordInfo.Content = passwordInfo;
            CredentialsInfo.Content = string.Empty;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (!IsValidEmail(EMail.Text))
            {
                CredentialsInfo.Content = "The email address you provided is not valid";
                check = false;
            }
            else if (EMail.Text == string.Empty)
            {
                CredentialsInfo.Content = "You must enter an email address in order to authenticate";
                check = false;
            }
            else if (Password.Password == string.Empty)
            {
                CredentialsInfo.Content = "You must enter a password in order to authenticate";
                check = false;
            }
            if (!check)
                return;
            CredentialsInfo.Content = "Please wait...loging you in";
            DefaultViewModel.LoginPageVM.Email = "jean-baptiste.lechelon@epitech.eu";
            Password.Password = "Epitech42#";
            if ((ButtonRegister.IsChecked == true ? DefaultViewModel.LoginPageVM.RegisterAsync(Password.Password) : DefaultViewModel.LoginPageVM.LoginAsync(Password.Password)).Result == true)
            {
                ButtonReset_Click(null, null);
                ChangeState(AppState.Category);
            }
            else
            {
                ButtonReset_Click(null, null);
                CredentialsInfo.Content =  "Login failed.";
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonReset_Click(null, null);
            ChangeState(AppState.None);
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
                    SubMainGrid.IsEnabled = true;
                    Credentials.Visibility = System.Windows.Visibility.Hidden;
                    CategoryList.Visibility = System.Windows.Visibility.Hidden;
                    SourceList.Visibility = System.Windows.Visibility.Hidden;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    FeedContent.Visibility = System.Windows.Visibility.Hidden;
                    GoToPrevious.Visibility = System.Windows.Visibility.Hidden;
                    MGroups.IsEnabled = false;
                    MFeeds.IsEnabled = false;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                case AppState.Login:
                    CredentialsTitle.Content = "Authenticate";
                    if (curAppState != AppState.Register)
                    {
                        prevAppState = curAppState;
                        Credentials.Visibility = System.Windows.Visibility.Visible;
                        SubMainGrid.IsEnabled = false;
                    }
                    else
                        ButtonRegister.IsChecked = false;
                    curAppState = state;
                    return true;
                case AppState.Register:
                    CredentialsTitle.Content = "Register";
                    if (curAppState != AppState.Login)
                    {
                        SubMainGrid.IsEnabled = false;
                        Credentials.Visibility = System.Windows.Visibility.Visible;
                    }
                    ButtonRegister.IsChecked = true;
                    curAppState = state;
                    return true;
                case AppState.Category:
                    if (curAppState == AppState.Login || curAppState == AppState.Register)
                    {
                        SubMainGrid.IsEnabled = true;
                        Credentials.Visibility = System.Windows.Visibility.Hidden;
                        MGroups.IsEnabled = true;
                        MFeeds.IsEnabled = true;
                    }
                    CategoryList.Visibility = System.Windows.Visibility.Visible;
                    SourceList.Visibility = System.Windows.Visibility.Hidden;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    GoToPrevious.Visibility = System.Windows.Visibility.Hidden;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                case AppState.Flux:
                    if (curAppState == AppState.Login || curAppState == AppState.Register)
                    {
                        SubMainGrid.IsEnabled = true;
                        Credentials.Visibility = System.Windows.Visibility.Hidden;
                        MGroups.IsEnabled = true;
                        MFeeds.IsEnabled = true;
                    }
                    CategoryList.Visibility = System.Windows.Visibility.Hidden;
                    SourceList.Visibility = System.Windows.Visibility.Visible;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    GoToPrevious.Visibility = System.Windows.Visibility.Visible;
                    GoToPrevious.Content = "Return to categories list";
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                case AppState.Item:
                    if (curAppState == AppState.Login || curAppState == AppState.Register)
                    {
                        SubMainGrid.IsEnabled = true;
                        Credentials.Visibility = System.Windows.Visibility.Hidden;
                        MGroups.IsEnabled = true;
                        MFeeds.IsEnabled = true;
                    }
                    CategoryList.Visibility = System.Windows.Visibility.Hidden;
                    SourceList.Visibility = System.Windows.Visibility.Visible;
                    FeedList.Visibility = System.Windows.Visibility.Visible;
                    GoToPrevious.Visibility = System.Windows.Visibility.Visible;
                    GoToPrevious.Content = "Return to sources list";
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                case AppState.AddCategory:
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

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonRegister.IsChecked == true)
            {
                ChangeState(AppState.Register);
                curAppState = AppState.Register;
            }
            else
            {
                ChangeState(AppState.Login);
                curAppState = AppState.Login;
            }
        }

        private void SBAddGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SBDelGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SBEditGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SBAddFeed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SBDelFeed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SBEditFeed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeState(AppState.Category);
            List<CategoryDTO> items = CategoryList.SelectedItems as List<CategoryDTO>;
            if (items.Count > 0)
            {
                //    DefaultViewModel.CategoryPageVM.SetNewSource(DefaultViewModel.CategoryPageVM.GetCompleteUrl(Url), item.Id);
            }
        }

        private void SourceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void FeedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
