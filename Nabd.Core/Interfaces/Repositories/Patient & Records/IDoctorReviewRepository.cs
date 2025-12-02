using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Feedback; // لاستخدام DoctorReview
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Feedback
{
    // هذا الريبوزيتوري مخصص لتقييمات المرضى للأطباء
    public interface IDoctorReviewRepository : IGenericRepository<DoctorReview>
    {
        // 1. جلب جميع التقييمات لطبيب محدد
        Task<IEnumerable<DoctorReview>> GetByDoctorIdAsync(Guid doctorId);

        // 2. جلب التقييمات المرتبطة بموعد محدد
        Task<DoctorReview?> GetByAppointmentIdAsync(Guid appointmentId);

        // 3. جلب آخر N تقييم (مهم لعرضها في البروفايل)
        Task<IEnumerable<DoctorReview>> GetLatestReviewsAsync(Guid doctorId, int count = 10);

        // 4. تحديث متوسط تقييم الطبيب بعد إضافة/تعديل تقييم جديد
        Task<double> CalculateAverageRatingAsync(Guid doctorId);
    }
}