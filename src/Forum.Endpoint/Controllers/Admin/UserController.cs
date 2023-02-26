using Forum.Application.Models;
using Forum.Application.Services;
using Forum.Domain.Dtoes.Users;
using Forum.Domain.Entities.Account;
using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Commands.Users.Create;
using Forum.Persistence.Features.Commands.Users.Remove;
using Forum.Persistence.Features.Commands.Users.Update;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IRepository<AppUser> _userRepository;

        public UserController(IRepository<AppUser> userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("Pagenation")]
        [HttpPost]
        public async Task<IActionResult> Pagenation(GridQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _userRepository.GetAllAsync<UserSummary>(query, cancellationToken));
        }

        [HttpGet("byUserName/{userName}")]
        public async Task<UserDetails> GetByUserName(String userName)
        {
            return await _userRepository.FirstOrDefaultAsync<UserDetails>(u => u.UserName == userName);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> Remove([FromQuery] RemoveUserCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }
    }
}
