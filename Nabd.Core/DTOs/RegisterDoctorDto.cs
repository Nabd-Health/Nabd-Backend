using Nabd.Core.Enums; // عشان Governorate و Gender
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; // عشان MedicalSpecialty
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;


namespace Nabd.Core.DTOs
{
    public class RegisterDoctorDto
    {
        // =======================================================
        // 1. بيانات الحساب (Account Info)
        // =======================================================

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم العائلة مطلوب")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [MinLength(6, ErrorMessage = "كلمة المرور يجب أن تكون 6 أحرف على الأقل")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public Gender Gender { get; set; } // ذكر/أنثى

        // =======================================================
        // 2. البيانات المهنية (Professional Info)
        // =======================================================

        [Required(ErrorMessage = "التخصص الطبي مطلوب")]
        public MedicalSpecialty Specialization { get; set; }

        [Required(ErrorMessage = "رقم ترخيص مزاولة المهنة مطلوب")]
        public string MedicalLicenseNumber { get; set; } = string.Empty; // مهم للتوثيق

        public string? Bio { get; set; } // نبذة عن الدكتور

        public int YearsOfExperience { get; set; } // عدد سنوات الخبرة

        // =======================================================
        // 3. بيانات العيادة الأساسية (Primary Clinic Info)
        // =======================================================
        [Required(ErrorMessage = "اسم العيادة مطلوب")]
        public string ClinicName { get; set; } = string.Empty;
        [Required(ErrorMessage = "سعر الكشف مطلوب")]
        [Range(0, 10000, ErrorMessage = "سعر الكشف يجب أن يكون منطقياً")]
        public decimal ConsultationFee { get; set; }

        [Required(ErrorMessage = "عنوان العيادة مطلوب")]
        public string ClinicAddress { get; set; } = string.Empty;

        public string? City { get; set; } // المدينة (مثل: الزقازيق)

        public Governorate Governorate { get; set; } // المحافظة (مثل: الشرقية)
    }
}