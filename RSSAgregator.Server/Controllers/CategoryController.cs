using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RSSAgregator.Database.DataContext;
using RSSAgregator.Database.Manager;

namespace RSSAgregator.Server.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public int Add(int userId, string name)
        {
            CategoryManager.AddCategory(new FeedCategory
            {
                CreationDate = DateTime.Now,
                Name = name,
                Public = true,
                UserId = userId
            });

            return 1;
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }

        [HttpDelete]
        public void DeleteAll(int id)
        {
            
        }

    }
}
