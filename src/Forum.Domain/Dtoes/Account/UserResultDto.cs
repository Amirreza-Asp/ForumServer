using Forum.Domain.Entities.Account;

namespace Forum.Domain.Dtoes.Account
{
    public class UserResultDto
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Name { get; set; }
        public String Family { get; set; }
        public String Image { get; set; }
        public String Token { get; set; }
        public bool IsMale { get; set; }
        public DateTime Age { get; set; }
        public Guid RefreshToken { get; set; }
        public String Role { get; set; }

        public static UserResultDto FromUser(AppUser user, String token, Guid refreshToken, String role) =>
            new UserResultDto
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                Family = user.Family,
                IsMale = user.IsMale,
                Email = user.Email,
                Image = user.Photo != null ? user.Photo?.Url : "",
                Token = token,
                RefreshToken = refreshToken,
                Role = role
            };
    }
}
