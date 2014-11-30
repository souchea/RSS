using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.Model.RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.ViewModel
{
    public class FeedPageViewModel : BaseViewModel
    {
        private ObservableCollection<FeedDTO> _feedList;

        public ObservableCollection<FeedDTO> FeedList
        {
            get { return _feedList; }
            set
            {
                _feedList = value;
                NotifyPropertyChanged("FeedList");
            }
        }

        private string _feedUpdate;

        public string FeedUpdate
        {
            get { return _feedUpdate; }
            set
            {
                _feedUpdate = value;
                NotifyPropertyChanged("FeedUpdate");
            }
        }

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

        private IServiceManager ServiceManager { get; set; }

        public FeedPageViewModel(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            FeedList = new ObservableCollection<FeedDTO>();
        }

        public List<FeedDTO> GetMoreFeeds()
        {
            List<FeedDTO> newFeeds = new List<FeedDTO>();
           // newFeeds = ServiceManager.GetOldFeeds(FeedList[FeedList.Count - 1].PublishDate, 10);
            return (newFeeds);
        }

        public async void SetFeedList(SourceDTO source)
        {
            FeedName = source.Title;
            var feeds = await ServiceManager.GetFeedsAsync(source.Id, 10);
            FeedList = new ObservableCollection<FeedDTO>(feeds);
        }
    }
}
