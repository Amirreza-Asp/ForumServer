using Forum.Domain.Dtoes.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Queries.Account.RefreshTokenLogin
{
    public class RefreshTokenLoginCommand : IRequest<UserResultDto>
    {
        [Required]
        public Guid RefreshToken { get; set; }
    }
}
