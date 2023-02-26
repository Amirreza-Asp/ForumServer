using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Profile.ProfileDetails
{
    public class ProfileDetailsQueryHandler : IRequestHandler<ProfileDetailsQuery, Domain.Dtoes.Profile.ProfileDetails>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProfileDetailsQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.Dtoes.Profile.ProfileDetails> Handle(ProfileDetailsQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.Users
                    .Where(b => b.UserName == request.UserName)
                    .ProjectTo<Domain.Dtoes.Profile.ProfileDetails>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
