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

        // ==========================================
        // I. Core Retrieval
        // ==========================================

        public async Task<Appointment?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.ConsultationRecord)
                .Include(a => a.DoctorReview)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AppointmentDate)
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
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAndDateRangeAsync(
            Guid doctorId, DateTime startDate, DateTime endDate, List<AppointmentStatus>? statuses = null)
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

            return await query.OrderBy(a => a.AppointmentDate).ToListAsync();
        }

        public async Task<bool> HasConflictingAppointmentAsync(
            Guid doctorId, DateTime newStartTime, DateTime newEndTime, Guid? excludeAppointmentId = null)
        {
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

            return await query.AnyAsync(a =>
                a.AppointmentDate < newEndTime &&
                a.AppointmentDate.AddMinutes(a.EstimatedDurationMinutes) > newStartTime
            );
        }

        // ==========================================
        // II. Statistics & Dashboard 
        // ==========================================

       
        public async Task<int> GetTodayAppointmentsCountAsync(Guid doctorId)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            return await _dbSet.CountAsync(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate >= today &&
                a.AppointmentDate < tomorrow &&
                a.Status != AppointmentStatus.Cancelled);
        }

        public async Task<int> GetUniquePatientsCountAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(a => a.DoctorId == doctorId)
                .Select(a => a.PatientId)
                .Distinct()
                .CountAsync();
        }

        public async Task<int> GetPendingAppointmentsCountAsync(Guid doctorId)
        {
            return await _dbSet.CountAsync(a =>
                a.DoctorId == doctorId &&
                a.Status == AppointmentStatus.Pending);
        }

        public async Task<int> GetCompletedAppointmentsCountAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(a => a.DoctorId == doctorId && a.Status == AppointmentStatus.Completed)
                .CountAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync(Guid doctorId)
        {
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
            Guid doctorId, DateTime? startDate, DateTime? endDate)
        {
            var query = _dbSet.Where(a => a.DoctorId == doctorId);

            if (startDate.HasValue) query = query.Where(a => a.AppointmentDate >= startDate.Value);
            if (endDate.HasValue) query = query.Where(a => a.AppointmentDate < endDate.Value);

            return await query
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        // ==========================================
        // III. Pagination & Filters
        // ==========================================

        public async Task<(IEnumerable<Appointment> Appointments, int TotalCount)> GetByDoctorIdWithFiltersAsync(
            Guid doctorId, DateTime? startDate, DateTime? endDate, AppointmentStatus? status,
            int pageNumber, int pageSize, string sortBy, string sortOrder)
        {
            var query = _dbSet.Include(a => a.Patient).Where(a => a.DoctorId == doctorId);

            if (startDate.HasValue) query = query.Where(a => a.AppointmentDate >= startDate.Value);
            if (endDate.HasValue) query = query.Where(a => a.AppointmentDate < endDate.Value);
            if (status.HasValue) query = query.Where(a => a.Status == status.Value);

            var totalCount = await query.CountAsync();

            query = sortBy?.ToLower() switch
            {
                "date" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(a => a.AppointmentDate) : query.OrderBy(a => a.AppointmentDate),
                "status" => query.OrderBy(a => a.Status),
                _ => query.OrderByDescending(a => a.AppointmentDate)
            };

            var appointments = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (appointments, totalCount);
        }
    }
}