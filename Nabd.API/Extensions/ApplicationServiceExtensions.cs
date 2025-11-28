using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Core.Interfaces;
using Nabd.Infrastructure.Data;
using Nabd.Infrastructure.Repositories;

namespace Nabd.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        // دالة الامتداد: وظيفتها تجميع كل الخدمات اللي بنحتاجها في API
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // 1. تسجيل الداتابيز (DbContext)
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // استخدام SQL Server والبحث عن الرابط في appsettings.json
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // 2. تسجيل الـ Unit of Work (النظام الاحترافي)
            // نستخدم AddScoped لضمان استخدام نسخة واحدة لكل طلب (Request)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 3. تسجيل الـ Generic Repository (كمساعد لـ UnitOfWork)
            // هذا التسجيل يضمن أن UnitOfWork يستطيع خلق أي Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}