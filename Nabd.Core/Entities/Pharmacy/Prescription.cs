using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Medical;  // ConsultationRecord
using Nabd.Core.Entities.Profiles; // Doctor & Patient
using Nabd.Core.Enums;             // PrescriptionStatus
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Pharmacy
{
    public class Prescription : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الروشتة دي تبع مين ومنين؟)
        // ==========================================

        // الروشتة لازم تكون نابعة من كشف (عشان الـ AI يربط التشخيص بالعلاج)
        public Guid ConsultationRecordId { get; set; }
        public virtual required ConsultationRecord ConsultationRecord { get; set; }

        public Guid DoctorId { get; set; }
        public virtual required Doctor Doctor { get; set; }

        public Guid PatientId { get; set; }
        public virtual required Patient Patient { get; set; }

        // ==========================================
        // 2. Prescription Details (بيانات الوثيقة)
        // ==========================================

        // كود فريد للروشتة (عشان المريض يصرفها من أي صيدلية بالكود ده)
        // مثال: "RX-9821-XYZ"
        [Required]
        [MaxLength(20)]
        public required string UniqueCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; } // صلاحية الروشتة (فيه أدوية جدول لازم تتصرف خلال 3 أيام)

        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active;

        // ==========================================
        // 3. Metadata & AI Context
        // ==========================================

        [MaxLength(500)]
        public string? Notes { get; set; } // ملاحظات الصيدلي أو تعليمات عامة للمريض

        // هل الـ AI راجع الروشتة دي؟ (Safety Check)
        public bool IsReviewedByAI { get; set; } = false;

        // لو الـ AI طلع تحذير (مثال: "تداخل دوائي خطير")
        public string? AIAlertsJson { get; set; }

        // ==========================================
        // 4. Contents (الأدوية)
        // ==========================================

        public virtual ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}