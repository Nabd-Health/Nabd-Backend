using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Shared.Configurations; 

namespace Nabd.Shared.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
         
            var corsSettings = configuration.GetSection("CorsSettings").Get<CorsSettings>();


            if (corsSettings == null) return services;

            services.AddCors(options =>
            {
                options.AddPolicy(corsSettings.PolicyName, builder =>
                {
                    
                    builder.WithOrigins(corsSettings.AllowedOrigins)
                           .AllowAnyMethod() // GET, POST, PUT, DELETE...
                           .AllowAnyHeader(); // Content-Type, Authorization...

                
                    if (corsSettings.AllowCredentials)
                        builder.AllowCredentials();
                });
            });

            return services;
        }
    }
}