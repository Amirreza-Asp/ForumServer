using Forum.Application.Services;
using Forum.Domain;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Infrastructure.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetId()
        {
            return Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(AppClaim.Id));
        }
    }
}
