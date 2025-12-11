using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Enums.Identity;    
using Nabd.Core.Interfaces.Repositories.Base; 

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
    
    public interface IDoctorDocumentRepository : IGenericRepository<DoctorDocument>
    {
        
        Task<IEnumerable<DoctorDocument>> GetByDoctorIdAsync(Guid doctorId);

        
        Task<IEnumerable<DoctorDocument>> GetPendingDocumentsAsync();

      
        Task<IEnumerable<DoctorDocument>> GetByStatusAsync(VerificationDocumentStatus status);
    }
}