using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Interfaces.Repositories.Identity;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Identity
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbSet
                .Include(rt => rt.AppUser) // في نبض اسمها AppUser
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(rt => rt.AppUserId == userId) // في نبض اسمها AppUserId
                .OrderByDescending(rt => rt.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            return await _dbSet
                .Where(rt => rt.AppUserId == userId
                    && rt.RevokedOn == null // لم يتم إلغاؤه
                    && rt.ExpiresOn > now)  // لم تنتهِ صلاحيته
                .OrderByDescending(rt => rt.CreatedAt)
                .ToListAsync();
        }

        public async Task RevokeTokenAsync(string token, string? reason = null, string? revokedByIp = null)
        {
            var refreshToken = await GetByTokenAsync(token);

            if (refreshToken != null && refreshToken.RevokedOn == null)
            {
                // تحديث الحالة فقط (الحفظ سيتم في UnitOfWork)
                refreshToken.RevokedOn = DateTime.UtcNow;
                refreshToken.ReasonRevoked = reason;
                refreshToken.RevokedByIp = revokedByIp;

                Update(refreshToken);
            }
        }

        public async Task RevokeAllUserTokensAsync(Guid userId, string? reason = null)
        {
            // نجلب التوكنات النشطة فقط لنلغيها
            var activeTokens = await GetActiveTokensByUserIdAsync(userId);

            foreach (var token in activeTokens)
            {
                token.RevokedOn = DateTime.UtcNow;
                token.ReasonRevoked = reason ?? "Revoked by system (Logout or Password Change)";
                
                Update(token);
            }
            // ملاحظة: الحفظ SaveChanges يتم استدعاؤه في الـ Service عبر UnitOfWork
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var now = DateTime.UtcNow;
            
            // نجلب التوكنات المنتهية
            var expiredTokens = await _dbSet
                .Where(rt => rt.ExpiresOn < now)
                .ToListAsync();

            if (expiredTokens.Any())
            {
                // حذف جماعي
                _dbSet.RemoveRange(expiredTokens);
                // هنا ممكن نحتاج SaveChanges فورياً لأن دي عملية صيانة Background Job
                // لكن حسب الـ Pattern سنتركها للـ UnitOfWork إذا تم استدعاؤها من Service
            }
        }

        public async Task<bool> IsTokenActiveAsync(string token)
        {
            var now = DateTime.UtcNow;
            return await _dbSet
                .AnyAsync(rt => rt.Token == token
                    && rt.RevokedOn == null
                    && rt.ExpiresOn > now);
        }
    }
}