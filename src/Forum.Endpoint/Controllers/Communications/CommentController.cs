using Forum.Endpoint.Utility;
using Forum.Persistence.Features.Commands.Comments.AddCommentReaction;
using Forum.Persistence.Features.Commands.Comments.CreateComment;
using Forum.Persistence.Features.Commands.Comments.RemoveComment;
using Forum.Persistence.Features.Commands.Comments.UpdateComment;
using Forum.Persistence.Features.Queries.Comments.CommentsPagenation;
using Forum.Persistence.Features.Queries.Comments.GetUnreadComments;
using Forum.Persistence.Features.Queries.Comments.HasUnreadComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers.Communications
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : BaseApiController
    {

        [Route("Pagenation")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Pagenation([FromBody] CommentsPagenationQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [Route("UpsertReaction")]
        [HttpPut]
        public async Task<IActionResult> UpsertReaction([FromBody] UpsertCommentReactionCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

        [Route("NumberUnreadComments")]
        [HttpGet]
        public async Task<IActionResult> NumberUnreadComments([FromQuery] NumberUnreadCommentsQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("GetUnreadComments")]
        [HttpGet]
        public async Task<IActionResult> GetUnreadComments([FromQuery] GetUnreadCommentsQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(query, cancellationToken);
        }

        [Route("Remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] RemoveCommentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.HandleAsync(command, cancellationToken);
        }

    }
}
