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
using RSSAgregator.Shared.Common;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.Model.RSSAgregator.Shared.Model;

namespace RSSAgregator.Mobile.Common
{
    public class StorageManager : IStorageManager
    {
        public async void SaveFileAsync(string name, SyndicationFeed toWrite)
        {
            XmlDocument newFeed = toWrite.GetXmlDocument(SyndicationFormat.Rss20);
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            await newFeed.SaveToFileAsync(file);
        }

        public async Task<List<CategoryDTO>> GetStoredCategories()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("categories.xml");

                var fileStream = await file.OpenStreamForReadAsync();

                //var fileStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("categories.xml");

                var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));

                return serializer.ReadObject(fileStream) as List<CategoryDTO>;
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
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("sources.xml");

                var fileStream = await file.OpenStreamForReadAsync();

                var serializer = new DataContractSerializer(typeof(List<SourceDTO>));

                return serializer.ReadObject(fileStream) as List<SourceDTO>;
            }
            catch (Exception)
            {
                return new List<SourceDTO>();

            }
        }

        public async void StoreCategories(List<CategoryDTO> toStore)
        {
            var serializer = new DataContractSerializer(typeof(List<CategoryDTO>));

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("categories.xml", CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                serializer.WriteObject(fileStream, toStore);
            }
        }

        public async void StoreSources(List<SourceDTO> toStore)
        {
            var serializer = new DataContractSerializer(typeof (List<SourceDTO>));

            try
            {
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("sources.xml", CreationCollisionOption.ReplaceExisting);

                using (var fileStream = await file.OpenStreamForWriteAsync())
                {
                    serializer.WriteObject(fileStream, toStore);
                }

                //using (
                //    var fileStream =
                //        await
                //            ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync("sources.xml",
                //                CreationCollisionOption.OpenIfExists))
                //{
                //    serializer.WriteObject(fileStream, toStore);
                //}
            }
            catch { }
        }
    }
}
