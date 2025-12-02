using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Pharmacy
{
    public class CreatePrescriptionRequest
    {
        // ==========================================
        // 1. Linkage (الربط بالكشف)
        // ==========================================

        [Required(ErrorMessage = "رقم الكشف الطبي مطلوب.")]
        public Guid ConsultationRecordId { get; set; }
        // ملاحظة: من خلال هذا الـ ID، النظام سيجلب التشخيص (Diagnosis) ويرسله للـ AI مع الأدوية

        // ==========================================
        // 2. Metadata (ملاحظات عامة)
        // ==========================================

        [MaxLength(500, ErrorMessage = "الملاحظات لا يجب أن تتجاوز 500 حرف.")]
        public string? Notes { get; set; } // تعليمات عامة للمريض (مثل: "الراحة التامة")

        // ==========================================
        // 3. The Drugs (قائمة الأدوية)
        // ==========================================

        [Required(ErrorMessage = "يجب إضافة دواء واحد على الأقل.")]
        [MinLength(1, ErrorMessage = "الروشتة فارغة.")]
        public List<CreatePrescriptionItemDto> Items { get; set; } = new();
    }

    /// <summary>
    /// تفاصيل صنف واحد في الروشتة
    /// </summary>
    public class CreatePrescriptionItemDto
    {
        [Required(ErrorMessage = "يجب اختيار الدواء.")]
        public Guid MedicationId { get; set; }
        // نستخدم ID لضمان أننا نعرف "المادة الفعالة" المخزنة في الداتابيز (عشان الـ AI)

        [Required(ErrorMessage = "الجرعة مطلوبة.")]
        [MaxLength(50)]
        public required string Dosage { get; set; } // مثال: "500mg"

        [Required(ErrorMessage = "التكرار مطلوب.")]
        [MaxLength(100)]
        public required string Frequency { get; set; } // مثال: "كل 8 ساعات"

        [MaxLength(50)]
        public string? Duration { get; set; } // مثال: "لمدة 5 أيام"

        [MaxLength(200)]
        public string? Instructions { get; set; } // مثال: "بعد الأكل"
    }
}