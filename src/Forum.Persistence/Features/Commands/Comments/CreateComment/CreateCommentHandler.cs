using Forum.Application.Services;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Comments.CreateComment
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<CreateCommentHandler> _logger;
        private readonly IAggressionScoreService _aggressionScoreService;

        public CreateCommentHandler(AppDbContext context, ILogger<CreateCommentHandler> logger, IUserAccessor userAccessor, IAggressionScoreService aggressionScoreService)
        {
            _context = context;
            _logger = logger;
            _userAccessor = userAccessor;
            _aggressionScoreService = aggressionScoreService;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_context.Topics.Any(b => b.Id == request.TopicId))
                throw new AppException("Topic not found");

            var aggScoreResult = _aggressionScoreService.Predict(request.Content);
            if (aggScoreResult.Prediction)
                throw new AppException("Your comment contains aggression words and cannot be added");


            var authorId = _userAccessor.GetId();

            var comment = new Comment
            {
                AuthorId = authorId,
                Content = request.Content,
                Id = request.Id,
                TopicId = request.TopicId
            };

            _context.Comments.Add(comment);

            if (await _context.SaveChangesAsync(cancellationToken) > 0)
                _logger.LogInformation($"User {_userAccessor.GetUserName()} add comment with id {request.Id} to topic with id {request.TopicId} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
