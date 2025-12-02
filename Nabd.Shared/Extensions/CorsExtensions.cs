using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Shared.Configurations; // استخدام إعدادات نبض

namespace Nabd.Shared.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // قراءة الإعدادات
            var corsSettings = configuration.GetSection("CorsSettings").Get<CorsSettings>();

            // حماية ضد الإعدادات المفقودة
            if (corsSettings == null) return services;

            services.AddCors(options =>
            {
                options.AddPolicy(corsSettings.PolicyName, builder =>
                {
                    // السماح للنطاقات المحددة في appsettings.json
                    builder.WithOrigins(corsSettings.AllowedOrigins)
                           .AllowAnyMethod() // GET, POST, PUT, DELETE...
                           .AllowAnyHeader(); // Content-Type, Authorization...

                    // السماح بالـ Credentials (Cookies/Auth Headers)
                    if (corsSettings.AllowCredentials)
                        builder.AllowCredentials();
                });
            });

            return services;
        }
    }
}