using System;
using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;

namespace Nabd.Application.DTOs.Patient
{
    // هذا الكلاس هو مدخل البيانات (Input) لعمليات التحديث الجزئي لملف المريض (Settings Page)
    public class UpdatePatientProfileDto
    {
        // ==================================================
        // 1. Personal Info (Demographics)
        // ==================================================

        [MaxLength(100)]
        public string? FullName { get; set; } // نستخدم string? لأنه يمكن تحديث الاسم فقط

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        public Gender? Gender { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public string? MaritalStatus { get; set; }

        // ==================================================
        // 2. Medical & Vitals (For tracking/safety)
        // ==================================================

        public BloodType? BloodType { get; set; }

        // [Range] لضمان إدخال قيمة منطقية في الطول والوزن
        [Range(1.0, 300.0)]
        public double? Height { get; set; }

        [Range(1.0, 500.0)]
        public double? Weight { get; set; }

        // [Allergies/Chronic Diseases] تُجمع هنا لسهولة التحديث من شاشة واحدة
        [MaxLength(500)]
        public string? ChronicDiseases { get; set; }

        [MaxLength(500)]
        public string? Allergies { get; set; }

        // ==================================================
        // 3. Emergency Contact
        // ==================================================

        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        public string? EmergencyContactPhone { get; set; }

        // يمكن إضافة:
        // public string? ProfilePictureUrl { get; set; }
    }
}