using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Entities.Account;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Account.ChangeImage
{
    public class ChangeImageHandler : IRequestHandler<ChangeImageCommand, String>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly AppDbContext _context;
        private readonly ILogger<ChangeImageHandler> _logger;
        private readonly IPhotoManager _photoManager;
        private readonly IHostingEnvironment _hostEnv;

        public ChangeImageHandler(IUserAccessor userAccessor, AppDbContext context, ILogger<ChangeImageHandler> logger, IPhotoManager photoManager, IHostingEnvironment hostEnv)
        {
            _userAccessor = userAccessor;
            _context = context;
            _logger = logger;
            _photoManager = photoManager;
            _hostEnv = hostEnv;
        }

        public async Task<String> Handle(ChangeImageCommand request, CancellationToken cancellationToken)
        {
            var user =
                await _context.Users
                    .Where(b => b.UserName == _userAccessor.GetUserName())
                    .Include(b => b.Photo)
                    .FirstOrDefaultAsync(cancellationToken);

            var lastPhotoUrl = user.Photo != null ? user.Photo.Url : null;

            user.Photo = new UserPhoto
            {
                Id = user.Photo != null ? user.Photo.Id : Guid.NewGuid(),
                Name = request.File.FileName,
                Url = Guid.NewGuid() + Path.GetExtension(request.File.FileName)
            };

            if (await _context.SaveChangesAsync() > 0)
            {
                String upload = _hostEnv.WebRootPath;

                // remove old photo
                if (!String.IsNullOrEmpty(lastPhotoUrl))
                {
                    String lastPhotoPath = upload + SD.UserPhotoPath + lastPhotoUrl;

                    if (File.Exists(lastPhotoPath))
                        _photoManager.Delete(lastPhotoPath);
                }

                // add new photo
                String newPhotoUrl = upload + SD.UserPhotoPath + user.Photo.Url;
                await _photoManager.SaveAsync(request.File, newPhotoUrl, cancellationToken);

                // log information
                String addOrChange = String.IsNullOrEmpty(lastPhotoUrl) ? "add" : "change";
                _logger.LogInformation($"User {_userAccessor.GetUserName()} {addOrChange} photo at {DateTime.UtcNow}");

                return user.Photo.Url;
            }

            return lastPhotoUrl;
        }
    }
}
