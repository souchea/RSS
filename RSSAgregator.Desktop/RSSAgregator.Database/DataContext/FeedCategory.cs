namespace RSSAgregator.Database.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedCategory")]
    public partial class FeedCategory
    {
        public FeedCategory()
        {
            FeedSources = new HashSet<FeedSource>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public byte Public { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<FeedSource> FeedSources { get; set; }

        public virtual User User { get; set; }
    }
}
