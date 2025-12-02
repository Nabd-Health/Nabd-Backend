using Nabd.Application.DTOs.Medical; // لاستخدام MedicalHistoryItemResponse و ConsultationRecordSummaryDto
using Nabd.Application.DTOs.Pharmacy; // لاستخدام PrescriptionResponse
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; // BloodType, Gender
using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Profiles
{
    /// <summary>
    /// الملف الطبي الكامل للمريض (360-Degree View)
    /// يُستخدم في صفحة الطبيب عند فتح ملف المريض.
    /// </summary>
    public class PatientFullProfileDto
    {
        // ==========================================
        // 1. Personal & Demographic Data
        // ==========================================
        public Guid PatientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }

        // البيانات الديموغرافية (مهمة للتشخيص)
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string? JobTitle { get; set; }
        public string? City { get; set; }

        // ==========================================
        // 2. Vital Statistics (AI Features)
        // ==========================================
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public double? Weight { get; set; }
        public double? Height { get; set; }

        // ==========================================
        // 3. Medical History (Timeline)
        // ==========================================

        // (يستخدم MedicalHistoryItemResponse الموجود في DTOs.Medical)
        public List<MedicalHistoryItemResponse> MedicalHistory { get; set; } = new();

        // ==========================================
        // 4. Clinical History (السجلات والروشتات)
        // ==========================================

        // آخر الروشتات التي صرفها المريض (لمعرفة الأدوية الحالية)
        public List<PrescriptionResponse> RecentPrescriptions { get; set; } = new();

        // تاريخ الزيارات السابقة (لمتابعة تطور الحالة)
        // (يستخدم ConsultationRecordSummaryDto الموجود في DTOs.Medical)
        public List<ConsultationRecordSummaryDto> PastVisits { get; set; } = new();
    }
}