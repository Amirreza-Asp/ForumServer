using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Account
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid Token { get; set; }

        [Range(0, int.MaxValue)]
        public int Expire { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        [NotMapped]
        public DateTime ExpireDate => CreateDate.AddDays(Expire);

    }
}
