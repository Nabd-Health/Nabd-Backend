using System;
using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;
using System.Text.Json.Serialization;

namespace Nabd.Application.DTOs.Utility
{
    // هذا DTO يمثل سجل مراقبة (Audit Log) ويعرض ملخصًا لتغيير البيانات
    public class AuditLogDto
    {
        // ==================================================
        // 1. Identity & Action Context (من؟ ومتى؟)
        // ==================================================

        public required Guid Id { get; set; }

        [Required]
        // نوع العملية (مثال: Create, Update, Delete)
        public required AuditType Action { get; set; }

        public required DateTime Timestamp { get; set; }

        public required Guid UserId { get; set; } // الذي قام بالعملية
        public required string UserName { get; set; } // اسمه وقت العملية
        public string? UserRole { get; set; } // دوره (Doctor/Admin)

        // ==================================================
        // 2. Entity & Target (ماذا حدث وأين؟)
        // ==================================================

        [Required]
        [MaxLength(100)]
        public required string EntityName { get; set; } // مثال: Doctor, Patient, Prescription

        [MaxLength(50)]
        public string? PrimaryKey { get; set; } // ID السجل المتأثر (مثال: Guid of the Patient)

        // ==================================================
        // 3. Change Details (التفاصيل الجنائية)
        // ==================================================

        // القيمة القديمة (JSON) قبل التعديل
        public string? OldValues { get; set; }

        // القيمة الجديدة (JSON) بعد التعديل
        public string? NewValues { get; set; }

        // الأعمدة التي تم تعديلها فقط (مثال: Price, Specialization)
        public string? AffectedColumns { get; set; }

        // ==================================================
        // 4. Security & Context
        // ==================================================

        public string? IpAddress { get; set; }
        public bool Success { get; set; } = true; // هل نجحت العملية؟
    }
}