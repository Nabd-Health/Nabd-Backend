using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Medical;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.AI
{
    public class AIDiagnosisLog : BaseEntity
    {
        // ==========================================
        // 1. Linkage (سياق التشخيص)
        // ==========================================
        public Guid ConsultationRecordId { get; set; }
        public virtual ConsultationRecord ConsultationRecord { get; set; } = null!;

        // ==========================================
        // 2. AI Input & Output (ماذا دخل وماذا خرج)
        // ==========================================

        [Required]
        public required string InputSymptoms { get; set; } // النص الذي حلله الموديل

        [Required]
        public required string AIResponseJson { get; set; } // النتيجة (JSON) تحتوي على الأمراض المحتملة ونسب الثقة

        public double HighestConfidenceScore { get; set; } // أعلى نسبة ثقة وصل لها الموديل (لتحليل الأداء)

        public string ModelVersion { get; set; } = "v1.0"; // إصدار الموديل (مهم للمقارنة بعد التحديث)

        // ==========================================
        // 3. Doctor Feedback (حلقة التعلم)
        // ==========================================


        /// <summary>
        public long? ProcessingDurationMs { get; set; } // سرعة الاستجابة
        public DateTime RequestTimestamp { get; set; } = DateTime.UtcNow;

        // Feedback loop properties
        public string? DoctorAction { get; set; } // "Accepted", "Modified", "Rejected"
        public string? CorrectedDiagnosis { get; set; } // كان اسمها DoctorCorrection في النسخة السابقة
        public string? FeedbackNotes { get; set; } // ملاحظات الطبيب
        /// </summary>
        public bool? IsHelpful { get; set; } // هل ساعد التشخيص الطبيب؟

        public bool? WasCorrect { get; set; } // هل كان التشخيص صحيحاً؟

        public string? DoctorCorrection { get; set; } // التشخيص الصحيح الذي اختاره الطبيب (Ground Truth)

        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    }
}