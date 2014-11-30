using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSAgregator.Server.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public List<SourceDTO> Feeds { get; set; }

        public string Name { get; set; }
    }
}
