using System;
using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;
using System.Text.Json.Serialization;

namespace Nabd.Application.DTOs.Patient
{
    // هذا الكلاس هو مخرج بيانات ملف المريض (يستخدم في الـ Patient Portal)
    public class PatientProfileDto
    {
        // ==========================================
        // 1. Identification & Demographics
        // ==========================================

        public required Guid Id { get; set; }

        [MaxLength(14)]
        public string? NationalId { get; set; } // يستخدم للبحث الداخلي (Lookup)

        public required string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string? PhoneNumber { get; set; }

        // خاصية محسوبة (للتسهيل على الـ Frontend)
        public int Age { get; set; }

        // ==========================================
        // 2. Medical Baseline & Alerts (الأكثر أهمية)
        // ==========================================

        public BloodType BloodType { get; set; }

        public double? Height { get; set; }
        public double? Weight { get; set; }

        // [مهم للـ AI]: الحساسية والأمراض المزمنة (للتنبيهات)
        public string? Allergies { get; set; }
        public string? ChronicDiseases { get; set; }

        // ==========================================
        // 3. Location & Emergency
        // ==========================================

        public string? Address { get; set; }
        public string? City { get; set; }

        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
    }
}