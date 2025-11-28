
using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities
{
    public class Patient : BaseEntity
    {
        // ==========================================
        // 1. Identity & Verification (الهوية)
        // ==========================================
        public required string ApplicationUserId { get; set; } // الربط بحساب الدخول

        [Required]
        [MaxLength(14)] // الرقم القومي المصري 14 رقم
        public required string NationalId { get; set; } // مهم جداً لعدم تكرار ملفات المرضى

        // ==========================================
        // 2. Personal Info (البيانات الشخصية)
        // ==========================================
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; } // Enum

        [Phone]
        public required string PhoneNumber { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; } // الوظيفة (مهمة لبعض الأمراض المهنية)
        public string? MaritalStatus { get; set; } // الحالة الاجتماعية

        // ==========================================
        // 3. Medical Profile (الملف الطبي الأساسي) - وقود الـ AI
        // ==========================================

        public BloodType BloodType { get; set; } = BloodType.Unknown; // Enum (A+, O-, etc.)

        public double? Height { get; set; } // بالسنتيمتر
        public double? Weight { get; set; } // بالكيلوجرام (لحساب جرعات الأدوية)

        // تخزين الأمراض المزمنة (نصياً حالياً، أو JSON)
        // مثال: "Diabetes, Hypertension"
        public string? ChronicDiseases { get; set; }

        // تخزين الحساسية (مهم جداً لموديل الـ Prescription Analyzer)
        // مثال: "Penicillin, Peanuts"
        public string? Allergies { get; set; }

        // ملخص التاريخ الطبي القديم (للموديل)
        public string? MedicalHistorySummary { get; set; }

        // ==========================================
        // 4. Emergency Contact (الطوارئ)
        // ==========================================

        [MaxLength(100)]
        public string? EmergencyContactName { get; set; } // اسم قريب له

        [Phone]
        public string? EmergencyContactPhone { get; set; } // رقم القريب

        // ==========================================
        // 5. Insurance (التأمين - Future Proofing)
        // ==========================================

        public bool HasInsurance { get; set; } = false;
        public string? InsuranceProvider { get; set; } // اسم شركة التأمين
        public string? InsuranceNumber { get; set; }   // رقم البوليصة

        // ==========================================
        // 6. Relationships (العلاقات)
        // ==========================================

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // خاصية محسوبة (Not Mapped) لحساب العمر تلقائياً
        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
    }
}