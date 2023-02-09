using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WPSUR.Services.Constants;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Services
{
    public sealed class TokenService : ITokenService
    {
        public TokenService()
        {
        }

        public string GetToken(User user)
        {
            JwtSecurityToken jwt = new(
                issuer: AuthenticationOptions.ISSUER,
                audience: AuthenticationOptions.AUDIENCE,
                claims: GetClaims(user),
                expires: AuthenticationOptions.GetExpirationDate(),
                signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            JwtSecurityTokenHandler tokenHandler = new();
            return tokenHandler.WriteToken(jwt);
        }

        private IReadOnlyCollection<Claim> GetClaims(User user)
            => new List<Claim>
                {
                    new Claim(ClaimTypes.UserData, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                };
    }
}
