using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Pharmacy
{
    public class PrescriptionResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Identification (الهوية)
        // ==========================================
        public string UniqueCode { get; set; } = string.Empty; // كود الروشتة (RX-...)
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; } // صلاحية الصرف
        public string Status { get; set; } = string.Empty; // "Active", "Dispensed", "Expired"

        // ==========================================
        // 2. Context (الأطراف)
        // ==========================================
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public Guid ConsultationRecordId { get; set; } // للعودة للكشف المرتبط

        // ==========================================
        // 3. Clinical Content (المحتوى الطبي)
        // ==========================================
        public string? Notes { get; set; } // تعليمات عامة

        // قائمة الأدوية (تستخدم DTO الذي أنشأناه سابقاً)
        public List<PrescriptionItemDto> Items { get; set; } = new();

        // ==========================================
        // 4. AI & Safety (الأمان)
        // ==========================================
        public bool IsReviewedByAI { get; set; } // هل مرت على الموديل؟
        public bool HasSafetyAlerts { get; set; } // هل فيها تحذيرات؟ (لعرض علامة خطر)
    }
}