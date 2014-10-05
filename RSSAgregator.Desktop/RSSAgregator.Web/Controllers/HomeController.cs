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

        // GET: json categories
        [Route("categories")]
        public ActionResult GetCategories()
        {
            return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet );
        }

    }

}