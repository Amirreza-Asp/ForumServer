using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Application.Services;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Dtoes.Topics;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Exceptions;
using Forum.Domain.ViewModels.Home;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Home.FindTopic
{
    public class FindTopicQueryHandler : IRequestHandler<FindTopicQuery, TopicDetailsViewModel>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FindTopicQueryHandler(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userAccessor = userAccessor;
        }

        public async Task<TopicDetailsViewModel> Handle(FindTopicQuery request, CancellationToken cancellationToken)
        {
            var topicEntity =
                await _context.Topics
                    .Where(b => b.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (topicEntity == null)
                throw new AppException("Selected topic not found");

            topicEntity.View++;

            await _context.SaveChangesAsync(cancellationToken);

            var topic = _mapper.Map<TopicDetails>(topicEntity);

            var author =
                await _context.Users
                    .Where(b => b.Id == topic.AuthorId)
                    .ProjectTo<TopicDetailsViewModel.AuthorDetails>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

            var community =
                await _context.Communities
                    .Where(b => b.Id == topic.CommunityId)
                    .ProjectTo<CommunitySummary>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return new TopicDetailsViewModel { Topic = topic, Author = author, Community = community };

            var feelingTopic =
                await _context.FeelingTopic
                    .Where(b => b.TopicId == topic.Id && b.By == _userAccessor.GetUserName())
                    .FirstOrDefaultAsync();

            if (feelingTopic == null)
                return new TopicDetailsViewModel { Topic = topic, Author = author, Community = community };

            return new TopicDetailsViewModel
            {
                Topic = topic,
                Author = author,
                Community = community,
                Feeling = feelingTopic.Feeling == Feeling.Like ? "like" : "dislike"
            };
        }
    }
}
