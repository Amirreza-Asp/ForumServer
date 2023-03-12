using Forum.Application.Services;
using Forum.Domain.Dtoes.Topics;
using Forum.Domain.Entities.Communications;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : BaseApiController
    {
        private readonly IRepository<Topic> _topicRepository;

        public TopicController(IRepository<Topic> topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet("GetByCommunityId/{communityId}")]
        public async Task<List<TopicSummary>> GetByCummunityId(Guid communityId, CancellationToken cancellationToken)
        {
            return await _topicRepository.GetAllAsync<TopicSummary>(b => b.CommunityId == communityId, cancellationToken);
        }
    }
}
