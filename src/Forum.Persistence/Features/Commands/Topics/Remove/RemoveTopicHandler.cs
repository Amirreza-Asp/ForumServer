using Forum.Application.Services;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Topics.Remove
{
    public class RemoveTopicHandler : IRequestHandler<RemoveTopicCommand>
    {
        private readonly ILogger<RemoveTopicHandler> _logger;
        private readonly IUserAccessor _userAccessor;
        private readonly AppDbContext _context;

        public RemoveTopicHandler(AppDbContext context, ILogger<RemoveTopicHandler> logger, IUserAccessor userAccessor)
        {
            _context = context;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(RemoveTopicCommand request, CancellationToken cancellationToken)
        {
            var topic =
                await _context.Topics
                    .AsNoTracking()
                    .FirstOrDefaultAsync(topic => topic.Id == request.Id, cancellationToken);

            if (topic == null)
                throw new AppException("Topic not found");


            _context.Topics.Remove(topic);

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"topic with title {topic.Title} and id {topic.Id} deleted by {_userAccessor.GetUserName()} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
