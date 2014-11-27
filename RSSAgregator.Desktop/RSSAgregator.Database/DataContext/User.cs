namespace RSSAgregator.Database.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            FeedCategories = new HashSet<FeedCategory>();
            FeedSources = new HashSet<FeedSource>();
        }

        public int Id { get; set; }

        [Required]
        public string Roles { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FacebookId { get; set; }

        [StringLength(50)]
        public string TwitterId { get; set; }

        [StringLength(50)]
        public string GoogleId { get; set; }

        public DateTime SignUpDate { get; set; }

        public DateTime LastSignInDate { get; set; }

        public virtual ICollection<FeedCategory> FeedCategories { get; set; }

        public virtual ICollection<FeedSource> FeedSources { get; set; }
    }
}
