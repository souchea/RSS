using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Models;
using RSSAgregator.Shared.Common;

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

        private SourceDTO _currentDto = new SourceDTO();

        private IServiceManager ServiceManager { get; set; }

        public FeedPageViewModel(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            FeedList = new ObservableCollection<FeedDTO>();
        }

        public async Task<bool> GetMoreFeeds()
        {
            List<FeedDTO> newFeed = await ServiceManager.GetFeedsToDateAsync(_currentDto.Id, 10, FeedList.Last().PublishDate.DateTime);
            foreach (FeedDTO t in newFeed)
            {
                FeedList.Add(t);
            }
            if (newFeed.Any())
            {
                return (true);
            }
            return (false);
        }

        public async Task<bool> GetNewFeeds()
        {
            List<FeedDTO> newFeed = await ServiceManager.GetFeedsToDateAsync(_currentDto.Id, 10, FeedList.First().PublishDate.DateTime);
            newFeed.AddRange(FeedList);
            FeedList.Clear();
            foreach (FeedDTO t in newFeed)
            {
                FeedList.Add(t);
            }
            if (newFeed.Any())
            {
                return (true);
            }
            return (false);
        }

        public async void SetFeedList(SourceDTO source)
        {
            FeedName = source.Title;
            _currentDto = source;
            var feeds = await ServiceManager.GetFeedsAsync(source.Id, 20);
            FeedList = new ObservableCollection<FeedDTO>(feeds);
            await ServiceManager.SendReadAsync(source.Id);
        }
    }
}
