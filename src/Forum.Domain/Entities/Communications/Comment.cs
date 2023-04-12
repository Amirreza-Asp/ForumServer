using Forum.Domain.Entities.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Communications
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Range(0, int.MaxValue)]
        public int Like { get; set; }

        [Range(0, int.MaxValue)]
        public int DisLike { get; set; }

        [Required]
        public bool ReadByAuthor { get; set; } = false;

        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public AppUser Author { get; set; }

        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }

        public ICollection<CommentReaction> Reactions { get; set; } = new List<CommentReaction>();
    }
}
