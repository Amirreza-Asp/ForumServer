using Forum.Domain.Entities.Account;

namespace Forum.Domain.Dtoes.Account
{
    public class UserResultDto
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String Image { get; set; }
        public String Token { get; set; }
        public Guid RefreshToken { get; set; }
        public String Role { get; set; }

        public static UserResultDto FromUser(AppUser user, String token, Guid refreshToken, String role) =>
            new UserResultDto
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Image = user.Photos.FirstOrDefault(b => b.IsMain)?.Url,
                Token = token,
                RefreshToken = refreshToken,
                Role = role
            };
    }
}
