using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Pharmacy
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        // 1. جلب الروشتة كاملة بالأدوية وتفاصيل الدكتور والمريض
        Task<Prescription?> GetByIdWithDetailsAsync(Guid id);

        // 2. جلب جميع روشتات مريض معين (التاريخ الدوائي)
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId);

        // 3. جلب الروشتات التي كتبها طبيب معين
        Task<IEnumerable<Prescription>> GetByDoctorIdAsync(Guid doctorId);

        // 4. جلب الروشتة الخاصة بكشف معين (Linkage)
        Task<Prescription?> GetByConsultationIdAsync(Guid consultationId);

        // 5. (تحليل بيانات): جلب الروشتات التي تحتوي على دواء معين
        // (مفيد لمعرفة "مين كتب الدواء ده ولمين؟")
        Task<IEnumerable<Prescription>> GetPrescriptionsContainingMedicationAsync(Guid medicationId);
    }
}