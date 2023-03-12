using Forum.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.FluentConfig
{
    public class FluentAppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder
                .HasMany(b => b.UserRole)
                .WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId);
        }
    }
}
