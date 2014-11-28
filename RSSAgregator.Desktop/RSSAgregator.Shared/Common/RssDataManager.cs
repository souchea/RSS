using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.Common
{
    public class RssDataManager : IDataManager
    {
        #region Dependencies 

        public IStorageManager StorageManager { get; set; }

        public IServiceManager ServiceManager { get; set; }

        #endregion

        #region Properties

        public List<SourceDTO> SourceList { get; set; }

        public List<CategoryDTO> CategoryList { get; set; }

        #endregion

        #region Events

        public event EventHandler SourceChanged;

        public event EventHandler CategoryChanged;

        #endregion

        public RssDataManager(IStorageManager storageManager, IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            StorageManager = storageManager;

            CategoryList = new List<CategoryDTO>();
            SourceList = new List<SourceDTO>();

            SetCategoryList();
        }

        public async void SetCategoryList()
        {
            CategoryList = await StorageManager.GetStoredCategories();
            if (CategoryChanged != null)
            {
                CategoryChanged(this, new EventArgs());
            }

            try
            {
                var onlineCategories = await ServiceManager.GetCategoriesAsync("599de3d2-811f-42fa-8544-a7b0975d3baf");
                //CategoryList = onlineCategories;
                StorageManager.StoreCategories(onlineCategories);
                if (CategoryChanged != null)
                {
                    CategoryChanged(this, new EventArgs());
                }
            }
            catch
            {
            }
            SetSourceList();
        }

        public async void SetSourceList()
        {
            SourceList = await StorageManager.GetStoredSources();
            if (SourceChanged != null)
            {
                SourceChanged(this, new EventArgs());
            }

            try
            {
                SourceList = CategoryList.SelectMany(category => category.Feeds).ToList(); ;
                
                StorageManager.StoreSources(SourceList);
                if (SourceChanged != null)
                {
                    SourceChanged(this, new EventArgs());
                }
            }
            catch
            {

            }
        }
    }
}
