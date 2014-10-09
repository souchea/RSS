using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSSAgregator.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Route("home")]
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: categories
        [Route("categories")]
        public ActionResult GetCategories()
        {
            //return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet );

            return PartialView("Categories");

        }

        // GET: channels
        [Route("{category}/channels")]
        public ActionResult GetChannels(string category)
        {
            //return Json(new { foo = category, baz = "Blech" }, JsonRequestBehavior.AllowGet );


            return PartialView("Channels");

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