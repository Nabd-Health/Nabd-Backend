using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Medical;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Medical
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<Appointment?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                // .Include(a => a.Doctor.ClinicBranch) // يمكن إضافتها لاحقاً لو العلاقة موجودة
                .Include(a => a.ConsultationRecord) // عشان نشوف هل تم الكشف ولا لسه
                .Include(a => a.DoctorReview)       // عشان نشوف هل المريض قيم الموعد ده ولا لأ
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AppointmentDate) // الأحدث أولاً
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAndDateAsync(Guid doctorId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _dbSet
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId
                    && a.AppointmentDate >= startOfDay
                    && a.AppointmentDate < endOfDay)
                .OrderBy(a => a.AppointmentDate) // ترتيب تصاعدي (من الصبح لليل)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAndDateRangeAsync(
            Guid doctorId,
            DateTime startDate,
            DateTime endDate,
            List<AppointmentStatus>? statuses = null)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(a => a.DoctorId == doctorId
                    && a.AppointmentDate >= startDate
                    && a.AppointmentDate < endDate);

            if (statuses != null && statuses.Any())
            {
                query = query.Where(a => statuses.Contains(a.Status));
            }

            return await query
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<bool> HasConflictingAppointmentAsync(
            Guid doctorId,
            DateTime newStartTime,
            DateTime newEndTime,
            Guid? excludeAppointmentId = null)
        {
            // المنطق: يوجد تعارض لو (بداية الموعد الجديد < نهاية الموعد القديم) AND (نهاية الموعد الجديد > بداية الموعد القديم)
            // في نبض: نهاية الموعد القديم = AppointmentDate + EstimatedDurationMinutes

            var query = _dbSet
                .AsNoTracking()
                .Where(a =>
                    a.DoctorId == doctorId
                    && a.Status != AppointmentStatus.Cancelled
                    && a.Status != AppointmentStatus.NoShow
                );

            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a => a.Id != excludeAppointmentId.Value);
            }

            // التحقق من التداخل الزمني
            // ملاحظة: AddMinutes في LINQ بتتحول لـ DATEADD في SQL Server
            return await query.AnyAsync(a =>
                a.AppointmentDate < newEndTime &&
                a.AppointmentDate.AddMinutes(a.EstimatedDurationMinutes) > newStartTime
            );
        }

        // ==========================================
        // Statistics & Dashboard
        // ==========================================

        public async Task<int> GetUniquePatientsCountAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(a => a.DoctorId == doctorId)
                .Select(a => a.PatientId)
                .Distinct()
                .CountAsync();
        }

        public async Task<int> GetCompletedAppointmentsCountAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(a => a.DoctorId == doctorId && a.Status == AppointmentStatus.Completed)
                .CountAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync(Guid doctorId)
        {
            // في نبض الخاصية اسمها Price
            return await _dbSet
                .Where(a => a.DoctorId == doctorId && a.Status == AppointmentStatus.Completed)
                .SumAsync(a => a.Price);
        }

        public async Task<decimal> GetMonthlyRevenueAsync(Guid doctorId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            return await _dbSet
                .Where(a => a.DoctorId == doctorId
                    && a.Status == AppointmentStatus.Completed
                    && a.AppointmentDate >= startDate
                    && a.AppointmentDate < endDate)
                .SumAsync(a => a.Price);
        }

        public async Task<Dictionary<AppointmentStatus, int>> GetAppointmentStatisticsByDoctorIdAsync(
            Guid doctorId,
            DateTime? startDate,
            DateTime? endDate)
        {
            var query = _dbSet.Where(a => a.DoctorId == doctorId);

            if (startDate.HasValue)
                query = query.Where(a => a.AppointmentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(a => a.AppointmentDate < endDate.Value);

            return await query
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        // ==========================================
        // Pagination & Filters
        // ==========================================

        public async Task<(IEnumerable<Appointment> Appointments, int TotalCount)> GetByDoctorIdWithFiltersAsync(
            Guid doctorId,
            DateTime? startDate,
            DateTime? endDate,
            AppointmentStatus? status,
            int pageNumber,
            int pageSize,
            string sortBy,
            string sortOrder)
        {
            var query = _dbSet
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId);

            // 1. Filtering
            if (startDate.HasValue)
                query = query.Where(a => a.AppointmentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(a => a.AppointmentDate < endDate.Value);

            if (status.HasValue)
                query = query.Where(a => a.Status == status.Value);

            var totalCount = await query.CountAsync();

            // 2. Sorting
            // ترتيب ذكي: الحالات "الجارية" أولاً، ثم "المؤكدة"، ثم الباقي حسب التاريخ
            query = query
                .OrderByDescending(a => a.Status == AppointmentStatus.InProgress ? 3 : 0)
                .ThenByDescending(a => a.Status == AppointmentStatus.CheckedIn ? 2 : 0)
                .ThenByDescending(a => a.Status == AppointmentStatus.Confirmed ? 1 : 0)
                .ThenBy(a => a.AppointmentDate);

            // 3. Paging
            var appointments = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (appointments, totalCount);
        }
    }
}