using Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Community> Communities { get; set; }
    }
}
