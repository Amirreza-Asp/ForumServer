using Forum.Domain;
using Forum.Domain.Dtoes.Account;
using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using Forum.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Account.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserResultDto>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(AppDbContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ILogger<RegisterCommandHandler> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<UserResultDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Age = request.Age,
                Email = request.Email,
                Name = request.Name,
                Family = request.Family,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsMale = request.IsMale
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new AppException(String.Join('\n', result.Errors.Select(b => b.Description)));
            }

            await _userManager.AddToRoleAsync(user, request.Role ?? SD.UserRole);

            var refreshToken = TokenService.UpsertRefreshToken(user);

            _context.Add(refreshToken);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {request.UserName} Registered in {DateTime.UtcNow}");

            return UserResultDto.FromUser(user, TokenService.Create(user), refreshToken.Token, request.Role ?? SD.UserRole);
        }
    }
}
