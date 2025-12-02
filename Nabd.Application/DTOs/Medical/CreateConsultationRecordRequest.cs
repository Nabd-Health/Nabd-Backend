using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Medical
{
    public class CreateConsultationRecordRequest
    {
        // ==========================================
        // 1. Linkage (الربط)
        // ==========================================
        [Required(ErrorMessage = "رقم الموعد مطلوب.")]
        public Guid AppointmentId { get; set; }

        // ==========================================
        // 2. Subjective Data (شكوى المريض - NLP Input)
        // ==========================================

        [Required(ErrorMessage = "الشكوى الرئيسية مطلوبة.")]
        [MaxLength(200, ErrorMessage = "الشكوى يجب ألا تزيد عن 200 حرف.")]
        public required string ChiefComplaint { get; set; } // "صداع نصفي"

        [Required(ErrorMessage = "تفاصيل الأعراض مطلوبة للتشخيص.")]
        public required string Symptoms { get; set; } // "ألم في الجانب الأيمن يزداد مع الضوء..."

        public string? HistoryOfPresentIllness { get; set; } // تاريخ المرض الحالي

        // ==========================================
        // 3. Objective Data (العلامات الحيوية - AI Features)
        // ==========================================
        // وضعنا Range Validation لمنع الأرقام غير المنطقية التي قد تربك الـ AI

        [Range(35, 42, ErrorMessage = "درجة الحرارة غير منطقية (35-42).")]
        public double? Temperature { get; set; }

        [Range(60, 250, ErrorMessage = "ضغط الدم الانقباضي غير منطقي.")]
        public int? SystolicBloodPressure { get; set; } // 120

        [Range(40, 150, ErrorMessage = "ضغط الدم الانبساطي غير منطقي.")]
        public int? DiastolicBloodPressure { get; set; } // 80

        [Range(30, 200, ErrorMessage = "معدل النبض غير منطقي.")]
        public int? HeartRate { get; set; }

        [Range(10, 60, ErrorMessage = "معدل التنفس غير منطقي.")]
        public int? RespiratoryRate { get; set; }

        [Range(70, 100, ErrorMessage = "نسبة الأكسجين غير منطقية.")]
        public double? OxygenSaturation { get; set; }

        [Range(2, 300, ErrorMessage = "الوزن غير منطقي.")]
        public double? WeightAtVisit { get; set; }

        public string? PhysicalExaminationNotes { get; set; }

        // ==========================================
        // 4. Assessment (التشخيص - AI Ground Truth)
        // ==========================================

        public string? ProvisionalDiagnosis { get; set; } // التشخيص المبدئي

        [Required(ErrorMessage = "التشخيص النهائي مطلوب لإغلاق الكشف.")]
        public required string FinalDiagnosis { get; set; } // التشخيص المعتمد

        // ==========================================
        // 5. Plan (الخطة)
        // ==========================================

        public string? TreatmentPlan { get; set; } // نصائح عامة (راحة، سوائل)

        public DateTime? RecommendedFollowUpDate { get; set; }
    }
}