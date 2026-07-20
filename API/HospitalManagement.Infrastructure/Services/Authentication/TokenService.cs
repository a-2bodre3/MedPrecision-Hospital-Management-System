using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Structure;
using HospitalManagement.Domain.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalManagement.Infrastructure.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            var tokenKey = config["TokenKey"];
            if (string.IsNullOrEmpty(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey), "TokenKey is missing in appsettings.");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        }

        public string CreateToken(User user, Role role, IList<Permission> permissions, Branch branch)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("UserId",user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("FullName",$"{user.FirstName} {user.LastName}"),
            new Claim("IsActive" , user.IsActive.ToString()),
            new Claim(ClaimTypes.Role,role.Name),
            new Claim("Branch",branch.Name),
            new Claim("ImageUrl",$"https://localhost:7042/images/{user.ImageUrl}"),
        };
            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permissions", permission.Token));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
