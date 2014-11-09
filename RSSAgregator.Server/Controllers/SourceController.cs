using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RSSAgregator.Database.DataContext;
using RSSAgregator.Database.Manager;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Controllers
{
    public class SourceController : ApiController
    {

        [HttpPost]
        public int Add(string url, int id, int catId)
        {
            var newSource = new FeedSource
            {
                CategoryId = catId,
                CreationDate = DateTime.Now,
                Public = true,
                Url = url,
                UserId = id
            };
            SourceManager.AddSource(newSource);

            return newSource.Id;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var toDelete = SourceManager.GetSourceById(id);
            SourceManager.DeleteSource(toDelete);
        }

        [HttpGet]
        public List<FeedItemDTO> GetItems(int id, int nb)
        {
            return new List<FeedItemDTO>();
        }
    }
}
