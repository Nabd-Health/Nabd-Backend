using Microsoft.EntityFrameworkCore;
using Nabd.Application.Services.Identity;
using Nabd.Core.Interfaces;
using Nabd.Infrastructure.Data;
using Nabd.Infrastructure.Repositories;

namespace Nabd.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // ✅ التعديل: استخدام NabdDbContext بدلاً من ApplicationDbContext
            services.AddDbContext<NabdDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}