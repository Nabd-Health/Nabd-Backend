using Nabd.Core.Entities.Base;
using Nabd.Core.Enums; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Pharmacy
{
    public class Medication : BaseEntity
    {
        // ==========================================
        // 1. Identification 
        // ==========================================

        [Required]
        [MaxLength(150)]
        public required string TradeName { get; set; } 

        [Required]
        [MaxLength(150)]
      
        public required string ScientificName { get; set; } 

        [MaxLength(100)]
        public string? Manufacturer { get; set; } 

        // ==========================================
        // 2. Specifications
        // ==========================================

        [Required]
        [MaxLength(50)]
        public required string Strength { get; set; } 

        [Required]
        public required MedicationForm Form { get; set; } 

      
        [MaxLength(50)]
        public string? Barcode { get; set; }

        // ==========================================
        // 3. Info & Metadata
        // ==========================================

        [MaxLength(1000)]
        public string? Description { get; set; } 

        [MaxLength(500)]
        public string? Contraindications { get; set; } 

        public bool IsActive { get; set; } = true;

       
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ReferencePrice { get; set; }

        // ==========================================
        // 4. Relationships
        // ==========================================

  
        public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
    }
}