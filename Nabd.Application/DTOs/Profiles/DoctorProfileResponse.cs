using Nabd.Application.DTOs.Operations; // لاستخدام ClinicBranchResponse
using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Profiles
{
    /// <summary>
    /// البروفايل العام للطبيب كما يظهر للمريض في نتائج البحث أو صفحة التفاصيل
    /// </summary>
    public class DoctorProfileResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Identity & Professional Info
        // ==========================================
        public string FullName { get; set; } = string.Empty; // "د. أحمد محمد"
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; } // النبذة التعريفية

        public string Specialization { get; set; } = string.Empty; // "Cardiology" (Description)
        public int YearsOfExperience { get; set; }

        // ==========================================
        // 2. Status & Quality (الجودة)
        // ==========================================
        public bool IsVerified { get; set; } // العلامة الزرقاء
        public double AverageRating { get; set; } // 4.8
        public int TotalReviews { get; set; } // (150 Review)

        // ==========================================
        // 3. Operational Info (للحجز)
        // ==========================================
        public decimal ConsultationFee { get; set; } // السعر المبدئي (Base Fee)
        public int SessionDurationMinutes { get; set; }

        // قائمة الفروع المتاحة لهذا الطبيب (عشان المريض يختار أقرب فرع)
        public List<ClinicBranchResponse> Branches { get; set; } = new();
    }
}