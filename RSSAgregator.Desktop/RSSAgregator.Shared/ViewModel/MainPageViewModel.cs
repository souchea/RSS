using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ninject;
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

        private int _selectedSourceIndex;

        public int SelectedSourceIndex
        {
            get { return _selectedSourceIndex; }
            set
            {
                _selectedSourceIndex = value;
                NotifyPropertyChanged("SelectedSourceIndex");
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

        #region Dependencies

        private IServiceManager ServiceManager { get; set; }

        private IDataManager RssDataManager { get; set; }

        #endregion

        public MainPageViewModel(IServiceManager serviceManager, IDataManager dataManager)
        {
            ServiceManager = serviceManager;
            RssDataManager = dataManager;

            SourceList = new ObservableCollection<SourceDTO>();;

            RssDataManager.CategoryChanged += SetCategoryList;
            RssDataManager.SourceChanged += SetSourceList;
        }

        public void DeleteCategories(List<object> catNameList)
        {
            var list = catNameList.OfType<CategoryDTO>().ToList();
            foreach (CategoryDTO t in catNameList)
            {
                ServiceManager.DeleteCategory(t.Id);
                CategoryList.Remove(t);
            }
        }

        private void SetSourceList(object sender, EventArgs e)
        {
            SourceList = new ObservableCollection<SourceDTO>(RssDataManager.SourceList);
        }

        private void SetCategoryList(object sender, EventArgs e)
        {
            CategoryList = new ObservableCollection<CategoryDTO>(RssDataManager.CategoryList);
        }


        public async void AddCategory()
        {
            var result = await ServiceManager.AddCategoryAsync("599de3d2-811f-42fa-8544-a7b0975d3baf", ToAddCategoryText);
            if (result)
            {
            }
        }
    }
}