using Forum.Domain.Dtoes.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Account.Register
{
    public class RegisterCommand : IRequest<UserResultDto>
    {
        [Required]
        public String UserName { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Family { get; set; }

        [Required]
        public DateTime Age { get; set; }

        public bool IsMale { get; set; }

        [Required]
        public String Password { get; set; }

        public String Role { get; set; }


        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        public String PhoneNumber { get; set; }
    }
}
