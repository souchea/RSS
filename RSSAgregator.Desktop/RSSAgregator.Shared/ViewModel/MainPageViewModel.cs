﻿using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using RSSAgregator.Models;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Shared.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    { 
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

        private bool _networkStatus;

        public bool NetworkStatus
        {
            get { return _networkStatus; }
            set
            {
                _networkStatus = value;
                NotifyPropertyChanged("NetworkStatus");
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
                NotifyPropertyChanged("MostViewedSourceList");
            }
        }

        public ObservableCollection<SourceDTO> MostViewedSourceList
        {
            get { return new ObservableCollection<SourceDTO>(SourceList.OrderBy(x => x.ViewedNumber)); }
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

        private ILoginManager LoginManager { get; set; }

        #endregion

        public MainPageViewModel(IServiceManager serviceManager, IDataManager dataManager, ILoginManager loginManager)
        {
            ServiceManager = serviceManager;
            RssDataManager = dataManager;
            LoginManager = loginManager;

            SourceList = new ObservableCollection<SourceDTO>();;

            RssDataManager.CategoryChanged += SetCategoryList;
            RssDataManager.SourceChanged += SetSourceList;
        }

        public async void DeleteCategories(IEnumerable<object> catNameList)
        {
            var list = catNameList.OfType<CategoryDTO>().ToList();
            foreach (CategoryDTO t in list)
            {
                bool sucess = ServiceManager.DeleteCategory(t.Id).Result;
                CategoryList.Remove(t);
            }
            RssDataManager.StorageManager.StoreCategories(LoginManager.UserId, CategoryList.ToList());
        }

        private void SetSourceList(object sender, EventArgs e)
        {
            SourceList = new ObservableCollection<SourceDTO>(RssDataManager.SourceList);
        }

        private void SetCategoryList(object sender, EventArgs e)
        {
            CategoryList = new ObservableCollection<CategoryDTO>(RssDataManager.CategoryList);
        }

        private async Task<List<CategoryDTO>> RefreshCategoryList()
        {
            List<CategoryDTO> newList = await ServiceManager.GetCategoriesAsync(LoginManager.UserId);
            CategoryList = new ObservableCollection<CategoryDTO>(newList);
            return (newList);
        }

        public async void RefreshSourceList()
        {
            List<SourceDTO> newList = new List<SourceDTO>();
            foreach (CategoryDTO t in CategoryList)
            {
                newList.AddRange(t.Feeds);
            }
            SourceList = new ObservableCollection<SourceDTO>(newList);
            RssDataManager.StorageManager.StoreSources(LoginManager.UserId, newList);

        }

        public async void AddCategory()
        {
            var result = await ServiceManager.AddCategoryAsync(LoginManager.UserId, ToAddCategoryText);
            if (result)
            {
                RssDataManager.StorageManager.StoreCategories(LoginManager.UserId, await RefreshCategoryList());
            }
        }
    }
}