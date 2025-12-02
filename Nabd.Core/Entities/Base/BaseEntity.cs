using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        // 1. الهوية (Identity)
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // ==========================================
        // 2. Auditing (من شريان - AuditableEntity)
        // ==========================================

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; } // خليناها String عشان تمشي مع الـ JWT Claims

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        // ==========================================
        // 3. Soft Delete (من شريان - SoftDeletableEntity)
        // ==========================================

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        // ==========================================
        // 4. Concurrency (إضافة نبض - احترافية)
        // ==========================================

        // ده بيمنع تضارب البيانات (Optimistic Concurrency)
        [Timestamp]
        public required byte[] RowVersion { get; set; }
    }
}