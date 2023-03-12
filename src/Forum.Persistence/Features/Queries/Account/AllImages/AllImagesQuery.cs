using Forum.Domain.Dtoes.Profile;
using MediatR;

namespace Forum.Persistence.Features.Queries.Account.AllImages
{
    public class AllImagesQuery : IRequest<List<PhotoDetails>>
    {
        public String UserName { get; set; }
    }
}
