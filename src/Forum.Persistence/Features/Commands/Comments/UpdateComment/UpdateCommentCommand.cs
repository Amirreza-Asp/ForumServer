using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Comments.UpdateComment
{
    public class UpdateCommentCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public String Content { get; set; }
    }
}
