using Forum.Application.Services;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Comments.AddCommentReaction
{
    public class UpsertCommentReactionHandler : IRequestHandler<UpsertCommentReactionCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<UpsertCommentReactionHandler> _logger;

        public UpsertCommentReactionHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<UpsertCommentReactionHandler> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpsertCommentReactionCommand request, CancellationToken cancellationToken)
        {
            // get comment
            var comment =
                await _context.Comments
                    .AsNoTracking()
                    .Where(b => b.Id == request.CommentId)
                    .FirstOrDefaultAsync(cancellationToken);

            if (comment == null)
                throw new AppException("Comment not found");

            // get comment reaction
            var commentReaction =
                await _context.CommentReactions
                    .AsNoTracking()
                    .Where(b => b.CommentId == request.CommentId && b.By == _userAccessor.GetUserName())
                    .FirstOrDefaultAsync(cancellationToken);

            // create new comment reaction when comment reaction not found
            if (commentReaction == null)
            {
                commentReaction = new CommentReaction
                {
                    Id = Guid.NewGuid(),
                    By = _userAccessor.GetUserName(),
                    CommentId = request.CommentId,
                    Feeling = GetFeeling(request.Reaction)
                };
                comment.Like += GetFeeling(request.Reaction) == Feeling.Like ? 1 : 0;
                comment.DisLike += GetFeeling(request.Reaction) == Feeling.DisLike ? 1 : 0;

                _context.CommentReactions.Add(commentReaction);
            }
            else
            {
                comment = FixReaction(commentReaction, comment, request.Reaction);
                commentReaction.Feeling = GetFeeling(request.Reaction);
                _context.CommentReactions.Update(commentReaction);
            }

            _context.Comments.Update(comment);
            if (await _context.SaveChangesAsync() > 0)
                _logger.LogInformation($"User {_userAccessor.GetUserName()} add reaction {request.Reaction} to comment with id {comment.Id} at {DateTime.UtcNow}");


            return Unit.Value;
        }

        private Feeling GetFeeling(String reaction)
        {
            return reaction.ToLower() == "like" ? Feeling.Like : Feeling.DisLike;
        }

        private Comment FixReaction(CommentReaction commentReaction, Comment comment, String reaction)
        {
            var feeling = GetFeeling(reaction);

            if (commentReaction.Feeling == feeling)
                return comment;

            if (commentReaction.Feeling == Feeling.Like)
            {
                comment.Like--;
                comment.DisLike++;
            }
            else
            {
                comment.DisLike--;
                comment.Like++;
            }

            return comment;
        }
    }
}
