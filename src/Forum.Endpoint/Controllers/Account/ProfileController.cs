using Forum.Application.Models;
using Forum.Application.Repositories;
using Forum.Domain.Dtoes.Profile;
using Forum.Domain.Entities.Account;
using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Queries.Account.AllImages;
using Forum.Persistence.Features.Queries.Profile.ProfileDetails;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseApiController
    {
        private readonly IRepository<AppUser> _userRepository;

        public ProfileController(IRepository<AppUser> userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("Pagenation")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] GridQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _userRepository.GetAllAsync<AppUser>(query, cancellationToken));
        }

        [Route("Details")]
        [HttpGet]
        public async Task<ProfileDetails> Get([FromQuery] ProfileDetailsQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.Send(query, cancellationToken);
        }


        [Route("AllImages")]
        [HttpGet]
        public async Task<IActionResult> AllImages([FromQuery] AllImagesQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }
    }
}
