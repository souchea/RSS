using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Models;

namespace RSSAgregator.Shared.ViewModel
{
    public class FeedViewerPageViewModel : BaseViewModel
    {
        private string _feedName;

        public string FeedName
        {
            get { return _feedName; }
            set
            {
                _feedName = value;
                NotifyPropertyChanged("FeedName");
            }
        }

        private string _feedSummary;

        public string FeedSummary
        {
            get { return _feedSummary; }
            set
            {
                _feedSummary = value;
                NotifyPropertyChanged("FeedSummary");
            }
        }

        private string _feedDate;

        public string FeedDate
        {
            get { return _feedDate; }
            set
            {
                _feedDate = value;
                NotifyPropertyChanged("FeedDate");
            }
        }

        public async void SetViewerPage(FeedDTO feed)
        {
            FeedName = feed.Title;
            FeedDate = feed.PublishDate.DateTime.ToString();
            FeedSummary = feed.Summary;

            SaveFeed(feed);
        }

        private async void SaveFeed(FeedDTO feed)
        {

        }
    }
}
