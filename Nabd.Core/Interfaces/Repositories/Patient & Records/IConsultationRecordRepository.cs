using System;
using System.Threading.Tasks;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Medical
{
    public interface IConsultationRecordRepository : IGenericRepository<ConsultationRecord>
    {
        
        Task<ConsultationRecord?> GetByIdWithDetailsAsync(Guid id);

     
        Task<ConsultationRecord?> GetByAppointmentIdAsync(Guid appointmentId);

        
        Task<ConsultationRecord?> GetForDiagnosisAnalysisAsync(Guid consultationId);
    }
}