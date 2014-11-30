using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Model.RSSAgregator.Shared.Model;

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
            FeedSummary = feed.Summary;
            FeedDate = feed.PublishDate.DateTime.ToString();
            SaveFeed(feed);
        }

        private async void SaveFeed(FeedDTO feed)
        {
            
        }
    }
}
