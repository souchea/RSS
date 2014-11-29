using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Desktop.Manager
{
    public class StorageManager : IStorageManager
    {
        #region IStorageManager Members

        public async  Task<List<Shared.Model.CategoryDTO>> GetStoredCategories()
        {
            return new List<CategoryDTO>();
        }

        public async Task<List<Shared.Model.SourceDTO>> GetStoredSources()
        {
           return new List<SourceDTO>();
        }

        public void StoreCategories(List<Shared.Model.CategoryDTO> toStore)
        {
            
        }

        public void StoreSources(List<Shared.Model.SourceDTO> toStore)
        {
           ;
        }

        #endregion
    }
}
