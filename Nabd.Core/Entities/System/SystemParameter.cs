using Nabd.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.System
{
    // جدول لحفظ إعدادات النظام الديناميكية (مثل إصدارات موديل الـ AI)
    public class SystemParameter : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Key { get; set; } // مثال: "AI.Diagnosis.ModelVersion"

        [Required]
        public required string Value { get; set; } // مثال: "v1.2.0"

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}