using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Feedback; 
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Feedback
{
    
    public interface IDoctorReviewRepository : IGenericRepository<DoctorReview>
    {
        
        Task<IEnumerable<DoctorReview>> GetByDoctorIdAsync(Guid doctorId);

      
        Task<DoctorReview?> GetByAppointmentIdAsync(Guid appointmentId);

      
        Task<IEnumerable<DoctorReview>> GetLatestReviewsAsync(Guid doctorId, int count = 10);

      
        Task<double> CalculateAverageRatingAsync(Guid doctorId);
    }
}