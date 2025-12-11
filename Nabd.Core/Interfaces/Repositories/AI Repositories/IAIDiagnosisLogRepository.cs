using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.AI;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.AI
{
    public interface IAIDiagnosisLogRepository : IGenericRepository<AIDiagnosisLog>
    {
       
        Task<IEnumerable<AIDiagnosisLog>> GetByConsultationIdAsync(Guid consultationId);


        Task<IEnumerable<AIDiagnosisLog>> GetLogsForRetrainingAsync();


        Task<double> GetModelAccuracyPercentageAsync();
    }
}