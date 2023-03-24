using Forum.Application.Services;
using Forum.Domain.Entities.Communications;
using MediatR;

namespace Forum.Persistence.Features.Commands.Topics.Create
{
    public class CreateTopicHandler : IRequestHandler<CreateTopicCommand>
    {
        private readonly AppDbContext _context;
        private readonly IUserAccessor _userAccessor;

        public CreateTopicHandler(AppDbContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetId();

            var topic = new Topic
            {
                Id = request.Id,
                AuthorId = userId,
                CommunityId = request.CommunityId,
                Content = request.Content,
                Title = request.Title,
            };

            _context.Topics.Add(topic);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
