using Forum.Domain.Entities.Account;
using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.Entities.Communications
{
    public class Community
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public String Icon { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public CommunityManager Manager { get; set; }
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}
