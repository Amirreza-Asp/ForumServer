using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Account
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Family { get; set; }

        [Required]
        public DateTime Age { get; set; }

        public bool IsMale { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public string FullName => string.Concat(Name, " ", Family);

        public ICollection<UserPhoto> Photos { get; set; } = new List<UserPhoto>();
        public RefreshToken RefreshToken { get; set; }
        public ICollection<AppUserRole> UserRole { get; set; } = new List<AppUserRole>();
    }
}
