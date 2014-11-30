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
        public IEnumerable<FeedItemDTO> GetItems(int id, int nb)
        {
            var source = SourceManager.GetSourceById(id);
            XmlReader reader = XmlReader.Create(source.Url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            var feedList = new List<FeedItemDTO>();
                
            foreach (var feedItem in feed.Items)
            {
                feedList.Add(new FeedItemDTO
                {
                    Id = feedItem.Id ?? "",
                    BaseUri = feedItem.BaseUri ?? new Uri("http://www.test.com"),
                    Content = "",
                    PublishDate = feedItem.PublishDate,
                    Summary = feedItem.Summary != null ? feedItem.Summary.Text : "",
                    Title = feedItem.Title != null ? feedItem.Title.Text : ""
                });
            }

            return feedList;
        }

        [HttpPost]
        public void Read(int id)
        {
            var source = SourceManager.GetSourceById(id);

            // ajoute 1 view a la source
        }

        [HttpPost]
        public void SetState(int id)
        {
            var source = SourceManager.GetSourceById(id);

            // set le viewState de la source
        }



    }
}
