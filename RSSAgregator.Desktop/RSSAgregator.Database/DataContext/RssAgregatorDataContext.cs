namespace RSSAgregator.Database.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RssAgregatorDataContext : DbContext
    {
        public RssAgregatorDataContext()
            : base("name=RssAgregatorDataContext")
        {
        }

        public virtual DbSet<FeedCategory> FeedCategories { get; set; }
        public virtual DbSet<FeedSource> FeedSources { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<FeedCategory>()
                .HasMany(e => e.FeedSources)
                .WithRequired(e => e.FeedCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FeedCategories)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FeedSources)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
