using Nabd.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الربط بالمستخدم)
        // ==========================================
        public required Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; } = null!;

        // ==========================================
        // 2. Token Details (بيانات التوكن)
        // ==========================================
        public required string Token { get; set; }

        public DateTime ExpiresOn { get; set; }

        public required string CreatedByIp { get; set; }

        // (ملحوظة: CreatedOn موروثة من BaseEntity فمش هنكتبها تاني)

        // ==========================================
        // 3. Revocation (الإلغاء)
        // ==========================================
        public DateTime? RevokedOn { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReasonRevoked { get; set; }

        // ==========================================
        // 4. Security Rotation (التدوير - ميزة نبض)
        // ==========================================
        // التوكن الجديد الذي حل محل هذا التوكن (لسلسلة الأمان)
        public string? ReplacedByToken { get; set; }

        // ==========================================
        // 5. Computed Properties (حساب الحالة)
        // ==========================================
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

        public bool IsRevoked => RevokedOn != null;

        public bool IsActive => !IsRevoked && !IsExpired;
    }
}