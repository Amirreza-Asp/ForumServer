using Forum.Domain.Entities.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Communications
{
    public class Topic
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String Content { get; set; }

        [Range(0, int.MaxValue)]
        public int View { get; set; }

        [Range(0, int.MaxValue)]
        public int Like { get; set; }

        [Range(0, int.MaxValue)]
        public int DisLike { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(Community))]
        public Guid CommunityId { get; set; }

        public Community Community { get; set; }
        public AppUser Author { get; set; }
        public ICollection<TopicFile> Files { get; set; } = new List<TopicFile>();
    }
}
