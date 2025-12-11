using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Feedback
{
    public class CreateDoctorReviewRequest
    {
        [Required]
        public Guid AppointmentId { get; set; }

        // ---  (Quality Metrics) ---

        [Required]
        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
        public int OverallSatisfaction { get; set; } 

        [Required]
        [Range(1, 5)]
        public int WaitingTime { get; set; } 

        [Required]
        [Range(1, 5)]
        public int CommunicationQuality { get; set; } 

        [Required]
        [Range(1, 5)]
        public int ClinicCleanliness { get; set; } 

        [Required]
        [Range(1, 5)]
        public int ValueForMoney { get; set; }
       

        [StringLength(2000, ErrorMessage = "التعليق لا يجب أن يتجاوز 2000 حرف")]
        public string? Comment { get; set; }

   
        public bool IsAnonymous { get; set; }
    }
}