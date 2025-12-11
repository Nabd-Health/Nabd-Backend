using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Core.Entities.Identity; 
using Nabd.Infrastructure.Data; 
using System;

namespace Nabd.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
    
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, Role>(options =>
            {
                // 1. Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false; 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequiredLength = 6;

                // 2. User Settings
                options.User.RequireUniqueEmail = true;

                // 3. SignIn Settings
                options.SignIn.RequireConfirmedEmail = false; 
            })
            .AddEntityFrameworkStores<NabdDbContext>() 
            .AddDefaultTokenProviders();

            return services;
        }
    }
}