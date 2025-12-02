using System.Threading.Tasks;
using Nabd.Application.DTOs.AI;      // DTOs (Results & Feedback)
using Nabd.Application.DTOs.Medical; // Input (Symptoms)
using Nabd.Application.DTOs.Pharmacy;// Input (Drugs)

namespace Nabd.Application.Interfaces
{
    // هذا العقد مسؤول عن التواصل مع موديلات الذكاء الاصطناعي
    public interface IAIService
    {
        // ==========================================
        // 1. Diagnosis Aid (مساعد التشخيص)
        // ==========================================
        /// <summary>
        /// يرسل الأعراض والعلامات الحيوية للموديل ويستقبل التشخيصات المقترحة.
        /// </summary>
        /// <param name="request">بيانات الكشف (الشكوى، الأعراض، العلامات الحيوية).</param>
        /// <returns>قائمة التشخيصات ونسب الثقة.</returns>
        Task<AIDiagnosisResultDto> PredictDiagnosisAsync(CreateConsultationRecordRequest request);

        // ==========================================
        // 2. Prescription Analyzer (محلل الروشتات)
        // ==========================================
        /// <summary>
        /// يرسل الأدوية والتشخيص للموديل للكشف عن التفاعلات والأخطاء.
        /// </summary>
        /// <param name="request">بيانات الروشتة (الأدوية والجرعات).</param>
        /// <param name="diagnosis">التشخيص الذي كتبه الطبيب (للكشف عن Drug-Disease Interaction).</param>
        /// <returns>قائمة التحذيرات والتوصيات.</returns>
        Task<AIPrescriptionAnalysisResultDto> AnalyzePrescriptionAsync(CreatePrescriptionRequest request, string diagnosis);

        // ==========================================
        // 3. Feedback Loop (التعلم المستمر)
        // ==========================================
        /// <summary>
        /// يرسل قرار الطبيب النهائي ورد فعله على الاقتراح لتدريب الموديل لاحقاً.
        /// </summary>
        Task RecordFeedbackAsync(AIFeedbackRequest request);
    }
}