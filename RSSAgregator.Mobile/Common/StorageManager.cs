using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.Web.Syndication;
using RSSAgregator.Shared.Model.RSSAgregator.Shared.Model;

namespace RSSAgregator.Mobile.Common
{
    public class StorageManager
    {
        public static async void SaveFileAsync(string name, SyndicationFeed toWrite)
        {
            XmlDocument newFeed = toWrite.GetXmlDocument(SyndicationFormat.Rss20);
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            await newFeed.SaveToFileAsync(file);
        }
    }
}
