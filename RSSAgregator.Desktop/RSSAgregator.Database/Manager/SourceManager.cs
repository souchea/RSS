using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class SourceManager : ISourceManager
    {
        private RssAgregatorDataContext Context { get; set; }

        public SourceManager()
        {
            Context = new RssAgregatorDataContext();
        }

        ~SourceManager()
        {
            Context.Dispose();
        }

        public void AddSource(FeedSource toAdd)
        {
            try
            {
                Context.FeedSources.Add(toAdd);
                Context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteSource(FeedSource toDelete)
        {
            Context.FeedSources.Remove(toDelete);
            Context.SaveChanges();
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
            return (from source in Context.FeedSources
                select source).Take(number).ToList();

        }

        public FeedSource GetSourceById(int id)
        {
            return (from source in Context.FeedSources
                where source.Id == id
                select source).SingleOrDefault();
        }

        public void ReadSource(int id)
        {
            var newSource = (from source in Context.FeedSources
                where source.Id == id
                select source).SingleOrDefault();
            newSource.ViewedNumber += 1;
            Context.SaveChanges();
        }

        public void ChangeState(int id, string newState)
        {
            var newSource = (from source in Context.FeedSources
                where source.Id == id
                select source).SingleOrDefault();
            newSource.ViewState = newState;
            Context.SaveChanges();
        }

        public void ChangeName(int id, string newName)
        {
            var newSource = (from source in Context.FeedSources
                where source.Id == id
                select source).SingleOrDefault();
            newSource.Title = newName;
            Context.SaveChanges();
        }
    }
}
