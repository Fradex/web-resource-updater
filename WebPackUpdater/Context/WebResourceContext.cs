using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<ChangedWebResource> ChangedWebResources { get; set; }
        public virtual DbSet<Build> Builds { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<MessageType>().HasData();
        }
    }
}
