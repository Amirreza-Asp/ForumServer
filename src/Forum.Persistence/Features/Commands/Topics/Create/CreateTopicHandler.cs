using Forum.Application.Services;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Topics.Create
{
    public class CreateTopicHandler : IRequestHandler<CreateTopicCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<CreateTopicHandler> _logger;
        private readonly IAggressionScoreService _aggressionScoreService;

        public CreateTopicHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<CreateTopicHandler> logger, IAggressionScoreService aggressionScoreService)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
            _aggressionScoreService = aggressionScoreService;
        }

        public async Task<Unit> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var titleAggScoreResult = _aggressionScoreService.Predict(request.Title);
            if (titleAggScoreResult.Prediction)
                throw new AppException("Topic title contains aggression words and cannot be added");

            var contentAggScoreResult = _aggressionScoreService.Predict(request.Content);
            if (contentAggScoreResult.Prediction)
                throw new AppException("Topic content contains aggression words and cannot be added");

            var userId = _userAccessor.GetId();

            var topic = new Topic
            {
                Id = request.Id,
                AuthorId = userId,
                CommunityId = request.CommunityId,
                Content = request.Content,
                Title = request.Title,
            };

            _context.Topics.Add(topic);

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"New topic with title {topic.Title} and id {topic.Id} created by {_userAccessor.GetUserName()} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
