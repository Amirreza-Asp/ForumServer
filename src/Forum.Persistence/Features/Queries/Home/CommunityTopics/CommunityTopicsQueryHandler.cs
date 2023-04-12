using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Application.Models;
using Forum.Application.Utility;
using Forum.Domain.Dtoes.Topics;
using Forum.Domain.Entities.Communications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Persistence.Features.Queries.Home.CommunityTopics
{
    public class CommunityTopicsQueryHandler : IRequestHandler<CommunityTopicsQuery, ListActionResult<TopicSummary>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CommunityTopicsQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListActionResult<TopicSummary>> Handle(CommunityTopicsQuery request, CancellationToken cancellationToken)
        {
            var queryContext = _context.Topics.AsQueryable();

            if (request.SortBy != null && request.SortBy.Length > 0)
                queryContext = queryContext.SortMeDynamically(request.SortBy, true);

            var data =
               await queryContext
                    .Where(GetExpression(request))
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ProjectTo<TopicSummary>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);



            var total = await _context.Topics.CountAsync(GetExpression(request), cancellationToken);

            return new ListActionResult<TopicSummary>
            {
                Data = data,
                Total = total,
                Size = request.Size,
                Page = request.Page
            };
        }

        private Expression<Func<Topic, bool>> GetExpression(CommunityTopicsQuery request)
        {
            return b => b.CommunityId == request.CommunityId &&
                           (request.From == default || request.From <= b.CreatedAt) &&
                           (request.To == default || request.To >= b.CreatedAt) &&
                           (String.IsNullOrEmpty(request.Title) || b.Title.Contains(request.Title)) &&
                           (String.IsNullOrEmpty(request.Author) || (b.Author.Name + " " + b.Author.Family).Contains(request.Author));
        }

    }
}
