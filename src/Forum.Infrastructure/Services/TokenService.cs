using Forum.Domain;
using Forum.Domain.Entities.Account;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum.Infrastructure.Services
{
    public static class TokenService
    {
        public static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dklfnasdklsdjlfsdjlfnlfnsdklfnklefnsdklfnasdklfnsdjlfwofwifewfoiwf"));
        }

        public static string Create(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(AppClaim.UserName , user.UserName),
                new Claim(AppClaim.Id , user.Id.ToString())
            };

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(AppClaim.Email, user.Email));

            if (!string.IsNullOrEmpty(user.PhoneNumber))
                claims.Add(new Claim(AppClaim.PhoneNumber, user.PhoneNumber));

            var key = GetKey();
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static RefreshToken UpsertRefreshToken(AppUser user)
        {
            return new RefreshToken
            {
                Id = user.RefreshToken?.Id ?? Guid.NewGuid(),
                Token = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                Expire = 30,
                UserId = user.Id
            };
        }
    }
}
