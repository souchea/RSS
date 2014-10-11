using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace RSSAgregator.Web.Models
{
    public class Channel
    {
        public Channel(string title, string description)
        {
            Title = title;
            Description = description;
        }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}