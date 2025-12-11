using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Entities.Feedback; 
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Medical
{
    public class Appointment : BaseEntity
    {
        // ==========================================
        // 1. The Parties 
        // ==========================================

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        // ==========================================
        // 2. Schedule & Timing 
        // ==========================================

        public DateTime AppointmentDate { get; set; } 

        public int EstimatedDurationMinutes { get; set; } = 30;


        public DateTime? ActualArrivalDate { get; set; }

        // ==========================================
        // 3. Follow-up Logic 
        // ==========================================

       
        public Guid? PreviousAppointmentId { get; set; }
        public Appointment? PreviousAppointment { get; set; }

        // ==========================================
        // 4. Status & Lifecycle 
        // ==========================================

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

     
        public string? CancellationReason { get; set; }
        public bool CancelledByPatient { get; set; } 

        // ==========================================
        // 5. Appointment Context 
        // ==========================================

        public AppointmentType Type { get; set; } = AppointmentType.ClinicVisit;

        [MaxLength(500)]
        public string? ReasonForVisit { get; set; } 

        [MaxLength(500)]
        public string? AdministrativeNotes { get; set; } 

        // ==========================================
        // 6. Financials 
        // ==========================================

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsPaid { get; set; } = false;
        public string? PaymentTransactionId { get; set; }

        // ==========================================
        // 7. Outcomes 
        // ==========================================

      
        public ConsultationRecord? ConsultationRecord { get; set; }

        
        public DoctorReview? DoctorReview { get; set; }
    }
}