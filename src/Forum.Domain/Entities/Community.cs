using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.Entities
{
    public class Community
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Title { get; set; }

        public String Description { get; set; }

        //[Required]
        public String Image { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
