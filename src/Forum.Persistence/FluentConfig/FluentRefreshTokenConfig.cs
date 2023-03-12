using Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.FluentConfig
{
    public class FluentRefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasOne(b => b.User)
                .WithOne(b => b.RefreshToken)
                .HasForeignKey<RefreshToken>(b => b.UserId);
        }
    }
}
