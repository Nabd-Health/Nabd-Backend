using Nabd.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.System
{
    public class SystemParameter : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Key { get; set; } // مفتاح الإعداد (مثال: "MaxDailyAppointments")

        [Required]
        [MaxLength(500)]
        public required string Value { get; set; } // القيمة (مثال: "50")

        [MaxLength(200)]
        public string? Description { get; set; } // شرح للإعداد (عشان الأدمن يفهم ده بيعمل إيه)

        public string? Group { get; set; } // لتجميع الإعدادات (مثال: "AI_Settings", "Finance")
    }
}