using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Medical;
using Nabd.Core.Interfaces.Repositories.Medical;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Medical
{
    public class MedicalHistoryItemRepository : GenericRepository<MedicalHistoryItem>, IMedicalHistoryItemRepository
    {
        public MedicalHistoryItemRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicalHistoryItem>> GetByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                // لا نحتاج لعمل Include(Patient) هنا لأننا غالباً نكون في صفحة المريض بالفعل
                .Where(mhi => mhi.PatientId == patientId)
                // التعديل: الترتيب حسب تاريخ الحدث (الأحدث فالأقدم) لبناء Timeline طبي سليم
                .OrderByDescending(mhi => mhi.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalHistoryItem>> GetByTypeAsync(Guid patientId, HistoryEventType type)
        {
            return await _dbSet
                .Where(mhi => mhi.PatientId == patientId && mhi.EventType == type)
                .OrderByDescending(mhi => mhi.EventDate)
                .ToListAsync();
        }
    }
}