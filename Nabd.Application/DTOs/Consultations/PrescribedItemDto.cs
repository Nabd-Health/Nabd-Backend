using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Consultations
{
    // يمثل عنصراً واحداً في الروشتة (دواء أو تحليل)
    public class PrescribedItemDto
    {
        // 1. Identification
        public Guid? MedicationId { get; set; } // لو كان دواءً (لجلب المادة الفعالة)
        public string? ItemName { get; set; } // الاسم المدخل من الطبيب (مثال: CBC Panel)

        // 2. Dosage/Context (لتحليل الروشتات)
        public string? Dosage { get; set; } // مثال: 500mg
        public string? Frequency { get; set; } // مثال: 3 times a day
    }
}