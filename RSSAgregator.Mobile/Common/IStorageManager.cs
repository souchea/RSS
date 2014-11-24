using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace RSSAgregator.Mobile.Common
{
    public interface IStorageManager
    {
        void SaveFileAsync(string name, SyndicationFeed toWrite);
    }
}
