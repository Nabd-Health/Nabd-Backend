using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Pharmacy; // عشان يشوف Prescription
using Nabd.Core.Entities.AI;       // عشان يشوف AIDiagnosisLog
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Medical
{
    public class ConsultationRecord : BaseEntity
    {
        // ==========================================
        // 1. Linkage (الربط بالموعد)
        // ==========================================

        public Guid AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;

        // ==========================================
        // 2. (S)ubjective Data: شكوى المريض (AI Input Model 1)
        // ==========================================

        [Required]
        // الشكوى الرئيسية باختصار (مثال: "سخونية وكحة")
        public required string ChiefComplaint { get; set; }

        [Required]
        // الأعراض التفصيلية (النص الكامل اللي هيروح لموديل NLP)
        public required string Symptoms { get; set; }

        // تاريخ المرض الحالي (HPI) - اختياري
        public string? HistoryOfPresentIllness { get; set; }

        // ==========================================
        // 3. (O)bjective Data: العلامات الحيوية (AI Features)
        // ==========================================
        // حولناها لأرقام عشان الـ AI يقدر يحلل الخطورة

        public string? PhysicalExaminationNotes { get; set; } // الفحص السريري

        // العلامات الحيوية (Vitals)
        public double? Temperature { get; set; }         // الحرارة
        public int? SystolicBloodPressure { get; set; }  // الضغط الانقباضي (120)
        public int? DiastolicBloodPressure { get; set; } // الضغط الانبساطي (80)
        public int? HeartRate { get; set; }              // النبض (BPM)
        public int? RespiratoryRate { get; set; }        // معدل التنفس
        public double? OxygenSaturation { get; set; }    // نسبة الأكسجين (SPO2)
        public double? WeightAtVisit { get; set; }       // الوزن (مهم لجرعات الأطفال)

        // ==========================================
        // 4. (A)ssessment: التشخيص (AI Output / Ground Truth)
        // ==========================================

        // التشخيص المبدئي (ممكن يكون اقتراح الـ AI)
        public string? ProvisionalDiagnosis { get; set; }

        [Required]
        // التشخيص النهائي المعتمد من الطبيب (ده اللي هنستخدمه لتدريب الموديل)
        public required string FinalDiagnosis { get; set; }

        // كود المرض العالمي (ICD-10) - لو حبينا نربط بنظام تأمين
        public string? DiagnosisCode { get; set; }

        // ==========================================
        // 5. (P)lan: الخطة العلاجية (Prescription AI Input)
        // ==========================================

        public string? TreatmentPlan { get; set; } // نصائح عامة (راحة، سوائل..)

        public string? PrescriptionNotes { get; set; } // ملاحظات إضافية على الروشتة

        public DateTime? RecommendedFollowUpDate { get; set; } // موعد المتابعة

        // ==========================================
        // 6. AI Metadata & Relationships
        // ==========================================

        public bool WasAIAssisted { get; set; } = false; // هل استخدم الـ AI؟

        // سجلات الحوار مع الـ AI (الاقتراحات اللي طلعت في الكشف ده)
        public virtual ICollection<AIDiagnosisLog> AIDiagnosisLogs { get; set; } = new List<AIDiagnosisLog>();

        // الروشتات والأدوية اللي اتكتبت بناءً على الكشف
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        // المرفقات (أشعة وتحاليل خاصة بالكشف ده)
        public virtual ICollection<MedicalAttachment> Attachments { get; set; } = new List<MedicalAttachment>();
    }
}