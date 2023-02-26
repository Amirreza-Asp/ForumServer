using Forum.Application.Models;
using Forum.Application.Services;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Entities;
using Forum.Endpoint.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommunityController : ControllerBase
    {
        private readonly IRepository<Community> _communityRepository;

        public CommunityController(IRepository<Community> communityRepository)
        {
            _communityRepository = communityRepository;
        }

        [Route("Pagenation")]
        [HttpPost]
        public async Task<ListActionResult<CommunitySummary>> Pagenation([FromBody] GridQuery query, CancellationToken cancellationToken)
        {
            return await _communityRepository.GetAllAsync<CommunitySummary>(query, cancellationToken);
        }

        [HttpGet("Find/{id}")]
        public async Task<CommunityDetails> Find(Guid id, CancellationToken cancellationToken)
        {
            return await _communityRepository.FirstOrDefaultAsync<CommunityDetails>(c => c.Id == id, cancellationToken);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Community community, CancellationToken cancellationToken)
        {
            return await RequestHandler.HandleAsync(_communityRepository, _communityRepository.Create, community, cancellationToken);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Community community, CancellationToken cancellationToken)
        {
            return await RequestHandler.HandleAsync(_communityRepository, _communityRepository.Update, community, cancellationToken);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
        {
            return await RequestHandler.Remove(_communityRepository, id, cancellationToken);
        }
    }
}
