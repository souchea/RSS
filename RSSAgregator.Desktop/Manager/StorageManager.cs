﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using RSSAgregator.Shared.Common;
using RSSAgregator.Models;

namespace RSSAgregator.Desktop.Manager
{
    public class StorageManager : IStorageManager
    {
        #region IStorageManager Members

        public async Task<List<CategoryDTO>> GetStoredCategories(string userId)
            {
                try
                {
                    String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "categories.xml");
                    FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Write, 4096, true);
                    
                    var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));
                    return serializer.ReadObject(stream) as List<CategoryDTO>;
                }
                catch (Exception)
                {
                    return new List<CategoryDTO>();
                }
            }

        public async Task<List<SourceDTO>> GetStoredSources(string userId)
            {
                try
                {
                    String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "sources.xml");
                    FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Write, 4096, true);

                    var serializer = new DataContractSerializer(typeof(List<SourceDTO>));
                    return serializer.ReadObject(stream) as List<SourceDTO>;
                }
                catch (Exception)
                {
                    return new List<SourceDTO>();
                }
            }

        public void StoreCategories(string userId, List<CategoryDTO> toStore)
            {
                String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "categories.xml");
                new FileInfo(path).Directory.Create();
                FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read, 4096, true);

                var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));
                serializer.WriteObject(stream, toStore);
                stream.Close();
            }

        public void StoreSources(string userId, List<SourceDTO> toStore)
            {
                String path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSSAgregator", "sources.xml");
                new FileInfo(path).Directory.Create();
                FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read, 4096, true);
                var serializer = new DataContractSerializer(typeof(List<SourceDTO>));
                serializer.WriteObject(stream, toStore);
                stream.Close();
            }
        #endregion
    }
}
