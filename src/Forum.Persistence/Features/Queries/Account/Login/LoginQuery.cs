using Forum.Domain.Dtoes.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Queries.Account.Login
{
    public class LoginQuery : IRequest<UserResultDto>
    {
        [Required]
        public String UserName { get; set; }

        [Required]
        public String Password { get; set; }
    }

}
