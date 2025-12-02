using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Operations; // لاستخدام AppointmentStatus
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Medical
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // ==========================================
        // I. Core Retrieval & Conflict Checks
        // ==========================================

        // 1. استرجاع موعد مع تفاصيل كاملة (Doctor, Patient, Consultation Record)
        Task<Appointment?> GetByIdWithDetailsAsync(Guid id);

        // 2. جلب المواعيد للمريض
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);

        // 3. جلب المواعيد للطبيب
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);

        // 4. جلب المواعيد للطبيب في تاريخ محدد
        Task<IEnumerable<Appointment>> GetByDoctorIdAndDateAsync(Guid doctorId, DateTime date);

        // 5. جلب المواعيد للطبيب في نطاق زمني محدد مع إمكانية الفلترة بالحالة
        Task<IEnumerable<Appointment>> GetByDoctorIdAndDateRangeAsync(
            Guid doctorId,
            DateTime startDate,
            DateTime endDate,
            List<AppointmentStatus>? statuses = null);

        // 6. التحقق من تضارب المواعيد (مهم لـ Booking Engine)
        Task<bool> HasConflictingAppointmentAsync(
            Guid doctorId,
            DateTime startTime,
            DateTime endTime,
            Guid? excludeAppointmentId = null);

        // ==========================================
        // II. Doctor Dashboard Statistics (الاحتفاظ بالوظائف الإحصائية)
        // ==========================================

        // 7. جلب عدد المرضى الفريدين (لإحصائيات نمو قاعدة العملاء)
        Task<int> GetUniquePatientsCountAsync(Guid doctorId);

        // 8. جلب إحصائيات المواعيد المكتملة لتقييم الأداء
        Task<int> GetCompletedAppointmentsCountAsync(Guid doctorId);

        // 9. جلب الإيرادات الإجمالية
        Task<decimal> GetTotalRevenueAsync(Guid doctorId);

        // 10. جلب الإيرادات الشهرية
        Task<decimal> GetMonthlyRevenueAsync(Guid doctorId, int year, int month);

        // 11. جلب إحصائيات الحالات (Pending, Cancelled, etc.)
        Task<Dictionary<AppointmentStatus, int>> GetAppointmentStatisticsByDoctorIdAsync(
            Guid doctorId,
            DateTime? startDate,
            DateTime? endDate);

        // ==========================================
        // III. Paginated Queries (لأداء أفضل)
        // ==========================================

        // 12. جلب المواعيد مع الفلترة والـ Pagination (مهمة لصفحات الجداول الكبيرة)
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