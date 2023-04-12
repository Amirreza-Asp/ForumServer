using Forum.Domain.Dtoes.Home;
using MediatR;

namespace Forum.Persistence.Features.Queries.Home.CommunityList
{
    public class CommunitiesListQuery : IRequest<List<CommunitiesListDto>>
    {
    }
}
