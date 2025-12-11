using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Pharmacy
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        
        Task<Prescription?> GetByIdWithDetailsAsync(Guid id);

      
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId);

        Task<IEnumerable<Prescription>> GetByDoctorIdAsync(Guid doctorId);

       
        Task<Prescription?> GetByConsultationIdAsync(Guid consultationId);

        Task<IEnumerable<Prescription>> GetPrescriptionsContainingMedicationAsync(Guid medicationId);
    }
}