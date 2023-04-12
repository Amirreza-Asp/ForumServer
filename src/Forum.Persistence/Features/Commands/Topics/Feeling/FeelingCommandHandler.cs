using Forum.Application.Services;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Topics.Interest
{
    public class FeelingCommandHandler : IRequestHandler<FeelingCommand>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FeelingCommandHandler> _logger;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _contextAccessor;

        public FeelingCommandHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<FeelingCommandHandler> logger, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<Unit> Handle(FeelingCommand request, CancellationToken cancellationToken)
        {
            var topic =
                await _context.Topics
                    .Where(b => b.Id == request.TopicId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

            if (topic == null)
                throw new AppException("Topic not found");



            var ftp =
                await _context.FeelingTopic
                    .Where(b => b.TopicId == request.TopicId && b.By == _userAccessor.GetUserName())
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);


            if (ftp != null)
            {
                topic = FixLikeAndDisLike(topic, ftp, request.Interest);
                ftp.Feeling = request.Interest ? Feeling.Like : Feeling.DisLike;
                _context.FeelingTopic.Update(ftp);
            }
            else
            {
                ftp = new FeelingTopic
                {
                    Id = Guid.NewGuid(),
                    By = _userAccessor.GetUserName(),
                    Feeling = request.Interest ? Feeling.Like : Feeling.DisLike,
                    TopicId = topic.Id
                };
                topic.Like += request.Interest ? 1 : 0;
                topic.DisLike += !request.Interest ? 1 : 0;

                _context.Add(ftp);
            }


            _context.Topics.Update(topic);
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                String feel = request.Interest ? "Like" : "DisLike";
                _logger.LogInformation($"User {_userAccessor.GetUserName()} {feel} topic {topic.Title} with id {topic.Id} at {DateTime.UtcNow}");
            }

            return Unit.Value;
        }

        private Topic FixLikeAndDisLike(Topic topic, FeelingTopic ftp, bool interest)
        {
            var convertedFeel = interest ? Feeling.Like : Feeling.DisLike;

            if (convertedFeel == ftp.Feeling)
                return topic;

            if (convertedFeel == Feeling.Like)
            {
                topic.DisLike--;
                topic.Like++;
            }
            else
            {
                topic.DisLike++;
                topic.Like--;
            }

            return topic;
        }
    }
}
