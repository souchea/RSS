using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Desktop.Manager
{
    public class StorageManager : IStorageManager
    {
        #region IStorageManager Members

            public async Task<List<CategoryDTO>> GetStoredCategories()
            {
                try
                {
                    String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "categories.xml");
                    FileStream stream = File.OpenRead(path);

                    var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));

                    return serializer.ReadObject(stream) as List<CategoryDTO>;
                }
                catch (Exception)
                {
                    return new List<CategoryDTO>();
                }
            }

            public async Task<List<SourceDTO>> GetStoredSources()
            {
                try
                {
                    String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "sources.xml");
                    FileStream stream = File.OpenRead(path);

                    var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));

                    return serializer.ReadObject(stream) as List<SourceDTO>;
                }
                catch (Exception)
                {
                    return new List<SourceDTO>();
                }
            }

            public void StoreCategories(List<CategoryDTO> toStore)
            {
                String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "categories.xml");
                FileStream stream = File.Open(path, FileMode.Create);
                var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));
                serializer.WriteObject(stream, toStore);
            }

            public void StoreSources(List<SourceDTO> toStore)
            {
                String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "sources.xml");
                FileStream stream = File.Open(path, FileMode.Create);
                var serializer = new DataContractSerializer(typeof(List<SourceDTO>));
                serializer.WriteObject(stream, toStore);
            }
        #endregion
    }
}
