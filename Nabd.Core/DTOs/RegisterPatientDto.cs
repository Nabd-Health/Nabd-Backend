using Nabd.Core.Enums; // Gender
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; // BloodType (تأكد من وجود هذا الـ Enum)
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.DTOs
{
    public class RegisterPatientDto
    {
        // ==========================================
        // 1. بيانات الحساب (Account Info)
        // ==========================================

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم العائلة مطلوب")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [MinLength(6, ErrorMessage = "كلمة المرور يجب أن تكون 6 أحرف على الأقل")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; } = string.Empty;

        // ==========================================
        // 2. البيانات الشخصية (Patient Profile)
        // ==========================================

        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [MaxLength(14, ErrorMessage = "الرقم القومي يجب ألا يتجاوز 14 رقم")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "النوع مطلوب")]
        public Gender Gender { get; set; }

        // ==========================================
        // 3. بيانات طبية أولية (اختياري للـ AI)
        // ==========================================

        public BloodType? BloodType { get; set; } // اختياري عند التسجيل

        public string? ChronicDiseases { get; set; } // أمراض مزمنة

        public string? Allergies { get; set; } // حساسية
    }
}