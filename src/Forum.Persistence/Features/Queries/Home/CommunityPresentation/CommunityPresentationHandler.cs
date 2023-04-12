using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Domain.Dtoes.Home;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Home.CommunityPresentation
{
    public class CommunityPresentationHandler : IRequestHandler<CommunityPresentationQuery, CommunityPresentationDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CommunityPresentationHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommunityPresentationDto> Handle(CommunityPresentationQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.Communities
                    .Where(b => b.Id == request.CommunityId)
                    .ProjectTo<CommunityPresentationDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
