using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Communtieis.SetManager
{
    public class SetManagerCommand : IRequest
    {
        [Required]
        public Guid CommunityId { get; set; }

        [Required]
        public String UserName { get; set; }
    }
}
