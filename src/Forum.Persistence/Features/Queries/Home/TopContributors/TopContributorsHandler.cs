using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Domain.Dtoes.Home;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Home.TopContributors
{
    public class TopContributorsHandler : IRequestHandler<TopContributorsQuery, List<TopContributorsDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public TopContributorsHandler(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TopContributorsDto>> Handle(TopContributorsQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.Users
                    .OrderByDescending(b => b.Topics.Count())
                    .Take(5)
                    .ProjectTo<TopContributorsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
