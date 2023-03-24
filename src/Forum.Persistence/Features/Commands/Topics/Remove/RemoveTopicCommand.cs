using MediatR;

namespace Forum.Persistence.Features.Commands.Topics.Remove
{
    public class RemoveTopicCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
