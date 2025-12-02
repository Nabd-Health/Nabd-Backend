using System;

namespace Nabd.Application.DTOs.Operations
{
    public class AppointmentListItemDto
    {
        public Guid Id { get; set; }

        // ============================
        // 1. Patient Info (للعرض السريع)
        // ============================
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string? PatientPhoneNumber { get; set; }
        public string? PatientProfileImageUrl { get; set; } // [إضافة مهمة للـ UI]

        // ============================
        // 2. Timing
        // ============================
        public DateTime AppointmentDate { get; set; }

        // نصيحة: اترك التنسيق (HH:mm) للـ Frontend حسب توقيت جهاز المستخدم
        // لكن لو مصمم تبعته من السيرفر، خليه string عادي والـ Mapper يملأه
        public int DurationMinutes { get; set; }

        // ============================
        // 3. Context & Status
        // ============================
        public string AppointmentType { get; set; } = string.Empty; // "كشف"، "استشارة"
        public string Status { get; set; } = string.Empty; // "مؤكد"، "ملغي"

        public string? ReasonForVisit { get; set; } // [إضافة مهمة]: "صداع مستمر"

        // ============================
        // 4. Financials
        // ============================
        public decimal Price { get; set; }
    }
}