using MediatR;
using Microsoft.AspNetCore.Http;

namespace Forum.Persistence.Features.Commands.Account.ChangeImage
{
    public class ChangeImageCommand : IRequest<String>
    {
        public IFormFile File { get; set; }
    }
}
