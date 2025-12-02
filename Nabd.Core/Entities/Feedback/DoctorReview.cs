using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Medical; // عشان يشوف Appointment
using Nabd.Core.Entities.Profiles; // عشان يشوف Doctor و Patient
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Feedback
{
    public class DoctorReview : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الربط)
        // ==========================================

        // التقييم مرتبط بموعد محدد (عشان نضمن إن المريض زار الدكتور فعلاً)
        public Guid AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;

        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;

        // ==========================================
        // 2. Ratings (معايير التقييم - من 1 لـ 5)
        // ==========================================

        [Range(1, 5)]
        public int OverallSatisfaction { get; set; } // الرضا العام

        [Range(1, 5)]
        public int WaitingTime { get; set; } // وقت الانتظار

        [Range(1, 5)]
        public int CommunicationQuality { get; set; } // التواصل

        [Range(1, 5)]
        public int ClinicCleanliness { get; set; } // النظافة

        [Range(1, 5)]
        public int ValueForMoney { get; set; } // القيمة مقابل السعر

        // ==========================================
        // 3. Text Feedback (التعليق)
        // ==========================================

        [MaxLength(500)]
        public string? Comment { get; set; }

        public bool IsAnonymous { get; set; } = false; // هل يريد إخفاء اسمه؟
        public bool IsEdited { get; set; } = false; // هل تم تعديل التقييم؟

        // ==========================================
        // 4. Doctor Response (حق الرد - Enterprise Feature)
        // ==========================================

        [MaxLength(300)]
        public string? DoctorReply { get; set; } // رد الدكتور على التقييم
        public DateTime? DoctorRepliedAt { get; set; }

        // ==========================================
        // 5. Computed (حسابي)
        // ==========================================

        // متوسط التقييم لهذا الكشف (مش بيتخزن في الداتابيز، بيتحسب وقتي)
        [NotMapped]
        public double AverageRating => (OverallSatisfaction + WaitingTime + CommunicationQuality + ClinicCleanliness + ValueForMoney) / 5.0;
    }
}