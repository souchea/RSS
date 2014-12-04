using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Http;
using System.Xml;
using RSSAgregator.Database.DataContext;
using RSSAgregator.Database.Manager;
using RSSAgregator.Models;

namespace RSSAgregator.Server.Controllers
{
    //[Authorize]
    public class SourceController : ApiController
    {
        protected ISourceManager SourceManager { get; set; }

        protected ICategoryManager CategoryManager { get; set; }

        public SourceController(ISourceManager sourceManager, ICategoryManager categoryManager)
        {
            SourceManager = sourceManager;
            CategoryManager = categoryManager;
        }
        
        [HttpPost]
        //[Scope("isLogged")]
        public int Add(string id, [FromUri]string url, [FromUri]int catId)
        {
            var newSource = new FeedSource
            {
                CategoryId = catId,
                CreationDate = DateTime.Now,
                Public = true,
                Url = url,
                UserId = id,
                Title = GetTitleFromUrl(url),
                ViewedNumber = 0,
                ViewState = "none"
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
        //[Scope("isLogged")]
        public void Delete(int id)
        {
            var toDelete = SourceManager.GetSourceById(id);
            SourceManager.DeleteSource(toDelete);
        }

        [HttpGet]
        //[Scope("isLogged")]
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

            return feedList.Take(nb);
        }

        [HttpGet]
        //[Scope("isLogged")]
        public IEnumerable<FeedItemDTO> GetItemsFromDate(int id, int year, int month, int day, int hour, int minute)
        {
            var source = SourceManager.GetSourceById(id);
            XmlReader reader = XmlReader.Create(source.Url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            var feedList = new List<FeedItemDTO>();

            var feedItems = (from feedItemDto in feed.Items
                where feedItemDto.PublishDate.Date > new DateTime(year, month, day, hour, minute, 0)
                select feedItemDto); 

            foreach (var feedItem in feedItems)
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

        [HttpGet]
        //[Scope("isLogged")]
        public IEnumerable<FeedItemDTO> GetItemsToDate(int id, int nb, int year, int month, int day, int hour, int minute)
        {
            var source = SourceManager.GetSourceById(id);
            XmlReader reader = XmlReader.Create(source.Url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            var feedList = new List<FeedItemDTO>();

            var feedItems = (from feedItemDto in feed.Items
                where feedItemDto.PublishDate.Date < new DateTime(year, month, day, hour, minute, 0)
                orderby feedItemDto.PublishDate descending 
                 select feedItemDto);

            foreach (var feedItem in feedItems)
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

            return feedList.Take(nb);
        }

        [HttpGet]
        //[Scope("isLogged")]
        public void Read(int id)
        {
            SourceManager.ReadSource(id);
        }

        [HttpPost]
        //[Scope("isLogged")]
        public void SetState(int id, string state)
        {
            var source = SourceManager.GetSourceById(id);
        }
    }
}
