using MediatR;

namespace Forum.Persistence.Features.Commands.Comments.RemoveComment
{
    public class RemoveCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
    }
}
