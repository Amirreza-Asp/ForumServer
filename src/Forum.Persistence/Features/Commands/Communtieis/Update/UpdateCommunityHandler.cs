using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Commands.Communtieis.Update
{
    public class UpdateCommunityHandler : IRequestHandler<UpdateCommunityCommand>
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostEnv;
        private readonly IPhotoManager _photoManager;

        public UpdateCommunityHandler(AppDbContext context, IHostingEnvironment hostEnv, IPhotoManager photoManager)
        {
            _context = context;
            _hostEnv = hostEnv;
            _photoManager = photoManager;
        }

        public async Task<Unit> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Communities.AnyAsync(b => b.Id == request.Id))
                throw new AppException("Community not found");

            var community =
                await _context.Communities
                    .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            community.Title = request.Title;
            community.Description = request.Description;

            var upload = _hostEnv.WebRootPath;
            String oldImgPath = upload + SD.CommunityPhotoPath + community.Image;
            String newImgName = Guid.NewGuid() + Path.GetExtension(community.Image);
            String newImgPath = upload + SD.CommunityPhotoPath + newImgName;


            if (!String.IsNullOrEmpty(request.Image))
            {
                File.Delete(oldImgPath);
                await _photoManager.SaveFromBase64Async(request.Image, newImgPath);
                community.Image = newImgName;
            }


            String oldIconPath = upload + SD.CommunityIconPath + community.Icon;
            String newIconName = Guid.NewGuid() + ".png";
            String newIconPath = upload + SD.CommunityIconPath + newIconName;

            if (!string.IsNullOrEmpty(request.Icon))
            {
                File.Delete(oldIconPath);
                await _photoManager.SaveFromBase64Async(request.Icon, newIconPath);
                community.Icon = newIconName;
            }

            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
