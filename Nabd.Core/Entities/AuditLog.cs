
using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities
{
    public class AuditLog : BaseEntity
    {
        // ==========================================
        // 1. Who? (الفاعل)
        // ==========================================

        public required string UserId { get; set; } // مين اللي عمل العملية؟
        public string? UserName { get; set; } // اسمه وقت العملية (عشان لو غير اسمه بعدين)
        public string? UserRole { get; set; } // كان دكتور ولا أدمن؟

        // ==========================================
        // 2. What? (الحدث)
        // ==========================================

        public required string EntityName { get; set; } // اسم الجدول (مثال: Doctor, Patient)
        public string? PrimaryKey { get; set; } // رقم الصف المتأثر (Record ID)

        public AuditType Action { get; set; } // نوع العملية (Create, Update, Delete)

        // ==========================================
        // 3. Details (التفاصيل - The Forensic Data)
        // ==========================================

        // القيم القديمة (JSON) - قبل التعديل
        // مثال: { "Price": 200 }
        public string? OldValues { get; set; }

        // القيم الجديدة (JSON) - بعد التعديل
        // مثال: { "Price": 300 }
        public string? NewValues { get; set; }

        // الحقول اللي اتغيرت بس (عشان مانخزنش كل حاجة)
        // مثال: "Price, PhoneNumber"
        public string? AffectedColumns { get; set; }

        // ==========================================
        // 4. Context (السياق)
        // ==========================================

        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // الساعة كام؟
        public string? IpAddress { get; set; } // من أي جهاز؟
        public string? UserAgent { get; set; } // من متصفح إيه؟ (Chrome/Mobile)
        public bool Success { get; set; } = true; // هل العملية نجحت ولا فشلت؟
    }
}