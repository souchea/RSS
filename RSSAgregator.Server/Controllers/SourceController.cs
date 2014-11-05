using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RSSAgregator.Server.Controllers
{
    public class SourceController : ApiController
    {
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Add(string url, int catId)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
