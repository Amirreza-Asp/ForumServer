using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Application.Repositories.Communications;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Entities.Communications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Forum.Persistence.Reposiotories.Communications
{
    public class CommunityRepository : Repository<Community>, ICommunityRepository
    {
        private readonly IMemoryCache _memoryCache;

        private readonly String _communitiesListKey = "CommunitiesListKey";

        public CommunityRepository(AppDbContext context, IMapper mapper, IMemoryCache memoryCache) : base(context, mapper)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<CommunitySummary>> GetTopCommunitiesAsync(CancellationToken cancellationToken)
        {
            List<CommunitySummary> communities;
            if (_memoryCache.TryGetValue<List<CommunitySummary>>(_communitiesListKey, out communities))
                return communities;

            communities =
               await _context.Communities
                   .OrderByDescending(b => b.Topics.Count())
                   .Take(7)
                   .ProjectTo<CommunitySummary>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            _memoryCache.Set(_communitiesListKey, communities, DateTimeOffset.UtcNow.AddMinutes(15));

            return communities;
        }
    }
}
