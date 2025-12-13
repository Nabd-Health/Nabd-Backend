using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Pharmacy; 
using Nabd.Core.Entities.AI;       
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Medical
{
    public class ConsultationRecord : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;

        // ==========================================
        // 2. (S)ubjective Data
        // ==========================================

        [Required]
    
        public required string ChiefComplaint { get; set; }

        [Required]
       
        public required string Symptoms { get; set; }

   
        public string? HistoryOfPresentIllness { get; set; }

        // ==========================================
        // 3. (O)bjective Data: (AI Features)
        // ==========================================
        

        public string? PhysicalExaminationNotes { get; set; } 

     
        public double? Temperature { get; set; }         
        public int? SystolicBloodPressure { get; set; }  
        public int? DiastolicBloodPressure { get; set; } 
        public int? HeartRate { get; set; }             
        public int? RespiratoryRate { get; set; }        
        public double? OxygenSaturation { get; set; }   
        public double? WeightAtVisit { get; set; }       

        // ==========================================
        // 4. (A)ssessment: التشخيص (AI Output / Ground Truth)
        // ==========================================

     
        public string? ProvisionalDiagnosis { get; set; }

        [Required]
     
        public required string FinalDiagnosis { get; set; }

        public string? DiagnosisCode { get; set; }

        // ==========================================
        // 5. (P)lan: الخطة العلاجية (Prescription AI Input)
        // ==========================================

        public string? TreatmentPlan { get; set; } 

        public string? PrescriptionNotes { get; set; } 

        public DateTime? RecommendedFollowUpDate { get; set; } 

        // ==========================================
        // 6. AI Metadata & Relationships
        // ==========================================

        public bool WasAIAssisted { get; set; } = false; 

      
        public virtual ICollection<AIDiagnosisLog> AIDiagnosisLogs { get; set; } = new List<AIDiagnosisLog>();

  
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        
        public virtual ICollection<MedicalAttachment> Attachments { get; set; } = new List<MedicalAttachment>();
    }
}