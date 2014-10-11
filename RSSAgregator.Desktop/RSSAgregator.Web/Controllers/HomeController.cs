using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSSAgregator.Web.Models;

namespace RSSAgregator.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: categories
        [Route("categories")]
        public ActionResult GetCategories()
        {
            //return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet );

            var list = new List<Category>
            {
                new Category("Category1", 23),
                new Category("Category2", 13),
                new Category("Category3", 43),
                new Category("Category4", 3),
                
            };
            return PartialView("Categories", list);

        }

        // GET: channels
        [Route("{category}/channels")]
        public ActionResult GetChannels(string category)
        {
            //return Json(new { foo = category, baz = "Blech" }, JsonRequestBehavior.AllowGet );

            var list = new List<Channel>
            {
                new Channel("Channel1", "ma petite description"),
                new Channel("Channel2", "ma petite description"),
                new Channel("Channel3", "ma petite description"),
                new Channel("Channel4", "ma petite description"),
                
            };

            return PartialView("Channels", list);

        }


        // GET: items
        [Route("{channel}/items")]
        public ActionResult GetItems(string channel)
        {
            //return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet );

            return PartialView("Items");

        }



    }

}