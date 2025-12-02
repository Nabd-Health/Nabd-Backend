using Nabd.Core.Entities.Operations; // لاستخدام ClinicBranch
using Nabd.Core.Entities.Profiles;  // لاستخدام Doctor
using Nabd.Core.Enums;              // لاستخدام Governorate
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nabd.Core.Interfaces.Repositories.Operations
{
    // هذا الريبوزيتوري يمثل نقطة الوصول للبيانات الخاصة بفروع العيادات (Clinic Branches)
    public interface IClinicBranchRepository : IGenericRepository<ClinicBranch>
    {
        // 1. الوظيفة الأساسية: جلب الفرع المرتبط بطبيب معين (One-to-One/Many-to-One)
        // (يمكن أن يكون للطبيب أكثر من فرع)
        Task<IEnumerable<ClinicBranch>> GetBranchesByDoctorIdAsync(Guid doctorId);

        // 2. البحث الجغرافي: جلب الفروع القريبة من موقع جغرافي معين (مهم لصفحة المريض)
        Task<IEnumerable<ClinicBranch>> GetBranchesNearLocationAsync(
            double latitude,
            double longitude,
            double radiusInKm);

        // 3. (دمج) جلب الفروع حسب المحافظة (للفلترة السريعة)
        Task<IEnumerable<ClinicBranch>> GetBranchesByGovernorateAsync(Governorate governorate);

        // 4. (دمج) جلب الفرع مع كل العلاقات المطلوبة في صفحة التفاصيل
        // (يشمل Photos, Services, Schedules)
        Task<ClinicBranch?> GetBranchWithAllDetailsAsync(Guid clinicBranchId);
    }
}