using Forum.Application.Models;
using Forum.Domain.Dtoes.Topics;
using MediatR;

namespace Forum.Persistence.Features.Queries.Home.CommunityTopics
{
    public class CommunityTopicsQuery : IRequest<ListActionResult<TopicSummary>>
    {
        public Guid CommunityId { get; set; }
        public String SortBy { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 5;
    }
}
