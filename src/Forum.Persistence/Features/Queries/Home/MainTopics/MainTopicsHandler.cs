using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Application.Models;
using Forum.Domain.Dtoes.Topics;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Home.MainTopics
{
    public class MainTopicsHandler : IRequestHandler<MainTopicsQuery, ListActionResult<TopicSummary>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MainTopicsHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListActionResult<TopicSummary>> Handle(MainTopicsQuery request, CancellationToken cancellationToken)
        {
            var size = 10;
            List<TopicSummary> data;

            if (request.Filter.ToLower() == "view")
                data =
                    await _context.Topics
                        .OrderByDescending(b => b.View)
                        .Skip((request.Page - 1) * size)
                        .Take(size)
                        .ProjectTo<TopicSummary>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
            else if (request.Filter.ToLower() == "recent")
                data =
                    await _context.Topics
                        .OrderByDescending(b => b.CreatedAt)
                        .Skip((request.Page - 1) * size)
                        .Take(size)
                        .ProjectTo<TopicSummary>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
            else if (request.Filter.ToLower() == "popular")
                data =
                    await _context.Topics
                        .OrderByDescending(b => b.Like)
                        .Skip((request.Page - 1) * size)
                        .Take(size)
                        .ProjectTo<TopicSummary>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
            else
                data =
                    await _context.Topics
                        .Skip((request.Page - 1) * size)
                        .Take(size)
                        .ProjectTo<TopicSummary>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            var total = await _context.Topics.CountAsync(cancellationToken);

            return new ListActionResult<TopicSummary>
            {
                Data = data,
                Total = total,
                Size = size,
                Page = request.Page
            };
        }

    }
}
