
using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities
{
    public class AIDiagnosisLog : BaseEntity
    {
        // ==========================================
        // 1. Context (سياق الطلب)
        // ==========================================

        // الكشف المرتبط بهذه العملية
        public Guid ConsultationRecordId { get; set; }
        public required ConsultationRecord ConsultationRecord { get; set; }

        // نوع العملية: هل بنسأل عن تشخيص؟ ولا بنحلل روشتة؟
        // (عشان الجدول ده بيخدم الموديلين)
        public AIRequestType RequestType { get; set; } = AIRequestType.Diagnosis;

        // ==========================================
        // 2. Input Data (ماذا أرسلنا للموديل؟)
        // ==========================================

        // النص الأساسي المرسل (الأعراض أو الأدوية)
        public required string InputText { get; set; }

        // لقطة (Snapshot) من العلامات الحيوية وقت الإرسال (JSON)
        // عشان لو المريض ضغطه اتغير بعدين، نعرف الموديل حكم بناءً على إيه وقتها
        public string? InputVitalsSnapshot { get; set; }

        // ==========================================
        // 3. Model Metadata (بيانات الموديل نفسه)
        // ==========================================

        // اسم الموديل المستخدم (مثال: "Nabd-BERT-Ar")
        [MaxLength(50)]
        public required string ModelName { get; set; }

        // رقم إصدار الموديل (مهم جداً للمقارنة لاحقاً - A/B Testing)
        // مثال: "v1.0.2"
        [MaxLength(20)]
        public required string ModelVersion { get; set; }

        // ==========================================
        // 4. Performance Metrics (مراقبة الأداء)
        // ==========================================

        public DateTime RequestTimestamp { get; set; } // متى خرج الطلب من عندنا
        public DateTime ResponseTimestamp { get; set; } // متى وصل الرد

        // الوقت المستغرق بالمللي ثانية (Latency)
        // لو الرقم ده زاد عن حد معين، بنبعت Alert للادمن
        public long ProcessingDurationMs { get; set; }

        // ==========================================
        // 5. Output Data (رد الموديل)
        // ==========================================

        // الرد الخام القادم من البايثون (Full JSON Response)
        // بنخزنه عشان لو حبينا نعمل Debugging
        public required string RawResponseJson { get; set; }

        // أعلى اقتراح الموديل قاله (Top Prediction) - عشان الكويري السريع
        [MaxLength(100)]
        public string? TopPrediction { get; set; }

        // نسبة الثقة في أعلى اقتراح (0.0 - 1.0)
        public double TopConfidenceScore { get; set; }

        // ==========================================
        // 6. Feedback Loop (التعلم والتحسين) - الأهم
        // ==========================================

        // ماذا فعل الطبيب بالاقتراح؟
        public AIDoctorAction DoctorAction { get; set; } = AIDoctorAction.NoAction;

        // لو الدكتور رفض الاقتراح، اختار إيه بداله؟ (الحقيقة المطلقة Ground Truth)
        // ده العمود اللي هنستخدمه عشان نعيد تدريب الموديل (Re-training)
        public string? CorrectedDiagnosis { get; set; }

        // ملاحظات الدكتور على أداء الـ AI (اختياري)
        // مثال: "الموديل تجاهل أن المريض مريض سكر"
        [MaxLength(500)]
        public string? FeedbackNotes { get; set; }
    }
}