using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Enums.Medical;     
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Medical
{
    public class MedicalHistoryItem : BaseEntity
    {
        // ==========================================
        // 1. Linkage
        // ==========================================

        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        // ==========================================
        // 2. Event Details 
        // ==========================================

        public HistoryEventType EventType { get; set; } 

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; } 

        [MaxLength(1000)]
        public string? Details { get; set; } 

        // ==========================================
        // 3. Metadata 
        // ==========================================

        
        public DateTime EventDate { get; set; }

      
        public bool IsCritical { get; set; } = false;
    }
}