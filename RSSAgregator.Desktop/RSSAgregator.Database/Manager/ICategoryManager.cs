using System.Collections.Generic;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public interface ICategoryManager
    {
        void AddCategory(FeedCategory toAdd);
        void RenameModel(int categoryId, string newName);
        void DeleteCategory(FeedCategory toDelete);
        List<FeedCategory> GetAllCategories();
        List<FeedCategory> GetByUserId(string id);
        List<FeedCategory> GetCategoriesNumber(int number = 5);
        FeedCategory GetCategoryById(int id);
    }
}