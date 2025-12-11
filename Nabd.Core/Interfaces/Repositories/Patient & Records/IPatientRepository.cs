using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Interfaces.Repositories.Base;
using Nabd.Core.DTOs; 

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        
        Task<Patient?> GetByIdWithDetailsAsync(Guid id);

       
        Task<Patient?> GetByEmailAsync(string email);

        
        Task<IEnumerable<Patient>> GetPatientsWithMedicalHistoryAsync();

       
        Task<(IEnumerable<DoctorPatientDto> Patients, int TotalCount)> GetDoctorPatientsOptimizedAsync(
            Guid doctorId,
            int pageNumber,
            int pageSize);

        
        Task<Patient?> GetPatientWithLocationAsync(Guid patientId);

       
        Task RemoveAsync(Patient patient, bool softDelete = true);
    }
}