using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Domain.Dtoes.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Account.AllImages
{
    public class AllImagesQueryHandler : IRequestHandler<AllImagesQuery, List<PhotoDetails>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AllImagesQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PhotoDetails>> Handle(AllImagesQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.UserPhotos
                    .Where(b => b.User.UserName == request.UserName)
                    .ProjectTo<PhotoDetails>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }


    }
}
