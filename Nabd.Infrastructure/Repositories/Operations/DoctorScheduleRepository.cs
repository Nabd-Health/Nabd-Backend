using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Interfaces.Repositories.Operations;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Operations
{
    public class DoctorScheduleRepository : GenericRepository<DoctorSchedule>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(s => s.ClinicBranch) // عشان نعرض اسم الفرع بجانب الموعد
                .Where(s => s.DoctorId == doctorId)
                // الترتيب: الأحد (0) -> الإثنين (1) ... ثم حسب وقت البداية
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAndDayAsync(Guid doctorId, DayOfWeek day)
        {
            return await _dbSet
                .Include(s => s.ClinicBranch)
                .Where(s => s.DoctorId == doctorId && s.DayOfWeek == day)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<bool> HasOverlappingScheduleAsync(
            Guid doctorId,
            DayOfWeek day,
            TimeSpan newStartTime,
            TimeSpan newEndTime,
            Guid? excludeId = null)
        {
            // معادلة كشف التداخل بين فترتين زمنيتين:
            // (StartA < EndB) AND (EndA > StartB)

            var query = _dbSet
                .AsNoTracking()
                .Where(s =>
                    s.DoctorId == doctorId &&
                    s.DayOfWeek == day &&
                    !s.IsDayOff); // لا نعتبر الأيام الأجازة تضارباً (اختياري حسب البيزنس)

            // لو بنعمل Update لجدول موجود، لازم نستثني الجدول ده نفسه من المقارنة
            if (excludeId.HasValue)
            {
                query = query.Where(s => s.Id != excludeId.Value);
            }

            return await query.AnyAsync(s =>
                s.StartTime < newEndTime &&
                s.EndTime > newStartTime
            );
        }
    }
}