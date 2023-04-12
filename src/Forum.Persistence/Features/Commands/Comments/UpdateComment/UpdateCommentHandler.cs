using Forum.Application.Services;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Comments.UpdateComment
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<UpdateCommentHandler> _logger;
        private readonly IAggressionScoreService _aggressionScoreService;

        public UpdateCommentHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<UpdateCommentHandler> logger, IAggressionScoreService aggressionScoreService)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
            _aggressionScoreService = aggressionScoreService;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var aggScoreResult = _aggressionScoreService.Predict(request.Content);
            if (aggScoreResult.Prediction)
                throw new AppException("Your comment contains aggression words and cannot be updated");


            var comment =
                await _context.Comments
                    .Where(b => b.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (comment == null)
                throw new AppException("Comment not found");

            comment.Content = request.Content;
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
                _logger.LogInformation($"User {_userAccessor.GetUserName()} update comment with id {request.Id} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
