using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class CategoryManager
    {
        public static void AddCategory(FeedCategory toAdd)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.FeedCategories.Add(toAdd);
                context.SaveChanges();
            }
        }

        public static void RenameModel(int categoryId, string newName)
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

        public static void DeleteCategory(FeedCategory toDelete)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.FeedCategories.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public static List<FeedCategory> GetAllCategories()
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from category in context.FeedCategories
                        select category).ToList();
            }
        }

        public static List<FeedCategory> GetByUserId(int id)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from category in context.FeedCategories
                        where category.UserId == id
                        select category).ToList();
            }
        }

        public static List<FeedCategory> GetCategoriesNumber(int number = 5)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from category in context.FeedCategories
                        select category).Take(number).ToList();
            }
        }

        public static FeedCategory GetCategoryById(int id)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from category in context.FeedCategories
                        where category.Id == id
                        select category).SingleOrDefault();
            }
        }
    }
}
