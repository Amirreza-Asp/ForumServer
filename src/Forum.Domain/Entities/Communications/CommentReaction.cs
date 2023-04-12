using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Communications
{
    public class CommentReaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String By { get; set; }

        [Required]
        public Feeling Feeling { get; set; }

        public Guid CommentId { get; set; }
        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}
