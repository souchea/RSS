using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Models;

namespace RSSAgregator.Shared.Common
{
    public interface IStorageManager
    {
        Task<List<CategoryDTO>> GetStoredCategories(string userId);


        Task<List<SourceDTO>> GetStoredSources(string userId);


        void StoreCategories(string userId, List<CategoryDTO> toStore);


        void StoreSources(string userId, List<SourceDTO> toStore);



    }
}
