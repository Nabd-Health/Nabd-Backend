using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Nabd.Shared.Extensions
{
    public static class DatabaseExtensions
    {

       
        public static IServiceCollection AddDatabaseConfiguration<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            
            var connectionString = configuration.GetConnectionString("DefaultConnection");


            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection String 'DefaultConnection' is missing in appsettings.json");
            }

           
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                   
                    sqlOptions.MigrationsAssembly("Nabd.Infrastructure");

                  
                    sqlOptions.CommandTimeout(120); 
                });

            });

            return services;
        }
    }
}