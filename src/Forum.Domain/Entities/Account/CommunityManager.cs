using Forum.Domain.Entities.Communications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Account
{
    public class CommunityManager
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CommunityId { get; set; }
        [ForeignKey(nameof(CommunityId))]
        public Community Community { get; set; }

        public Guid ManagerId { get; set; }
        [ForeignKey(nameof(ManagerId))]
        public AppUser Manager { get; set; }
    }
}
