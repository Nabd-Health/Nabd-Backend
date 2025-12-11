using Nabd.Core.Entities.Base;
using Nabd.Core.Enums.Medical; 
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Pharmacy
{
    public class PrescriptionItem : BaseEntity
    {
        // ==========================================
        // 1. Linkage
        // ==========================================

        public Guid PrescriptionId { get; set; }
        public virtual required Prescription Prescription { get; set; }

        public Guid MedicationId { get; set; }
        public virtual required Medication Medication { get; set; }

        // ==========================================
        // 2. Dosing Instructions
        // ==========================================

        [Required]
        [MaxLength(100)]
        public required string Dosage { get; set; } 

        [Required]
        [MaxLength(100)]
        public required string Frequency { get; set; } 

        [MaxLength(50)]
        public string? Duration { get; set; } 

        public AdministrationRoute Route { get; set; } = AdministrationRoute.Oral; 

        // ==========================================
        // 3. Patient Instructions 
        // ==========================================

        [MaxLength(500)]
        public string? Instructions { get; set; } 

        [MaxLength(200)]
        public string? Notes { get; set; }
    }
}