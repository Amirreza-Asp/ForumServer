using Forum.Domain.Dtoes.Comments;
using MediatR;

namespace Forum.Persistence.Features.Queries.Comments.GetUnreadComments
{
    public class GetUnreadCommentsQuery : IRequest<List<CommentSummary>>
    {
    }
}
