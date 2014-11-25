using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSAgregator.Shared.Model
{
    using System.Text;
    using System.Threading.Tasks;

    namespace RSSAgregator.Shared.Model
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
}
