using Microsoft.AspNetCore.Identity;
using System;

namespace Nabd.Core.Entities.Identity
{
    // كلاس الأدوار (يرث من IdentityRole لتوافق مع ASP.NET Identity)
    public class Role : IdentityRole<Guid>
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
        public string? Description { get; set; }
    }
}