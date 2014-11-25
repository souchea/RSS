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

        private IServiceManager ServiceManager { get; set; }

        public CategoriesViewModel(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            SetCategoryList();
        }

        public async void SetCategoryList()
        {
            CategoryList = new ObservableCollection<CategoryDTO>(await ServiceManager.GetCategoriesAsync(3));
        }

        public async void SetNewFeed(string url, string cat)
        {
            var service = new WebApiServiceManager();
            int catId = 0;

            foreach (CategoryDTO t in CategoryList)
            {
                if (t.Name == cat)
                    catId = t.Id;
            }

            var result = await service.AddSourceAsync(3, cat, url);
        }
    }
}
