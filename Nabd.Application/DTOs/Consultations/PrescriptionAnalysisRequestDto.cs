using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Consultations
{
    // هذا DTO هو مدخل البيانات لموديل تحليل الروشتات (Prescription/Test Analyzer Model)
    public class PrescriptionAnalysisRequestDto
    {
        // ==================================================
        // 1. Request Context (سياق الطلب)
        // ==================================================

        // ID ملف الكشف (لمساعدتنا في تسجيل الرد في جدول AIDiagnosisLog)
        public required Guid ConsultationId { get; set; }

        // ID المريض (للحصول على بيانات الحساسية)
        public required Guid PatientId { get; set; }

        // ==================================================
        // 2. Diagnosis (المحور الأساسي للتحليل)
        // ==================================================

        [Required(ErrorMessage = "The final diagnosis is required for analysis.")]
        // التشخيص النهائي (النص) الذي اعتمده الطبيب (مثال: Anemia, Bacterial Pneumonia)
        public required string FinalDiagnosisText { get; set; }

        // ==================================================
        // 3. Current Prescribed Items (ما تمت كتابته بالفعل)
        // ==================================================

        // قائمة بالعناصر التي كتبها الطبيب حالياً (للبحث عن العناصر الناقصة)
        public List<PrescribedItemDto> CurrentItems { get; set; } = new List<PrescribedItemDto>();

        // ==================================================
        // 4. Patient Safety Context (الأمان)
        // ==================================================

        // سجل الحساسية (لمنع الـ AI من اقتراح دواء يتعارض مع حساسية المريض)
        public string? PatientAllergies { get; set; }

        // الوزن (لتحليل الجرعات في حالة الأطفال أو الأدوية الحساسة للوزن)
        public double? PatientWeight { get; set; }
    }
}