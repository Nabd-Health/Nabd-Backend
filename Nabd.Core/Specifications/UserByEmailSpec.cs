using Nabd.Core.Entities;
using Nabd.Core.Specifications;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Nabd.Core.Specifications
{
    public class UserByEmailSpec : BaseSpecification<AppUser>
    {
        public UserByEmailSpec(string email, bool includeRefreshTokens = false)
            : base(u => u.Email == email)
        {
            ApplyPaging(0, 1);

            // جلب البروفايلات في نفس الكويري لتجنب N+1 Problem
            AddInclude(u => u.DoctorProfile!);
            AddInclude(u => u.PatientProfile!);

            // جلب RefreshTokens فقط أثناء عملية Login/RenewToken
            if (includeRefreshTokens)
            {
                AddInclude(u => u.RefreshTokens);
            }
        }
    }
}