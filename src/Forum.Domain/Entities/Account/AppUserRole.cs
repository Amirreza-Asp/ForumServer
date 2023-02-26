using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Account
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }


        [ForeignKey(nameof(RoleId))]
        public AppRole Role { get; set; }
    }
}
