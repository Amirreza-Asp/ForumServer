using AutoMapper;
using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Dtoes.Account;
using Forum.Domain.Entities;
using Forum.Domain.Entities.Account;
using Forum.Domain.Queries.Shared;
using Forum.Endpoint.Utility;
using Forum.Infrastructure.Services;
using Forum.Persistence.Features.Commands.Account.AddImage;
using Forum.Persistence.Features.Commands.Account.Register;
using Forum.Persistence.Features.Queries.Account.AllImages;
using Forum.Persistence.Features.Queries.Account.Login;
using Forum.Persistence.Features.Queries.Account.RefreshTokenLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers
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
            return await RequestHandler.HandleAsync(Mediator, command, cancellationToken);
        }



        [Route("Current")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Current(CancellationToken cancellationToken)
        {
            var id = _userAccessor.GetId();

            var user = await _userRepository.FirstOrDefaultAsync<AppUser>(u => u.Id == id, cancellationToken);
            return Ok(ConvertToUserResult(user));
        }


        #region images
        [Route("Image")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddImage(IFormFile command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new AddImageCommand { File = command }, cancellationToken));
        }

        [Route("Image")]
        [HttpGet]
        public async Task<IActionResult> GetImage([FromQuery] ImageQuery query, CancellationToken cancellationToken)
        {
            String upload = _hostEnv.WebRootPath;
            String path = $"{upload}{SD.UserPhotoPath}{query.Name}";
            var image = await _photoManager.ResizeAsync(path, query.Width, query.Height);
            String extension = Path.GetExtension(query.Name);

            return File(image, $"image/{extension.Substring(1)}");
        }

        [Route("AllImages")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllImages([FromQuery] AllImagesQuery query, CancellationToken cancellationToken)
        {
            return await RequestHandler.HandleAsync(Mediator, query, cancellationToken);
        }
        #endregion



        private UserResultDto ConvertToUserResult(AppUser user)
        {
            return new UserResultDto
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Image = user.Photos.FirstOrDefault()?.Url,
                Token = TokenService.Create(user)
            };
        }
    }
}
