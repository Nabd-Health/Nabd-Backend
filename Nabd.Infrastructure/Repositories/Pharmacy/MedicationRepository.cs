using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Pharmacy;
using Nabd.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Pharmacy
{
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Medication>> SearchAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return new List<Medication>();

            var lowerTerm = term.ToLower();

            // بنبحث في الاسم التجاري والمادة الفعالة
            // وبنتأكد إن الدواء IsActive
            return await _dbSet
                .Where(m => m.IsActive &&
                           (m.TradeName.ToLower().Contains(lowerTerm) ||
                            m.ScientificName.ToLower().Contains(lowerTerm)))
                .OrderBy(m => m.TradeName) // ترتيب أبجدي
                .Take(20) // نرجع أول 20 نتيجة بس عشان الأداء
                .ToListAsync();
        }

        public async Task<Medication?> GetByBarcodeAsync(string barcode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.Barcode == barcode);
        }

        public async Task<IEnumerable<Medication>> GetMostPrescribedAsync(int topCount = 10)
        {
            return await _dbSet
                .Where(m => m.IsActive)
                // هنا بنرتب حسب عدد مرات ظهوره في جدول PrescriptionItems
                .OrderByDescending(m => m.PrescriptionItems.Count)
                .Take(topCount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Medication>> GetActiveMedicationsAsync()
        {
            return await _dbSet
                .Where(m => m.IsActive)
                .OrderBy(m => m.TradeName)
                .ToListAsync();
        }
    }
}