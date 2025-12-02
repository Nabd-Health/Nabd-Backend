using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Feedback
{
    public class CreateDoctorReviewRequest
    {
        [Required]
        public Guid AppointmentId { get; set; }

        // --- معايير الجودة الخمسة (Quality Metrics) ---

        [Required]
        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
        public int OverallSatisfaction { get; set; } // الرضا العام

        [Required]
        [Range(1, 5)]
        public int WaitingTime { get; set; } // وقت الانتظار

        [Required]
        [Range(1, 5)]
        public int CommunicationQuality { get; set; } // جودة التواصل/الاستماع

        [Required]
        [Range(1, 5)]
        public int ClinicCleanliness { get; set; } // النظافة

        [Required]
        [Range(1, 5)]
        public int ValueForMoney { get; set; } // القيمة مقابل السعر

        // --- المحتوى النصي ---

        [StringLength(2000, ErrorMessage = "التعليق لا يجب أن يتجاوز 2000 حرف")]
        public string? Comment { get; set; }

        // هل يريد المريض إخفاء اسمه؟
        public bool IsAnonymous { get; set; }
    }
}