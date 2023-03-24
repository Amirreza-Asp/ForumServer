using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Topics.Update
{
    public class UpdateTopicCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public String Content { get; set; }
        [Required]
        public Guid CommunityId { get; set; }
    }
}
