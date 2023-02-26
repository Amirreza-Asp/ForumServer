using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Domain.Entities.Account;

namespace Forum.Domain.Entities
{
    public class UserPhoto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Url { get; set; }

        public bool IsMain { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }
}
