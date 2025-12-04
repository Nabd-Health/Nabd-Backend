using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Core.Entities.Identity; // AppUser, Role
using Nabd.Infrastructure.Data; // NabdDbContext
using System;

namespace Nabd.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        // ✅ التعديل: شلنا <TContext> وغيرنا الاسم لـ AddIdentityServices
        // عشان يطابق اللي مكتوب في Program.cs
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, Role>(options =>
            {
                // 1. Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false; // للتسهيل في التست
                options.Password.RequireNonAlphanumeric = false; // للتسهيل في التست
                options.Password.RequiredLength = 6;

                // 2. User Settings
                options.User.RequireUniqueEmail = true;

                // 3. SignIn Settings
                options.SignIn.RequireConfirmedEmail = false; // خليها false دلوقتي عشان نجرب بسرعة
            })
            .AddEntityFrameworkStores<NabdDbContext>() // ✅ ربطنا بـ NabdDbContext مباشرة
            .AddDefaultTokenProviders();

            return services;
        }
    }
}