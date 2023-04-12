using Forum.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Comments.HasUnreadComments
{
    public class NumberUnreadCommentsHandler : IRequestHandler<NumberUnreadCommentsQuery, int>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;

        public NumberUnreadCommentsHandler(AppDbContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<int> Handle(NumberUnreadCommentsQuery request, CancellationToken cancellationToken)
        {
            return
                await _context.Comments
                    .Where(b =>
                        b.Topic.AuthorId == _userAccessor.GetId() &&
                        b.ReadByAuthor == false &&
                        b.AuthorId != _userAccessor.GetId())
                    .CountAsync(cancellationToken);
        }
    }
}
