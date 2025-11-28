
using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities
{
    public class Appointment : BaseEntity
    {
        // ==========================================
        // 1. The Parties (أطراف الموعد)
        // ==========================================

        public Guid DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public Guid PatientId { get; set; }
        public required Patient Patient { get; set; }

        // ==========================================
        // 2. Schedule & Timing (التوقيت)
        // ==========================================

        public DateTime AppointmentDate { get; set; } // وقت بداية الموعد

        // المدة المتوقعة (بتيجي من إعدادات الدكتور)
        public int EstimatedDurationMinutes { get; set; } = 30;

        // وقت الوصول الفعلي (عشان نحسب وقت الانتظار Waiting Time - مهم للجودة)
        public DateTime? ActualArrivalDate { get; set; }

        // ==========================================
        // 3. Status & Lifecycle (دورة الحياة)
        // ==========================================

        // الحالة هي أهم حقل للتحكم في السيستم
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        // لو اتلغى، مين لغاه وليه؟
        public string? CancellationReason { get; set; }
        public bool CancelledByPatient { get; set; } // true = patient, false = doctor/system

        // ==========================================
        // 4. Appointment Details (تفاصيل الكشف)
        // ==========================================

        // نوع الموعد: كشف عيادة، كشف أونلاين، استشارة مجانية
        public AppointmentType Type { get; set; } = AppointmentType.ClinicVisit;

        // السبب اللي المريض كتبه وهو بيحجز (Chief Complaint مبدئي)
        [MaxLength(500)]
        public string? ReasonForVisit { get; set; }

        // ملاحظات داخلية لموظف الاستقبال (مثلاً: المريض على كرسي متحرك)
        [MaxLength(500)]
        public string? AdministrativeNotes { get; set; }

        // ==========================================
        // 5. Financials (الماليات) - Future Proofing
        // ==========================================

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // السعر وقت الحجز (لأن الدكتور ممكن يغلي سعره بعدين)

        public bool IsPaid { get; set; } = false;

        public string? PaymentTransactionId { get; set; } // رقم العملية لو دفع أونلاين (Stripe/Paymob)

        // ==========================================
        // 6. Medical Outcome (النتيجة الطبية)
        // ==========================================

        // ربط مع الكشف الطبي (One-to-One)
        // الموعد بيخلص، بيطلع منه ConsultationRecord
        public ConsultationRecord? ConsultationRecord { get; set; }
    }
}