using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Notifications.Users.ChangePassowrd
{
    public class ChangePasswordHandler : INotificationHandler<ChangePasswordNotif>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ChangePasswordNotif> _logger;

        public ChangePasswordHandler(AppDbContext context, UserManager<AppUser> userManager, ILogger<ChangePasswordNotif> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task Handle(ChangePasswordNotif notification, CancellationToken cancellationToken)
        {
            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager, notification.User, notification.Password);
                if (!result.Succeeded)
                {
                    String errors = String.Join('\n', result.Errors.Select(b => b.Description));
                    throw new AppException(errors);
                }
            }

            notification.User.PasswordHash = _userManager.PasswordHasher.HashPassword(notification.User, notification.Password);

            _context.Users.Update(notification.User);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"User {notification.User.UserName} updated password at {DateTime.UtcNow}");
        }
    }
}
