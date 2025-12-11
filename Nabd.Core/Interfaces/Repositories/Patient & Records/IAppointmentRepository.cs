using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Medical
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // ==========================================
        // I. Core Retrieval
        // ==========================================
        Task<Appointment?> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAndDateAsync(Guid doctorId, DateTime date);

        Task<IEnumerable<Appointment>> GetByDoctorIdAndDateRangeAsync(
            Guid doctorId,
            DateTime startDate,
            DateTime endDate,
            List<AppointmentStatus>? statuses = null);

        Task<bool> HasConflictingAppointmentAsync(
            Guid doctorId,
            DateTime startTime,
            DateTime endTime,
            Guid? excludeAppointmentId = null);

        // ==========================================
        // II. Doctor Dashboard Statistics
        // ==========================================

        Task<int> GetTodayAppointmentsCountAsync(Guid doctorId);
        Task<int> GetUniquePatientsCountAsync(Guid doctorId);
        Task<int> GetPendingAppointmentsCountAsync(Guid doctorId);
        Task<int> GetCompletedAppointmentsCountAsync(Guid doctorId);
        Task<decimal> GetTotalRevenueAsync(Guid doctorId);
        Task<decimal> GetMonthlyRevenueAsync(Guid doctorId, int year, int month);

        Task<Dictionary<AppointmentStatus, int>> GetAppointmentStatisticsByDoctorIdAsync(
            Guid doctorId, DateTime? startDate, DateTime? endDate);

        // ==========================================
        // III. Advanced
        // ==========================================
        Task<(IEnumerable<Appointment> Appointments, int TotalCount)> GetByDoctorIdWithFiltersAsync(
            Guid doctorId,
            DateTime? startDate,
            DateTime? endDate,
            AppointmentStatus? status,
            int pageNumber,
            int pageSize,
            string sortBy,
            string sortOrder);
    }
}