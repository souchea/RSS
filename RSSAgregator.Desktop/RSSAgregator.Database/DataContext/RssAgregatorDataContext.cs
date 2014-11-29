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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<FeedCategory> FeedCategories { get; set; }
        public virtual DbSet<FeedSource> FeedSources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<FeedCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<FeedCategory>()
                .HasMany(e => e.FeedSources)
                .WithRequired(e => e.FeedCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
