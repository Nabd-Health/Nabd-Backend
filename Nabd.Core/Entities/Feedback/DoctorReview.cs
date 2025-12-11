using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Medical; 
using Nabd.Core.Entities.Profiles; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Feedback
{
    public class DoctorReview : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================

      
        public Guid AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;

        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;

        // ==========================================
        // 2. Ratings 
        // ==========================================

        [Range(1, 5)]
        public int OverallSatisfaction { get; set; } 

        [Range(1, 5)]
        public int WaitingTime { get; set; } 

        [Range(1, 5)]
        public int CommunicationQuality { get; set; } 

        [Range(1, 5)]
        public int ClinicCleanliness { get; set; } 

        [Range(1, 5)]
        public int ValueForMoney { get; set; } 

        // ==========================================
        // 3. Text Feedback 
        // ==========================================

        [MaxLength(500)]
        public string? Comment { get; set; }

        public bool IsAnonymous { get; set; } = false; 
        public bool IsEdited { get; set; } = false; 

        // ==========================================
        // 4. Doctor Response 
        // ==========================================

        [MaxLength(300)]
        public string? DoctorReply { get; set; } 
        public DateTime? DoctorRepliedAt { get; set; }

        // ==========================================
        // 5. Computed 
        // ==========================================

        // متوسط التقييم لهذا الكشف (مش بيتخزن في الداتابيز، بيتحسب وقتي)
        [NotMapped]
        public double AverageRating => (OverallSatisfaction + WaitingTime + CommunicationQuality + ClinicCleanliness + ValueForMoney) / 5.0;
    }
}