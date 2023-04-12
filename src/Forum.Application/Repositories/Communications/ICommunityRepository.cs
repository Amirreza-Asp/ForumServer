using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Entities.Communications;

namespace Forum.Application.Repositories.Communications
{
    public interface ICommunityRepository : IRepository<Community>
    {
        Task<List<CommunitySummary>> GetTopCommunitiesAsync(CancellationToken cancellationToken);
    }
}
