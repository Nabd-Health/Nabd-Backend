using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Identity
{
  
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        // ==========================================
        // I. Authentication & Identity
        // ==========================================

    
        Task<AppUser?> GetByEmailAsync(string email);

        
        Task<bool> IsUserNameOrEmailTakenAsync(string userName, string email);

       
        Task<AppUser?> GetByIdWithRefreshTokensAsync(Guid userId);


        Task<AppUser?> GetByIdWithProfileAsync(Guid userId);

        // ==========================================
        // II. System Management & Roles
        // ==========================================

        Task<IEnumerable<AppUser>> GetUsersByTypeAsync(UserType userType);

        
        Task<string?> GetSecurityStampAsync(Guid userId);


        Task<IEnumerable<AppUser>> GetPendingVerificationUsersAsync();
    }
}