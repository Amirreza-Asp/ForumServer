using Forum.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.FluentConfig
{
    public class FluentAppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .HasMany(b => b.UserRole)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            builder
                .HasQueryFilter(b => b.IsDeleted == false);
        }
    }
}
