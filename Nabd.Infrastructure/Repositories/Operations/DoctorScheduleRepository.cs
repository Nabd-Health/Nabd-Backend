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
                .Include(s => s.ClinicBranch)
                .Where(s => s.DoctorId == doctorId)
               
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
         
            var query = _dbSet
                .AsNoTracking()
                .Where(s =>
                    s.DoctorId == doctorId &&
                    s.DayOfWeek == day &&
                    !s.IsDayOff); 
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