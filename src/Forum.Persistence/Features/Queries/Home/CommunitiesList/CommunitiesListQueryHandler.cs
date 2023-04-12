using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Domain.Dtoes.Home;
using Forum.Persistence.Features.Queries.Home.CommunityList;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Home.CommunitiesList
{
    public class CommunitiesListQueryHandler : IRequestHandler<CommunitiesListQuery, List<CommunitiesListDto>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CommunitiesListQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CommunitiesListDto>> Handle(CommunitiesListQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.Communities
                    .OrderByDescending(c => c.Topics.Count)
                    .ProjectTo<CommunitiesListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
