using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.Dtoes.Roles
{
    public class RoleDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public String Title { get; set; }

        public String Description { get; set; }
    }
}
