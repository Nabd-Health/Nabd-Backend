using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// نستخدم Nabd.Core.Entities.Operations للـ Entity DoctorSchedule
using Nabd.Core.Entities.Operations;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Operations
{
    // هذا الريبوزيتوري مخصص لجداول عمل الطبيب وأوقات الإتاحة
    public interface IDoctorScheduleRepository : IGenericRepository<DoctorSchedule>
    {
        // 1. جلب جميع جداول الطبيب (في كل الفروع)
        Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId);

        // 2. جلب جداول الطبيب ليوم محدد في الأسبوع (مثال: كل أيام الإثنين)
        // (نفترض أن SysDayOfWeek تم تعريفه كـ Enum في Core)
        Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAndDayAsync(Guid doctorId, DayOfWeek day);

        // 3. التحقق من تضارب في أوقات العمل قبل إضافة جدول جديد
        // (مهم جدًا لمنع التضارب في جداول الطبيب الواحد)
        Task<bool> HasOverlappingScheduleAsync(
            Guid doctorId,
            DayOfWeek day,
            TimeSpan startTime, // تم تعديل TimeOnly إلى TimeSpan
            TimeSpan endTime, // تم تعديل TimeOnly إلى TimeSpan
            Guid? excludeId = null);
    }
}