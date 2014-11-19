﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.Web.Syndication;
using RSSAgregator.Mobile.Model;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Mobile.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<FeedDto.FeedData> _Feeds = new ObservableCollection<FeedDto.FeedData>();
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

        public async Task<FeedDto.FeedData> GetFeedAsync(string feedUriString)
        {
            SyndicationClient client = new SyndicationClient();
            Uri feedUri = new Uri(feedUriString);

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                // This code is executed after RetrieveFeedAsync returns the SyndicationFeed. 
                // Process it and copy the data we want into our FeedData and FeedItem classes. 
                FeedDto.FeedData feedData = new FeedDto.FeedData();

                feedData.Title = feed.Title.Text;
                if (feed.Subtitle != null && feed.Subtitle.Text != null)
                {
                    feedData.Description = feed.Subtitle.Text;
                }
                // Use the date of the latest post as the last updated date. 
                feedData.PubDate = feed.Items[0].PublishedDate.DateTime;

                foreach (SyndicationItem item in feed.Items)
                {
                    FeedDto.FeedItem feedItem = new FeedDto.FeedItem();
                    feedItem.Title = item.Title.Text;
                    feedItem.PubDate = item.PublishedDate.DateTime;
                    feedItem.Author = item.Authors[0].Name.ToString();
                    if (feed.SourceFormat == SyndicationFormat.Atom10)
                    {
                        feedItem.Content = item.Content.Text;
                        feedItem.Link = new Uri(feedUriString + item.Id);
                    }
                    else if (feed.SourceFormat == SyndicationFormat.Rss20)
                    {
                        feedItem.Content = item.Summary.Text;
                        feedItem.Link = item.Links[0].Uri;
                    }
                    feedData.Items.Add(feedItem);
                }
                return feedData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
