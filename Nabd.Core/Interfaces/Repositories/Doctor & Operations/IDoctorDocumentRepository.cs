using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Profiles; // لاستخدام DoctorDocument
using Nabd.Core.Enums.Identity;    // لاستخدام VerificationDocumentStatus
using Nabd.Core.Interfaces.Repositories.Base; // لاستخدام IGenericRepository

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
    // واجهة التعامل مع وثائق توثيق الأطباء (البطاقة، الشهادات، التراخيص)
    public interface IDoctorDocumentRepository : IGenericRepository<DoctorDocument>
    {
        // 1. جلب جميع الوثائق الخاصة بطبيب معين
        Task<IEnumerable<DoctorDocument>> GetByDoctorIdAsync(Guid doctorId);

        // 2. جلب الوثائق التي تحتاج إلى مراجعة (حالتها Pending)
        // هذا الميثود مخصص للوحة تحكم الأدمن/المراجع
        Task<IEnumerable<DoctorDocument>> GetPendingDocumentsAsync();

        // 3. جلب الوثائق حسب حالة معينة (مثال: جلب كل المرفوض للحصر)
        Task<IEnumerable<DoctorDocument>> GetByStatusAsync(VerificationDocumentStatus status);
    }
}