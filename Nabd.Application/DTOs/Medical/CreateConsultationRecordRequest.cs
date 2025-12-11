using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Medical
{
    public class CreateConsultationRecordRequest
    {
        // ==========================================
        // 1. Linkage
        // ==========================================
        [Required(ErrorMessage = "رقم الموعد مطلوب.")]
        public Guid AppointmentId { get; set; }

        // ==========================================
        // 2. Subjective Data (شكوى المريض - NLP Input)
        // ==========================================

        [Required(ErrorMessage = "الشكوى الرئيسية مطلوبة.")]
        [MaxLength(200, ErrorMessage = "الشكوى يجب ألا تزيد عن 200 حرف.")]
        public required string ChiefComplaint { get; set; } 

        [Required(ErrorMessage = "تفاصيل الأعراض مطلوبة للتشخيص.")]
        public required string Symptoms { get; set; } 

        public string? HistoryOfPresentIllness { get; set; } 

        // ==========================================
        // 3. Objective Data ( AI Features)
        // ==========================================


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
        // 4. Assessment 
        // ==========================================

        public string? ProvisionalDiagnosis { get; set; } 

        [Required(ErrorMessage = "التشخيص النهائي مطلوب لإغلاق الكشف.")]
        public required string FinalDiagnosis { get; set; } 

        // ==========================================
        // 5. Plan 
        // ==========================================

        public string? TreatmentPlan { get; set; } 

        public DateTime? RecommendedFollowUpDate { get; set; }
    }
}