using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Interfaces.Repositories.Base;
using Nabd.Core.DTOs; // ✅ هذا هو السطر المفقود والمهم جداً

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // 1. استرجاع المريض مع التفاصيل
        Task<Patient?> GetByIdWithDetailsAsync(Guid id);

        // 2. البحث بالإيميل
        Task<Patient?> GetByEmailAsync(string email);

        // 3. المرضى مع تاريخهم الطبي
        Task<IEnumerable<Patient>> GetPatientsWithMedicalHistoryAsync();

        // 4. ✅ هنا كان الإيرور بسبب نقص الـ using
        Task<(IEnumerable<DoctorPatientDto> Patients, int TotalCount)> GetDoctorPatientsOptimizedAsync(
            Guid doctorId,
            int pageNumber,
            int pageSize);

        // 5. البحث بالموقع
        Task<Patient?> GetPatientWithLocationAsync(Guid patientId);

        // 6. الحذف
        Task RemoveAsync(Patient patient, bool softDelete = true);
    }
}