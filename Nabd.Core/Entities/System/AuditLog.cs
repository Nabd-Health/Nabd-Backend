using Nabd.Core.Entities.Base;
using Nabd.Core.Enums; // عشان AuditType
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.System
{
    public class AuditLog : BaseEntity
    {
        // ==========================================
        // 1. Who? (الفاعل)
        // ==========================================

        public required string UserId { get; set; } // مين عمل الحركة دي؟

        [MaxLength(100)]
        public string? UserName { get; set; } // اسمه وقت العملية (Snapshot)

        [MaxLength(50)]
        public string? UserRole { get; set; } // دوره كان إيه وقتها؟ (Doctor/Admin)

        // ==========================================
        // 2. What? (الحدث)
        // ==========================================

        [Required]
        public required AuditType Action { get; set; } // (Create, Update, Delete, Login...)

        [Required]
        [MaxLength(100)]
        public required string EntityName { get; set; } // اسم الجدول المتأثر (Doctor, Appointment)

        [MaxLength(50)]
        public string? PrimaryKey { get; set; } // ID الصف اللي اتعدل

        // ==========================================
        // 3. How? (التفاصيل الجنائية)
        // ==========================================

        // القيم القديمة قبل التعديل (JSON)
        public string? OldValues { get; set; }

        // القيم الجديدة بعد التعديل (JSON)
        public string? NewValues { get; set; }

        // أسماء الأعمدة اللي اتغيرت بس
        public string? AffectedColumns { get; set; }

        // ==========================================
        // 4. Context (السياق الأمني)
        // ==========================================

        [MaxLength(50)]
        public string? IpAddress { get; set; } // دخل منين؟

        public string? UserAgent { get; set; } // نوع المتصفح/الجهاز

        public string? TraceId { get; set; } // لتتبع العملية في الـ Logs (Correlation ID)

        public bool IsSuccess { get; set; } = true; // هل العملية نجحت ولا فشلت؟
    }
}