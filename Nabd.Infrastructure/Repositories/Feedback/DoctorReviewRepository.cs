using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Feedback;
using Nabd.Core.Interfaces.Repositories.Feedback;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Feedback
{
    public class DoctorReviewRepository : GenericRepository<DoctorReview>, IDoctorReviewRepository
    {
        public DoctorReviewRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DoctorReview>> GetByDoctorIdAsync(Guid doctorId)
        {
            // جلب التقييمات للدكتور، مرتبة من الأحدث للأقدم، مع بيانات المريض
            return await _dbSet
                .Include(r => r.Patient) // نحتاج بيانات المريض لعرض الاسم والصورة بجانب التقييم
                .Where(r => r.DoctorId == doctorId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<DoctorReview?> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return await _dbSet
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(r => r.AppointmentId == appointmentId);
        }

        public async Task<IEnumerable<DoctorReview>> GetLatestReviewsAsync(Guid doctorId, int count = 10)
        {
            return await _dbSet
                .Include(r => r.Patient)
                .Where(r => r.DoctorId == doctorId)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<double> CalculateAverageRatingAsync(Guid doctorId)
        {
            // بما أن AverageRating هي Computed Property (C# only)
            // نقوم بكتابة معادلة الحساب داخل LINQ ليقوم EF Core بترجمتها لـ SQL
            // هذا أسرع بكثير من جلب البيانات للذاكرة وحسابها

            var ratingsQuery = _dbSet.Where(r => r.DoctorId == doctorId);

            if (!await ratingsQuery.AnyAsync())
                return 0;

            return await ratingsQuery.AverageAsync(r =>
                (double)(r.OverallSatisfaction +
                         r.WaitingTime +
                         r.CommunicationQuality +
                         r.ClinicCleanliness +
                         r.ValueForMoney) / 5.0
            );
        }
    }
}