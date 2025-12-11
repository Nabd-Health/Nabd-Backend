using Nabd.Core.Entities.Operations; 
using Nabd.Core.Entities.Profiles;  
using Nabd.Core.Enums;             
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nabd.Core.Interfaces.Repositories.Operations
{
    
    public interface IClinicBranchRepository : IGenericRepository<ClinicBranch>
    {
     
        Task<IEnumerable<ClinicBranch>> GetBranchesByDoctorIdAsync(Guid doctorId);

        
        Task<IEnumerable<ClinicBranch>> GetBranchesNearLocationAsync(
            double latitude,
            double longitude,
            double radiusInKm);

   
        Task<IEnumerable<ClinicBranch>> GetBranchesByGovernorateAsync(Governorate governorate);

        Task<ClinicBranch?> GetBranchWithAllDetailsAsync(Guid clinicBranchId);
    }
}