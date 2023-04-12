using Forum.Domain.Dtoes.Home;
using MediatR;

namespace Forum.Persistence.Features.Queries.Home.CommunityPresentation
{
    public class CommunityPresentationQuery : IRequest<CommunityPresentationDto>
    {
        public Guid CommunityId { get; set; }
        public int Page { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public String UserName { get; set; }
        public String Title { get; set; }
    }
}
