using Nabd.Core.Entities.Base;
using Nabd.Core.Enums; // عشان DoctorDocumentType
using Nabd.Core.Enums.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Profiles
{
    public class DoctorDocument : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الوثيقة دي تبع مين؟)
        // ==========================================

        public Guid DoctorId { get; set; }
        public virtual required Doctor Doctor { get; set; }

        // ==========================================
        // 2. Document Info (بيانات الوثيقة)
        // ==========================================

        [Required]
        // نوع الوثيقة (بطاقة، كارنيه، شهادة)
        public DoctorDocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(500)]
        // رابط الصورة اللي اترفعت
        public required string FileUrl { get; set; }

        // ==========================================
        // 3. Verification Status (حالة المراجعة)
        // ==========================================

        // هل الوثيقة دي سليمة ومقبولة؟
        public bool IsVerified { get; set; } = false;

        // لو اترفضت، ليه؟ (مثال: "الصورة مهزوزة"، "منتهية الصلاحية")
        [MaxLength(200)]
        public string? RejectionReason { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}