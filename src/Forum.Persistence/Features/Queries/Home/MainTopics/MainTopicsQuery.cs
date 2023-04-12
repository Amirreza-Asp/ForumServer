using Forum.Application.Models;
using Forum.Domain.Dtoes.Topics;
using MediatR;

namespace Forum.Persistence.Features.Queries.Home.MainTopics
{
    public class MainTopicsQuery : IRequest<ListActionResult<TopicSummary>>
    {
        public String Filter { get; set; }
        public int Page { get; set; } = 1;
    }
}
