using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Identity
{
    // هذا الكلاس يمثل "حساب الدخول" الموحد للنظام
    public class AppUser : BaseEntity
    {
        // ==========================================
        // 1. Identity Info (الهوية الأساسية)
        // ==========================================

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [NotMapped]
        public string DisplayName => $"{FirstName} {LastName}";

        [Required, EmailAddress, MaxLength(200)]
        public required string Email { get; set; }

        [Required]
        public required string UserName { get; set; }

        public string? ProfilePictureUrl { get; set; } // (من شريان: صورة الحساب العامة)

        // ==========================================
        // 2. Authentication (المصادقة)
        // ==========================================

        [Required]
        public required string PasswordHash { get; set; }

        // بصمة أمنية تتغير مع كل تغيير للباسورد (لطرد الجلسات القديمة)
        [Required]
        public required string SecurityStamp { get; set; }

        // ==========================================
        // 3. Security & Verification (الأمان والتوثيق)
        // ==========================================

        public bool EmailConfirmed { get; set; } = false;
        public DateTime? EmailVerifiedAt { get; set; } // (من شريان: توقيت التفعيل)

        [Phone]
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = false;

        public bool TwoFactorEnabled { get; set; } = false;

        // --- Lockout System (الحظر المؤقت) ---
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }

        // ==========================================
        // 4. Audit & Tracking (المراقبة)
        // ==========================================

        public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;
        public string? LastLoginIp { get; set; } // (من شريان: IP آخر دخول)

        // ==========================================
        // 5. OAuth Support (دخول بجوجل/فيسبوك - من شريان)
        // ==========================================

        public bool IsOAuthAccount { get; set; } = false;
        public string? OAuthProvider { get; set; } // e.g., "Google", "Facebook"
        public string? OAuthProviderId { get; set; } // User ID from Google

        // ==========================================
        // 6. System Role (الدور)
        // ==========================================

        public required UserType UserType { get; set; }

        // ==========================================
        // 7. Relationships (الربط)
        // ==========================================

        // ربط ببروفايل الدكتور أو المريض (One-to-One)
        public Doctor? DoctorProfile { get; set; }
        public Patient? PatientProfile { get; set; }

        // قائمة التوكنز (للبقاء مسجل الدخول)
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}