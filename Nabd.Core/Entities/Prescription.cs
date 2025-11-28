
using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities
{
    public class Prescription : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الربط)
        // ==========================================

        // الروشتة دي طلعت من أنهي كشف؟
        public Guid ConsultationRecordId { get; set; }
        public required ConsultationRecord ConsultationRecord { get; set; }

        public Guid DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public Guid PatientId { get; set; }
        public required Patient Patient { get; set; }

        // ==========================================
        // 2. Details (التفاصيل)
        // ==========================================

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; } // الروشتة صالحة لحد امتى؟

        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active; // (نشطة، تم صرفها، ملغاة)

        // ملاحظات عامة على الروشتة (مثال: "يمنع تكرار الصرف")
        [MaxLength(500)]
        public string? Notes { get; set; }

        // كود فريد للروشتة (عشان الصيدلي يبحث بيه)
        public string UniqueCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        // ==========================================
        // 3. Contents (المحتوى)
        // ==========================================

        // قائمة الأدوية اللي جوه الروشتة
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}