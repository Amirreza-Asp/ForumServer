using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Comments.AddCommentReaction
{
    public class UpsertCommentReactionCommand : IRequest
    {
        [Required]
        public String Reaction { get; set; }
        [Required]
        public Guid CommentId { get; set; }
    }
}
