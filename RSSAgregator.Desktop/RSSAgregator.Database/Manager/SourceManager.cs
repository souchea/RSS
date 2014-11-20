using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class SourceManager : ISourceManager
    {
        public void AddSource(FeedSource toAdd)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.FeedSources.Add(toAdd);
                context.SaveChanges();
            }
        }

        public void DeleteSource(FeedSource toDelete)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.FeedSources.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public List<FeedSource> GetAllSources()
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from source in context.FeedSources
                        select source).ToList();
            }
        }

        public List<FeedSource> GetSourcesNumber(int number = 5)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from source in context.FeedSources
                        select source).Take(number).ToList();
            }
        }

        public FeedSource GetSourceById(int id)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from source in context.FeedSources
                        where source.Id == id
                        select source).SingleOrDefault();
            }
        }
    }
}
