using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Nabd.Shared.Extensions
{
    public static class DatabaseExtensions
    {
        // جعلنا الميثود Generic لتقبل أي DbContext (مثل NabdDbContext)
        // هذا يسمح لـ Shared بعدم معرفة مكان الـ DbContext الفعلي (Infrastructure)
        public static IServiceCollection AddDatabaseConfiguration<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            // 1. قراءة Connection String
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 2. التحقق من وجوده
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection String 'DefaultConnection' is missing in appsettings.json");
            }

            // 3. تسجيل الـ DbContext
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    // تحديد مكان الـ Migrations (يجب أن يكون اسم مشروع Infrastructure)
                    // ملاحظة: "Nabd.Infrastructure" هو اسم المشروع الذي سيحتوي على الـ Migrations
                    sqlOptions.MigrationsAssembly("Nabd.Infrastructure");

                    // زيادة وقت المهلة (Timeout) للعمليات الثقيلة (مثل التقارير)
                    sqlOptions.CommandTimeout(120); // 2 minutes
                });

                // تفعيل Split Query لتحسين أداء الاستعلامات المعقدة (التي تحتوي على Includes كثيرة)
                // هذا يمنع مشكلة "Cartesian Explosion"
               // options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            return services;
        }
    }
}