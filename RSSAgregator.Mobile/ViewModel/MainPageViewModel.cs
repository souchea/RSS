using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.Web.Syndication;
using RSSAgregator.Mobile.Common;
using RSSAgregator.Mobile.Model;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Mobile.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<FeedDto.FeedData> _Feeds = new ObservableCollection<FeedDto.FeedData>();

        private IStorageManager StorageManager { get; set; }

        public ObservableCollection<FeedDto.FeedData> Feeds
        {
            get
            {
                return this._Feeds;
            }
        }

        private string _account_ButtonText;

        public string Account_ButtonText
        {
            get { return _account_ButtonText; }
            set
            {
                _account_ButtonText = value;
                NotifyPropertyChanged("Account_ButtonText");
            }
        }

        private List<FeedDto.FeedItem> _lastFeed;

        public List<FeedDto.FeedItem> LastFeed
        {
            get { return _lastFeed; }
            set
            {
                _lastFeed = value;
                NotifyPropertyChanged("LastFeed");
            }
        }

        private List<CategoryDTO> _categoryList;

        public List<CategoryDTO> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                NotifyPropertyChanged("CategoryList");
            }
        }

        public MainPageViewModel()
        {
            SetCategoryList();
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            _account_ButtonText = "Login";

            if (localSettings.Values.Count > 0)
            {
                FeedDto.FeedData feeds = new FeedDto.FeedData();

                for (int i = 0; i < localSettings.Values.Count; i++)
                {
                    feeds = localSettings.Values.Values as FeedDto.FeedData;
                    for (int j = 0; j < feeds.Items.Count; j++)
                    {
                        _lastFeed.Add(feeds.Items[j]);
                    }
                }
            }
        }


        public async void SetCategoryList()
        {
            var service = new WebApiServiceManager();

            var result = await service.GetCategoriesAsync(3);
            var test = 4;

        }
    }
}
