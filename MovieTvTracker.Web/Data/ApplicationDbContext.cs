using Microsoft.EntityFrameworkCore;

namespace MovieTvTracker.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add items here if doing migrations 'Code First'
        public DbSet<WatchedMedia> WatchedMedia { get; set; }
        public DbSet<FavoriteActor> FavoriteActor { get; set; }
    }
}