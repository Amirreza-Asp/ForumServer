using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        public IMediator Mediator
        {
            get
            {
                if (_mediator != null)
                    return _mediator;

                _mediator = HttpContext.RequestServices.GetRequiredService<IMediator>();
                return _mediator;
            }
        }
    }
}
