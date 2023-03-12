using Forum.Domain.Dtoes.Account;
using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using Forum.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Account.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserResultDto>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginQueryHandler(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserResultDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user =
                 await _context.Users
                    .Where(b => b.UserName == request.UserName)
                    .Include(user => user.Photos)
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

                return UserResultDto.FromUser(user, TokenService.Create(user), refreshToken.Token, role.Name);
            }

            throw new AppException("Password is wrong");
        }

    }
}
