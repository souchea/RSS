using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.Common
{
    public interface IStorageManager
    {
        //void SaveFileAsync(string name, SyndicationFeed toWrite)


        Task<List<CategoryDTO>> GetStoredCategories();


        Task<List<SourceDTO>> GetStoredSources();


        void StoreCategories(List<CategoryDTO> toStore);


        void StoreSources(List<SourceDTO> toStore);

    }
}
