using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class CategoryManager : ICategoryManager
    {
        public void AddCategory(FeedCategory toAdd)
        {
            var context = new RssAgregatorDataContext();

            context.FeedCategories.Add(toAdd);
            context.SaveChanges();
        }

        public void RenameModel(int categoryId, string newName)
        {
            using (var context = new RssAgregatorDataContext())
            {
                var fromDbToUpdate = (from cate in context.FeedCategories
                                      where cate.Id == categoryId
                                      select cate).Single();
                fromDbToUpdate.Name = newName;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(FeedCategory toDelete)
        {
            using (var context = new RssAgregatorDataContext())
            {
                FeedCategory cat = (FeedCategory)context.FeedCategories.Where(b => b.Id == toDelete.Id).First();
                context.FeedCategories.Remove(cat);
                context.SaveChanges();
            }
        }

        public List<FeedCategory> GetAllCategories()
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from category in context.FeedCategories
                        select category).ToList();
            }
        }

        public List<FeedCategory> GetByUserId(string id)
        {
            var context = new RssAgregatorDataContext();

            return (from category in context.FeedCategories
                where category.UserId == id
                select category).ToList();

        }

        public List<FeedCategory> GetCategoriesNumber(int number = 5)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from category in context.FeedCategories
                        select category).Take(number).ToList();
            }
        }

        public FeedCategory GetCategoryById(int id)
        {
            var context = new RssAgregatorDataContext();
            
                return (from category in context.FeedCategories
                        where category.Id == id
                        select category).SingleOrDefault();
            
        }
    }
}
