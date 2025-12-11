using Nabd.Core.Specifications;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System;
using Nabd.Core.Entities.Identity;

namespace Nabd.Core.Specifications
{
    public class UserByEmailSpec : BaseSpecification<AppUser>
    {
        public UserByEmailSpec(string email, bool includeRefreshTokens = false)
            : base(u => u.Email == email)
        {
            ApplyPaging(0, 1);

            AddInclude(u => u.DoctorProfile!);
            AddInclude(u => u.PatientProfile!);

           
            if (includeRefreshTokens)
            {
                AddInclude(u => u.RefreshTokens);
            }
        }
    }
}