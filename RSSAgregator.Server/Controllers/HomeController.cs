using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using RSSAgregator.Database.DataContext;
using RSSAgregator.Database.Manager;
using RSSAgregator.Models;
using RSSAgregator.Server.APIAttribute;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        protected ICategoryManager CategoryManager { get; set; }
        protected ISourceManager SourceManager { get; set; }

        public HomeController(ICategoryManager categoryManager, ISourceManager sourceManager)
        {
            CategoryManager = categoryManager;
            SourceManager = sourceManager;
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserCategories = GetCategoriesByUserID(User.Identity.GetUserId());

            }

            return View();
        }

        public List<CategoryDTO> GetCategoriesByUserID(string id)
        {
            var categories = CategoryManager.GetByUserId(id);
            return (from category in categories
                    let sourceList = category.FeedSources.Select(source =>
                    new SourceDTO
                    {
                        Id = source.Id,
                        Title = source.Title,
                        ViewedNumber = source.ViewedNumber
                    }).ToList()
                    select new CategoryDTO
                    {
                        Id = category.Id,
                        Feeds = sourceList,
                        Name = category.Name
                    }).ToList();
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Team members";
            ViewBag.TeamMembers = new List<string>() { "Aurelien (souche_a)", "Flavien (di-bel_f)", "Jean-Baptiste (lechel_j)", "Thomas (tomase_n)", "Steeve (pommie_b)" };
            return View();
        }
    }
}