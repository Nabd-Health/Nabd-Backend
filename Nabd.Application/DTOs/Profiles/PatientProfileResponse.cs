using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; // Gender, BloodType
using System;

namespace Nabd.Application.DTOs.Profiles
{
    /// <summary>
    /// يمثل البيانات الأساسية لملف المريض (للعرض في صفحة البروفايل الشخصي)
    /// </summary>
    public class PatientProfileResponse
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; } // للربط بحساب المستخدم

        // ==========================================
        // 1. Identity & Contact
        // ==========================================
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }

        // ==========================================
        // 2. Demographics (مهمة للتشخيص)
        // ==========================================
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; } // (محسوب)
        public Gender Gender { get; set; }

        public string? City { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
        public string? MaritalStatus { get; set; }

        // ==========================================
        // 3. Medical Baseline (أساسيات طبية)
        // ==========================================
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public double? Weight { get; set; }
        public double? Height { get; set; }

        // ==========================================
        // 4. Safety & Emergency
        // ==========================================
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
    }
}