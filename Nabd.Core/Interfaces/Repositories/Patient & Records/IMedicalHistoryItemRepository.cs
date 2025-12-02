using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Medical; // لاستخدام MedicalHistoryType
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Medical
{
    // هذا الريبوزيتوري مخصص لعناصر التاريخ المرضي للمريض (عمليات، حساسية، إلخ)
    public interface IMedicalHistoryItemRepository : IGenericRepository<MedicalHistoryItem>
    {
        // 1. جلب جميع عناصر التاريخ المرضي لمريض محدد
        Task<IEnumerable<MedicalHistoryItem>> GetByPatientIdAsync(Guid patientId);

        // 2. جلب العناصر حسب النوع (مثال: جلب جميع الحساسيات فقط)
        Task<IEnumerable<MedicalHistoryItem>> GetByTypeAsync(Guid patientId, HistoryEventType type);
    }
}