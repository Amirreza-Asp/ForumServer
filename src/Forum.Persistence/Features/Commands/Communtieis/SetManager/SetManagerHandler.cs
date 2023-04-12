using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Commands.Communtieis.SetManager
{
    public class SetManagerHandler : IRequestHandler<SetManagerCommand>
    {
        private readonly AppDbContext _context;

        public SetManagerHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SetManagerCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Communities.AnyAsync(b => b.Id == request.CommunityId, cancellationToken))
                throw new AppException("Community not found");

            var user = await _context.Users.FirstOrDefaultAsync(b => b.UserName == request.UserName, cancellationToken);

            if (user == null)
                throw new AppException("User not found");

            var communityManager =
                    await _context.CommunityManager
                        .FirstOrDefaultAsync(b => b.CommunityId == request.CommunityId, cancellationToken);

            if (communityManager != null)
            {
                _context.CommunityManager.Remove(communityManager);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var newCommunityManager =
                new CommunityManager
                {
                    Id = Guid.NewGuid(),
                    CommunityId = request.CommunityId,
                    Manager = user
                };

            _context.CommunityManager.Add(newCommunityManager);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
