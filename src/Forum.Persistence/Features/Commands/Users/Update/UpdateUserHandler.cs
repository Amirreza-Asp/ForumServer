﻿using Forum.Application.Services;
using Forum.Domain.Entities.Account;
using Forum.Domain.Exceptions;
using Forum.Persistence.Features.Notifications.Users.ChangePassowrd;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Users.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(UserManager<AppUser> userManager, AppDbContext context, IMediator mediator, ILogger<UpdateUserHandler> logger, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _context = context;
            _mediator = mediator;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser =
                await _context.Users
                    .Where(b => b.Id == request.Id)
                    .Include(b => b.UserRole)
                        .ThenInclude(b => b.Role)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

            if (appUser == null)
                throw new AppException($"User not found");

            appUser = request.UpdateUser(appUser);

            _context.Update(appUser);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Password != "fake password")
            {
                var notif = new ChangePasswordNotif { User = appUser, Password = request.Password };
                await _mediator.Publish(notif, cancellationToken);
            }

            var userRoles = await _context.UserRoles.Where(b => b.UserId == appUser.Id).ToListAsync(cancellationToken);

            if (appUser.UserRole.First().Role.Name != request.Role)
            {
                await _userManager.RemoveFromRoleAsync(appUser, appUser.UserRole.First().Role.Name);
                await _userManager.AddToRoleAsync(appUser, request.Role);
            }

            _logger.LogInformation($"User {request.UserName} updated by {_userAccessor.GetUserName()} at {DateTime.UtcNow}");

            return Unit.Value;

        }
    }
}
