using Forum.Application.Models;
using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Queries.Shared;
using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Commands.Communtieis.Create;
using Forum.Persistence.Features.Commands.Communtieis.Update;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : BaseApiController
    {
        private readonly IRepository<Community> _communityRepository;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IPhotoManager _photoManager;

        public CommunityController(IRepository<Community> communityRepository, IWebHostEnvironment hostEnv, IPhotoManager photoManager)
        {
            _communityRepository = communityRepository;
            _hostEnv = hostEnv;
            _photoManager = photoManager;
        }


        [HttpPost]
        [Route("Pagenation")]
        public async Task<ListActionResult<CommunitySummary>> Pagenation([FromBody] GridQuery query, CancellationToken cancellationToken)
        {
            return await _communityRepository.GetAllAsync<CommunitySummary>(query, cancellationToken);
        }

        [HttpGet("Find/{id}")]
        public async Task<CommunityDetails> Find(Guid id, CancellationToken cancellationToken)
        {
            return await _communityRepository.FirstOrDefaultAsync<CommunityDetails>(c => c.Id == id, cancellationToken);
        }

        [HttpGet]
        [Route("Image")]
        public async Task<FileResult> Image([FromQuery] ImageQuery query, CancellationToken cancellationToken)
        {
            String upload = _hostEnv.WebRootPath;
            String path = $"{upload}{SD.CommunityPhotoPath}{query.Name}";
            var image = await _photoManager.ResizeAsync(path, query.Width, query.Height);
            String extension = Path.GetExtension(query.Name);

            return File(image, $"image/{extension.Substring(1)}");
        }

        [HttpGet("Icon")]
        public async Task<FileResult> Icon([FromQuery] ImageQuery query, CancellationToken cancellationToken)
        {
            String upload = _hostEnv.WebRootPath;
            String path = $"{upload}{SD.CommunityIconPath}{query.Name}";
            var image = await _photoManager.ResizeAsync(path, query.Width, query.Height);
            String extension = Path.GetExtension(query.Name);

            return File(image, $"image/{extension.Substring(1)}");
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommunityCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCommunityCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
        {
            return await RequestHandler.Remove(_communityRepository, id, cancellationToken);
        }
    }
}
