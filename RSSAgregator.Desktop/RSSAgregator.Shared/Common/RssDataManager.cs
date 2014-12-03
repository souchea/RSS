using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using RSSAgregator.Models;

namespace RSSAgregator.Shared.Common
{
    public class RssDataManager : IDataManager
    {
        #region Dependencies 

        public IStorageManager StorageManager { get; set; }

        public IServiceManager ServiceManager { get; set; }

        public ILoginManager LoginManager { get; set; }

        #endregion

        #region Properties

        public List<SourceDTO> SourceList { get; set; }

        public List<CategoryDTO> CategoryList { get; set; }

        #endregion

        #region Events

        public event EventHandler SourceChanged;

        public event EventHandler CategoryChanged;

        #endregion

        public RssDataManager(IStorageManager storageManager, IServiceManager serviceManager, ILoginManager loginManager)
        {
            ServiceManager = serviceManager;
            StorageManager = storageManager;
            LoginManager = loginManager;

            CategoryList = new List<CategoryDTO>();
            SourceList = new List<SourceDTO>();

            LoginManager.UserChanged += SetCategoryList;

            SetCategoryList();
        }

        public async void SetCategoryList(object sender, EventArgs eventArgs)
        {
            var cat = await StorageManager.GetStoredCategories(LoginManager.UserId);
            if (cat != null)
                CategoryList = cat;
            if (CategoryChanged != null)
            {
                CategoryChanged(this, new EventArgs());
            }

            try
            {
                var onlineCategories = await ServiceManager.GetCategoriesAsync(LoginManager.UserId);
                CategoryList = onlineCategories;
                StorageManager.StoreCategories(LoginManager.UserId, onlineCategories);
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

        public void SetCategoryList()
        {
            SetCategoryList(null, null);
        }

        public async void SetSourceList()
        {
            SourceList = await StorageManager.GetStoredSources(LoginManager.UserId);
            if (SourceChanged != null)
            {
                SourceChanged(this, new EventArgs());
            }

            try
            {
                SourceList = CategoryList.SelectMany(category => category.Feeds).ToList(); ;
                
                StorageManager.StoreSources(LoginManager.UserId, SourceList);
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
