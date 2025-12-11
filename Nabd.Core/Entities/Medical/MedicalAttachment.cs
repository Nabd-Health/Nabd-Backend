using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Enums.Operations;  
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Medical
{
    public class MedicalAttachment : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================

        public Guid PatientId { get; set; }
        public virtual required Patient Patient { get; set; }

        
        public Guid? ConsultationRecordId { get; set; }
        public virtual ConsultationRecord? ConsultationRecord { get; set; }

      
        public required string UploadedByUserId { get; set; }

        // ==========================================
        // 2. File Metadata 
        // ==========================================

        [Required]
        [MaxLength(250)]
        public required string FileName { get; set; } 

        [Required]
        public required string FileUrl { get; set; } 

        [MaxLength(50)]
        public required string FileType { get; set; } 

        public long FileSizeInBytes { get; set; } 

        // ==========================================
        // 3. Medical Context 
        // ==========================================

        public MedicalAttachmentType AttachmentType { get; set; } 

        [MaxLength(500)]
        public string? Description { get; set; } 

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // ==========================================
        // 4. AI Integration 
        // ==========================================

        public bool IsAnalyzedByAI { get; set; } = false;

        public string? AIAnalysisResultJson { get; set; }


        public string? AISummary { get; set; }
    }
}