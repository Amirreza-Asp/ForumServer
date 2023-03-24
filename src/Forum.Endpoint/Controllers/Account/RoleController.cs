using AutoMapper;
using Forum.Application.Repositories;
using Forum.Domain.Dtoes.Roles;
using Forum.Domain.Entities.Account;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<AppRole> _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRepository<AppRole> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<RoleDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAllAsync<RoleDto>(cancellationToken: cancellationToken);
        }
    }
}
