using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Nabd.Core.Entities.Identity;

namespace Nabd.Application.Interfaces
{
    public interface ITokenService
    {
    
        string CreateToken(AppUser user);

      
        Task<RefreshToken> GenerateRefreshToken(Guid userId, string ipAddress);


        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

  
    }
}