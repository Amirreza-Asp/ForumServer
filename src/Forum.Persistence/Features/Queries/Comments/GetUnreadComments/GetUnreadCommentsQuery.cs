using Forum.Application.Models;
using Forum.Domain.Dtoes.Comments;
using MediatR;

namespace Forum.Persistence.Features.Queries.Comments.GetUnreadComments
{
    public class GetUnreadCommentsQuery : IRequest<ListActionResult<UnreadCommentSummary>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
