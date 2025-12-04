using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Interfaces.Repositories.Identity;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Identity
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(NabdDbContext context) : base(context)
        {
        }

        // ==========================================
        // I. Authentication & Identity
        // ==========================================

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserNameOrEmailTakenAsync(string userName, string email)
        {
            // نتحقق إذا كان الاسم أو الإيميل موجودين مسبقاً
            return await _dbSet.AnyAsync(u => u.UserName == userName || u.Email == email);
        }

        public async Task<AppUser?> GetByIdWithRefreshTokensAsync(Guid userId)
        {
            // بنحتاج التوكنز هنا عشان عملية الـ Token Rotation أو الـ Revoke
            return await _dbSet
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<AppUser?> GetByIdWithProfileAsync(Guid userId)
        {
            // بنعمل Include للإثنين، والـ EF Core ذكي كفاية هيجيب اللي موجود فيهم بس (لان العلاقة Nullable)
            return await _dbSet
                .Include(u => u.DoctorProfile)
                .Include(u => u.PatientProfile)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        // ==========================================
        // II. System Management & Roles
        // ==========================================

        public async Task<IEnumerable<AppUser>> GetUsersByTypeAsync(UserType userType)
        {
            return await _dbSet
                .Where(u => u.UserType == userType)
                .ToListAsync();
        }

        public async Task<string?> GetSecurityStampAsync(Guid userId)
        {
            // Select بتجيب العمود ده بس من الداتابيز، وده أسرع بكتير
            return await _dbSet
                .Where(u => u.Id == userId)
                .Select(u => u.SecurityStamp)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetPendingVerificationUsersAsync()
        {
            // المستخدمين الذين لم يفعلوا بريدهم الإلكتروني بعد
            return await _dbSet
                .Where(u => !u.EmailConfirmed)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }
    }
}