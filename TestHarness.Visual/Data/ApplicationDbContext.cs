using Microsoft.EntityFrameworkCore;
using TestHarness.Visual.Models;

namespace TestHarness.Visual.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add items here if doing migrations 'Code First'
        public DbSet<WatchedMedia> WatchedMedia { get; set; }
    }
}