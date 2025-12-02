using System;

namespace Nabd.Application.DTOs.Medical
{
    public class MedicalHistoryItemResponse
    {
        public Guid Id { get; set; }

        // نوع الحدث (نصي للعرض: حساسية، عملية، مرض مزمن)
        // (سيقوم الـ Mapper بجلب الـ Description من الـ Enum هنا)
        public string Type { get; set; } = string.Empty;

        // العنوان (مثال: "Penicillin Allergy" أو "Appendectomy")
        public string Title { get; set; } = string.Empty;

        // التفاصيل الإضافية (مثال: "يسبب طفح جلدي شديد")
        public string? Details { get; set; }

        // تاريخ الحدوث الفعلي (Clinical Date)
        // (متى تمت العملية أو متى تم تشخيص المرض؟ وليس متى تم التسجيل في السيستم)
        public DateTime EventDate { get; set; }

        // هل هو خطير؟ (High Priority)
        // (يستخدم لتمييز العنصر باللون الأحمر في الـ UI ولتنبيه الـ AI)
        public bool IsCritical { get; set; }
    }
}