using Nabd.Core.Entities.Base;
using Nabd.Core.Enums; // عشان MedicationForm
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Pharmacy
{
    public class Medication : BaseEntity
    {
        // ==========================================
        // 1. Identification (هوية الدواء)
        // ==========================================

        [Required]
        [MaxLength(150)]
        public required string TradeName { get; set; } // الاسم التجاري (Panadol)

        [Required]
        [MaxLength(150)]
        // [مهم جداً للـ AI]: الموديل بيقارن المواد الفعالة عشان يكتشف التعارض
        public required string ScientificName { get; set; } // المادة الفعالة (Paracetamol)

        [MaxLength(100)]
        public string? Manufacturer { get; set; } // الشركة المصنعة

        // ==========================================
        // 2. Specifications (المواصفات)
        // ==========================================

        [Required]
        [MaxLength(50)]
        public required string Strength { get; set; } // التركيز (500mg, 1g)

        [Required]
        public required MedicationForm Form { get; set; } // الشكل (أقراص، شراب، حقن)

        // الباركود العالمي (عشان لو هنربط مع صيدليات خارجية أو Scan بالكامل)
        [MaxLength(50)]
        public string? Barcode { get; set; }

        // ==========================================
        // 3. Info & Metadata
        // ==========================================

        [MaxLength(1000)]
        public string? Description { get; set; } // وصف الاستخدام / دواعي الاستعمال

        [MaxLength(500)]
        public string? Contraindications { get; set; } // موانع الاستعمال (نصي)

        public bool IsActive { get; set; } = true; // هل الدواء متاح في السوق؟

        // السعر الاسترشادي (اختياري)
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ReferencePrice { get; set; }

        // ==========================================
        // 4. Relationships
        // ==========================================

        // عشان نعرف الدواء ده اتوصف كام مرة في السيستم
        public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
    }
}