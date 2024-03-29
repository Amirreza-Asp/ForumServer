﻿using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Entities.Communications;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Communtieis.Create
{
    public class CreateCommunityHandler : IRequestHandler<CreateCommunityCommand>
    {
        private readonly IPhotoManager _photoManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostEnv;
        private readonly ILogger<CreateCommunityHandler> _logger;
        private readonly IUserAccessor _userAccessor;

        public CreateCommunityHandler(AppDbContext context, IPhotoManager photoManager, IHostingEnvironment hostEnv, ILogger<CreateCommunityHandler> logger, IUserAccessor userAccessor)
        {
            _context = context;
            _photoManager = photoManager;
            _hostEnv = hostEnv;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = new Community
            {
                Id = request.Id,
                Description = request.Description,
                CreateAt = DateTime.UtcNow,
                Title = request.Title,
                Image = Guid.NewGuid() + request.ImageExtension,
                Icon = Guid.NewGuid() + ".png",
            };

            _context.Communities.Add(community);

            var upload = _hostEnv.WebRootPath;
            String imgPath = upload + SD.CommunityPhotoPath + community.Image;
            String iconPath = upload + SD.CommunityIconPath + community.Icon;


            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                await _photoManager.SaveFromBase64Async(request.Image, imgPath, cancellationToken);
                await _photoManager.SaveFromBase64Async(request.Icon, iconPath, cancellationToken);

                _logger.LogInformation($"Community {community.Title} created in {DateTime.UtcNow} by {_userAccessor.GetUserName()}");
            }

            return Unit.Value;
        }
    }
}
