using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Comments.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public String Content { get; set; }

        [Required]
        public Guid TopicId { get; set; }
    }
}
