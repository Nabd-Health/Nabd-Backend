using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Pharmacy
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        // 1. البحث عن دواء بالاسم التجاري أو العلمي (مهم جداً للدكتور أثناء كتابة الروشتة)
        Task<IEnumerable<Medication>> SearchAsync(string term);

        // 2. البحث بالباركود (ميزة جديدة في نبض لدعم الماسحات الضوئية)
        Task<Medication?> GetByBarcodeAsync(string barcode);

        // 3. الأدوية الأكثر وصفاً (للإحصائيات أو الاقتراحات السريعة)
        Task<IEnumerable<Medication>> GetMostPrescribedAsync(int topCount = 10);

        // 4. جلب الأدوية النشطة فقط (عشان الدكتور ميكتبش دواء ناقص أو ملغي)
        Task<IEnumerable<Medication>> GetActiveMedicationsAsync();
    }
}