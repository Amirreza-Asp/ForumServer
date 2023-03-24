using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Commands.Topics.Update
{
    public class UpdateTopicHandler : IRequestHandler<UpdateTopicCommand>
    {
        private readonly AppDbContext _context;

        public UpdateTopicHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic =
                await _context.Topics
                    .FirstOrDefaultAsync(topic => topic.Id == request.Id, cancellationToken);

            if (topic == null)
                throw new AppException("Selected topic is not found");

            topic.Title = request.Title;
            topic.CommunityId = request.CommunityId;
            topic.Content = request.Content;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
