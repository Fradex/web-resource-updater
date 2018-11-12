﻿using Microsoft.EntityFrameworkCore;
using WebPackUpdater.Model;

namespace WebPackUpdater.Context
{
    public class WebResourceContext : DbContext
    {
        public WebResourceContext()
        {
        }

        public WebResourceContext(DbContextOptions<WebResourceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WebResourceMap> WebResourceMaps { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<MessageType>().HasData();
        }
    }
}