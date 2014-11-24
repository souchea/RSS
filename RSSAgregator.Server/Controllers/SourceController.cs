using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Web.Http;
using System.Xml;
using RSSAgregator.Database.DataContext;
using RSSAgregator.Database.Manager;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Controllers
{
    public class SourceController : ApiController
    {
        protected ISourceManager SourceManager { get; set; }

        public SourceController(ISourceManager sourceManager)
        {
            SourceManager = sourceManager;
        }

        [HttpPost]
        public int Add(int id, string url, int catId)
        {
            var newSource = new FeedSource
            {
                CategoryId = catId,
                CreationDate = DateTime.Now,
                Public = true,
                Url = url,
                UserId = id,
                Title = GetTitleFromUrl(url)
            };
            SourceManager.AddSource(newSource);

            return newSource.Id;
        }

        public string GetTitleFromUrl(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed.Title.Text;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var toDelete = SourceManager.GetSourceById(id);
            SourceManager.DeleteSource(toDelete);
        }

        [HttpGet]
        public IEnumerable<SyndicationItem> GetItems(int id, int nb)
        {
            var source = SourceManager.GetSourceById(id);
            XmlReader reader = XmlReader.Create(source.Url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            // todo: voir si je renvois un SyndicationItem ou un DTO
            //foreach (SyndicationItem item in feed.Items)
            //{
            //    String subject = item..Text;    
            //    String summary = item.Summary.Text;  
            //}

            return feed.Items;
        }
    }
}
