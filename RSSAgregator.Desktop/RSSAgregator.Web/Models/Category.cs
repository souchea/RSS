using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSAgregator.Web.Models
{
    public class Category
    {
        public Category(string title, int channelsNb)
        {
            Title = title;
            ChannelsNb = channelsNb;
        }

        public string Title { get; set;}
        public int ChannelsNb { get; set; }

    }
}