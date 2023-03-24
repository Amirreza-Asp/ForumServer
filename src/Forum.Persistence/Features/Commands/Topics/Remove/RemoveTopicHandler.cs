using Forum.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Commands.Topics.Remove
{
    public class RemoveTopicHandler : IRequestHandler<RemoveTopicCommand>
    {
        private readonly AppDbContext _context;

        public RemoveTopicHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveTopicCommand request, CancellationToken cancellationToken)
        {
            var topic =
                await _context.Topics
                    .AsNoTracking()
                    .FirstOrDefaultAsync(topic => topic.Id == request.Id, cancellationToken);

            if (topic == null)
                throw new AppException("Topic not found");

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
