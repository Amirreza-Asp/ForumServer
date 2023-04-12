using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Forum.Persistence.Features.Commands.Comments.RemoveComment
{
    public class RemoveCommentHandler : IRequestHandler<RemoveCommentCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<RemoveCommentHandler> _logger;

        public RemoveCommentHandler(AppDbContext context, IUserAccessor userAccessor, ILogger<RemoveCommentHandler> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var comment =
                await _context.Comments
                    .Where(b => b.Id == request.CommentId)
                    .FirstOrDefaultAsync(cancellationToken);

            if (comment == null)
                throw new AppException("Comment not found");

            var user =
                await _context.Users
                    .Include(b => b.UserRole)
                        .ThenInclude(b => b.Role)
                    .FirstOrDefaultAsync(b => b.Id == _userAccessor.GetId(), cancellationToken);

            var role = user.UserRole.First().Role.Name;


            if (role != SD.AdminRole &&
               role != SD.SuperManagerRole &&
               comment.AuthorId != _userAccessor.GetId())
                throw new AppException("You do not have access to delete comments");

            _context.Comments.Remove(comment);

            if (await _context.SaveChangesAsync(cancellationToken) > 0)
                _logger.LogInformation($"User {_userAccessor.GetUserName()} remove comment with id {request.CommentId} at {DateTime.UtcNow}");

            return Unit.Value;
        }
    }
}
