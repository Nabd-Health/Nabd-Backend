using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Nabd.Core.Entities.Identity;

namespace Nabd.Core.Interfaces
{
    public interface ITokenService
    {
        // 1. توليد التوكن الرئيسي (JWT)
        string CreateToken(AppUser user);

        // 2. توليد Refresh Token جديد
        Task<RefreshToken> GenerateRefreshToken(Guid userId, string ipAddress);

        // 3. قراءة التوكن المنتهي (للتجديد)
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        // ❌ تم حذف RenewTokenAsync و ReplaceRefreshToken 
        // لأن مكانهم الصحيح منطقياً في AuthService وليس هنا
    }
}