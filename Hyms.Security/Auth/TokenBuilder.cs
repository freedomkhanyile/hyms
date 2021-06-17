using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Hyms.Security.Auth
{
    public class TokenBuilder : ITokenBuilder
    {
 
        public string Build(string name, string[] roles, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            ClaimsIdentity identity = new ClaimsIdentity(
             new GenericIdentity(name, "Bearer"),
             claims);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOptions.Issuer,
                Audience = TokenAuthOptions.Audience,
                SigningCredentials = TokenAuthOptions.SigningCredentials,
                Subject = identity,
                Expires = expireDate
            });

            return handler.WriteToken(securityToken);
        }
    }
}
