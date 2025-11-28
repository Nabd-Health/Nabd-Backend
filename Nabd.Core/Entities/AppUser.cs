using Nabd.Core.Enums; // تم نقل الـ Enums إلى Shared
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Nabd.Core.Entities
{
    // ⚠️ تم وضعه public لحل خطأ "inaccessible due to its protection level"
    public class AppUser : BaseEntity
    {
        // ==========================================
        // 1. Identity Info (بيانات الهوية)
        // ==========================================

        // required: لحل تحذير CS8618. هذا يعني أن هذا الحقل يجب ملؤه دائماً.
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [NotMapped]
        public string DisplayName => $"{FirstName} {LastName}";

        [Required, EmailAddress]
        public required string Email { get; set; } // الـ [Required] للداتابيز، والـ required للكومبايلر

        public required string UserName { get; set; }

        // ==========================================
        // 2. Authentication (المصادقة)
        // ==========================================

        public required string PasswordHash { get; set; }

        // مفتاح أمني: يتغير كل ما اليوزر يغير الباسورد لطرد الجلسات القديمة
        public required string SecurityStamp { get; set; }

        // ==========================================
        // 3. Verification & Security (التوثيق والحماية) - Enterprise
        // ==========================================

        public bool EmailConfirmed { get; set; } = false;

        public string? PhoneNumber { get; set; } // قد يكون null
        public bool PhoneNumberConfirmed { get; set; } = false;

        public bool TwoFactorEnabled { get; set; } = false;

        // نظام الحظر (Lockout System)
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }

        // ==========================================
        // 4. System Role (الدور في النظام)
        // ==========================================

        public required UserType UserType { get; set; }

        public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;

        // ==========================================
        // 5. Relationships (الربط بالبيزنس)
        // ==========================================

        // ربط One-to-One: بروفايل الدكتور والمريض (قد يكون المستخدم دكتور أو مريض، أو لا شيء)
        public Doctor? DoctorProfile { get; set; }
        public Patient? PatientProfile { get; set; }

        // توكن التجديد (عشان يفضل مسجل دخول)
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}