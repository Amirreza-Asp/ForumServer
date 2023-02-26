using Forum.Domain.Dtoes.Account;
using Forum.Domain.Entities.Account;
using Forum.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Forum.Persistence.Features.Queries.Account.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommand, UserResultDto>
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public RefreshTokenLoginCommandHandler(AppDbContext context, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<UserResultDto> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
        {
            var refreshToken =
                 await _context.RefreshTokens
                    .Where(b => b.Token == request.RefreshToken)
                    .Include(b => b.User)
                        .ThenInclude(b => b.Photos)
                    .FirstOrDefaultAsync(cancellationToken);

            if (refreshToken == null)
                throw new AuthenticationException("Refresh token not found");

            if (refreshToken.ExpireDate < DateTime.UtcNow)
                throw new AuthenticationException("Refresh token expired");



            await _signInManager.SignInAsync(refreshToken.User, false);

            var userRoles =
                await _context.UserRoles
                    .FirstOrDefaultAsync(b => b.UserId == refreshToken.UserId, cancellationToken);

            var role = await _context.Roles.FindAsync(userRoles.RoleId);

            refreshToken.Token = Guid.NewGuid();
            refreshToken.CreateDate = DateTime.Now;

            _context.Update(refreshToken);
            await _context.SaveChangesAsync();

            return UserResultDto.FromUser(
                    refreshToken.User,
                    TokenService.Create(refreshToken.User),
                    refreshToken.Token,
                    role.Name);
        }
    }
}
