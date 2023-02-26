using MediatR;
using Microsoft.AspNetCore.Http;

namespace Forum.Persistence.Features.Commands.Account.AddImage
{
    public class AddImageCommand : IRequest
    {
        public IFormFile File { get; set; }
    }
}
