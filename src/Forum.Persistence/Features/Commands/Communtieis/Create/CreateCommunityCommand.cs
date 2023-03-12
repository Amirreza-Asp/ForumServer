using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Communtieis.Create
{
    public class CreateCommunityCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public String Title { get; set; }

        public String Description { get; set; }

        [Required]
        public String Image { get; set; }

        [Required]
        public String Icon { get; set; }

        [Required]
        public String ImageExtension { get; set; }
    }
}
