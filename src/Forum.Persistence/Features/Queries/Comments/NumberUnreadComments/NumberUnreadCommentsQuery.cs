using MediatR;

namespace Forum.Persistence.Features.Queries.Comments.HasUnreadComments
{
    public class NumberUnreadCommentsQuery : IRequest<int>
    {
    }
}
