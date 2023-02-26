using Forum.Domain.Entities.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Commands.Users.Create
{
    public class CreateUserCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Family { get; set; }

        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        [Required]
        public bool IsMale { get; set; }

        [Required]
        public DateTime Age { get; set; }

        [Required]
        public String Role { get; set; }


        public AppUser ConvertToUser() =>
            new AppUser
            {
                Id = Id,
                UserName = UserName,
                Name = Name,
                Family = Family,
                Email = Email,
                PhoneNumber = PhoneNumber,
                IsMale = IsMale,
                Age = Age
            };
    }
}
