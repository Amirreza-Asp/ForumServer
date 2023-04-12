using Forum.Application.Models;
using Forum.Domain.Dtoes.Comments;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Queries.Comments.CommentsPagenation
{
    public class CommentsPagenationQuery : IRequest<ListActionResult<CommentSummary>>
    {
        [Required]
        public Guid TopicId { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
