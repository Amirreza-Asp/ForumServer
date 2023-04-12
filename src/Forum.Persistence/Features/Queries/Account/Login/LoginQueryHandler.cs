using Forum.Domain.Dtoes.Account;
using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using Forum.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Queries.Account.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserResultDto>
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginQueryHandler> _logger;

        public LoginQueryHandler(AppDbContext context, SignInManager<AppUser> signInManager, ILogger<LoginQueryHandler> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<UserResultDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user =
                 await _context.Users
                    .Where(b => b.UserName == request.UserName)
                    .Include(user => user.Photo)
                    .Include(user => user.RefreshToken)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new AppException("UserName is wrong");



            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);


            if (result.Succeeded)
            {
                var userRoles =
                    await _context.UserRoles
                 .FirstOrDefaultAsync(b => b.UserId == user.Id, cancellationToken);

                var role = await _context.Roles.FindAsync(userRoles.RoleId);


                var refreshToken = TokenService.UpsertRefreshToken(user);

                if (user.RefreshToken == null)
                    _context.Add(refreshToken);
                else
                    _context.Update(refreshToken);

                await _context.SaveChangesAsync();
                _logger.LogInformation($"User {request.UserName} entered at {DateTime.UtcNow} , \t login with userName and password");

                return UserResultDto.FromUser(user, TokenService.Create(user), refreshToken.Token, role.Name);
            }

            _logger.LogWarning($"Attempting to login the user {request.UserName} at {DateTime.UtcNow}, " +
                $"but entered password is wrong , \t password : {request.Password}");

            throw new AppException("Password is wrong");
        }

    }
}
