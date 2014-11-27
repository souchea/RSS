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
    [Authorize]
    public class CategoryController : ApiController
    {
        [HttpGet]
        public List<CategoryDTO> Get(int id)
        {
            var categories = CategoryManager.GetByUserId(id);
            return (from category in categories
                let sourceList = category.FeedSources.Select(source => new SourceDTO
                {
                    Id = source.Id,
                    // todo rajouter un champ Title a la source
                    Title = "title"
                }).ToList()
                select new CategoryDTO
                {
                    Id = category.Id, Feeds = sourceList
                }).ToList();
        }

        [HttpPost]
        public int Add(int userId, string param)
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
        public void Rename(int id, string name)
        {
            CategoryManager.RenameModel(id, name);
        }

        [HttpDelete]
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
