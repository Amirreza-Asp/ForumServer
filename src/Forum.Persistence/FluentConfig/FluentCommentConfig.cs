using Forum.Domain.Entities.Communications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.FluentConfig
{
    public class FluentCommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(b => b.Topic)
                .WithMany(b => b.Comments)
                .HasForeignKey(b => b.TopicId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
