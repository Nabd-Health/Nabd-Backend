using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;
using System;

namespace Nabd.Application.DTOs.Auth
{
    // هذا الكلاس هو مدخل البيانات لإنشاء مستخدم (AppUser) وبروفايل مريض (Patient Entity)
    public class RegisterPatientDto
    {
        // ==================================================
        // 1. User/Authentication Fields (Mapped to AppUser)
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

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        // ==================================================
        // 2. Patient Profile Fields (Mapped to Patient Entity)
        // ==================================================

        [Required(ErrorMessage = "National ID is required for healthcare records.")]
        // ضمان أن يكون الرقم القومي 14 خانة بالضبط (Standard Egyptian ID)
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be exactly 14 characters.")]
        public required string NationalId { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public required DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public required Gender Gender { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        // Medical Baseline (مهم للـ AI والتشخيص الأولي)
        public BloodType BloodType { get; set; } = BloodType.Unknown; // فصيلة الدم

        [MaxLength(500, ErrorMessage = "Allergies notes are too long.")]
        public string? Allergies { get; set; } // سجل الحساسية (حقل نصي لتسجيل ملاحظات الـ AI)

        // Emergency Contact Info (ضرورية في الطوارئ)
        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        public string? EmergencyContactPhone { get; set; }

        // يمكن إضافة:
        // public string? InsuranceProvider { get; set; }
    }
}