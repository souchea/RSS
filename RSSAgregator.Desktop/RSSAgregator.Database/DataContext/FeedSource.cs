namespace RSSAgregator.Database.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedSource")]
    public partial class FeedSource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Url { get; set; }

        public bool Public { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int ViewedNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string ViewState { get; set; }

        public virtual FeedCategory FeedCategory { get; set; }
    }
}
