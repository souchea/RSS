using System.Collections.Generic;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public interface ISourceManager
    {
        void AddSource(FeedSource toAdd);
        void DeleteSource(FeedSource toDelete);
        List<FeedSource> GetAllSources();
        List<FeedSource> GetSourcesNumber(int number = 5);
        FeedSource GetSourceById(int id);
    }
}