using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private List<CategoryDTO> _categoryList;

        public List<CategoryDTO> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                NotifyPropertyChanged("CategoryList");
            }
        }

        public MainPageViewModel()
        {
            SetCategoryList();
        }


        public async void SetCategoryList()
        {

    
        }
    }
}