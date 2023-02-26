using Forum.Domain.Entities;
using Forum.Domain.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>()
                .HasOne(b => b.User)
                .WithOne(b => b.RefreshToken)
                .HasForeignKey<RefreshToken>(b => b.UserId);

            builder.Entity<AppUser>()
                .HasMany(b => b.UserRole)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            builder.Entity<AppRole>()
                .HasMany(b => b.UserRole)
                .WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId);

            builder.Entity<AppUser>()
                .HasQueryFilter(b => b.IsDeleted == false);

        }
    }
}
