using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Users.Remove
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<RemoveUserHandler> _logger;

        public RemoveUserHandler(AppDbContext context, ILogger<RemoveUserHandler> logger, IUserAccessor userAccessor)
        {
            _context = context;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user =
                await _context.Users
                    .Where(b => b.UserName == request.UserName)
                    .Include(b => b.UserRole)
                        .ThenInclude(b => b.Role)
                    .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new AppException("UserName is wrong");

            if (user.UserRole.Any(b => b.Role.Name == SD.AdminRole))
                throw new AppException("The user Admin cannot be deleted");

            user.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogWarning($"User {user.UserName} Deleted by {_userAccessor.GetUserName()} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
