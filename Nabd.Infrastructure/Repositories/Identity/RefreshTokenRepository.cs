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
                .Include(rt => rt.AppUser) 
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(rt => rt.AppUserId == userId) 
                .OrderByDescending(rt => rt.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            return await _dbSet
                .Where(rt => rt.AppUserId == userId
                    && rt.RevokedOn == null 
                    && rt.ExpiresOn > now) 
                .OrderByDescending(rt => rt.CreatedAt)
                .ToListAsync();
        }

        public async Task RevokeTokenAsync(string token, string? reason = null, string? revokedByIp = null)
        {
            var refreshToken = await GetByTokenAsync(token);

            if (refreshToken != null && refreshToken.RevokedOn == null)
            {
               
                refreshToken.RevokedOn = DateTime.UtcNow;
                refreshToken.ReasonRevoked = reason;
                refreshToken.RevokedByIp = revokedByIp;

                Update(refreshToken);
            }
        }

        public async Task RevokeAllUserTokensAsync(Guid userId, string? reason = null)
        {
           
            var activeTokens = await GetActiveTokensByUserIdAsync(userId);

            foreach (var token in activeTokens)
            {
                token.RevokedOn = DateTime.UtcNow;
                token.ReasonRevoked = reason ?? "Revoked by system (Logout or Password Change)";
                
                Update(token);
            }
          
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var now = DateTime.UtcNow;
            
           
            var expiredTokens = await _dbSet
                .Where(rt => rt.ExpiresOn < now)
                .ToListAsync();

            if (expiredTokens.Any())
            {
        
                _dbSet.RemoveRange(expiredTokens);
              
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