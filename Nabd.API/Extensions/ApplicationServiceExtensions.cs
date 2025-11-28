using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Core.Interfaces;
using Nabd.Infrastructure.Data;
using Nabd.Infrastructure.Repositories;

// ⚠️ السطر ده ضروري عشان يعرف كلاس الـ Assembly (مثل Assembly.GetExecutingAssembly)
using System.Reflection;

namespace Nabd.API.Extensions
{
    // 1. الكلاس لازم يكون static عشان نقدر نعمل Extension Methods
    public static class ApplicationServiceExtensions
    {
        // 2. دالة الامتداد: وظيفتها تجميع كل الخدمات اللي بنحتاجها في API
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // 3. تسجيل الداتابيز (DbContext)
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // استخدام SQL Server والبحث عن الرابط في appsettings.json
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // ==========================================================
            // 4. تسجيل الـ Repositories والـ UoW
            // ==========================================================

            // تسجيل الـ Unit of Work (النظام الاحترافي)
            // نستخدم AddScoped لضمان استخدام نسخة واحدة لكل طلب (Request)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // تسجيل الـ Generic Repository (كمساعد لـ UnitOfWork)
            // هذا التسجيل يضمن أن UnitOfWork يستطيع خلق أي Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // 5. تسجيل الـ AutoMapper
            // نطلب من AutoMapper أن يبحث عن ملفات الـ Profile في Assembly الخاص بمشروع Application
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}