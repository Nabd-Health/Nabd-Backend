using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; // Doctor & Patient
using Nabd.Core.Entities.Feedback; // DoctorReview
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Medical
{
    public class Appointment : BaseEntity
    {
        // ==========================================
        // 1. The Parties (أطراف الموعد)
        // ==========================================

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        // ==========================================
        // 2. Schedule & Timing (التوقيت)
        // ==========================================

        public DateTime AppointmentDate { get; set; } // وقت بداية الموعد

        public int EstimatedDurationMinutes { get; set; } = 30;

        // وقت الوصول الفعلي (عشان نحسب وقت الانتظار Waiting Time - مهم للجودة)
        public DateTime? ActualArrivalDate { get; set; }

        // ==========================================
        // 3. Follow-up Logic (سلسلة المتابعة - من شريان)
        // ==========================================

        // لو الموعد ده "متابعة" (FollowUp)، لازم نعرف هو متابعة لأنهي كشف أصلي؟
        public Guid? PreviousAppointmentId { get; set; }
        public Appointment? PreviousAppointment { get; set; }

        // ==========================================
        // 4. Status & Lifecycle (دورة الحياة)
        // ==========================================

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        // تفاصيل الإلغاء (لتحليل أسباب إلغاء الحجوزات)
        public string? CancellationReason { get; set; }
        public bool CancelledByPatient { get; set; } // true = patient, false = clinic

        // ==========================================
        // 5. Appointment Context (تفاصيل الكشف)
        // ==========================================

        public AppointmentType Type { get; set; } = AppointmentType.ClinicVisit;

        [MaxLength(500)]
        public string? ReasonForVisit { get; set; } // الشكوى المبدئية

        [MaxLength(500)]
        public string? AdministrativeNotes { get; set; } // ملاحظات السكرتارية

        // ==========================================
        // 6. Financials (الماليات)
        // ==========================================

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsPaid { get; set; } = false;
        public string? PaymentTransactionId { get; set; }

        // ==========================================
        // 7. Outcomes (النتائج)
        // ==========================================

        // الكشف الطبي الناتج عن هذا الموعد (One-to-One)
        public ConsultationRecord? ConsultationRecord { get; set; }

        // تقييم المريض لهذا الموعد تحديداً (من شريان)
        public DoctorReview? DoctorReview { get; set; }
    }
}