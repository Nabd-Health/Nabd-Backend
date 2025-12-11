using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Nabd.Core.Entities.Operations;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Operations
{

    public interface IDoctorScheduleRepository : IGenericRepository<DoctorSchedule>
    {
      
        Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId);

       
        Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAndDayAsync(Guid doctorId, DayOfWeek day);

      
        Task<bool> HasOverlappingScheduleAsync(
            Guid doctorId,
            DayOfWeek day,
            TimeSpan startTime, 
            TimeSpan endTime, 
            Guid? excludeId = null);
    }
}