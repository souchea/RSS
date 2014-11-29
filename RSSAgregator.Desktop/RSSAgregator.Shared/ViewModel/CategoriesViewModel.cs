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

        #endregion

        public CategoriesViewModel(IDataManager dataManager)
        {
            RssDataManager = dataManager;
            SetCategoryList(null, null);

            RssDataManager.CategoryChanged += SetCategoryList;
        }

        private void SetCategoryList(object sender, EventArgs e)
        {
            CategoryList = new ObservableCollection<CategoryDTO>(RssDataManager.CategoryList);
        }


        public async void SetNewFeed(string url, string cat)
        {
            // todo /!\  ne pas passer un string mais un ID  /!\
            //RssDataManager.AddSource(3, url, cat)

            // todo cette fonction pue la merde serieux
            var service = new WebApiServiceManager();
            int catId = 0;

            foreach (CategoryDTO t in CategoryList)
            {
                if (t.Name == cat)
                    catId = t.Id;
            }

            var result = await service.AddSourceAsync("599de3d2-811f-42fa-8544-a7b0975d3baf", catId, url);
        }
    }
}
