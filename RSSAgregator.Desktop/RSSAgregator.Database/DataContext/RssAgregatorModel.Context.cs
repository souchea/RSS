﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RSSAgregator.Database.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RSSAgregatorServer_dbEntities : DbContext
    {
        public RSSAgregatorServer_dbEntities()
            : base("name=RSSAgregatorServer_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FeedCategory> FeedCategories { get; set; }
        public virtual DbSet<FeedSource> FeedSources { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}