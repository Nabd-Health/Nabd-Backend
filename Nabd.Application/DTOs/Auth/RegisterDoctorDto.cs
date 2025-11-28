using System.ComponentModel.DataAnnotations;
// (يفترض وجود using Nabd.Core.Enums;)

namespace Nabd.Application.DTOs.Auth
{
    // هذا الكلاس هو مدخل البيانات (Blueprint) لإنشاء مستخدم (AppUser) وبروفايل طبيب (Doctor Entity)
    public class RegisterDoctorDto
    {
        // ==================================================
        // 1. AppUser / Identity Fields
        // ==================================================

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        // يجب أن نضمن أن الباسوورد سليم وآمن في مرحلة الـ API Validation
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        // [Compare] هو أفضل طريقة لضمان التطابق قبل أن يبدأ أي لوجيك معقد
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        // ==================================================
        // 2. Doctor Profile Fields (Mapped to Doctor Entity)
        // ==================================================

        [Required(ErrorMessage = "Specialization is required.")]
        [MaxLength(100)]
        public required string Specialization { get; set; }

        [Required(ErrorMessage = "Medical License Number is required for verification.")]
        [MaxLength(50)]
        public required string MedicalLicenseNumber { get; set; } // يُستخدم في مرحلة تفعيل الحساب لاحقاً

        // [Range] لتحديد نطاق منطقي لقيمة الخبرة
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years.")]
        public int YearsOfExperience { get; set; } = 0;

        [Required(ErrorMessage = "Consultation Fee is required.")]
        // [Range] لضمان عدم إرسال قيمة سالبة أو مبالغ فيها
        [Range(10, 10000, ErrorMessage = "Fee must be between 10 and 10000.")]
        public decimal ConsultationFee { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; } // Bio & Description for patients (Marketing)

        // Optional Fields for Future-Proofing
        public DateTime? DateOfBirth { get; set; } // لتحديد العمر بدقة

        // يمكن إضافة خاصية للـ Photo هنا إذا أردنا رفع الصورة مع التسجيل
        // public string? ProfilePictureUrl { get; set; } 
    }
}