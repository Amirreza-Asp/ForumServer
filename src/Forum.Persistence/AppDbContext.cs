using Forum.Domain.Entities.Account;
using Forum.Domain.Entities.Communications;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Forum.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Community> Communities { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicFile> TopicFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
