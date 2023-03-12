using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Commands.Users.Remove
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand>
    {
        private readonly AppDbContext _context;

        public RemoveUserHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user =
                await _context.Users
                    .Where(b => b.UserName == request.UserName)
                    .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new AppException("UserName is wrong");

            user.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
