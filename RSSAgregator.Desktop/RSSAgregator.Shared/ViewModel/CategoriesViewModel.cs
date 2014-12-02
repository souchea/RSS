using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.ViewModel
{
    public class CategoriesViewModel : BaseViewModel
    {
        private ObservableCollection<CategoryDTO> _categoryList;

        public ObservableCollection<CategoryDTO> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                NotifyPropertyChanged("CategoryList");
            }
        }

        #region Dependencies


        private IDataManager RssDataManager { get; set; }

        private ILoginManager LoginManager { get; set; }

        #endregion

        public CategoriesViewModel(IDataManager dataManager, ILoginManager loginManager)
        {
            RssDataManager = dataManager;
            LoginManager = loginManager;

            RssDataManager.CategoryChanged += SetCategoryList;
            GetCategoryList();
        }

        public string GetCompleteUrl(string url)
        {
            if (!url.Contains("http://"))
            {
                url = url.Insert(0, "http://");
            }
            return (url);
        }

        private void SetCategoryList(object sender, EventArgs e)
        {
            CategoryList = new ObservableCollection<CategoryDTO>(RssDataManager.CategoryList);
        }

        private void GetCategoryList()
        {
            RssDataManager.SetCategoryList();
            CategoryList = new ObservableCollection<CategoryDTO>(RssDataManager.CategoryList);
        }


        public async void SetNewSource(string url, int catId)
        {
            //RssDataManager.AddSource(3, url, cat)

            // todo cette fonction pue la merde serieux
            var service = new WebApiServiceManager();

            var result = await service.AddSourceAsync(LoginManager.UserId, catId, url);
        }
    }
}
