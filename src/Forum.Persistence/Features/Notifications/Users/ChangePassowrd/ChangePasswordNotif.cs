using Forum.Domain.Entities.Account;
using MediatR;

namespace Forum.Persistence.Features.Notifications.Users.ChangePassowrd
{
    public class ChangePasswordNotif : INotification
    {
        public AppUser User { get; set; }
        public string Password { get; set; }
    }
}
