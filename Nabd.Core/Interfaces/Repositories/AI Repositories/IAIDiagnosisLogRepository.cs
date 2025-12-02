using Nabd.Core.Entities.AI;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nabd.Core.Interfaces.Repositories.AI
{
    // هذا الريبوزيتوري مخصص لتسجيل سجلات أداء ومخرجات الذكاء الاصطناعي (AI Black Box)
    public interface IAIDiagnosisLogRepository : IGenericRepository<AIDiagnosisLog>
    {
        // ==========================================
        // I. Retrieval for Learning & Auditing
        // ==========================================

        /// <summary>
        /// جلب جميع سجلات الـ AI المرتبطة بكشف طبي محدد (مهم لملف المريض)
        /// </summary>
        Task<IEnumerable<AIDiagnosisLog>> GetLogsByConsultationIdAsync(Guid consultationRecordId);

        /// <summary>
        /// جلب السجلات التي تحتاج إلى تقييم (حيث يكون الطبيب قد أدخل تشخيصاً نهائياً)
        /// هذا هو فلتر (Ground Truth) لجمع بيانات إعادة التدريب.
        /// </summary>
        Task<IEnumerable<AIDiagnosisLog>> GetLogsForRetrainingAsync(
            string modelName,
            string modelVersion);

        /// <summary>
        /// جلب تقارير الأداء التي تشمل إجراء معين من الطبيب (مثال: جلب جميع الحالات التي "رفض" فيها الطبيب اقتراح الـ AI)
        /// </summary>
        Task<IEnumerable<AIDiagnosisLog>> GetLogsByDoctorActionAsync(
            AIDoctorAction action,
            DateTime startDate,
            DateTime endDate);

        // ==========================================
        // II. Aggregation (الإحصائيات)
        // ==========================================

        /// <summary>
        /// حساب نسبة دقة الموديل (Accuracy) على مدى فترة زمنية أو إصدار معين
        /// (للحصول على KPIs للـ AI)
        /// </summary>
        Task<double> CalculateModelAccuracyAsync(
            string modelName,
            string modelVersion,
            DateTime? startDate = null,
            DateTime? endDate = null);
    }
}