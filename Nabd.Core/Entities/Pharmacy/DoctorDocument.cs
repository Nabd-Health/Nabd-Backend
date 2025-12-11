using Nabd.Core.Entities.Base;
using Nabd.Core.Enums; 
using Nabd.Core.Enums.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Profiles
{
    public class DoctorDocument : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================

        public Guid DoctorId { get; set; }
        public virtual required Doctor Doctor { get; set; }

        // ==========================================
        // 2. Document Info 
        // ==========================================

        [Required]
      
        public DoctorDocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(500)]
        
        public required string FileUrl { get; set; }

        // ==========================================
        // 3. Verification Status 
        // ==========================================

     
        public bool IsVerified { get; set; } = false;

        
        [MaxLength(200)]
        public string? RejectionReason { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}