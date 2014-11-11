using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RSSAgregator.Mobile.Model
{
    public class FeedDto
    {
        public class FeedData
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime PubDate { get; set; }

            private List<FeedItem> _Items = new List<FeedItem>();
            public List<FeedItem> Items
            {
                get
                {
                    return this._Items;
                }
            }
        }

        public class FeedItem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Content { get; set; }
            public DateTime PubDate { get; set; }
            public Uri Link { get; set; }
            public BitmapImage Pic { get; set; }
            public int StateFeed { get; set; }
        }
    }
}
