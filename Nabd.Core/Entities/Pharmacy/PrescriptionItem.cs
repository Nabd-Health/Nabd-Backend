using Nabd.Core.Entities.Base;
using Nabd.Core.Enums.Medical; // AdministrationRoute
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Pharmacy
{
    public class PrescriptionItem : BaseEntity
    {
        // ==========================================
        // 1. Linkage
        // ==========================================

        public Guid PrescriptionId { get; set; }
        public virtual required Prescription Prescription { get; set; }

        public Guid MedicationId { get; set; }
        public virtual required Medication Medication { get; set; }

        // ==========================================
        // 2. Dosing Instructions (الجرعة الدقيقة للـ AI)
        // ==========================================

        [Required]
        [MaxLength(100)]
        public required string Dosage { get; set; } // الجرعة (مثال: "500mg")

        [Required]
        [MaxLength(100)]
        public required string Frequency { get; set; } // التكرار (مثال: "Every 8 hours")

        [MaxLength(50)]
        public string? Duration { get; set; } // المدة (مثال: "5 Days")

        public AdministrationRoute Route { get; set; } = AdministrationRoute.Oral; // طريقة الأخذ (فم، حقن..)

        // ==========================================
        // 3. Patient Instructions (تعليمات للمريض)
        // ==========================================

        [MaxLength(500)]
        public string? Instructions { get; set; } // (مثال: "يؤخذ بعد الأكل بساعة")

        [MaxLength(200)]
        public string? Notes { get; set; }
    }
}