using Nabd.Core.Enums; // Gender
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; // BloodType
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Identity
{
    public class RegisterPatientDto
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
        // 2. Personal Profile (Patient Entity)
        // ==========================================

        [Required]
        public required string NationalId { get; set; } // ضروري لربط السجلات

        [Required]
        public DateTime DateOfBirth { get; set; } // لحساب العمر (مهم للتشخيص)

        [Required]
        public Gender Gender { get; set; } // (Male/Female)

        // ==========================================
        // 3. Medical Baseline (AI Initial Input)
        // ==========================================
        // نطلب هذه البيانات عند التسجيل لنعطي الـ AI "صورة مبدئية" عن المريض

        public BloodType BloodType { get; set; } = BloodType.Unknown;

        // أمراض مزمنة (مثل: السكر، الضغط) - نص حر
        public string? ChronicDiseases { get; set; }

        // حساسية (مثل: البنسلين، الفراولة) - نص حر
        public string? Allergies { get; set; }
    }
}