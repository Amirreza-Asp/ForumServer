using Forum.Domain.Dtoes.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Account.Update
{
    public class UpdateAccountCommand : IRequest<UserResultDto>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Family { get; set; }

        [Required]
        public DateTime Age { get; set; }

        public bool isMale { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
