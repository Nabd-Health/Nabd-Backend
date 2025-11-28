using System;
using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Doctor
{
    // هذا DTO يمثل البروفايل العام للطبيب (واجهة العرض للمريض)
    public class DoctorProfileDto
    {
        // ==================================================
        // 1. Identity & Core Info
        // ==================================================

        public required Guid Id { get; set; } // Doctor Entity ID

        public required string FullName { get; set; }

        [EmailAddress]
        public required string Email { get; set; } // يستخدم لسهولة التواصل أو كـ Unique ID

        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; } // رابط الصورة

        // ==================================================
        // 2. Professional & Expertise
        // ==================================================

        [Required]
        public required string Specialization { get; set; } // مثال: باطنة، أطفال

        [MaxLength(500)]
        public string? Bio { get; set; } // النبذة التسويقية

        public int YearsOfExperience { get; set; }

        // ==================================================
        // 3. Financials & Status
        // ==================================================

        // سعر الكشف
        public decimal ConsultationFee { get; set; }

        // مدة الكشف (مهم لتقسيم الـ Slots في صفحة الحجز)
        public int SessionDurationMinutes { get; set; }

        public string? City { get; set; } // للمساعدة في الفلترة الجغرافية

        // حالة الحساب (Active/Pending)
        public DoctorStatus Status { get; set; }

        // ==================================================
        // 4. Ratings & Social Proof
        // ==================================================

        public double AverageRating { get; set; } // متوسط التقييم (4.9)
        public int TotalReviews { get; set; } // عدد المقيّمين

        // ==================================================
        // 5. Schedule & Clinic Linkage
        // ==================================================

        // List of DTOs for the doctor's working locations/branches
        // public List<ClinicBranchDto> Branches { get; set; }
    }
}