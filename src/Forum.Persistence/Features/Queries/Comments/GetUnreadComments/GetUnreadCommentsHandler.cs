using AutoMapper;
using Forum.Application.Models;
using Forum.Application.Services;
using Forum.Domain.Dtoes.Comments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Comments.GetUnreadComments
{
    public class GetUnreadCommentsHandler : IRequestHandler<GetUnreadCommentsQuery, ListActionResult<UnreadCommentSummary>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetUnreadCommentsHandler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<ListActionResult<UnreadCommentSummary>> Handle(GetUnreadCommentsQuery request, CancellationToken cancellationToken)
        {
            var data =
                await _context.Comments
                    .Where(b =>
                        b.Topic.AuthorId == _userAccessor.GetId() &&
                        b.ReadByAuthor == false &&
                        b.AuthorId != _userAccessor.GetId())
                    .Include(b => b.Author)
                        .ThenInclude(b => b.Photo)
                    .Include(b => b.Topic)
                    .OrderByDescending(b => b.CreatedAt)
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync(cancellationToken);

            var total =
                await _context.Comments
                    .Where(b =>
                        b.Topic.AuthorId == _userAccessor.GetId() &&
                        b.ReadByAuthor == false &&
                        b.AuthorId != _userAccessor.GetId())
                    .CountAsync(cancellationToken);

            if (data.Any())
            {
                foreach (var item in data)
                {
                    item.ReadByAuthor = true;
                }

                await _context.SaveChangesAsync(cancellationToken);
            }

            return new ListActionResult<UnreadCommentSummary>
            {
                Data = _mapper.Map<List<UnreadCommentSummary>>(data),
                Total = total,
                Page = request.Page,
                Size = request.Size
            };
        }
    }
}
