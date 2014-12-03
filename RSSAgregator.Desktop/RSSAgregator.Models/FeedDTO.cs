using System;

namespace RSSAgregator.Models
{
    public class FeedDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Uri BaseUri { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public string Summary { get; set; }
    }
}
