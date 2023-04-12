using AutoMapper;
using Forum.Application.Models;
using Forum.Application.Services;
using Forum.Domain.Dtoes.Comments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Comments.CommentsPagenation
{
    public class CommentsPagenationHandler : IRequestHandler<CommentsPagenationQuery, ListActionResult<CommentSummary>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public CommentsPagenationHandler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<ListActionResult<CommentSummary>> Handle(CommentsPagenationQuery request, CancellationToken cancellationToken)
        {
            var data =
                await _context.Comments
                    .Where(b => b.TopicId == request.TopicId)
                    .OrderBy(b => b.CreatedAt)
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

            var authorId =
                await _context.Topics
                    .Where(b => b.Id == request.TopicId)
                    .Select(b => b.AuthorId)
                    .FirstOrDefaultAsync(cancellationToken);


            if (_userAccessor.GetId() == authorId && data.Any(b => !b.ReadByAuthor))
            {
                foreach (var item in data)
                {
                    var statusBeforeUpdate = item.ReadByAuthor;
                    item.ReadByAuthor = true;

                    if (!statusBeforeUpdate)
                        _context.Comments.Update(item);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }

            var total =
                await _context.Comments
                    .Where(b => b.TopicId == request.TopicId)
                    .CountAsync(cancellationToken);

            return new ListActionResult<CommentSummary>
            {
                Data = _mapper.Map<List<CommentSummary>>(data),
                Total = total,
                Size = request.Size,
                Page = request.Page
            };
        }
    }
}
