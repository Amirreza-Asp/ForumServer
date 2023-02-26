using Forum.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Account.AllImages
{
    public class AllImagesQueryHandler : IRequestHandler<AllImagesQuery, List<UserPhoto>>
    {
        private readonly AppDbContext _context;

        public AllImagesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserPhoto>> Handle(AllImagesQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.UserPhotos
                    .Where(b => b.User.UserName == request.UserName)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }


    }
}
