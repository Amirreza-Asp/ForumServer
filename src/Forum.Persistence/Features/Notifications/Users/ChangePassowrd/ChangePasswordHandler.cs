using Forum.Domain.Entities.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Persistence.Features.Notifications.Users.ChangePassowrd
{
    public class ChangePasswordHandler : INotificationHandler<ChangePasswordNotif>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ChangePasswordHandler(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Handle(ChangePasswordNotif notification, CancellationToken cancellationToken)
        {
            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager, notification.User, notification.Password);
                if (!result.Succeeded)
                {
                    String errors = String.Join('\n', result.Errors.Select(b => b.Description));
                    throw new ApplicationException(errors);
                }
            }

            notification.User.PasswordHash = _userManager.PasswordHasher.HashPassword(notification.User, notification.Password);

            _context.Users.Update(notification.User);
            await _context.SaveChangesAsync();
        }
    }
}
