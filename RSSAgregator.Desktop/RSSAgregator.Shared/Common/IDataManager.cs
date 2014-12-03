using System;
using System.Collections.Generic;
using Ninject;
using RSSAgregator.Models;

namespace RSSAgregator.Shared.Common
{
    public interface IDataManager
    {
        IStorageManager StorageManager { get; set; }

        [Inject]
        IServiceManager ServiceManager { get; set; }

        List<SourceDTO> SourceList { get; set; }
        List<CategoryDTO> CategoryList { get; set; }
        event EventHandler SourceChanged;
        event EventHandler CategoryChanged;
        void SetCategoryList();
        void SetSourceList();
    }
}