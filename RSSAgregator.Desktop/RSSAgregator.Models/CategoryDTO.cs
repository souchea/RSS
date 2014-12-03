using System.Collections.Generic;

namespace RSSAgregator.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public List<SourceDTO> Feeds { get; set; }

        public string Name { get; set; }
    }
}
