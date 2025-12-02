using System;

namespace Nabd.Application.DTOs.Operations
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Timing & Schedule (التوقيت)
        // ==========================================
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }

        // [ميزة جودة]: وقت الوصول الفعلي (عشان نحسب المريض انتظر قد إيه)
        public DateTime? ActualArrivalDate { get; set; }

        // ==========================================
        // 2. Status & Type (الحالة)
        // ==========================================
        public string Status { get; set; } = string.Empty; // "Pending", "Completed", "Cancelled"
        public string Type { get; set; } = string.Empty;   // "Regular", "FollowUp"

        // لو الموعد اتلغى، بنعرض السبب
        public string? CancellationReason { get; set; }

        // ==========================================
        // 3. Patient Info (بيانات المريض - Context)
        // ==========================================
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string? PatientPhoneNumber { get; set; }
        public string? PatientProfileImageUrl { get; set; }
        public int? PatientAge { get; set; } // محسوب في الـ Mapper
        public string? PatientGender { get; set; }

        // ==========================================
        // 4. Doctor Info (بيانات الطبيب - للمريض)
        // ==========================================
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string? ClinicBranchName { get; set; } // اسم الفرع

        // ==========================================
        // 5. Clinical Context (السياق الطبي)
        // ==========================================
        public string? ReasonForVisit { get; set; } // شكوى المريض المبدئية عند الحجز

        // روابط للنتائج (عشان الـ UI يعمل زرار "View Medical Record")
        public Guid? ConsultationRecordId { get; set; }
        public bool HasPrescription { get; set; } // هل اتكتب له علاج؟

        // ==========================================
        // 6. Financials (الماليات)
        // ==========================================
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
    }
}