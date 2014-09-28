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
            
        }

        public static void DeleteSource(FeedSource toDelete)
        {
            
        }

        public static List<FeedSource> GetAllSources()
        {
            return new List<FeedSource>();
        }

        public static List<FeedSource> GetSourcesNumber(int number)
        {
            return new List<FeedSource>();
        }

        public static FeedSource GetSourceById(int id)
        {
            return new FeedSource();
        }
    }
}
