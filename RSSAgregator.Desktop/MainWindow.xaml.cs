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
using RSSAgregator.Models;
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
                Close();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            EMail.Text = String.Empty;
            Password.Password = String.Empty;
            EmailInfo.Content = emailInfo;
            PasswordInfo.Content = passwordInfo;
            CredentialsInfo.Content = string.Empty;
        }

        private async void ButtonValidate_Click(object sender, RoutedEventArgs e)
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
            CredentialsInfo.Content = (curAppState == AppState.Login? "Please wait...loging you in" : "Please wait...registering your credentials");
            DefaultViewModel.LoginPageVM.Email = "jean-baptiste.lechelon@epitech.eu";
            Password.Password = "Epitech42#";
            if ((ButtonRegister.IsChecked == true ? await DefaultViewModel.LoginPageVM.RegisterAsync(Password.Password) : await DefaultViewModel.LoginPageVM.LoginAsync(Password.Password)))
            {
                ButtonReset_Click(null, null);
                ChangeState(AppState.Category);
            }
            else
            {
                ButtonReset_Click(null, null);
                CredentialsInfo.Content =  (curAppState == AppState.Login ? "Login failed!" : "Registering failed!");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonReset_Click(null, null);
            ChangeState(prevAppState);
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
                #region State None
                case AppState.None:
                    SubMainGrid.IsEnabled = true;
                    Credentials.Visibility = System.Windows.Visibility.Hidden;
                    CategoryList.Visibility = System.Windows.Visibility.Hidden;
                    SourceList.Visibility = System.Windows.Visibility.Hidden;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    FeedContent.Visibility = System.Windows.Visibility.Hidden;
                    CategoryChange.Visibility = System.Windows.Visibility.Hidden;
                    NavPrevious.Visibility = System.Windows.Visibility.Hidden;
                    NavNext.Visibility = System.Windows.Visibility.Hidden;
                    MLoad.IsEnabled = false;
                    MGroups.IsEnabled = false;
                    MFeeds.IsEnabled = false;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State Login
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
                #endregion
                #region State Register
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
                #endregion
                #region State Category
                case AppState.Category:
                    if (curAppState == AppState.Login || curAppState == AppState.Register)
                    {
                        Credentials.Visibility = System.Windows.Visibility.Hidden;
                    }
                    SubMainGrid.IsEnabled = true;
                    CategoryChange.Visibility = System.Windows.Visibility.Hidden;
                    NavPrevious.Visibility = System.Windows.Visibility.Visible;
                    NavNext.Visibility = System.Windows.Visibility.Visible;
                    CategoryList.Visibility = System.Windows.Visibility.Visible;
                    SourceList.Visibility = System.Windows.Visibility.Hidden;
                    FeedContent.Visibility = System.Windows.Visibility.Visible;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    FeedUri.Visibility = System.Windows.Visibility.Hidden;
                    NavPrevious.Background = Brushes.DarkBlue;
                    NavNext.Background = Brushes.DarkBlue;
                    MLoad.IsEnabled = false;
                    MGroups.IsEnabled = true;
                    MFeeds.IsEnabled = false;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State Flux
                case AppState.Flux:
                    if (curAppState == AppState.Login || curAppState == AppState.Register)
                        Credentials.Visibility = System.Windows.Visibility.Hidden;
                    SubMainGrid.IsEnabled = true;
                    CategoryChange.Visibility = System.Windows.Visibility.Hidden;
                    NavPrevious.Visibility = System.Windows.Visibility.Visible;
                    NavPrevious.Background = Brushes.Green;
                    NavNext.Visibility = System.Windows.Visibility.Visible;
                    NavNext.Background = Brushes.Green;
                    CategoryList.Visibility = System.Windows.Visibility.Hidden;
                    SourceList.Visibility = System.Windows.Visibility.Visible;
                    FeedContent.Visibility = System.Windows.Visibility.Visible;
                    FeedList.Visibility = System.Windows.Visibility.Hidden;
                    FeedUri.Visibility = System.Windows.Visibility.Hidden;
                    MLoad.IsEnabled = false;
                    MGroups.IsEnabled = false;
                    MFeeds.IsEnabled = true;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State Item
                case AppState.Item:
                    if (curAppState == AppState.Login || curAppState == AppState.Register)
                    {
                        Credentials.Visibility = System.Windows.Visibility.Hidden;
                    }
                    SubMainGrid.IsEnabled = true;
                    CategoryChange.Visibility = System.Windows.Visibility.Hidden;
                    NavPrevious.Visibility = System.Windows.Visibility.Visible;
                    NavNext.Visibility = System.Windows.Visibility.Visible;
                    NavPrevious.Background = Brushes.DarkBlue;
                    NavNext.Background = Brushes.DarkBlue;
                    CategoryList.Visibility = System.Windows.Visibility.Hidden;
                    SourceList.Visibility = System.Windows.Visibility.Visible;
                    FeedContent.Visibility = System.Windows.Visibility.Visible;
                    FeedList.Visibility = System.Windows.Visibility.Visible;
                    FeedUri.Visibility = System.Windows.Visibility.Visible;
                    MLoad.IsEnabled = false;
                    MGroups.IsEnabled = false;
                    MFeeds.IsEnabled = false;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State ItemContent
                case AppState.ItemContent:
                    MLoad.IsEnabled = true;
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State AddCategory
                case AppState.AddCategory:
                    SubMainGrid.IsEnabled = false;
                    CategoryChange.Visibility = System.Windows.Visibility.Visible;
                    CategoryChangeTitle.Content = "Add Category";
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State EditCategory
                case AppState.EditCategory:
                    SubMainGrid.IsEnabled = false;
                    CategoryChange.Visibility = System.Windows.Visibility.Visible;
                    CategoryChangeTitle.Content = "Edit Category";
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State DelCategory
                case AppState.DelCategory:
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State AddFlux
                case AppState.AddFlux:
                    SubMainGrid.IsEnabled = false;
                    CategoryChange.Visibility = System.Windows.Visibility.Visible;
                    CategoryChangeTitle.Content = "Add Source";
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
                #region State DelFlux
                case AppState.DelFlux:
                    prevAppState = curAppState;
                    curAppState = state;
                    return true;
                #endregion
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

        private bool PreviousState()
        {
            if (curAppState == AppState.None)
                return false;
            int i;
            if ((i = stateChain.IndexOf(curAppState)) > 0)
                return ChangeState(stateChain[i - 1]);
            if (curAppState == AppState.Register
                || curAppState == AppState.AddCategory || curAppState == AppState.EditCategory || curAppState == AppState.DelCategory
                || curAppState == AppState.AddFlux || curAppState == AppState.EditFlux || curAppState == AppState.DelFlux)
                return ChangeState(prevAppState);
            return false;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonRegister.IsChecked == true)
                ChangeState(AppState.Register);
            else
                ChangeState(AppState.Login);
        }

        private void SBAddGroup_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(AppState.AddCategory);
        }

        private void SBDelGroup_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(AppState.DelCategory);
            List<CategoryDTO> lst = new List<CategoryDTO>();
            foreach (CategoryDTO c in CategoryList.SelectedItems)
            {
                lst.Add(c);
            }
            DefaultViewModel.MainPageVM.DeleteCategories(lst);
            PreviousState();
        }

        private void SBAddFeed_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(AppState.AddFlux);
        }

        private void SBDelFeed_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(AppState.DelFlux);
            List<SourceDTO> lst = new List<SourceDTO>();
            foreach (SourceDTO c in SourceList.SelectedItems)
            {
                lst.Add(c);
            }
            DefaultViewModel.SourcePageVM.DeleteSources(lst);
            PreviousState();
        }

        private void RegisterButtonsArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ButtonValidate_Click(null, null);
        }

        private void CategoryChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CategoryChangeValidate_Click(null, null);
        }

        private void CategoryChangeValidate_Click(object sender, RoutedEventArgs e)
        {
            if (curAppState == AppState.AddCategory)
            {
                DefaultViewModel.MainPageVM.AddCategory();
            }
            else if (curAppState == AppState.AddFlux)
            {
                CategoryDTO c = CategoryList.SelectedItem as CategoryDTO;
                DefaultViewModel.CategoryPageVM.SetNewSource(CategoryName.Text, c.Id);
            }
            DefaultViewModel.MainPageVM.RefreshSourceList();
            PreviousState();
        }

        private void CategoryChangeCancel_Click(object sender, RoutedEventArgs e)
        {
            DefaultViewModel.MainPageVM.ToAddCategoryText = string.Empty;
            ChangeState(prevAppState);
        }

        private void NavPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (curAppState == AppState.ItemContent)
                PreviousState();
            PreviousState();
        }

        private void NavNext_Click(object sender, RoutedEventArgs e)
        {
            if (curAppState == AppState.Category && CategoryList.SelectedItem != null)
            {
                CategoryDTO c = CategoryList.SelectedItem as CategoryDTO;
                DefaultViewModel.SourcePageVM.SetCategoryList(c.Name);
                NextState();
            }
            else if (curAppState == AppState.Flux && SourceList.SelectedItem != null)
            {
                DefaultViewModel.FeedPageVM.SetFeedList(SourceList.SelectedItem as SourceDTO);
                NextState();
            }
            else if ((curAppState == AppState.Item || curAppState == AppState.ItemContent)&& FeedList.SelectedItem != null)
            {
                FeedDTO f = FeedList.SelectedItem as FeedDTO;
                FeedBody.Content = f.Content;
                FeedTitle.Content = f.Title;
                FeedDate.Content = "Publishing date : " + f.PublishDate.DateTime;
                NextState();
            }
        }

        private void SourceChangeValidate_Click(object sender, RoutedEventArgs e)
        {
            NextState();
        }

        private void SourceChangeCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FeedUri_Click(object sender, RoutedEventArgs e)
        {
            if (FeedList.SelectedItem != null && curAppState == AppState.ItemContent)
            {
                FeedDTO f = FeedList.SelectedItem as FeedDTO;
                System.Diagnostics.Process.Start(f.Id);
            }
        }

        private void MLoad_Click(object sender, RoutedEventArgs e)
        {
            DefaultViewModel.FeedPageVM.GetMoreFeeds();
        }

        private void FeedList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavNext_Click(null, null);
        }
    }
}
