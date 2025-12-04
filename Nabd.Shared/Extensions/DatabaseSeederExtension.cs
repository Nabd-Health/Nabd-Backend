using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Core.Interfaces; // لاستخدام IDbSeeder

namespace Nabd.Shared.Extensions
{
    public static class DatabaseSeederExtension
    {
        // هذه الميثود يتم استدعاؤها في Program.cs (app.SeedDatabaseAsync())
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            // إنشاء Scope جديد للحصول على الخدمات (لأن Seeder يحتاج لـ DbContext و UserManager)
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                // نطلب الـ Seeder من الـ Container (سيتم تنفيذه في Infrastructure)
                var seeder = services.GetService<IDbSeeder>();

                if (seeder != null)
                {
                    await seeder.SeedAsync();
                }
            }
            catch (Exception ex)
            {
                // في بيئة الإنتاج، يفضل استخدام ILogger
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; // إعادة رمي الخطأ لإيقاف التشغيل إذا فشل الـ Seeding الأساسي
            }
        }

        // (اختياري) لتنظيف الداتابيز بالكامل - مفيد في مرحلة التطوير فقط
        public static async Task ClearDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                // نفترض وجود ميثود ClearAsync في الواجهة
                // var seeder = services.GetService<IDbSeeder>();
                // if (seeder != null) await seeder.ClearAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while clearing the database: {ex.Message}");
                throw;
            }
        }
    }
}