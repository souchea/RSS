using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class SourceManager
    {
        public static void AddSource(FeedSource toAdd)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.FeedSources.Add(toAdd);
                context.SaveChanges();
            }
        }

        public static void DeleteSource(FeedSource toDelete)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.FeedSources.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public static List<FeedSource> GetAllSources()
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from source in context.FeedSources
                        select source).ToList();
            }
        }

        public static List<FeedSource> GetSourcesNumber(int number = 5)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from source in context.FeedSources
                        select source).Take(number).ToList();
            }
        }

        public static FeedSource GetSourceById(int id)
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
