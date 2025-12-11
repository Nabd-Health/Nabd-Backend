using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Medical;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.AI
{
    public class AIDiagnosisLog : BaseEntity
    {
        // ==========================================
        // 1. Linkage (سياق التشخيص)
        // ==========================================
        public Guid ConsultationRecordId { get; set; }
        public virtual ConsultationRecord ConsultationRecord { get; set; } = null!;

        // ==========================================
        // 2. AI Input & Output (ماذا دخل وماذا خرج)
        // ==========================================

        [Required]
        public required string InputSymptoms { get; set; } 

        [Required]
        public required string AIResponseJson { get; set; } 

        public double HighestConfidenceScore { get; set; } 

        public string ModelVersion { get; set; } = "v1.0"; 

        // ==========================================
        // 3. Doctor Feedback (حلقة التعلم)
        // ==========================================


        
        public long? ProcessingDurationMs { get; set; } 
        public DateTime RequestTimestamp { get; set; } = DateTime.UtcNow;

       
        public string? DoctorAction { get; set; } 
        public string? CorrectedDiagnosis { get; set; } 
        public string? FeedbackNotes { get; set; } 
      
        public bool? IsHelpful { get; set; } 

        public bool? WasCorrect { get; set; } 

        public string? DoctorCorrection { get; set; } 

        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    }
}