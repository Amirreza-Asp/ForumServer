using MediatR;

namespace Forum.Persistence.Features.Commands.Topics.Interest
{
    public class FeelingCommand : IRequest
    {
        public Guid TopicId { get; set; }
        public bool Interest { get; set; }
    }
}
