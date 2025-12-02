using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity; // لاستخدام AIDoctorAction
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.AI
{
    /// <summary>
    /// طلب تسجيل التغذية العكسية (Feedback Loop).
    /// يُستخدم عندما يتخذ الطبيب قراراً نهائياً بشأن اقتراح الـ AI.
    /// هذه البيانات هي "الوقود" لإعادة تدريب الموديل (Re-training Dataset).
    /// </summary>
    public class AIFeedbackRequest
    {
        // ==========================================
        // 1. Linkage (الربط بالسياق)
        // ==========================================

        /// <summary>
        /// معرف الكشف الطبي الذي تم فيه الاقتراح.
        /// </summary>
        [Required]
        public Guid ConsultationRecordId { get; set; }

        /// <summary>
        /// اسم الموديل الذي أعطى الاقتراح (لتحديد أي موديل يحتاج تحسين).
        /// مثال: "Nabd-Diagnosis-v2"
        /// </summary>
        [Required]
        public required string ModelName { get; set; }

        /// <summary>
        /// نسخة الموديل (لتتبع الانحراف في الأداء Model Drift).
        /// </summary>
        [Required]
        public required string ModelVersion { get; set; }

        // ==========================================
        // 2. Doctor's Reaction (رد الفعل)
        // ==========================================

        /// <summary>
        /// ماذا فعل الطبيب بالاقتراح؟
        /// (Accepted, Rejected, Modified, Ignored)
        /// </summary>
        [Required]
        public AIDoctorAction Action { get; set; }

        // ==========================================
        // 3. The Ground Truth (الحقيقة المطلقة للتدريب)
        // ==========================================

        /// <summary>
        /// التشخيص النهائي الصحيح الذي اعتمده الطبيب.
        /// (مهم جداً: إذا رفض الطبيب اقتراح الـ AI، فهذا الحقل هو الإجابة الصحيحة التي سيتعلمها الموديل).
        /// </summary>
        [Required]
        public required string CorrectedDiagnosis { get; set; }

        /// <summary>
        /// قائمة الأدوية النهائية المعتمدة (في حالة موديل تحليل الروشتات).
        /// </summary>
        public List<string>? FinalPrescribedMedications { get; set; }

        // ==========================================
        // 4. Qualitative Data (بيانات نوعية)
        // ==========================================

        /// <summary>
        /// ملاحظات الطبيب: لماذا رفض الاقتراح؟
        /// مثال: "المريض لديه حساسية لم يدركها الموديل" أو "الأعراض غير نمطية".
        /// مفيد لتحليل الأخطاء يدوياً (Error Analysis).
        /// </summary>
        [MaxLength(500, ErrorMessage = "الملاحظات يجب ألا تتجاوز 500 حرف.")]
        public string? Comments { get; set; }

        /// <summary>
        /// تقييم الطبيب لدقة الاقتراح (من 1 إلى 5).
        /// اختياري، ولكنه مفيد لحساب "رضا المستخدم" عن الـ AI.
        /// </summary>
        [Range(1, 5)]
        public int? Rating { get; set; }
    }
}