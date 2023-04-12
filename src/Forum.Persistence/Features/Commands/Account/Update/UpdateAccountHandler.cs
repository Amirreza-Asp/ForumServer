using Forum.Application.Services;
using Forum.Domain.Dtoes.Account;
using Forum.Domain.Exceptions;
using Forum.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Account.Update
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, UserResultDto>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<UpdateAccountHandler> _logger;

        public UpdateAccountHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<UpdateAccountHandler> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<UserResultDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            if (_userAccessor.GetUserName() != request.UserName)
                throw new AppException("You cannot change account information other than your account");

            var user =
                await _context.Users
                    .Include(b => b.UserRole)
                        .ThenInclude(b => b.Role)
                    .Include(b => b.RefreshToken)
                    .Include(b => b.Photo)
                    .Where(b => b.UserName == request.UserName)
                    .FirstOrDefaultAsync(cancellationToken);

            user.Name = request.Name;
            user.Family = request.Family;
            user.IsMale = request.isMale;
            user.Age = request.Age;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;

            var refreshToken = TokenService.UpsertRefreshToken(user);

            user.RefreshToken = refreshToken;

            await _context.SaveChangesAsync();
            _logger.LogInformation($"User {request.UserName} update information at {DateTime.UtcNow}");

            return UserResultDto.FromUser(user, TokenService.Create(user), refreshToken.Token, user.UserRole.First().Role.Name);
        }
    }
}
