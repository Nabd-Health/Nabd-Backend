using Nabd.Core.Entities.Base;
using Nabd.Core.Enums; 
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.System
{
    public class AuditLog : BaseEntity
    {
        // ==========================================
        // 1. Who? (الفاعل)
        // ==========================================

        public required string UserId { get; set; } 

        [MaxLength(100)]
        public string? UserName { get; set; } 

        [MaxLength(50)]
        public string? UserRole { get; set; } 

        // ==========================================
        // 2. What? 
        // ==========================================

        [Required]
        public required AuditType Action { get; set; } 

        [Required]
        [MaxLength(100)]
        public required string EntityName { get; set; } 
        [MaxLength(50)]
        public string? PrimaryKey { get; set; } 

        // ==========================================
        // 3. How? 
        // ==========================================


        public string? OldValues { get; set; }


        public string? NewValues { get; set; }

        public string? AffectedColumns { get; set; }

        // ==========================================
        // 4. Context 
        // ==========================================

        [MaxLength(50)]
        public string? IpAddress { get; set; } 

        public string? UserAgent { get; set; } 

        public string? TraceId { get; set; } 

        public bool IsSuccess { get; set; } = true; 
    }
}