using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Interfaces.Repositories.Base; // ستحتاج لإنشاء هذا الملف لـ IGenericRepository

namespace Nabd.Core.Interfaces.Repositories.Identity
{
    // هذا الريبوزيتوري مسؤول عن إدارة دورة حياة Refresh Token (الخاص بالأمان)
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        // 1. استرجاع التوكن بواسطة قيمته
        Task<RefreshToken?> GetByTokenAsync(string token);

        // 2. استرجاع جميع التوكنات (المستخدمة وغير المستخدمة) لمستخدم معين
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId);

        // 3. استرجاع التوكنات النشطة فقط (غير المنتهية وغير الملغاة)
        Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId);

        // 4. إلغاء توكن محدد (Revoke)
        Task RevokeTokenAsync(string token, string? reason = null, string? revokedByIp = null);

        // 5. إلغاء جميع التوكنات الخاصة بالمستخدم (عند تغيير الباسورد مثلاً)
        Task RevokeAllUserTokensAsync(Guid userId, string? reason = null);

        // 6. تنظيف الداتابيز من التوكنات المنتهية الصلاحية
        Task DeleteExpiredTokensAsync();

        // 7. التحقق من حالة التوكن (نشط/ملغى/منتهي)
        Task<bool> IsTokenActiveAsync(string token);
    }
}