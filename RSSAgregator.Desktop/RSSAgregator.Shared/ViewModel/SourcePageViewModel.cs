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
    public class SourcePageViewModel : BaseViewModel
    {
        private string _sourceNameText;

        public string SourceNameText
        {
            get { return _sourceNameText; }
            set
            {
                _sourceNameText = value;
                NotifyPropertyChanged("SourceNameText");
            }
        }

        private ObservableCollection<SourceDTO> _sourceList;

        public ObservableCollection<SourceDTO> SourceList
        {
            get { return _sourceList; }
            set
            {
                _sourceList = value;
                NotifyPropertyChanged("SourceList");
            }
        }

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

        public SourcePageViewModel(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            SourceList = new ObservableCollection<SourceDTO>();
        }

        public async void SetCategoryList(string catId)
        {
            SourceNameText = catId;
            CategoryList = new ObservableCollection<CategoryDTO>(await ServiceManager.GetCategoriesAsync(3));
            SetSourceList(catId);
        }

        private void SetSourceList(string catId)
        {
            foreach (CategoryDTO t1 in CategoryList)
            {
                if (t1.Name != catId) continue;
                foreach (SourceDTO t in t1.Feeds)
                {
                    SourceList.Add(t);
                }
            }
        }
    }
}
