using Forum.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Utility
{
    public static class RequestHandler
    {

        public static async Task<IActionResult> HandleAsync<TEntity>(IRepository<TEntity> repository, Action<TEntity> action, TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            try
            {
                action(entity);
                var result = await repository.SaveAsync(cancellationToken);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        public static async Task<IActionResult> Remove<TEntity>(IRepository<TEntity> repository, object id, CancellationToken cancellationToken) where TEntity : class
        {
            try
            {
                repository.Remove(id);
                var result = await repository.SaveAsync(cancellationToken);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        public static async Task<IActionResult> HandleAsync(this IMediator mediator, IRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(request, cancellationToken);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public static async Task<IActionResult> HandleAsync<TResult>(this IMediator mediator, IRequest<TResult> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(request, cancellationToken);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
