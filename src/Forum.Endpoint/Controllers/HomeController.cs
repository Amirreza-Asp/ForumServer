using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Queries.Home.CommunityList;
using Forum.Persistence.Features.Queries.Home.CommunityPresentation;
using Forum.Persistence.Features.Queries.Home.CommunityTopics;
using Forum.Persistence.Features.Queries.Home.FindTopic;
using Forum.Persistence.Features.Queries.Home.MainTopics;
using Forum.Persistence.Features.Queries.Home.TopContributors;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : BaseApiController
    {
        [Route("FindTopic")]
        [HttpGet]
        public async Task<IActionResult> FindTopic([FromQuery] FindTopicQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("CommunitiesList")]
        [HttpGet]
        public async Task<IActionResult> CommunitiesList([FromQuery] CommunitiesListQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("CommunityPresentation")]
        [HttpGet]
        public async Task<IActionResult> CommunityPresentation([FromQuery] CommunityPresentationQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("CommunityTopics")]
        [HttpPost]
        public async Task<IActionResult> CommunityTopics([FromBody] CommunityTopicsQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("MainTopics")]
        [HttpGet]
        public async Task<IActionResult> MainTopics([FromQuery] MainTopicsQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("TopContributors")]
        [HttpGet]
        public async Task<IActionResult> TopContributors([FromQuery] TopContributorsQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

    }
}
