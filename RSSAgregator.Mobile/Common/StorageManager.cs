using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.Web.Syndication;
using RSSAgregator.Models;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Mobile.Common
{
    public class StorageManager : IStorageManager
    {
        public async Task<List<CategoryDTO>> GetStoredCategories(string userId)
        {
            try
            {
                await
                    ApplicationData.Current.LocalFolder.CreateFolderAsync(String.Format(@"{0}/", userId),
                        CreationCollisionOption.FailIfExists);
            }
            catch
            {
            }

            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(String.Format(@"{0}/categories.xml", userId));

                var fileStream = await file.OpenStreamForReadAsync();

                var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));

                return serializer.ReadObject(fileStream) as List<CategoryDTO>;
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
                await
                    ApplicationData.Current.LocalFolder.CreateFolderAsync(String.Format(@"{0}/", userId),
                        CreationCollisionOption.FailIfExists);
            }
            catch
            {
            }

            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(String.Format(@"{0}/sources.xml", userId));

                var fileStream = await file.OpenStreamForReadAsync();

                var serializer = new DataContractSerializer(typeof(List<SourceDTO>));

                return serializer.ReadObject(fileStream) as List<SourceDTO>;
            }
            catch (Exception)
            {
                return new List<SourceDTO>();

            }
        }

        public async void StoreCategories(string userId, List<CategoryDTO> toStore)
        {
            var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));

            var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(String.Format("{0}", userId));

            var file = await folder.CreateFileAsync(String.Format("categories.xml"), CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                serializer.WriteObject(fileStream, toStore);
            }
        }

        public async void StoreSources(string userId, List<SourceDTO> toStore)
        {
            var serializer = new DataContractSerializer(typeof (List<SourceDTO>));

            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(String.Format("{0}", userId));

                var file = await folder.CreateFileAsync(String.Format("sources.xml"), CreationCollisionOption.ReplaceExisting);
                
                //var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(String.Format(@"{0}/sources.xml", userId), CreationCollisionOption.ReplaceExisting);

                using (var fileStream = await file.OpenStreamForWriteAsync())
                {
                    serializer.WriteObject(fileStream, toStore);
                }
            }
            catch { }
        }
    }
}
