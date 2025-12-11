using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Medical; 
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Medical
{
   
    public interface IMedicalHistoryItemRepository : IGenericRepository<MedicalHistoryItem>
    {
        
        Task<IEnumerable<MedicalHistoryItem>> GetByPatientIdAsync(Guid patientId);

    
        Task<IEnumerable<MedicalHistoryItem>> GetByTypeAsync(Guid patientId, HistoryEventType type);
    }
}