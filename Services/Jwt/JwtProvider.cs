using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewsModule.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsModule.Services.Jwt
{
    public class JwtProvider
    {
        private readonly JwtOptions options = new JwtOptions();

        public string GenerateToken(User user,IEnumerable<Claim> userClaims)
        {
            var claims = userClaims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var token = new JwtSecurityToken(
                claims: claims,
                //issuer: options.issuer,
                //audience: options.audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), 
                signingCredentials: new SigningCredentials(options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));// создание токена

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
