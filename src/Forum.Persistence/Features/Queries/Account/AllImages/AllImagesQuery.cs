using Forum.Domain.Entities;
using MediatR;

namespace Forum.Persistence.Features.Queries.Account.AllImages
{
    public class AllImagesQuery : IRequest<List<UserPhoto>>
    {
        public String UserName { get; set; }
    }
}
