using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {

        private string _account_ButtonText;

        public string Account_ButtonText
        {
            get { return _account_ButtonText; }
            set
            {
                _account_ButtonText = value;
                NotifyPropertyChanged("Account_ButtonText");
            }
        }

        private string _toAddCategoryText;

        public string ToAddCategoryText
        {
            get { return _toAddCategoryText; }
            set
            {
                _toAddCategoryText = value;
                NotifyPropertyChanged("ToAddCategoryText");
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

        #region Commands

        private RelayCommand _addCategoryCommand;

        public RelayCommand AddCategoryCommand
        {
            get
            {
                if (_addCategoryCommand == null)
                {
                    _addCategoryCommand = new RelayCommand(
                        AddCategory,
                        () => true);
                }
                return _addCategoryCommand;
            }
            set
            {
                _addCategoryCommand = value;
            }
        }


        #endregion

        private IServiceManager ServiceManager { get; set; }

        public MainPageViewModel(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            SourceList = new ObservableCollection<SourceDTO>();
            SetCategoryList();
        }


        public async void SetCategoryList()
        {
            CategoryList = new ObservableCollection<CategoryDTO>(await ServiceManager.GetCategoriesAsync(3));
            SetSourceList();

        }

        public void SetSourceList()
        {
            foreach (var category in CategoryList)
            {
                foreach (var source in category.Feeds)
                {
                    SourceList.Add(source);
                }
            }
        }

        public async void AddCategory()
        {
            var result = await ServiceManager.AddCategoryAsync(3, ToAddCategoryText);
            if (result)
            {
            }
        }
    }
}