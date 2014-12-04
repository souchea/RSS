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
using RSSAgregator.Models;
using RSSAgregator.Server.APIAttribute;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Controllers
{
    //[Authorize]
    public class CategoryController : ApiController
    {
        protected ICategoryManager CategoryManager { get; set; }

        protected ISourceManager SourceManager { get; set; }

        public CategoryController(ICategoryManager categoryManager, ISourceManager sourceManager)
        {
            CategoryManager = categoryManager;
            SourceManager = sourceManager;
        }

        [HttpGet]
        //[Scope("isLogged")]
        public List<CategoryDTO> Get(string id)
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

        [HttpPost]
        //[Scope("isLogged")]
        public int Add(string userId, string param)
        {
            var toCreateCategory = new FeedCategory
            {
                CreationDate = DateTime.Now,
                Name = param,
                Public = true,
                UserId = userId
            };
            CategoryManager.AddCategory(toCreateCategory);

            return toCreateCategory.Id;
        }

        [HttpPut]
        //[Scope("isLogged")]
        public void Rename(int id, string name)
        {
            CategoryManager.RenameModel(id, name);
        }

        [HttpDelete]
        //[Scope("isLogged")]
        public void Delete(int id)
        {
            var categoryToDelete = CategoryManager.GetCategoryById(id);

            foreach (var feedSource in categoryToDelete.FeedSources)
            {
                SourceManager.DeleteSource(feedSource);
            }
            CategoryManager.DeleteCategory(categoryToDelete);
        }

    }
}
