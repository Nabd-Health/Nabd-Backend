using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Feedback
{
    // DTO لرد الطبيب على التقييم
    public class ReplyToReviewRequest
    {
        [Required]
        public Guid ReviewId { get; set; } // للتأكد من ربط الرد بالتقييم الصحيح

        [Required(ErrorMessage = "الرد مطلوب")]
        [StringLength(500, ErrorMessage = "الرد لا يجب أن يتجاوز 500 حرف")]
        public string Reply { get; set; } = string.Empty;
    }

    // DTO لتعديل التقييم من قبل المريض
    public class UpdateDoctorReviewRequest
    {
        // (نفس حقول التقييم في Create، لكن بدون AppointmentId لأنه لا يتغير)
        [Range(1, 5)] public int OverallSatisfaction { get; set; }
        [Range(1, 5)] public int WaitingTime { get; set; }
        [Range(1, 5)] public int CommunicationQuality { get; set; }
        [Range(1, 5)] public int ClinicCleanliness { get; set; }
        [Range(1, 5)] public int ValueForMoney { get; set; }

        [StringLength(2000)]
        public string? Comment { get; set; }

        public bool IsAnonymous { get; set; }
    }
}