using AutoMapper;
using Forum.Application.Services;
using Forum.Domain.Dtoes.Comments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Comments.GetUnreadComments
{
    public class GetUnreadCommentsHandler : IRequestHandler<GetUnreadCommentsQuery, List<CommentSummary>>
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

        public async Task<List<CommentSummary>> Handle(GetUnreadCommentsQuery request, CancellationToken cancellationToken)
        {
            var data =
                await _context.Comments
                    .Where(b =>
                        b.Topic.AuthorId == _userAccessor.GetId() &&
                        b.ReadByAuthor == false &&
                        b.AuthorId != _userAccessor.GetId())
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

            if (data.Any())
            {
                foreach (var item in data)
                {
                    item.ReadByAuthor = true;
                    _context.Comments.Update(item);
                }

                await _context.SaveChangesAsync(cancellationToken);
            }

            return _mapper.Map<List<CommentSummary>>(data);
        }
    }
}
