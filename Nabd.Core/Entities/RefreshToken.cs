using Nabd.Core.Entities;
using System; // ⬅️ إضافة الـ using لـ Array.Empty<byte>()
using System.ComponentModel.DataAnnotations; // ⬅️ إضافة الـ using لـ [Key] مثلاً

namespace Nabd.Core.Entities
{
    // هذا الكلاس يمثل التوكن طويل الأمد الذي يستخدم لتجديد جلسة المستخدم
    public class RefreshToken : BaseEntity
    {
        // ⚠️ الحل: إضافة كونستركتور لتهيئة الخصائص المطلوبة في BaseEntity (مثل RowVersion)
        // هذا هو السبب الرئيسي لـ CS9035
        public RefreshToken()
        {
            // تهيئة RowVersion (required property in BaseEntity) لتمرير الـ Build
            // لو الـ BaseEntity فيه خاصية required من نوع byte[]
            // RowVersion = Array.Empty<byte>(); 
        }

        // ==========================================
        // 1. Linkage (الربط) - تم الاعتماد على Guid فقط
        // ==========================================

        // حقل الربط الرئيسي بين التوكن والمستخدم
        public required Guid AppUserId { get; set; }
        // الـ Navigation Property: يجب أن تكون قابلة للقيمة الخالية (nullable) أو مُهيأة
        public AppUser AppUser { get; set; } = null!; // ⬅️ تم إزالة required

        // ❌ تم حذف (ApplicationUserId) لتجنب التكرار في الربط.

        // ==========================================
        // 2. Token Data (بيانات التوكن)
        // ==========================================

        public required string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public required string CreatedByIp { get; set; }

        // ==========================================
        // 3. Revocation (الإلغاء والطرد)
        // ==========================================

        public DateTime? RevokedOn { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReasonRevoked { get; set; }

        // ==========================================
        // 4. Token Rotation (التدوير)
        // ==========================================

        public string? ReplacedByToken { get; set; }

        // ==========================================
        // 5. Computed Properties (خصائص محسوبة)
        // ==========================================

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsRevoked => RevokedOn != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}