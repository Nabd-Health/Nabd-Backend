using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities
{
    public class ConsultationRecord : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الربط)
        // ==========================================

        // الكشف ده تبع أنهي موعد؟
        public Guid AppointmentId { get; set; }
        public required Appointment Appointment { get; set; }

        // ==========================================
        // 2. Vital Signs (العلامات الحيوية - Objective)
        // ==========================================
        // دي بيانات رقمية مهمة جداً للـ AI عشان يحسب خطورة الحالة

        public double? Temperature { get; set; } // الحرارة (37.5)

        public int? SystolicBloodPressure { get; set; } // الضغط الانقباضي (120)
        public int? DiastolicBloodPressure { get; set; } // الضغط الانبساطي (80)

        public int? HeartRate { get; set; } // نبضات القلب (BPM)
        public int? RespiratoryRate { get; set; } // معدل التنفس

        public double? OxygenSaturation { get; set; } // نسبة الأكسجين (SPO2) - مهمة لأمراض الصدر

        public double? WeightAtVisit { get; set; } // الوزن وقت الزيارة (مهم لحساب جرعات الأطفال)

        // ==========================================
        // 3. Subjective Data (شكوى المريض - AI Input)
        // ==========================================

        [Required]
        public required string ChiefComplaint { get; set; } // الشكوى الرئيسية (مثال: "وجع في الصدر")

        public string? HistoryOfPresentIllness { get; set; } // تفاصيل القصة المرضية (HPI)

        [Required]
        public required string Symptoms { get; set; } // الأعراض مفصلة (النص ده اللي هيروح للـ NLP Model)

        // ==========================================
        // 4. Objective Data (الفحص السريري)
        // ==========================================

        public string? PhysicalExaminationNotes { get; set; } // الدكتور شاف إيه لما كشف بالسماعة؟

        // ==========================================
        // 5. Assessment (التشخيص - AI + Doctor)
        // ==========================================

        // التشخيص المبدئي (ممكن الـ AI هو اللي يملأه)
        public string? ProvisionalDiagnosis { get; set; }

        // التشخيص النهائي المعتمد من الطبيب
        [Required]
        public string FinalDiagnosis { get; set; }

        // كود المرض العالمي (ICD-10) - لو حبينا نربط بنظام تأمين مستقبلاً
        public string? DiagnosisCode { get; set; }

        // ==========================================
        // 6. Plan (الخطة العلاجية - Prescription Analyzer Input)
        // ==========================================

        public string? TreatmentPlan { get; set; } // نصائح عامة (مثال: "الراحة التامة، شرب سوائل")

        public string? PrescriptionNotes { get; set; } // ملاحظات الروشتة

        public string? LabOrderNotes { get; set; } // طلبات التحاليل (لو فيه)

        public string? RadiologyOrderNotes { get; set; } // طلبات الأشعة

        // موعد المتابعة المقترح (مثال: بعد أسبوع)
        public DateTime? RecommendedFollowUpDate { get; set; }

        // ==========================================
        // 7. AI & Relationships
        // ==========================================

        // هل الدكتور استخدم الـ AI في الكشف ده؟ (عشان الإحصائيات)
        public bool WasAIAssisted { get; set; } = false;

        // سجلات الحوار مع الـ AI (الاقتراحات اللي طلعت)
        public ICollection<AIDiagnosisLog> AIDiagnosisLogs { get; set; } = new List<AIDiagnosisLog>();

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}