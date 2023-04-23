using Forum.Domain.Dtoes.Home;
using MediatR;

namespace Forum.Persistence.Features.Queries.Home.TopContributors
{
    public class TopContributorsQuery : IRequest<List<TopContributorsDto>>
    {
    }
}
