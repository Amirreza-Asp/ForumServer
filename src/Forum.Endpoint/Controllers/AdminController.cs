using Forum.Application.Repositories;
using Forum.Domain.Entities.Account;
using Forum.Domain.Entities.Communications;
using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Queries.Admin.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseApiController
    {
        private readonly IRepository<Community> _communityRepository;
        private readonly IRepository<Topic> _topicRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<AppUser> _userRepository;

        public AdminController(IRepository<Community> communityRepository, IRepository<Topic> topicRepository, IRepository<Comment> commentRepository, IRepository<AppUser> userRepository)
        {
            _communityRepository = communityRepository;
            _topicRepository = topicRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [Route("Dashboard")]
        [HttpGet]
        public async Task<IActionResult> Dashboard([FromQuery] DashboardQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("CommunitiesCount")]
        [HttpGet]
        public async Task<int> CommunitiesCount(CancellationToken cancellationToken)
        {
            return await _communityRepository.CountAsync(cancellationToken);
        }

        [Route("TopicsCount")]
        [HttpGet]
        public async Task<int> TopicsCount(CancellationToken cancellationToken)
        {
            return await _topicRepository.CountAsync(cancellationToken);
        }

        [Route("CommentsCount")]
        [HttpGet]
        public async Task<int> CommentsCount(CancellationToken cancellationToken)
        {
            return await _commentRepository.CountAsync(cancellationToken);
        }

        [Route("UsersCount")]
        [HttpGet]
        public async Task<int> UsersCount(CancellationToken cancellationToken)
        {
            return await _userRepository.CountAsync(cancellationToken);
        }
    }
}
