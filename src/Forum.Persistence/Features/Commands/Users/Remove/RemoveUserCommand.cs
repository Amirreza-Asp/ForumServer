using MediatR;

namespace Forum.Persistence.Features.Commands.Users.Remove
{
    public class RemoveUserCommand : IRequest
    {
        public String UserName { get; set; }
    }
}
