using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities.Communications
{
    public class FeelingTopic
    {
        [Key]
        public Guid Id { get; set; }

        public String By { get; set; }

        public Feeling Feeling { get; set; }

        public Guid TopicId { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
    }

}
