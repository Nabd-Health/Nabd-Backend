using Nabd.Application.DTOs.Pharmacy; // لعرض الروشتة المرتبطة بالكشف
using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Medical
{
    public class ConsultationRecordResponse
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime Date { get; set; }

        // ==========================================
        // 1. Context (الأطراف)
        // ==========================================
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;

        // ==========================================
        // 2. Subjective (الشكوى والأعراض - AI Input)
        // ==========================================
        public string ChiefComplaint { get; set; } = string.Empty; // "صداع شديد"
        public string Symptoms { get; set; } = string.Empty;       // "نبض في الرأس، غثيان..."
        public string? HistoryOfPresentIllness { get; set; }

        // ==========================================
        // 3. Objective (العلامات الحيوية - AI Features)
        // ==========================================
        // بنرجعها مفصلة عشان تترسم Charts في الـ UI لو حبينا
        public double? Temperature { get; set; }
        public string? BloodPressure { get; set; } // "120/80" (ممكن ندمج Sys/Dia هنا للعرض)
        public int? HeartRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public double? OxygenSaturation { get; set; }
        public double? Weight { get; set; }

        public string? PhysicalExaminationNotes { get; set; }

        // ==========================================
        // 4. Assessment (التشخيص - AI Output/Ground Truth)
        // ==========================================
        public string FinalDiagnosis { get; set; } = string.Empty;
        public string? ProvisionalDiagnosis { get; set; }

        // هل ساعد الـ AI في هذا التشخيص؟ (لعرض شارة "AI Assisted")
        public bool WasAIAssisted { get; set; }

        // ==========================================
        // 5. Plan (الخطة العلاجية)
        // ==========================================
        public string? TreatmentPlan { get; set; } // نصائح عامة
        public DateTime? RecommendedFollowUpDate { get; set; }

        // الروشتة المصروفة في هذا الكشف
        public List<PrescriptionResponse> Prescriptions { get; set; } = new();

        // المرفقات (تحاليل أو أشعة تم رفعها أثناء الكشف)
        public List<string> AttachmentUrls { get; set; } = new();
    }
}