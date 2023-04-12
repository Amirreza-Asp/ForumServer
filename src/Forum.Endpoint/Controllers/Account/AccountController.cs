using AutoMapper;
using Forum.Application.Repositories;
using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Entities.Account;
using Forum.Domain.Queries.Shared;
using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Commands.Account.ChangeImage;
using Forum.Persistence.Features.Commands.Account.Register;
using Forum.Persistence.Features.Commands.Account.Update;
using Forum.Persistence.Features.Queries.Account.Login;
using Forum.Persistence.Features.Queries.Account.RefreshTokenLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IPhotoManager _photoManager;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IUserAccessor userAccessor, IRepository<AppUser> userRepository, IWebHostEnvironment hostEnv, IPhotoManager photoManager, IRepository<RefreshToken> refreshTokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _hostEnv = hostEnv;
            _photoManager = photoManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginQuery model, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(model, cancellationToken);
        }


        [HttpGet]
        [Route("RefreshTokenLogin")]
        public async Task<IActionResult> Login([FromQuery] RefreshTokenLoginCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }


        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        #region images
        [HttpPost]
        [Route("ChangePhoto")]
        [Authorize]
        public async Task<IActionResult> ChangePhoto([FromForm] ChangeImageCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [Route("Image")]
        [HttpGet]
        public async Task<IActionResult> GetImage([FromQuery] ImageQuery query, CancellationToken cancellationToken)
        {
            string upload = _hostEnv.WebRootPath;
            string path = $"{upload}{SD.UserPhotoPath}{query.Name}";
            var image = await _photoManager.ResizeAsync(path, query.Width, query.Height, cancellationToken);
            string extension = Path.GetExtension(query.Name);

            return File(image, $"image/{extension.Substring(1)}");
        }

        #endregion

    }
}
