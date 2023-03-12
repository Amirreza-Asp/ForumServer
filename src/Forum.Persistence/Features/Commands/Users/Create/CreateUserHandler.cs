using AutoMapper;
using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Persistence.Features.Commands.Users.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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

            return Unit.Value;
        }
    }
}
