using Domain.Entities;
using Domain.Request;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions jwtOptions;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
                this.jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(User user)
        {
            var envKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var keyString = !string.IsNullOrEmpty(envKey) ? envKey : jwtOptions.Key;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Mail),
                new Claim (ClaimTypes.Role, user.Role),
            };

            JwtSecurityToken token = new(
             issuer: jwtOptions.Issuer,
             audience: jwtOptions.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddMinutes(jwtOptions.ExpiresInMinutes),
             signingCredentials: creds
             );

           return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
      
    }
}
