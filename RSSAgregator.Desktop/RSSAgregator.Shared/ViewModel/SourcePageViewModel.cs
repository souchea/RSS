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

        private string _sourceEmptyText;

        public string SourceEmptyText
        {
            get { return _sourceEmptyText; }
            set
            {
                _sourceEmptyText = value;
                NotifyPropertyChanged("SourceEmptyText");
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

        #region Dependencies

        private IServiceManager ServiceManager { get; set; }

        private IDataManager DataManager { get; set; }

        private ILoginManager LoginManager { get; set; }

        #endregion

        public SourcePageViewModel(IServiceManager serviceManager, IDataManager dataManager, ILoginManager loginManager)
        {
            ServiceManager = serviceManager;
            DataManager = dataManager;
            LoginManager = loginManager;

            SourceList = new ObservableCollection<SourceDTO>();
        }

        public async void SetCategoryList(string catId)
        {
            SourceNameText = catId;
            CategoryList = new ObservableCollection<CategoryDTO>(await ServiceManager.GetCategoriesAsync(LoginManager.UserId));
            SetSourceList(catId);
        }

        public async void DeleteSources(IEnumerable<object> sourcesNameList)
        {
            var list = sourcesNameList.OfType<SourceDTO>().ToList();
            foreach (SourceDTO t in list)
            {
                bool sucess = await ServiceManager.DeleteSource(t.Id);
                SourceList.Remove(t);
            }
            DataManager.StorageManager.StoreSources(SourceList.ToList());
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
            if (!SourceList.Any())
                SourceEmptyText = "Vous n'avez aucun flux dans cette categorie";
        }
    }
}
