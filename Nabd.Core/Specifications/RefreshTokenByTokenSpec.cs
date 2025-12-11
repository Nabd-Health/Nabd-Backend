using Nabd.Core.Entities.Identity;
using Nabd.Core.Specifications;
using System.Linq.Expressions;

namespace Nabd.Core.Specifications
{
    
    public class RefreshTokenByTokenSpec : BaseSpecification<RefreshToken>
    {
        
        public RefreshTokenByTokenSpec(string token)
            : base(t => t.Token == token)
        {
           
        }

    
        public RefreshTokenByTokenSpec(string token, bool onlyActive)
            : base(t => t.Token == token && t.IsActive == onlyActive)
        {
            
            AddCriteria(t => t.IsDeleted == false);
        }
    }
}