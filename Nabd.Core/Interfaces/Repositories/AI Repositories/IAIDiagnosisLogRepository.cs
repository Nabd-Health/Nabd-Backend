using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.AI;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.AI
{
    public interface IAIDiagnosisLogRepository : IGenericRepository<AIDiagnosisLog>
    {
        // 1. جلب سجلات الـ AI لكشف معين (لعرضها للدكتور)
        Task<IEnumerable<AIDiagnosisLog>> GetByConsultationIdAsync(Guid consultationId);

        // 2. [مهم جداً]: جلب السجلات التي أخطأ فيها الـ AI (لإعادة التدريب)
        // هذه الدالة ستستخدم بواسطة Python Script لاحقاً لسحب الداتا
        Task<IEnumerable<AIDiagnosisLog>> GetLogsForRetrainingAsync();

        // 3. جلب إحصائيات دقة الموديل (مثال: 85% دقة)
        Task<double> GetModelAccuracyPercentageAsync();
    }
}