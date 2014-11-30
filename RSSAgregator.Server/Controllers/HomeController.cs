using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSSAgregator.Server.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Team members";
            ViewBag.TeamMembers = new List<string>() { "Aurelien", "lavien", "Jean-Baptiste", "Thomas", "Steeve" };
            return View();
        }
    }
}