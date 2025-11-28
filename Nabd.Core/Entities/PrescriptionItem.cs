using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities
{
    public class PrescriptionItem : BaseEntity
    {
        // ==========================================
        // 1. Linkage
        // ==========================================

        public Guid PrescriptionId { get; set; }
        public required Prescription Prescription { get; set; }

        public Guid MedicationId { get; set; }
        public required Medication Medication { get; set; }

        // ==========================================
        // 2. Dosage Instructions (الجرعة - AI Fuel)
        // ==========================================

        // الجرعة (مثال: "قرص واحد", "5 مل")
        [Required]
        [MaxLength(100)]
        public required string Dosage { get; set; }

        // التكرار (مثال: "كل 8 ساعات", "3 مرات يومياً")
        [Required]
        [MaxLength(100)]
        public required string Frequency { get; set; }

        // المدة (مثال: "لمدة 5 أيام")
        [MaxLength(100)]
        public required string Duration { get; set; }

        // تعليمات خاصة (مثال: "بعد الأكل", "على معدة فارغة")
        [MaxLength(200)]
        public string? Instructions { get; set; }

        // ملاحظة: البيانات دي بنفصلها كده عشان الـ AI يقدر يحسب
        // "Total Dose" = Dosage * Frequency * Duration
        // ويقارنها بالحد الأقصى المسموح بيه
    }
}