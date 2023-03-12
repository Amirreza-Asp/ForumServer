using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Communtieis.Update
{
    public class UpdateCommunityCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public String Title { get; set; }

        public String Description { get; set; }

        public String Image { get; set; }
        public String Icon { get; set; }
        public String ImageExtension { get; set; }
    }
}
