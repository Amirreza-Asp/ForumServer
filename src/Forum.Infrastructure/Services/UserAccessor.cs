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
            var id = _contextAccessor.HttpContext.User.FindFirstValue(AppClaim.Id);

            if (String.IsNullOrEmpty(id))
                return Guid.Empty;

            return Guid.Parse(id);
        }

        public string GetUserName()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(AppClaim.UserName);
        }

    }
}
