using Forum.Application.Models;
using Forum.Application.Repositories.Communications;
using Forum.Domain.Dtoes.Topics;
using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Commands.Topics.Create;
using Forum.Persistence.Features.Commands.Topics.Remove;
using Forum.Persistence.Features.Commands.Topics.Update;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : BaseApiController
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [Route("Pagenation")]
        [HttpPost]
        public async Task<ListActionResult<TopicSummary>> Pagenation([FromBody] GridQuery query, CancellationToken cancellationToken)
        {
            return await _topicRepository.GetAllAsync<TopicSummary>(query, cancellationToken);
        }

        [Route("Find/{id}")]
        [HttpGet]
        public async Task<TopicDetails> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _topicRepository.FirstOrDefaultAsync<TopicDetails>(b => b.Id == id, cancellationToken);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTopicCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }


        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTopicCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }


        [Route("Remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] RemoveTopicCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }
    }
}
