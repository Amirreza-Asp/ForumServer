using AutoMapper;
using Forum.Application.Services;
using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Users.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IUserAccessor _userAccessor;

        public CreateUserHandler(UserManager<AppUser> userManager, IMapper mapper, ILogger<CreateUserHandler> logger, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ConvertToUser();

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = String.Join(',', result.Errors.Select(u => u.Description));
                throw new AppException(errors);
            }


            var roleResult = await _userManager.AddToRoleAsync(user, request.Role);

            if (!roleResult.Succeeded)
            {
                var errors = String.Join(',', roleResult.Errors.Select(u => u.Description));
                throw new AppException(errors);
            }

            _logger.LogInformation($"Created new User with userName : {request.UserName} by {_userAccessor.GetUserName()} at {DateTime.UtcNow} with role : {request.Role}");
            return Unit.Value;
        }
    }
}
