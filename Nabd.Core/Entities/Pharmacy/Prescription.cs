using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Medical;  
using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Enums;             
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Pharmacy
{
    public class Prescription : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================

       
        public Guid ConsultationRecordId { get; set; }
        public virtual required ConsultationRecord ConsultationRecord { get; set; }

        public Guid DoctorId { get; set; }
        public virtual required Doctor Doctor { get; set; }

        public Guid PatientId { get; set; }
        public virtual required Patient Patient { get; set; }

        // ==========================================
        // 2. Prescription Details 
        // ==========================================

      
        [Required]
        [MaxLength(20)]
        public required string UniqueCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; } 

        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active;

        // ==========================================
        // 3. Metadata & AI Context
        // ==========================================

        [MaxLength(500)]
        public string? Notes { get; set; } 

      
        public bool IsReviewedByAI { get; set; } = false;

      
        public string? AIAlertsJson { get; set; }

        // ==========================================
        // 4. Contents 
        // ==========================================

        public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
    }
}