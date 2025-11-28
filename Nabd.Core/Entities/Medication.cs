using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;

namespace Nabd.Core.Entities
{
    public class Medication : BaseEntity
    {
        // ==========================================
        // 1. Drug Identity (هوية الدواء)
        // ==========================================

        [Required]
        [MaxLength(150)]
        public required string TradeName { get; set; } // الاسم التجاري (Panadol)

        [Required]
        [MaxLength(150)]
        public required string ScientificName { get; set; } // المادة الفعالة (Paracetamol) -> ده اللي الـ AI بيشتغل عليه

        [MaxLength(100)]
        public required string Manufacturer { get; set; } // الشركة المصنعة

        // ==========================================
        // 2. Specifications (المواصفات)
        // ==========================================

        public required string Strength { get; set; } // التركيز (مثال: 500mg, 1g)

        public MedicationForm Form { get; set; } // الشكل الدوائي (Enum: Tablet, Syrup, Injection)

        // كود الدواء العالمي (للمستقبل لو هنربط بصيدليات خارجية)
        public string? Barcode { get; set; }

        // ==========================================
        // 3. Metadata (بيانات إضافية)
        // ==========================================

        [MaxLength(500)]
        public string? Description { get; set; } // وصف الاستخدام العام

        public bool IsActive { get; set; } = true; // لو الدواء اتسحب من السوق نخليه false

        // العلاقة العكسية (عشان نعرف الدواء ده اتكتب كام مرة)
        public required ICollection<PrescriptionItem> PrescriptionItems { get; set; }
    }
}