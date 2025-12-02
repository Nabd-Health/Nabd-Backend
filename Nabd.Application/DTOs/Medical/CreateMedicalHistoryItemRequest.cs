using Nabd.Core.Enums.Medical; // تأكد أن الـ Enum ده موجود في Core
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Medical
{
    public class CreateMedicalHistoryItemRequest
    {
        // 1. السياق
        [Required(ErrorMessage = "رقم ملف المريض مطلوب.")]
        public Guid PatientId { get; set; }

        // 2. التصنيف (بيانات هيكلية للـ AI)
        [Required(ErrorMessage = "نوع السجل الطبي مطلوب.")]
        public HistoryEventType Type { get; set; } // (Allergy, Surgery, ChronicDisease)

        // 3. المحتوى (Structured Content)
        [Required(ErrorMessage = "العنوان مطلوب.")]
        [MaxLength(150, ErrorMessage = "العنوان لا يجب أن يتجاوز 150 حرف.")]
        public required string Title { get; set; } // مثال: "Penicillin Allergy" (أفضل من Text عايم)

        [MaxLength(500, ErrorMessage = "التفاصيل لا يجب أن تتجاوز 500 حرف.")]
        public string? Details { get; set; } // التفاصيل الإضافية

        // 4. الميتاداتا (الزمن والخطورة - أهم حاجة للـ AI)
        [Required(ErrorMessage = "تاريخ الحدوث مطلوب.")]
        public DateTime EventDate { get; set; } // عشان الـ AI يعرف الترتيب الزمني

        public bool IsCritical { get; set; } = false; // عشان يطلع Alert أحمر لو فيه تعارض
    }
}