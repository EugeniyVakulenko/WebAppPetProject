using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Domain.Interfaces
{
    public interface IJwtFactory
    {
        JwtSecurityToken GenerateDecodedToken(string token);
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        Task<ClaimsIdentity> GenerateClaimsIdentity(User user);
        string GetUserIdClaim(string token);
        string GetUserRoleClaim(string token);
    }
}
