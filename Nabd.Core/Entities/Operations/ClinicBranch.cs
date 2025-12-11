using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Enums;            
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Operations
{
    public class ClinicBranch : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================

        public Guid DoctorId { get; set; }
        public virtual required Doctor Doctor { get; set; }

        // ==========================================
        // 2. Branch Identity
        // ==========================================

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; } 

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; } 

        public string? LandlineNumber { get; set; } 

        // ==========================================
        // 3. Location Details
        // ==========================================
        

        public required Governorate Governorate { get; set; } 

        [Required]
        [MaxLength(50)]
        public required string City { get; set; } 

        [Required]
        [MaxLength(200)]
        public required string StreetAddress { get; set; } 

      
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; }

        // ==========================================
        // 4. Financials 
        // ==========================================

   
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CustomConsultationFee { get; set; }

        // ==========================================
        // 5. Status & Relations
        // ==========================================

        public bool IsActive { get; set; } = true;

     
        public virtual ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
    }
}