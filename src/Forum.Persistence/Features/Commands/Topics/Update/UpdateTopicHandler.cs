using Forum.Application.Services;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Topics.Update
{
    public class UpdateTopicHandler : IRequestHandler<UpdateTopicCommand>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UpdateTopicHandler> _logger;
        private readonly IUserAccessor _userAccessor;

        public UpdateTopicHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<UpdateTopicHandler> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic =
                await _context.Topics
                    .FirstOrDefaultAsync(topic => topic.Id == request.Id, cancellationToken);

            if (topic == null)
            {
                _logger.LogWarning($"User {_userAccessor.GetUserName()} wanted to edit a topic at {DateTime.UtcNow} , " +
                    $"but topic with entered id is not found");

                throw new AppException("Selected topic is not found");
            }

            topic.Title = request.Title;
            topic.CommunityId = request.CommunityId;
            topic.Content = request.Content;

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Update topic with title {topic.Title} and id {topic.Id} by {_userAccessor.GetUserName()} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
