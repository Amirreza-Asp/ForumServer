using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Communications
{
    public class TopicFile
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Url { get; set; }

        [ForeignKey(nameof(Topic))]
        public Guid TopicId { get; set; }


        public Topic Topic { get; set; }
    }
}
