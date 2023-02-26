using Microsoft.AspNetCore.Identity;

namespace Forum.Domain.Entities.Account
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }

        public ICollection<AppUserRole> UserRole { get; set; } = new List<AppUserRole>();
    }
}
