using Nabd.Core.Enums.Medical;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Identity
{
    public class RegisterDoctorDto
    {
        // ==========================================
        // 1. Account Info (AppUser)
        // ==========================================
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        // ==========================================
        // 2. Professional Info (Doctor Profile)
        // ==========================================
        [Required]
        public MedicalSpecialty Specialization { get; set; }

        [Required]
        public required string MedicalLicenseNumber { get; set; } // رقم الترخيص لمزاولة المهنة

        public int YearsOfExperience { get; set; }

        public string? Bio { get; set; } // نبذة مختصرة (اختياري في التسجيل)

        // ==========================================
        // 3. Operational Info (Default Clinic Branch)
        // ==========================================
        // نطلب هذه البيانات لإنشاء "الفرع الرئيسي" تلقائياً عند التسجيل

        [Required]
        public required string ClinicName { get; set; } // مثال: "عيادة الشفاء - الزقازيق"

        [Required]
        public required string ClinicAddress { get; set; }

        [Required]
        public required string City { get; set; } // مهم للبحث

        [Required]
        public decimal ConsultationFee { get; set; } // سعر الكشف المبدئي
    }
}