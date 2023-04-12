using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Entities.Account;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Account.AddImage
{
    public class AddImageCommandHandler : IRequestHandler<AddImageCommand>
    {
        private readonly IPhotoManager _photoManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostEnv;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<AddImageCommandHandler> _logger;

        public AddImageCommandHandler(IPhotoManager photoManager, IHostingEnvironment hostEnv, AppDbContext context, IUserAccessor userAccessor, ILogger<AddImageCommandHandler> logger)
        {
            _photoManager = photoManager;
            _hostEnv = hostEnv;
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            string imagePath = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

            var photo = new UserPhoto
            {
                Id = Guid.NewGuid(),
                Name = request.File.FileName,
                Url = imagePath,
                UserId = _userAccessor.GetId()
            };

            _context.UserPhotos.Add(photo);

            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                var upload = _hostEnv.WebRootPath;

                String imgPath = upload + SD.UserPhotoPath + photo.Url;
                await _photoManager.SaveAsync(request.File, imgPath, cancellationToken);

                _logger.LogInformation($"User {_userAccessor.GetUserName()} change image at {DateTime.UtcNow}");
            }

            return Unit.Value;
        }
    }
}
